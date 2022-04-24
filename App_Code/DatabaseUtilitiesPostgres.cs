using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using Microsoft.ApplicationBlocks.Data;
using System.Configuration;

namespace SqlStatementGenerator
{
    public class DatabaseUtilitiesPostgres
    {
        public static string connectionString = Convert.ToString(ConfigurationManager.AppSettings["SqlConnectionString"]);
        public static NpgsqlConnection GetConnection(string sConnection, string sDatabase)
        {
            NpgsqlConnection dbconnection = null;

            try
            {
                dbconnection = new NpgsqlConnection(sConnection);
                dbconnection.Open();

                if (sDatabase.Trim() != string.Empty)
                {
                    dbconnection.ChangeDatabase(sDatabase);
                }
            }
            catch (System.InvalidOperationException e)
            {
                throw new Exception("GetConnection error: " + e.Message);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw new Exception("GetConnection error2: " + e.Message);
            }

            return dbconnection;
        }

        public static DataTable GetDatabases(string sConnection)
        {
            NpgsqlConnection connection = null;
            DataSet ds = new DataSet();
            DataTable dt = null;

            try
            {
                connection = GetConnection(sConnection, string.Empty);
                if (connection == null)
                    return dt;

                string sDbCommand = "SELECT datname as DATABASE_NAME, 0 AS DATABASE_SIZE, NULL AS REMARKS FROM pg_database WHERE datistemplate = false ORDER BY DATABASE_NAME";
                using (NpgsqlDataAdapter sqldap = new NpgsqlDataAdapter(sDbCommand, connection))
                {
                    sqldap.Fill(ds);
                }

                if ((ds != null) && (ds.Tables.Count > 0))
                    dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception("GetDatabases error: " + ex.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return dt;
        }

        public static DataTable GetDatabasesForPostgres(string sConnection)
        {
            NpgsqlConnection connection = null;
            DataTable dt = null;
            DataSet ds = new DataSet();
            try
            {
                connection = GetConnection(sConnection, string.Empty);
                if (connection == null)
                    return dt;

                string sDbCommand = "SELECT name AS DATABASE_NAME, 0 AS DATABASE_SIZE, NULL AS REMARKS FROM master.dbo.sysdatabases WHERE HAS_DBACCESS(name) = 1  ORDER BY name";
                using (NpgsqlDataAdapter sqldap = new NpgsqlDataAdapter(sDbCommand, connection))
                {
                    sqldap.Fill(ds);
                }

                if ((ds != null) && (ds.Tables.Count > 0))
                    dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception("GetDatabases error: " + ex.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return dt;
        }

        public static DataTable GetDatabaseTables(string sConnection, string sDatabase)
        {
            NpgsqlConnection connection = null;
            DataTable dt = null;
            DataSet ds = new DataSet();
            try
            {
                connection = GetConnection(sConnection, sDatabase);
                if (connection == null)
                    return dt;
                string sqlString = @"select tablename as TABLE_NAME from pg_tables where schemaname = 'public';";
                NpgsqlCommand cmd = new NpgsqlCommand(sqlString, connection);

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(ds);

                if ((ds != null) && (ds.Tables.Count > 0))
                    dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception("GetDatabaseTables error: " + ex.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return dt;
        }

        public static DataTable GetDatabaseTableColumns(string sConnection, string sDatabase, string sTableName)
        {
            NpgsqlConnection connection = null;
            DataTable dt = null;
            DataSet ds = new DataSet();
            try
            {
                connection = GetConnection(sConnection, sDatabase);
                if (connection == null)
                    return dt;

                SqlParameter[] sqlParms = new SqlParameter[] { new SqlParameter("@table_name", sTableName) };

                using (NpgsqlDataAdapter sqldap = new NpgsqlDataAdapter("sp_columns", connection))
                {
                    sqldap.Fill(ds);
                }


                if ((ds != null) && (ds.Tables.Count > 0))
                    dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception("GetDatabaseTableColumns error: " + ex.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return dt;
        }

        public static DataTable LoadDataTable(string sConnection, string sDatabase, string sQuery)
        {
            NpgsqlConnection connection = null;
            DataTable dt = null;
            DataSet ds = new DataSet();
            try
            {
                connection = GetConnection(sConnection, sDatabase);
                if (connection == null)
                    return dt;

                using (NpgsqlDataAdapter sqldap = new NpgsqlDataAdapter(sQuery, connection))
                {
                    sqldap.Fill(ds);
                }
                if ((ds != null) && (ds.Tables.Count > 0))
                    dt = ds.Tables[0];
                
            }
            catch (Exception ex)
            {
                throw new Exception("GetDatabases error: " + ex.Message);
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return dt;
        }



        /// <summary>  
        /// ִ��SQL���  
        /// </summary>  
        /// <param name="sql">SQL</param>  
        /// <returns>�ɹ����ش���0������</returns>  
        public static int ExecuteSQL(string sql)
        {
            int num2 = -1;
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                using (NpgsqlCommand command = new NpgsqlCommand(sql, connection))
                {
                    try
                    {
                        connection.Open();
                        num2 = command.ExecuteNonQuery();
                    }
                    catch (NpgsqlException exception)
                    {
                        throw new Exception(exception.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            return num2;
        }

        //��������ִ�в�ѯ�������ؽ��������Ӱ������
        //ִ��SQL��䲢������Ӱ�������
        public static int ExecuteNonQuery(string sql, params NpgsqlParameter[] parameters)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    //foreach (SqlParameter param in parameters)
                    //{
                    //    cmd.Parameters.Add(param);
                    //}
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        //ִ�в�ѯ�������ز�ѯ�����صĽ�����е�һ�еĵ�һ�С����Զ�����л��С�
        public static object ExecuteScalar(string sql, params NpgsqlParameter[] parameters)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);
                    return cmd.ExecuteScalar();

                }
            }
        }




        //��ѯ�����ؽ����DataTable,һ��ֻ����ִ�в�ѯ����Ƚ��ٵ�sql��
        public static DataTable ExecuteDataTable(string sql, params NpgsqlParameter[] parameters)
        {
            using (NpgsqlConnection conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (NpgsqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    cmd.Parameters.AddRange(parameters);

                    NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(cmd);
                    DataSet dataset = new DataSet();
                    adapter.Fill(dataset);
                    return dataset.Tables[0];
                }
            }

            //��ѯ�ϴ�������� DateRead()����Ӧ�������÷�ҳ���ݣ���Ȼ��datatable���á�
        }
    }
}
