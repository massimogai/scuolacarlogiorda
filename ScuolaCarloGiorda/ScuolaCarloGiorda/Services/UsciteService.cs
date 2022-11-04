using ScuolaCarloGiorda.Models;

namespace ScuolaCarloGiorda.Services;

public class UsciteService
{
    private readonly SchoolDbContext _schoolDbContext;

    public UsciteService(SchoolDbContext schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }
   
    public void DiscardChanges(Uscita uscita)
    {
        _schoolDbContext.Entry(uscita).Reload();
    }
    public void AddOrUpdateUscita(Uscita uscita)
    {
        
        
        var cursor = _schoolDbContext.Uscite.Where(uscita1 => uscita1.UscitaId == uscita.UscitaId);
        int num = cursor.Count();
        if (num == 0)
        {
            _schoolDbContext.Uscite.Add(uscita);
        }
        else
        {
            Uscita uscitadb = cursor.First();
            uscitadb.Luogo = uscita.Luogo;
            
           
        }
        _schoolDbContext.SaveChanges();
      
      
    }
}