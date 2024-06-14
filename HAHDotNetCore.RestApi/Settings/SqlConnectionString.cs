using System;
using System.Data.SqlClient;

namespace HAHDotNetCore.RestApi;

internal static class SqlConnectionString
{
    public static SqlConnectionStringBuilder StringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = "localhost",
        InitialCatalog = "HAHDotNetCore",
        UserID = "SA",
        Password = "Password123",
        TrustServerCertificate = true
    };
}

