namespace ScuolaCarloGiorda.Models;

public class Corso
{
    public string Name { get; set; }
    public int CorsoId { get; set; }
    public List<Allievo> Allievi { get; set; }
    public List<Istruttore> Istruttori { get; set; }
    public List<Uscita> Uscite { get; set; }
    public TipoCorso TipoCorso { get; set; }
    
}

public enum TipoCorso
{
        AR1,SA1,SA2,A1,AL1
}
public class Uscita
{
    public int UscitaId { get; set; }
    public List<Istruttore> Istruttori { get; set; }
    public DateTime Data { get; set; }
    public string Luogo { get; set; }
    public int CorsoId { get; set; }
    
}

public class Istruttore
{
    public int IstruttoreId { get; set; }
    public string Nome { get; set; }
    public string Cognome { get; set; }

}

public class Allievo
{
    public int AllievoId { get; set; }
    public string Nome{ get; set; }
    public string Cognome{ get; set; }
    List<Giudizio> Giudizi{ get; set; }
    
}

public class Giudizio
{
    public int IstruttoreId { get; set; }
    public int UscitaId { get; set; }
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