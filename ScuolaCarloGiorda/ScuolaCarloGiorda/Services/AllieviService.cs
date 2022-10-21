using NPOI.SS.UserModel;
using ScuolaCarloGiorda.Models;

namespace ScuolaCarloGiorda.Services;

public class AllieviService
{
    private readonly SchoolDbContext _schoolDbContext;

    public AllieviService(SchoolDbContext schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }
    
    public List<Allievo> ListAllievi()
    {
        var corsi = _schoolDbContext.Allievi
            .ToList();
        return corsi;
    }

    public void AddOrUpdateAllievo(Allievo allievo)
    {
        
        var cursor = _schoolDbContext.Allievi.Where(allievo1 => allievo1.Cf == allievo.Cf);
        int num = cursor.Count();
        if (num == 0)
        {
            _schoolDbContext.Allievi.Add(allievo);
        }
        else
        {
            Allievo allievoDb = cursor.First();
            allievoDb.Nome = allievo.Nome;
            allievoDb.Cognome = allievo.Cognome;
            allievoDb.Mail = allievo.Mail;
          
        }
        _schoolDbContext.SaveChanges();
        }

    public void Delete(Allievo allievo)
    {
        _schoolDbContext.Allievi.Remove(allievo);
        _schoolDbContext.SaveChanges();
    }
    public void Save()
    {
        _schoolDbContext.SaveChanges();
    }

    public async Task<List<Allievo>> LoadFromStream(Stream stream)
    {
        Dictionary<string, int> _columnsLabDictionary = new();

        _columnsLabDictionary[StudentColumns.Nome.ToLower()] = -1;
        _columnsLabDictionary[StudentColumns.Cognome.ToLower()] = -1;
        _columnsLabDictionary[StudentColumns.CF.ToLower()] = -1;
        _columnsLabDictionary[StudentColumns.Mail.ToLower()] = -1;

        MemoryStream ms = new MemoryStream();
        await stream.CopyToAsync(ms);
        ms.Seek(0,SeekOrigin.Begin);
        NPOI.HSSF.UserModel.HSSFWorkbook hSSFWorkbook = new NPOI.HSSF.UserModel.HSSFWorkbook(ms);
        ISheet sheet = hSSFWorkbook.GetSheetAt(0);
        var headerRow = sheet.GetRow(0);
        ExtractColumnIndex(_columnsLabDictionary, headerRow);

        for (int row = 1; row <= sheet.LastRowNum; row++)
        {
            var sheetRow = sheet.GetRow(row);
            if (sheetRow != null)
            {
                string nome = sheetRow.GetCell(_columnsLabDictionary[StudentColumns.Nome.ToLower()]).StringCellValue;
                string cognome = sheetRow.GetCell(_columnsLabDictionary[StudentColumns.Cognome.ToLower()]).StringCellValue;
                string cf = sheetRow.GetCell(_columnsLabDictionary[StudentColumns.CF.ToLower()]).StringCellValue;
                string mail = sheetRow.GetCell(_columnsLabDictionary[StudentColumns.Mail.ToLower()]).StringCellValue;
                Allievo allievo = new Allievo
                {
                    Nome = nome,
                    Cognome = cognome,
                    Cf = cf,
                    Mail = mail
                };
                this.AddOrUpdateAllievo(allievo);
            }
        }

        return ListAllievi();
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
    private static class StudentColumns
    {
        public const string Nome = "Nome";
        public const string Cognome = "Cognome";
        public const string CF = "CF";
        public const string Mail = "Mail";
    }
}