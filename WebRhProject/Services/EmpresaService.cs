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
        public Empresa FindById(int id)
        {
            return _context.Empresa.Find(id);
        }
        public bool HasCompany(int empresaId)
        {
            return _context.Colaborador.Any(c => c.EmpresaId == empresaId);
        }
        public void Insert(Empresa obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
        public void Update(Empresa obj)
        {
            if (!_context.Empresa.Any(x => x.Id == obj.Id))
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

    }
}
