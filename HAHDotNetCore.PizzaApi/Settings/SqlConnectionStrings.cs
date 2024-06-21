using System;
using System.Data.SqlClient;

namespace HAHDotNetCore.PizzaApi.Settings;

internal static class SqlConnectionStrings
{
    public static SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder
    {
        DataSource = "localhost",
        InitialCatalog = "HAHDotNetCore",
        UserID = "SA",
        Password = "Password123",
        TrustServerCertificate = true
    };
}

