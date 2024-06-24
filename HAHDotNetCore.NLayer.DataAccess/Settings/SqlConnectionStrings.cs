using System;
using Microsoft.Data.SqlClient;

namespace HAHDotNetCore.NLayer.DataAccess.Settings;

internal static class SqlConnectionStrings
{
    public static SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = "localhost",
        InitialCatalog = "HAHDotNetCore",
        UserID = "SA",
        Password = "Password123",
        TrustServerCertificate = true
    };

}

