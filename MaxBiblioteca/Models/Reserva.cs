using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaxBiblioteca.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        
        public int IdUsuario { get; set; }

        public int IdLivro { get; set; }

        public DateTime DataReserva { get; set; }
    }
}