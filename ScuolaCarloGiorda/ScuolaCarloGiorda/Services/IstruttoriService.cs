using NPOI.SS.UserModel;
using ScuolaCarloGiorda.Models;

namespace ScuolaCarloGiorda.Services;

public class IstruttoriService
{
    private readonly SchoolDbContext _schoolDbContext;

    public IstruttoriService(SchoolDbContext schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }

    public List<Istruttore> ListIstruttori()
    {
        var corsi = _schoolDbContext.Istruttori
            .ToList();
        return corsi;
    }

    public void AddOrUpdateIstruttore(Istruttore istruttore)
    {
        var cursor = _schoolDbContext.Istruttori.Where(istruttore1 => istruttore1.Cf == istruttore.Cf);
        int num = cursor.Count();
        if (num == 0)
        {
            _schoolDbContext.Istruttori.Add(istruttore);
        }
        else
        {
            Istruttore istruttoredb = cursor.First();
            istruttoredb.Nome = istruttore.Nome;
            istruttoredb.Cognome = istruttore.Cognome;
            istruttoredb.Mail = istruttore.Mail;
           
        }
        _schoolDbContext.SaveChanges();
      
    }

    public void Delete(Istruttore istruttore)
    {
        _schoolDbContext.Istruttori.Remove(istruttore);
        _schoolDbContext.SaveChanges();
    }

    public void Save()
    {
        _schoolDbContext.SaveChanges();
    }

    public void DiscardChanges(Istruttore istruttore)
    {
        _schoolDbContext.Entry(istruttore).Reload();
    }

    public async Task LoadFromStream(Stream stream)
    {
        Dictionary<string, int> _columnsLabDictionary = new();

        _columnsLabDictionary[InstructorColumns.Nome.ToLower()] = -1;
        _columnsLabDictionary[InstructorColumns.Cognome.ToLower()] = -1;
        _columnsLabDictionary[InstructorColumns.CF.ToLower()] = -1;
        _columnsLabDictionary[InstructorColumns.Mail.ToLower()] = -1;
        MemoryStream ms = new MemoryStream();
       await stream.CopyToAsync(ms);
       ms.Seek(0,SeekOrigin.Begin);
        NPOI.HSSF.UserModel.HSSFWorkbook hSSFWorkbook = new NPOI.HSSF.UserModel.HSSFWorkbook(ms);
        NPOI.SS.UserModel.ISheet sheet = hSSFWorkbook.GetSheetAt(0);
        var headerRow = sheet.GetRow(0);
        ExtractColumnIndex(_columnsLabDictionary, headerRow);

        for (int row = 1; row <= sheet.LastRowNum; row++)
        {
            var sheetRow = sheet.GetRow(row);
            if (sheetRow != null)
            {
                string nome = sheetRow.GetCell(_columnsLabDictionary[InstructorColumns.Nome.ToLower()]).StringCellValue;
                string cognome = sheetRow.GetCell(_columnsLabDictionary[InstructorColumns.Cognome.ToLower()]).StringCellValue;
                string cf = sheetRow.GetCell(_columnsLabDictionary[InstructorColumns.CF.ToLower()]).StringCellValue;
                string mail = sheetRow.GetCell(_columnsLabDictionary[InstructorColumns.Mail.ToLower()]).StringCellValue;
                Istruttore istruttore = new Istruttore
                {
                    Nome = nome,
                    Cognome = cognome,
                    Cf = cf,
                    Mail = mail
                };
                this.AddOrUpdateIstruttore(istruttore);
            }
        }
    }

    private void ExtractColumnIndex(Dictionary<string, int> columnsDictionary, IRow row)
    {
        var unexpectedColumns = new List<string>();
        for (var i = 0; i < row.PhysicalNumberOfCells; i++)
        {
            var cell = row.GetCell(i);
            var cellName = cell.StringCellValue.Trim().ToLower();
            if (columnsDictionary.ContainsKey(cellName))
                columnsDictionary[cellName] = i;
            else
                unexpectedColumns.Add(cellName);
        }

        var missingColumns = new List<string>();
        foreach (var propertyName in columnsDictionary.Keys)
            if (columnsDictionary[propertyName] == -1)
                missingColumns.Add(propertyName);

        if (missingColumns.Count > 0 || unexpectedColumns.Count > 0)
        {
            var missingMessage = string.Join(",", missingColumns);
            var unexpectedFieldMessage = string.Join(",", unexpectedColumns);
            var message = "missing:" + missingMessage + "\n.Unexpected:" + unexpectedFieldMessage;
            var ex = new InvalidSheetException(message);
            throw ex;
        }
    }


    private static class InstructorColumns
    {
        public const string Nome = "Nome";
        public const string Cognome = "Cognome";
        public const string CF = "CF";
        public const string Mail = "Mail";
    }
}

internal class InvalidSheetException : Exception
{
    public InvalidSheetException(string message)
    {
        throw new NotImplementedException();
    }
}