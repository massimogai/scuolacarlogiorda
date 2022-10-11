using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using ScuolaCarloGiorda.Models;

namespace ScuolaCarloGiorda.Services;

public class SchoolDbContext : DbContext
{
    public DbSet<Corso> Corsi { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? connectionString=Environment.GetEnvironmentVariable("DATABASE_URL");
        if (connectionString == null)
        {
            connectionString = "Host=localhost;Database=scuolacarlogiorda;Username=postgres;Password=askme1st";
        }
       
        optionsBuilder.UseNpgsql(connectionString);
    }


    public  void Initialize()
    {
       Database.EnsureCreated();
    }
}