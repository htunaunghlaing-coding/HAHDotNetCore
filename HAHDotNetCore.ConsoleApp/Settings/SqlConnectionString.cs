﻿using System;
using System.Data.SqlClient;

namespace HAHDotNetCore.ConsoleApp.Settings;

internal static class SqlConnectionString
{
    public static SqlConnectionStringBuilder stringBuilder = new SqlConnectionStringBuilder()
    {
        DataSource = "localhost",
        InitialCatalog = "HAHDotNetCore",
        UserID = "SA",
        Password = "Password123",
        TrustServerCertificate = true
    };
}

