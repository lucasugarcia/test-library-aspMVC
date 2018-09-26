using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MaxBiblioteca.Models;

namespace MaxBiblioteca.DAO
{
    public class LivrosDAO
    {
        public void Adiciona(Livro livro)
        {
            using (var context = new BibliotecaContext())
            {
                context.Livros.Add(livro);
                context.SaveChanges();
            }
        }

        public IList<Livro> Lista()
        {
            using (var contexto = new BibliotecaContext())
            {
                return contexto.Livros.ToList();
            }
        }

        public Livro BuscaPorId(int id)
        {
            using (var contexto = new BibliotecaContext())
            {
                return contexto.Livros.Find(id);
            }
        }

        public void Atualiza(Livro livro)
        {
            using (var contexto = new BibliotecaContext())
            {
                contexto.Entry(livro).State = System.Data.Entity.EntityState.Modified;
                contexto.SaveChanges();
            }
        }
    }
}