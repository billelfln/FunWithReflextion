// File: Models/BaseModel.cs
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace FunWithReflextion
{
    public abstract class BaseModel<T> where T : new()
    {
        private static string connectionString = "Data Source=.;Initial Catalog=UserDatabase;Integrated Security=True";

        public static DataTable All()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT * FROM {typeof(T).Name}s";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
    }
}
