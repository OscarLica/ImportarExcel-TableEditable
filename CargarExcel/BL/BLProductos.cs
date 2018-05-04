using CargarExcel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;

namespace CargarExcel.BL
{
    public class BLProductos
    {
        private Identity.Context db = new Identity.Context();
        public void saveFile(HttpPostedFileBase file)
        {
            string name = string.Empty;
            string ruta = string.Empty;
            if (file != null)
            {
                name = file.FileName;
                ruta = HttpContext.Current.Server.MapPath("~/Uploads/" + name);
            }
            if (System.IO.Directory.Exists(ruta))
            {
                System.IO.Directory.Delete(ruta);
                file.SaveAs(ruta);
                cargaDatos(ruta);
            }
            else
            {
                file.SaveAs(ruta);
                cargaDatos(ruta);
            }


        }
        public void cargaDatos(string ruta)
        {

            Excel.Application application = new Excel.Application();
            Excel.Workbook workbook = application.Workbooks.Open(ruta);
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            Excel.Range range = worksheet.UsedRange;
            try
            {
                List<Productos> productos = new List<Productos>();
                List<Productos> productosExietntes = new List<Productos>();
                //recorrero las filas
                int fila = 1;
                foreach (Excel.Range item in range.Rows)
                {
                    if (fila > 1)
                    {
                        
                        if (existe(((Excel.Range)item.Cells[1]).Text))
                        {
                            string nombre = ((Excel.Range)item.Cells[1]).Text;
                            Models.Productos p = db.productos.FirstOrDefault(x=> x.nombre == nombre);
                            p.nombre = ((Excel.Range)item.Cells[1]).Text;
                            p.descripcion = ((Excel.Range)item.Cells[2]).Text;
                            p.precioCompra = Convert.ToDecimal(((Excel.Range)item.Cells[3]).Text);
                            p.precioVenta = Convert.ToDecimal(((Excel.Range)item.Cells[4]).Text);
                            p.iva = Convert.ToDecimal(((Excel.Range)item.Cells[5]).Text);
                            p.fecha = ((Excel.Range)item.Cells[6]).Text;
                            p.estado = Convert.ToBoolean(((Excel.Range)item.Cells[7]).Text);
                            db.SaveChanges();
                        }
                        else
                        {
                            Models.Productos p = new Models.Productos();
                            p.nombre = ((Excel.Range)item.Cells[1]).Text;
                            p.descripcion = ((Excel.Range)item.Cells[2]).Text;
                            p.precioCompra = Convert.ToDecimal(((Excel.Range)item.Cells[3]).Text);
                            p.precioVenta = Convert.ToDecimal(((Excel.Range)item.Cells[4]).Text);
                            p.iva = Convert.ToDecimal(((Excel.Range)item.Cells[5]).Text);
                            p.fecha = ((Excel.Range)item.Cells[6]).Text;
                            p.estado = Convert.ToBoolean(((Excel.Range)item.Cells[7]).Text);
                            db.productos.Add(p);
                        }


                    }
                    fila++;
                }
                workbook.Close();
                db.SaveChanges();
            }
            catch (Exception ex )
            {
                workbook.Close();
                throw;
            }
        }

        public bool existe(string nombre)
        {
            var pro = db.productos.Where(x => x.nombre == nombre).ToList().Count();
            return (pro > 0);
        }
    }
}