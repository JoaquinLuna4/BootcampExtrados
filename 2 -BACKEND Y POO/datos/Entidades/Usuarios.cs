using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace datos.Entidades

{ 
    public class Usuarios
    {
        public required int Id { get; set; }
        public required string Nombre { get; set; }

        public required string Email { get; set; }

        public required int Edad { get; set; }

    }
}
