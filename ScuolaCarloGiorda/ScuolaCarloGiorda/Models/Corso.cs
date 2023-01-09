using System.ComponentModel.DataAnnotations;

namespace ScuolaCarloGiorda.Models;

public class Corso
{
    /*
     * create extension hstore;
     */
     [Required] public string Name { get; set; }
    public int CorsoId { get; set; }
    public List<Allievo> Allievi { get; set; } = new List<Allievo>();
    public List<Istruttore> Istruttori { get; set; } = new List<Istruttore>();
    public  List<Uscita> Uscite { get; set; } = new List<Uscita>();
    public TipoCorso TipoCorso { get; set; } = TipoCorso.A1;
    public List<PresenzaAllievi> PresenzeAllievi { get; set; } = new ();
    public List<PresenzaIstruttori> PresenzeIstruttori { get; set; } =  new ();

}

public class PresenzaAllievi
{
    public int PresenzaAllieviId { get; set; }
    public Allievo Allievo { get; set; }
    public Uscita Uscita{ get; set; }
    public Corso Corso { get; set; }
    
}
public class PresenzaIstruttori
{
    public int PresenzaIstruttoriId { get; set; }
    public Istruttore Istruttore { get; set; }
    public Uscita Uscita{ get; set; }
    public Corso Corso { get; set; }
    
}

public enum TipoCorso
{
        AR1,SA1,SA2,A1,AL1
}
public class Uscita
{
    public int UscitaId { get; set; }
   
    public DateTime Data { get; set; }
    [Required] public string Luogo { get; set; }
    public  Corso Corso{ get; set; }
    
}

public class Istruttore
{
    public int IstruttoreId { get; set; }
    public string Nome { get; set; }
    public string Cognome { get; set; }
    public string Cf { get; set; }
    public string Mail { get; set; }
    public List<Corso> Corsi { get; set; } = new List<Corso>();
}

public class Allievo
{
    public int AllievoId { get; set; }
    public string Nome{ get; set; }
    public string Cognome{ get; set; }
    public List<Giudizio> Giudizi{ get; set; }
    public string Cf { get; set; }
    public string Mail { get; set; }
    public List<Corso> Corsi { get; set; } = new List<Corso>();
}

public class Giudizio
{
    public Istruttore Istruttore { get; set; }
    public Uscita Uscita { get; set; }
   
    public int GiudizioId { get; set; }
    public DateTime Data { get; set; }

}
public class GiudizioAR1:Giudizio
{


}
public class GiudizioAL1:Giudizio
{


}
public class GiudizioSA1:Giudizio
{


}
public class GiudizioSA2:Giudizio
{


}
public class GiudizioA1:Giudizio
{


}
public class GiudizioAffiancato:Giudizio
{


}