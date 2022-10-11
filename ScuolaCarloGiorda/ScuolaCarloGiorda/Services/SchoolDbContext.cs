using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using ScuolaCarloGiorda.Models;

namespace ScuolaCarloGiorda.Services;

public class SchoolDbContext : DbContext
{
    public DbSet<Corso> Corsi { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string? databaseurl=Environment.GetEnvironmentVariable("DATABASE_URL");
        Console.WriteLine("DB URL:"+databaseurl);
        
        string connectionString;
        if (databaseurl == null)
        {
            connectionString = "Host=localhost;Database=scuolacarlogiorda;Username=postgres;Password=askme1st";
        }
        else
        {
            Uri dbUri = new Uri(databaseurl);
            
            String username = dbUri.UserInfo.Split(":")[0];
            String password = dbUri.UserInfo.Split(":")[1];
            int port=dbUri.Port;
            string host=dbUri.Host;
            string dbname = dbUri.AbsolutePath.Substring(1);
            connectionString = $"Host={host};Database={dbname};Username={username};Password={password}";
        }
       
        optionsBuilder.UseNpgsql(connectionString);
    }
    //postgres://vakjjpygrcmifm:3c3e828a0022a77f7685884b5bdd26522256fbaf53d0cbfba0833b36cc546e7c@ec2-54-155-110-181.eu-west-1.compute.amazonaws.com:5432/d20m20ds2505g6

    public  void Initialize()
    {
       Database.EnsureCreated();
    }
}