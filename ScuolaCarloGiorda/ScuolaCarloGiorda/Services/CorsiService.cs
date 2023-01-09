using Microsoft.EntityFrameworkCore;
using ScuolaCarloGiorda.Models;

namespace ScuolaCarloGiorda.Services;

public class CorsiService
{
    private readonly SchoolDbContext _schoolDbContext;

    public CorsiService(SchoolDbContext schoolDbContext)
    {
        this._schoolDbContext = schoolDbContext;
    }

    public List<Corso> ListCorsi()
    {
        var corsi = _schoolDbContext.Corsi.Include(corso => corso.Uscite).Include(corso => corso.Allievi)
            .Include(corso => corso.Istruttori).Include(corso =>corso.PresenzeAllievi ).Include(corso =>corso.PresenzeIstruttori )
            .ToList();
        return corsi;
    }

    public void AddOrUpdate(Corso corso)
    {
        var cursor = _schoolDbContext.Corsi.Where(corso1 => corso1.CorsoId == corso.CorsoId);
        int num = cursor.Count();
        if (num == 0)
        {
            _schoolDbContext.Corsi.Add(corso);
        }

        _schoolDbContext.SaveChanges();
    }

    public void DiscardChanges(Corso corso)
    {
        _schoolDbContext.Entry(corso).Reload();
    }
}