using Microsoft.EntityFrameworkCore;
using DevIO.Business.Models;
using System.Linq;

namespace DevIO.Data.Context
{
  public class DataContext : DbContext
  {
    public DataContext(DbContextOptions options) : base(options)
    {
      ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
      ChangeTracker.AutoDetectChangesEnabled = false;
    }

    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }
    public DbSet<Fornecedor> Fornecedores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      foreach (var property in modelBuilder.Model.GetEntityTypes()
          .SelectMany(e => e.GetProperties()
              .Where(p => p.ClrType == typeof(string))))
        property.SetColumnType("varchar(100)");

      modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

      foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

      base.OnModelCreating(modelBuilder);
    }
  }
}