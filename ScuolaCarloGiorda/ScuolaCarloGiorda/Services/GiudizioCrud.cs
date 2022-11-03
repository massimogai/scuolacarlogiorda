using ScuolaCarloGiorda.Models;

namespace ScuolaCarloGiorda.Services;

public abstract class GiudizioCrud<T> where T:Giudizio
{private readonly SchoolDbContext _schoolDbContext;

    protected GiudizioCrud(SchoolDbContext schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }

    public abstract List<T> ListGiudizi(Allievo allievo);

}