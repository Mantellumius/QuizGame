namespace QuizGame.Server.DataAccess;

public static class ConnectionStringBuilder
{
    public static string BuildFrom(string? uriString)
    {
        if (uriString == null) throw new ArgumentNullException(nameof(uriString));
        var uri = new Uri(uriString);
        var db = uri.AbsolutePath.Trim('/');
        var user = uri.UserInfo.Split(':')[0];
        var passwd = uri.UserInfo.Split(':')[1];
        var port = uri.Port > 0 ? uri.Port : 5432;
        var connStr = $"Server={uri.Host};Database={db};User Id={user};Password={passwd};Port={port}";
        return connStr;
    }
}