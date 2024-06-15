using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace HAHDotNetCore.Shared;

public class DapperService
{
    private readonly string _SqlConnectionString;

    public DapperService(string sqlConnectionString)
    {
        _SqlConnectionString = sqlConnectionString;
    }

    public List<T> Query<T>(string query, Object? param = null)
    {
        using IDbConnection db = new SqlConnection(_SqlConnectionString);
        List<T> list = db.Query<T>(query, param).ToList();
        return list;
    }

    public T QueryFirstOrDefault<T>(string query, Object? param = null)
    {
        using IDbConnection db = new SqlConnection(_SqlConnectionString);
        var item= db.Query<T>(query, param).FirstOrDefault();
        return item!;
    }

    public int Execute(string query, Object? param = null)
    {
        using IDbConnection db = new SqlConnection(_SqlConnectionString);
        var result = db.Execute(query, param);
        return result;
    }
}

 