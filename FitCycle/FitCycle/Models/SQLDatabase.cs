using FitCycle.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace FitCycle
{
    public static class SQLDatabase
    {
        static int timeout = 30000;
        static string connStr = "Server=tcp:azureserver-christophcodet.database.windows.net,1433;Initial Catalog=TestSQLDatabase;Persist Security Info=False;User ID=FitCycleApp;Password=FitCycle#2020;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=" + (timeout / 1000) + ";";

        public static int GetSQLTimeout()
        {
            return timeout;
        }
        /// <summary>
        /// Gibt eine Liste mit Zeilen, die ein Dictinary mit den selektierten Werten (objects) und Spaltennamen (string) enthält, zurück
        /// </summary>
        public static async Task<List<Dictionary<string, object>>> ReadSQLAsync(string sqlcode)
        {
            using (var conn = new SqlConnection(connStr))
            {
                using (var command = await Task.Run(() => conn.CreateCommand()))
                {
                    command.CommandText = sqlcode;
                    try
                    {
                        conn.Open();
                    }
                    catch (SqlException ex)
                    {
                        throw new OperationCanceledException("Fehler bei der Verbindung: " + ex.Message);//<--------------statt Ex besser Fehlercode
                    }
                    var output = new List<Dictionary<string, object>>();
                    var row = new Dictionary<string, object>();
                    if (sqlcode.ToLower().Contains("select") == true)
                    {
                        try
                        {
                            using (var reader = await Task.Run(() => command.ExecuteReader()))
                            {
                                while (reader.Read())
                                {
                                    for (int i = 0; i < reader.FieldCount; i++)
                                    {
                                        if (reader.GetValue(i) != DBNull.Value)
                                        {
                                            row[reader.GetName(i)] = reader.GetValue(i);
                                        }
                                        else
                                        {
                                            row[reader.GetName(i)] = null;
                                        }
                                    }
                                    output.Add(new Dictionary<string, object>(row));
                                    row.Clear();
                                }
                            }
                        }
                        catch (SqlException ex)
                        {
                            output = null;
                            Debug.WriteLine("SQL Fehler: " + ex + " SQL: " + sqlcode);
                        }
                        catch (InvalidOperationException ex)
                        {
                            throw new InvalidOperationException("InvalidOperation:" + ex);
                        }
                    }
                    return output;
                }

            }

        }


        /// <summary>
        /// Führt SQL aus und liefert OUTPUT als int zurück. SQL muss OUTPUT als int definieren.
        /// </summary>
        public static async Task<int> ExecuteSQLAsync(string sqlcode)
        {
            if (sqlcode == null) { return 0; }
            using (var conn = new SqlConnection(connStr))
            {
                using (var command = await Task.Run(() => conn.CreateCommand()))
                {
                    command.CommandText = sqlcode;
                    try
                    {
                        conn.Open();
                    }
                    catch (SqlException ex)
                    {
                        Debug.WriteLine("Fehler bei der Verbindung: " + ex.Message);
                        return 0;
                    }
                    try
                    {
                        if (sqlcode.Contains("OUTPUT"))
                        {
                            Debug.WriteLine("Execute SQL: " + sqlcode);
                            var output = command.ExecuteScalar();
                            if (output != null)
                            {
                                return (int)output;
                            }
                            else { return 0; }
                        }
                        else
                        {
                            return 0;
                        }

                    }
                    catch (SqlException ex)
                    {
                        Debug.WriteLine("SQL Fehler: " + ex.Message);
                        return 0;
                    }
                    catch
                    {
                        Debug.WriteLine("Fehler bei ExecuteSQLAsync");
                        return 0;
                    }
                }

            }

        }
        /// <summary>
        /// Führt die in einer Liste angegebenen SQL-Codes (string) aus und liefert bei Erfolg eine Liste mit den neuen IDs zurück. (OUTPUT in SQL erforderlich)
        /// </summary>
        public static async Task<List<int>> ExecuteSQLsAsync(List<string> sqlcodes)
        {
            var outputlist = new List<int>();
            if (sqlcodes == null) { return outputlist; }
            using (var conn = new SqlConnection(connStr))
            {
                using (var command = await Task.Run(() => conn.CreateCommand()))
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (SqlException ex)
                    {
                        Debug.WriteLine("Fehler bei der Verbindung: " + ex.Message);
                        return outputlist;
                    }

                    foreach (string sql in sqlcodes)
                    {
                        if (sql != null)
                        {
                            command.CommandText = sql;
                            try
                            {
                                if (sql.ToLower().Contains("output"))
                                {
                                    Debug.WriteLine("Execute SQL: " + sql);
                                    var output = command.ExecuteScalar();
                                    if (output != null)
                                    {
                                        outputlist.Add((int)output);
                                    }
                                }

                            }
                            catch (SqlException ex)
                            {
                                Debug.WriteLine("SQL Fehler: " + ex.Message);
                                return outputlist;
                            }
                            catch (NullReferenceException ex)
                            {
                                Debug.WriteLine("SQL Fehler: " + ex.Message);
                                return outputlist;
                            }
                            catch
                            {
                                Debug.WriteLine("Fehler bei ExecuteSQLsAsync");
                                return outputlist;
                            }
                        }
                    }
                    return outputlist;
                }

            }

        }



        public static async Task<string> GetStringfromIDAsync(int id, string columnname, string tablename)
        {
            var sql = "SELECT " + columnname + " FROM " + tablename + " WHERE id=" + id;
            var output = new List<Dictionary<string,object>>();
            output = await ReadSQLAsync(sql);
            if (output.Count == 1)
            {
                return (string)output[0][columnname];
            }
            else
            {
                throw new ArgumentException("More than one element found");
            }
        }

    }
}
