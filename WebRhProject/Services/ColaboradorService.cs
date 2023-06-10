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
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
    }