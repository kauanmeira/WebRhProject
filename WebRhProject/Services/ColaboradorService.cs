    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
using WebRhProject.Data;
using WebRhProject.Models;
using WebRhProject.Services.Exceptions;

namespace WebRhProject.Services
{
    public class ColaboradorService
    {
        private readonly Contexto _context;

        public ColaboradorService(Contexto context)
        {
            _context = context;
        }

        public List<Colaborador> FindAll()
        {
            return _context.Colaborador.ToList();
        }

        public void Insert(Colaborador obj)
        {
            obj.Status = 1;
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Colaborador FindById(int id)
        {
            return _context.Colaborador.Include(obj => obj.Cargo).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id)
        {
            var obj = _context.Colaborador.Find(id);
            _context.Colaborador.Remove(obj);
            _context.SaveChanges();
        }


        public void Update(Colaborador obj)
        {
            if (!_context.Colaborador.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id not found");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
        public List<Colaborador> FindAllActive()
        {
            return _context.Colaborador.Where(c => c.Status == 1).ToList();
        }

        public bool Exists(string nome, string sobrenome)
        {
            return _context.Colaborador.Any(c => c.Nome == nome && c.Sobrenome == sobrenome);
        }
        public void Demitir(int id)
        {
            var colaborador = _context.Colaborador.Find(id);

            if (colaborador != null)
            {
                colaborador.Status = 0;
                colaborador.DataDemissao = DateTime.Now;

                _context.SaveChanges();
            }
        }
    }
}
