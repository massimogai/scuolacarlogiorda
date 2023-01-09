using ScuolaCarloGiorda.Models;

namespace ScuolaCarloGiorda.Services;

public class GiudizioAR1Crud:GiudizioCrud<GiudizioAR1>
{
    public override List<GiudizioAR1> ListGiudizi(Allievo allievo)
    {
        throw new NotImplementedException();
    }

    public override void DiscardChanges(GiudizioAR1 giudizio)
    {
        
    }

    public GiudizioAR1Crud(SchoolDbContext schoolDbContext) : base(schoolDbContext)
    {
    }


    public override void Save()
    {
      
    }
}