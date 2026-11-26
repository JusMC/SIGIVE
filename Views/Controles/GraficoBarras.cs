using System.Drawing.Drawing2D;

namespace SIGIVE.Views.Controles
{
    /// <summary>
    ///     Control propio que dibuja un gráfico de barras simple con GDI+ (System.Drawing).
    ///     Hereda de Control y sobrescribe OnPaint (polimorfismo): WinForms llama a OnPaint
    ///     cada vez que hay que redibujar el control.
    /// </summary>
    public class GraficoBarras : Control
    {
        // Datos del gráfico: una etiqueta y un valor por barra
        private List<string> etiquetas = new List<string>();
        private List<decimal> valores = new List<decimal>();

        /// <summary>
        ///     Texto que aparece arriba del gráfico.
        /// </summary>
        public string Titulo { get; set; } = "";

        /// <summary>
        ///     Color de las barras.
        /// </summary>
        public Color ColorBarras { get; set; } = Color.SteelBlue;

        /// <summary>
        ///     Formato de los números (N0 = sin decimales, N2 = dos decimales).
        /// </summary>
        public string FormatoValores { get; set; } = "N0";

        // Constructor
        public GraficoBarras()
        {
            // Evita el parpadeo al redibujar
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.White;
        }

        /// <summary>
        ///     Cambia los datos del gráfico y lo vuelve a dibujar.
        /// </summary>
        public void MostrarDatos(List<string> etiquetas, List<decimal> valores)
        {
            if (etiquetas.Count != valores.Count)
            {
                throw new ArgumentException("Debe haber la misma cantidad de etiquetas que de valores.");
            }

            this.etiquetas = etiquetas;
            this.valores = valores;

            // Invalidate le pide a WinForms que llame otra vez a OnPaint
            Invalidate();
        }

        /// <summary>
        ///     Dibuja el gráfico completo: título, líneas guía, barras y etiquetas.
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            using (Font fuenteTitulo = new Font(Font.FontFamily, 11, FontStyle.Bold))
            using (Font fuentePequena = new Font(Font.FontFamily, 8))
            using (SolidBrush brochaTexto = new SolidBrush(Color.FromArgb(60, 60, 60)))
            using (SolidBrush brochaBarras = new SolidBrush(ColorBarras))
            using (Pen lapizGuia = new Pen(Color.FromArgb(225, 225, 225)))
            using (Pen lapizEje = new Pen(Color.Gray))
            {
                // 1. Título
                g.DrawString(Titulo, fuenteTitulo, brochaTexto, 8, 8);

                if (valores.Count == 0)
                {
                    DibujarCentrado(g, "Sin datos para mostrar", Font, brochaTexto, ClientRectangle);
                    return;
                }

                // LINQ: el valor más alto define la altura máxima de las barras
                decimal maximo = valores.Max();
                if (maximo <= 0)
                {
                    maximo = 1;
                }

                // 2. Área del gráfico (deja espacio para los números de la izquierda y las etiquetas de abajo)
                float anchoNumeros = g.MeasureString(maximo.ToString(FormatoValores), fuentePequena).Width;
                float izquierda = anchoNumeros + 14;
                float arriba = 52; // espacio para el título y el valor de la barra más alta
                float derecha = Width - 12;
                float abajo = Height - 28;
                float alto = abajo - arriba;

                if (derecha <= izquierda || alto <= 0)
                {
                    return; // el control es demasiado pequeño para dibujar
                }

                // 3. Líneas guía horizontales con su valor (0%, 25%, 50%, 75%, 100% del máximo)
                for (int i = 0; i <= 4; i++)
                {
                    float y = abajo - alto * i / 4;
                    decimal valorGuia = maximo * i / 4;
                    g.DrawLine(lapizGuia, izquierda, y, derecha, y);

                    string texto = valorGuia.ToString(FormatoValores);
                    SizeF tamano = g.MeasureString(texto, fuentePequena);
                    g.DrawString(texto, fuentePequena, brochaTexto, izquierda - tamano.Width - 4, y - tamano.Height / 2);
                }

                // 4. Barras: cada una ocupa un "espacio" y la barra usa el 60% de ese espacio
                float espacio = (derecha - izquierda) / valores.Count;
                float anchoBarra = espacio * 0.6f;

                // Si las etiquetas no caben, se dibuja solo una de cada "salto" barras
                float anchoEtiqueta = etiquetas.Max(t => g.MeasureString(t, fuentePequena).Width);
                int salto = Math.Max(1, (int)Math.Ceiling(anchoEtiqueta / espacio));

                for (int i = 0; i < valores.Count; i++)
                {
                    float altoBarra = (float)(valores[i] / maximo) * alto;
                    float x = izquierda + espacio * i + (espacio - anchoBarra) / 2;
                    g.FillRectangle(brochaBarras, x, abajo - altoBarra, anchoBarra, altoBarra);

                    if (i % salto == 0)
                    {
                        // Etiqueta debajo de la barra
                        RectangleF zonaEtiqueta = new RectangleF(izquierda + espacio * i - espacio * (salto - 1) / 2,
                                                                 abajo + 4, espacio * salto, 20);
                        DibujarCentrado(g, etiquetas[i], fuentePequena, brochaTexto, zonaEtiqueta);

                        // Valor encima de la barra (solo si cabe)
                        string textoValor = valores[i].ToString(FormatoValores);
                        if (g.MeasureString(textoValor, fuentePequena).Width <= espacio)
                        {
                            RectangleF zonaValor = new RectangleF(izquierda + espacio * i, abajo - altoBarra - 16, espacio, 16);
                            DibujarCentrado(g, textoValor, fuentePequena, brochaTexto, zonaValor);
                        }
                    }
                }

                // 5. Eje inferior
                g.DrawLine(lapizEje, izquierda, abajo, derecha, abajo);
            }
        }

        /// <summary>
        ///     Dibuja un texto centrado dentro de un rectángulo (y lo corta con "..." si no cabe).
        /// </summary>
        private void DibujarCentrado(Graphics g, string texto, Font fuente, Brush brocha, RectangleF zona)
        {
            using (StringFormat formato = new StringFormat())
            {
                formato.Alignment = StringAlignment.Center;
                formato.LineAlignment = StringAlignment.Center;
                formato.Trimming = StringTrimming.EllipsisCharacter;
                formato.FormatFlags = StringFormatFlags.NoWrap;
                g.DrawString(texto, fuente, brocha, zona, formato);
            }
        }
    }
}
