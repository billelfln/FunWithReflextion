using FunWithReflextion.Attributes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace FunWithReflextion
{
    public abstract class BaseModel<T> where T : new()
    {
        private static string connectionString = "Data Source=.;Initial Catalog=UserDatabase;Integrated Security=True";

        public static DataTable All()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string tableName = GetTableName();
                    string query = $"SELECT * FROM {tableName}";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
            catch (SqlException ex)
            {
                // التعامل مع استثناءات SQL
                throw new ApplicationException("An error occurred while accessing the database.", ex);
            }
            catch (Exception ex)
            {
                // التعامل مع استثناءات عامة
                throw new ApplicationException("An unexpected error occurred.", ex);
            }
        }

        private static string GetTableName()
        {
            var tableNameAttribute = (TableNameAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(TableNameAttribute));
            if (tableNameAttribute != null)
            {
                return tableNameAttribute.TableName;
            }
            else
            {
                throw new InvalidOperationException($"The class {typeof(T).Name} does not have a TableName attribute.");
            }
        }

        public static void Add(T entity)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string tableName = GetTableName();
                    var properties = typeof(T).GetProperties();
                    var columns = string.Join(", ", properties.Select(p => p.Name));
                    var values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

                    string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values})";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        foreach (var property in properties)
                        {
                            command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(entity) ?? DBNull.Value);
                        }
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new ApplicationException("An error occurred while accessing the database.", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An unexpected error occurred.", ex);
            }
        }
        //in case of the Id is Auto number
        //public static void Add(T entity)
        //{
        //    try
        //    {
        //        using (SqlConnection connection = new SqlConnection(connectionString))
        //        {
        //            connection.Open();
        //            string tableName = GetTableName();
        //            var properties = typeof(T).GetProperties().Where(p => p.Name != "Id");
        //            var columns = string.Join(", ", properties.Select(p => p.Name));
        //            var values = string.Join(", ", properties.Select(p => $"@{p.Name}"));

        //            string query = $"INSERT INTO {tableName} ({columns}) VALUES ({values}); SELECT SCOPE_IDENTITY();";
        //            using (SqlCommand command = new SqlCommand(query, connection))
        //            {
        //                foreach (var property in properties)
        //                {
        //                    command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(entity) ?? DBNull.Value);
        //                }

        //                var id = Convert.ToInt32(command.ExecuteScalar());
        //                typeof(T).GetProperty("Id").SetValue(entity, id);
        //            }
        //        }
        //    }
        //    catch (SqlException ex)
        //    {
        //        throw new ApplicationException("An error occurred while accessing the database.", ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new ApplicationException("An unexpected error occurred.", ex);
        //    }
        //}

    }
}
