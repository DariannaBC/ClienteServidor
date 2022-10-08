using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienteServidor.Models
{
    class Apartado
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }

        public string Cedula { get; set; }

        public  DateTime Fecha { get; set; }
    }
}
