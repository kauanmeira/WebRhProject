using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebRhProject.Models;

namespace WebRhProject.Data
{
    public class Contexto : DbContext
    {
        public Contexto (DbContextOptions<Contexto> options)
            : base(options)
        {
        }

        public DbSet<Colaborador> Colaborador { get; set; } = default!;

        public DbSet<Cargo>? Cargo { get; set; }
        public DbSet<Empresa> Empresa { get; set; }
        public DbSet<WebRhProject.Models.Usuario>? Usuario { get; set; }
    }
}
