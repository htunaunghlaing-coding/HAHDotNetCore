using System;
using Microsoft.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;

namespace HAHDotNetCore.Shared;

public class AdoDotNetService
{
    private readonly string _sqlConnection;

    public AdoDotNetService(string sqlConnection)
    {
        _sqlConnection = sqlConnection;
    }

    public List<T> Query<T>(string query, params AdoDotNetParameter[]? parameters)
    {
        SqlConnection connection = new SqlConnection(_sqlConnection);
        connection.Open();

        SqlCommand cmd = new SqlCommand(query, connection);
        if (parameters is not null && parameters.Length > 0)
        {
            foreach (var item in parameters)
            {
                cmd.Parameters.AddWithValue(item.Key, item.Value);
            }

        }
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable data = new DataTable();
        adapter.Fill(data);

        connection.Close();

        string json = JsonConvert.SerializeObject(data);
        List<T> list = JsonConvert.DeserializeObject<List<T>>(json)!;

        return list;
    }

    public T QueryFirstOrDefault<T>(string query, params AdoDotNetParameter[]? parameters)
    {
        SqlConnection connection = new SqlConnection(_sqlConnection);
        connection.Open();

        SqlCommand cmd = new SqlCommand(query, connection);
        if (parameters is not null && parameters.Length > 0)
        {
            foreach (var item in parameters)
            {
                cmd.Parameters.AddWithValue(item.Key, item.Value);
            }

        }
        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
        DataTable data = new DataTable();
        adapter.Fill(data);

        connection.Close();

        string json = JsonConvert.SerializeObject(data);
        List<T> list = JsonConvert.DeserializeObject<List<T>>(json)!;

        return list[0];
    }

    public int Execute(string query, params AdoDotNetParameter[]? parameters)
    {
        SqlConnection connection = new SqlConnection(_sqlConnection);
        connection.Open();

        SqlCommand cmd = new SqlCommand(query, connection);
        if (parameters is not null && parameters.Length > 0)
        {
            foreach (var item in parameters)
            {
                cmd.Parameters.AddWithValue(item.Key, item.Value);
            }

        }
        var result = cmd.ExecuteNonQuery();

        connection.Close();
        return result;
    }


    public class AdoDotNetParameter
    {
        public AdoDotNetParameter() { }

        public AdoDotNetParameter(string key, object value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }
        public Object Value { get; set; }
    }

}

