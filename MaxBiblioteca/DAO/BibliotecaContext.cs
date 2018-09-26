using MaxBiblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MaxBiblioteca.DAO
{
    public class BibliotecaContext : DbContext
    {
        public DbSet<Livro> Livros { get; set; }

        public DbSet<Reserva> Reserva { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}