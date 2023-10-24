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
            obj.Ativo = true;
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Colaborador FindById(int id)
        {
            return _context.Colaborador
                .Include(colaborador => colaborador.Cargo)
                .FirstOrDefault(colaborador => colaborador.Id == id);
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
            return _context.Colaborador
                .Where(c => c.Ativo == true)
                .Join(
                    _context.Cargo,
                    colaborador => colaborador.CargoId,
                    cargo => cargo.Id,
                    (colaborador, cargo) => new { Colaborador = colaborador, Cargo = cargo }
                )
                .Join(
                    _context.Empresa,
                    colaboradorCargo => colaboradorCargo.Colaborador.EmpresaId,
                    empresa => empresa.Id,
                    (colaboradorCargo, empresa) => new Colaborador
                    {
                        Id = colaboradorCargo.Colaborador.Id,
                        Nome = colaboradorCargo.Colaborador.Nome,
                        Sobrenome = colaboradorCargo.Colaborador.Sobrenome,
                        SalarioBase = colaboradorCargo.Colaborador.SalarioBase,
                        Dependentes = colaboradorCargo.Colaborador.Dependentes,
                        Filhos = colaboradorCargo.Colaborador.Filhos,
                        DataNascimento = colaboradorCargo.Colaborador.DataNascimento,
                        DataAdmissao = colaboradorCargo.Colaborador.DataAdmissao,
                        Cargo = colaboradorCargo.Cargo,
                        Empresa = empresa 
                    }
                )
                .ToList();
        }

        public bool Exists(string cpf)
        {
            return _context.Colaborador.Any(c => c.CPF == cpf );
        }
        public void Demitir(int id)
        {
            var colaborador = _context.Colaborador.Find(id);

            if (colaborador != null)
            {
                colaborador.Ativo = false;
                colaborador.DataDemissao = DateTime.Now;

                _context.SaveChanges();
            }
        }
        public bool HasCompanyEmpresa(int colaboradorId)
        {
            return _context.Colaborador.Any(c => c.Id == colaboradorId && c.EmpresaId != null);
        }
        public bool HasCompanyCargo(int colaboradorId)
        {
            return _context.Colaborador.Any(c => c.Id == colaboradorId && c.CargoId != null);
        }

    }
}
