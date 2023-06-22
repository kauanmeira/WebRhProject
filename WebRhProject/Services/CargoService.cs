using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebRhProject.Data;
using WebRhProject.Models;

namespace WebRhProject.Services
{
    public class CargoService
    {
        private readonly Contexto _context;

        public CargoService(Contexto context)
        {
            _context = context;
        }

        public List<Cargo> FindAll()
        {
            return _context.Cargo.OrderBy(x => x.Nome).ToList();
        }
        public Cargo FindById(int id)
        {
            return _context.Cargo.Find(id);
        }
        public bool HasAnyCollaborators(int cargoId)
        {
            return _context.Colaborador.Any(colaborador => colaborador.CargoId == cargoId);
        }

    }
}