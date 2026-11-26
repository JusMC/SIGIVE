using ClosedXML.Excel;

namespace SIGIVE.Utilidades
{
    /// <summary>
    ///     Exporta el contenido de un DataGridView a un archivo Excel (.xlsx) usando ClosedXML.
    ///     Solo exporta las columnas visibles, con los mismos encabezados de la tabla.
    /// </summary>
    public static class ExportadorExcel
    {
        /// <summary>
        ///     Crea el archivo Excel con un título, la fecha de generación y la tabla.
        /// </summary>
        /// <param name="tabla">Tabla de la pantalla que se quiere exportar.</param>
        /// <param name="titulo">Título que aparece arriba del reporte.</param>
        /// <param name="ruta">Ruta donde se guarda el archivo.</param>
        /// <param name="pie">Texto opcional debajo de la tabla (por ejemplo, el total).</param>
        public static void Exportar(DataGridView tabla, string titulo, string ruta, string pie = "")
        {
            List<DataGridViewColumn> columnas = ObtenerColumnasVisibles(tabla);

            using (XLWorkbook libro = new XLWorkbook())
            {
                IXLWorksheet hoja = libro.Worksheets.Add("Reporte");

                // Título y fecha
                hoja.Cell(1, 1).Value = titulo;
                hoja.Cell(1, 1).Style.Font.Bold = true;
                hoja.Cell(1, 1).Style.Font.FontSize = 14;
                hoja.Cell(2, 1).Value = "Generado: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm");

                // Encabezados (fila 4)
                int filaEncabezado = 4;
                for (int c = 0; c < columnas.Count; c++)
                {
                    IXLCell celda = hoja.Cell(filaEncabezado, c + 1);
                    celda.Value = columnas[c].HeaderText;
                    celda.Style.Font.Bold = true;
                    celda.Style.Fill.BackgroundColor = XLColor.LightGray;
                }

                // Datos (desde la fila 5)
                int fila = filaEncabezado + 1;
                foreach (DataGridViewRow filaTabla in tabla.Rows)
                {
                    for (int c = 0; c < columnas.Count; c++)
                    {
                        object? valor = filaTabla.Cells[columnas[c].Index].Value;
                        EscribirValor(hoja.Cell(fila, c + 1), valor);
                    }
                    fila++;
                }

                // Bordes a toda la tabla
                if (columnas.Count > 0)
                {
                    IXLRange rango = hoja.Range(filaEncabezado, 1, fila - 1, columnas.Count);
                    rango.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    rango.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                }

                // Texto final (por ejemplo, el total)
                if (pie != "")
                {
                    hoja.Cell(fila + 1, 1).Value = pie;
                    hoja.Cell(fila + 1, 1).Style.Font.Bold = true;
                }

                hoja.Columns().AdjustToContents();
                libro.SaveAs(ruta);
            }
        }

        /// <summary>
        ///     Escribe el valor en la celda con su tipo real, para que Excel
        ///     pueda sumar números y ordenar fechas.
        /// </summary>
        private static void EscribirValor(IXLCell celda, object? valor)
        {
            if (valor == null)
            {
                return;
            }

            if (valor is decimal dinero)
            {
                celda.Value = (double)dinero;
                celda.Style.NumberFormat.Format = "#,##0.00";
            }
            else if (valor is int entero)
            {
                celda.Value = entero;
            }
            else if (valor is DateTime fecha)
            {
                celda.Value = fecha;
                celda.Style.DateFormat.Format = "dd/MM/yyyy HH:mm";
            }
            else
            {
                celda.Value = valor.ToString();
            }
        }

        /// <summary>
        ///     LINQ: columnas visibles en el mismo orden en que se ven en pantalla.
        /// </summary>
        private static List<DataGridViewColumn> ObtenerColumnasVisibles(DataGridView tabla)
        {
            return tabla.Columns.Cast<DataGridViewColumn>()
                .Where(c => c.Visible)
                .OrderBy(c => c.DisplayIndex)
                .ToList();
        }
    }
}
