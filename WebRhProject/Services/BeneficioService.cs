using System.Collections.Generic;
using System.Linq;
using WebRhProject.Data;
using WebRhProject.Models;

namespace WebRhProject.Services
{
    public class BeneficioService
    {
        private readonly Contexto _context;

        public BeneficioService(Contexto context)
        {
            _context = context;
        }

        public List<Beneficio> GetAllBeneficios()
        {
            return _context.Beneficio.ToList();
        }

        public Beneficio GetBeneficioById(int id)
        {
            return _context.Beneficio.FirstOrDefault(b => b.Id == id);
        }

        public void InsertBeneficio(Beneficio beneficio)
        {
            _context.Beneficio.Add(beneficio);
            _context.SaveChanges();
        }

        public void UpdateBeneficio(Beneficio beneficio)
        {
            _context.Beneficio.Update(beneficio);
            _context.SaveChanges();
        }

        public void DeleteBeneficio(int id)
        {
            var beneficio = _context.Beneficio.Find(id);
            if (beneficio != null)
            {
                _context.Beneficio.Remove(beneficio);
                _context.SaveChanges();
            }
        }
    }
}
