using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace SIGIVE.Utilidades
{
    /// <summary>
    ///     Exporta el contenido de un DataGridView a un archivo PDF usando QuestPDF.
    ///     Solo exporta las columnas visibles, con el mismo formato que se ve en pantalla.
    /// </summary>
    public static class ExportadorPdf
    {
        /// <summary>
        ///     Constructor estático: se ejecuta una sola vez, antes del primer uso.
        ///     QuestPDF pide declarar la licencia; la Community es gratis para proyectos académicos.
        /// </summary>
        static ExportadorPdf()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        /// <summary>
        ///     Crea el PDF con encabezado (título y fecha), la tabla y el número de página al pie.
        /// </summary>
        /// <param name="tabla">Tabla de la pantalla que se quiere exportar.</param>
        /// <param name="titulo">Título que aparece arriba del reporte.</param>
        /// <param name="ruta">Ruta donde se guarda el archivo.</param>
        /// <param name="pie">Texto opcional debajo de la tabla (por ejemplo, el total).</param>
        public static void Exportar(DataGridView tabla, string titulo, string ruta, string pie = "")
        {
            // LINQ: columnas visibles en el orden en que se ven en pantalla
            List<DataGridViewColumn> columnas = tabla.Columns.Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .OrderBy(c => c.DisplayIndex)
                .ToList();

            Document documento = Document.Create(contenedor =>
            {
                contenedor.Page(pagina =>
                {
                    // Hoja horizontal para que quepan más columnas
                    pagina.Size(PageSizes.A4.Landscape());
                    pagina.Margin(30);
                    pagina.DefaultTextStyle(estilo => estilo.FontSize(10));

                    // Encabezado
                    pagina.Header().Column(columna =>
                    {
                        columna.Item().Text(titulo).FontSize(18).Bold();
                        columna.Item().Text("SIGIVE - Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"))
                            .FontSize(9).FontColor(Colors.Grey.Darken1);
                    });

                    // Contenido: la tabla y el texto final
                    pagina.Content().PaddingVertical(10).Column(columna =>
                    {
                        columna.Item().Table(tablaPdf =>
                        {
                            // Todas las columnas del mismo ancho
                            tablaPdf.ColumnsDefinition(definicion =>
                            {
                                foreach (DataGridViewColumn c in columnas)
                                {
                                    definicion.RelativeColumn();
                                }
                            });

                            // Fila de encabezados (se repite en cada página)
                            tablaPdf.Header(encabezado =>
                            {
                                foreach (DataGridViewColumn c in columnas)
                                {
                                    IContainer contenedorEncabezado = encabezado.Cell()
                                        .Background(Colors.Grey.Lighten2).Padding(4);

                                    // Encabezados de números a la derecha, igual que sus valores
                                    if (EsColumnaNumerica(c))
                                    {
                                        contenedorEncabezado = contenedorEncabezado.AlignRight();
                                    }

                                    contenedorEncabezado.Text(c.HeaderText).Bold();
                                }
                            });

                            // Filas de datos
                            foreach (DataGridViewRow fila in tabla.Rows)
                            {
                                foreach (DataGridViewColumn c in columnas)
                                {
                                    DataGridViewCell celda = fila.Cells[c.Index];

                                    // FormattedValue respeta el formato de la columna (ej: N2, fechas)
                                    string texto = celda.FormattedValue?.ToString() ?? "";

                                    IContainer contenedorCelda = tablaPdf.Cell()
                                        .BorderBottom(0.5f).BorderColor(Colors.Grey.Lighten1).Padding(4);

                                    // Los números se alinean a la derecha
                                    if (EsColumnaNumerica(c))
                                    {
                                        contenedorCelda = contenedorCelda.AlignRight();
                                    }

                                    contenedorCelda.Text(texto);
                                }
                            }
                        });

                        if (pie != "")
                        {
                            columna.Item().PaddingTop(10).Text(pie).Bold();
                        }
                    });

                    // Pie de página: "Página X de Y"
                    pagina.Footer().AlignCenter().Text(texto =>
                    {
                        texto.Span("Página ");
                        texto.CurrentPageNumber();
                        texto.Span(" de ");
                        texto.TotalPages();
                    });
                });
            });

            documento.GeneratePdf(ruta);
        }

        /// <summary>
        ///     True si la columna guarda números (para alinearla a la derecha).
        /// </summary>
        private static bool EsColumnaNumerica(DataGridViewColumn columna)
        {
            return columna.ValueType == typeof(int)
                || columna.ValueType == typeof(decimal)
                || columna.ValueType == typeof(double);
        }
    }
}
