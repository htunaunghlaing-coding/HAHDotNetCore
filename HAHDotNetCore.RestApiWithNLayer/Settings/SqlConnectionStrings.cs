using System;
using System.Data.SqlClient;

namespace HAHDotNetCore.RestApiWithNLayer.Settings;

internal static class SqlConnectionStrings
{
    public static SqlConnectionStringBuilder sqlConnection = new SqlConnectionStringBuilder()
    {
        DataSource = "localhost",
        InitialCatalog = "HAHDotNetCore",
        UserID = "SA",
        Password = "Password123",
        TrustServerCertificate = true
    };
}

