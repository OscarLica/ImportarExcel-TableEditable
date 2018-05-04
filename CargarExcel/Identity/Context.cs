using CargarExcel.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CargarExcel.Identity
{
    public class Context: DbContext
    {
        public Context()
            :base("Default")
        {

        }
        public DbSet<Productos> productos { get; set; }
    }
}