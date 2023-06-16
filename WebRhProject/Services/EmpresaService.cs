using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebRhProject.Data;
using WebRhProject.Models;

namespace WebRhProject.Services
{
    public class EmpresaService
    {
        private readonly Contexto _context;

        public EmpresaService(Contexto context)
        {
            _context = context;
        }

        public void AddEmpresa(Empresa empresa)
        {
            _context.Empresa.Add(empresa);
            _context.SaveChanges();
        }

        public List<Empresa> FindAll()
        {
            return _context.Empresa.OrderBy(x => x.NomeFantasia).ToList();
        }
    }
}
