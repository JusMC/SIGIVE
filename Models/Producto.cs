namespace SIGIVE.Models
{
    /// <summary>
    ///     Representa un producto disponible para la venta.
    /// </summary>
    public class Producto
    {
        // Atributos
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public Categoria Categoria { get; set; }

        // Constructor
        public Producto(int codigo, string nombre, string descripcion, decimal precio, int stock, Categoria categoria)
        {
            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
            Stock = stock;
            Categoria = categoria;
        }

        /// <summary>
        ///     Resta la cantidad vendida del stock.
        ///     Devuelve true si se pudo descontar y false si no hay suficiente stock.
        /// </summary>
        public bool DescontarStock(int cantidad)
        {
            if (cantidad <= 0 || cantidad > Stock)
            {
                return false;
            }

            Stock = Stock - cantidad;
            return true;
        }

        /// <summary>
        ///     Texto que se muestra cuando el producto aparece en un ComboBox o lista.
        /// </summary>
        public override string ToString()
        {
            return Nombre;
        }
    }
}
