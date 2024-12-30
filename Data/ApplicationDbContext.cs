using GestaoVeiculo.Models;
using Microsoft.EntityFrameworkCore;

namespace GestaoVeiculo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<GestaoModel> Gestao { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }

    }
}
