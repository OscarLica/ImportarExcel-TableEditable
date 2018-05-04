using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CargarExcel.Models
{
    public class Productos
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal precioCompra { get; set; }
        public decimal precioVenta { get; set; }
        public decimal iva { get; set; }
        public string fecha { get; set; }
        public bool estado { get; set; }
    }
}