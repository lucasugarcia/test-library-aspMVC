using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MaxBiblioteca.Models;

namespace MaxBiblioteca.DAO
{
    public class ReservasDAO
    {
        public void Adiciona(Reserva reserva)
        {
            using (var context = new BibliotecaContext())
            {
                context.Reserva.Add(reserva);
                context.SaveChanges();
            }
        }

        public IList<Reserva> Lista()
        {
            using (var contexto = new BibliotecaContext())
            {
                return contexto.Reserva.ToList();
            }
        }

        public Reserva BuscaPorId(int id)
        {
            using (var contexto = new BibliotecaContext())
            {
                return contexto.Reserva.Find(id);
            }
        }

        public void Atualiza(Reserva reserva)
        {
            using (var contexto = new BibliotecaContext())
            {
                contexto.Entry(reserva).State = System.Data.Entity.EntityState.Modified;
                contexto.SaveChanges();
            }
        }
    }
}