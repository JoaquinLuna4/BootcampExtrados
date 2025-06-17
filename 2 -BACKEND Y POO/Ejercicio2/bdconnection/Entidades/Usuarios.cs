using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIclase.bdconnection.Entidades
{
    public class Usuarios
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }
        public string? Edad { get; set; }

            }
}
