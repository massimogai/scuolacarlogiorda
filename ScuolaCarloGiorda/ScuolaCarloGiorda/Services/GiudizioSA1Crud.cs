using ScuolaCarloGiorda.Models;

namespace ScuolaCarloGiorda.Services;

public class GiudizioSA1Crud:GiudizioCrud<GiudizioSA1>
{
    public override List<GiudizioSA1> ListGiudizi(Allievo allievo)
    {
        throw new NotImplementedException();
    }

    public override void DiscardChanges(GiudizioSA1 giudizio)
    {
        throw new NotImplementedException();
    }

    public override void Save()
    {
        throw new NotImplementedException();
    }

    public GiudizioSA1Crud(SchoolDbContext schoolDbContext) : base(schoolDbContext)
    {
    }
}