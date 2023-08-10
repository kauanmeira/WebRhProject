using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebRhProject.Data;
using WebRhProject.Models;
using WebRhProject.Services.Exceptions;

namespace WebRhProject.Services
{
    public class CargoService
    {
        private readonly Contexto _context;

        public CargoService(Contexto context)
        {
            _context = context;
        }
        public void Insert(Cargo obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
        public void Update(Cargo obj)
        {
            if (!_context.Cargo.Any(x => x.Id == obj.Id))
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
        public List<Cargo> FindAll()
        {
            return _context.Cargo.OrderBy(x => x.Nome).ToList();
        }
        public Cargo FindById(int id)
        {
            return _context.Cargo.Find(id);
        }


    }
}