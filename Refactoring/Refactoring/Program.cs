using System.Data;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

public interface IPassportView
{
    void ShowMessage(string message);
    void SetResult(string message);
}

public class PassportView : IPassportView
{
    private readonly TextBox _passportTextbox;
    private readonly TextBox _textResult;
    private readonly PassportChecker _passportChecker;

    public PassportView(CheckerFactory checkerFactory)
    {
        _passportChecker = checkerFactory.Create(this);
    }

    public void SetResult(string message)
    {
        _textResult.Text = message;
    }

    public void ShowMessage(string message)
    {
        MessageBox.Show(message);
    }

    public void OnButtonClick(object sender, EventArgs @event)
    {
        _passportChecker.CheckPassport(_passportTextbox.Text);
    }
}

public class PassportChecker
{
    private readonly IPassportView _view;
    private readonly PassportService _passportService;

    public PassportChecker(IPassportView view, PassportService passportService)
    {
        _view = view;
        _passportService = passportService;
    }

    public void CheckPassport(string rawData)
    {
        Passport passport = new Passport(rawData);

        bool? result = _passportService.GetAccessStatus(passport);

        if (result == null)
        {
            _view.SetResult($"Паспорт «{passport.Data}» в списке участников дистанционного голосования НЕ НАЙДЕН");

            return;
        }

        if (result.Value)
        {
            _view.SetResult($"По паспорту «{passport.Data}» доступ к бюллетеню на дистанционном электронном голосовании ПРЕДОСТАВЛЕН");
        }
        else
        {
            _view.SetResult($"По паспорту «{passport.Data}» доступ к бюллетеню на дистанционном электронном голосовании НЕ ПРЕДОСТАВЛЯЛСЯ");
        }
    }
}

public class PassportDatabase
{
    public DataTable GetDataTable(string hash)
    {
        string connectionString = string.Format("Data Source=" + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\db.sqlite");

        try
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            SQLiteDataAdapter sqLiteDataAdapter = new SQLiteDataAdapter(new SQLiteCommand(hash, connection));

            DataTable dataTable = new DataTable();
            sqLiteDataAdapter.Fill(dataTable);
            connection.Close();

            return dataTable;
        }
        catch (SQLiteException ex)
        {
            if (ex.ErrorCode != 1)
                return new DataTable();

            throw new FileNotFoundException("Файл db.sqlite не найден. Положите файл в папку вместе с exe.");
        }
    }
}

public class Passport
{
    public Passport(string rawData)
    {
        rawData = rawData.Trim().Replace(" ", string.Empty);

        if (string.IsNullOrWhiteSpace(rawData))
            throw new ArgumentException("Введите серию и номер паспорта");

        if (rawData.Length < 10)
            throw new ArgumentException("Неверный формат серии или номера паспорта");

        Data = rawData;
    }

    public string Data { get; }
}

public class PassportService
{
    private readonly PassportDatabase _database;
    private readonly PassportHasher _hasher;

    public PassportService(PassportDatabase database, PassportHasher hasher)
    {
        _database = database;
        _hasher = hasher;
    }

    public bool? GetAccessStatus(Passport passport)
    {
        string hash = _hasher.ComputeSha256Hash(passport.Data);
        DataTable dataTable = _database.GetDataTable($"select * from passports where num='{hash}' limit 1;");

        if (dataTable.Rows.Count > 0)
            return Convert.ToBoolean(dataTable.Rows[0].ItemArray[1]);

        return null;
    }
}

public class CheckerFactory
{
    public PassportChecker Create(IPassportView passportView)
    {
        PassportService passportService = new PassportService(new PassportDatabase(), new PassportHasher());

        return new PassportChecker(passportView, passportService);
    }
}

public class PassportHasher
{
    public string ComputeSha256Hash(string rawData)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
                builder.Append(bytes[i].ToString("x2"));

            return builder.ToString();
        }
    }
}
