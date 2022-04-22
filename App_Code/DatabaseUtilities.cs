using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Text;
using Microsoft.ApplicationBlocks.Data;

namespace SqlStatementGenerator
{
    public class DatabaseUtilities
    {

        public static SqlConnection GetConnection(string sConnection, string sDatabase)
        {
            SqlConnection dbconnection = null;

            try
            {
                dbconnection = new SqlConnection(sConnection);
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
            SqlConnection connection = null;
            DataTable dt = null;

            try
            {
                connection = GetConnection(sConnection, string.Empty);
                if (connection == null)
                    return dt;

                string sDbCommand = "SELECT name AS DATABASE_NAME, 0 AS DATABASE_SIZE, NULL AS REMARKS FROM master.dbo.sysdatabases WHERE HAS_DBACCESS(name) = 1  ORDER BY name";
                DataSet dset = SqlHelper.ExecuteDataset(connection, CommandType.Text, sDbCommand);

                if ((dset != null) && (dset.Tables.Count > 0))
                    dt = dset.Tables[0];
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
            SqlConnection connection = null;
            DataTable dt = null;

            try
            {
                connection = GetConnection(sConnection, string.Empty);
                if (connection == null)
                    return dt;

                string sDbCommand = "SELECT name AS DATABASE_NAME, 0 AS DATABASE_SIZE, NULL AS REMARKS FROM master.dbo.sysdatabases WHERE HAS_DBACCESS(name) = 1  ORDER BY name";
                DataSet dset = SqlHelper.ExecuteDataset(connection, CommandType.Text, sDbCommand);

                if ((dset != null) && (dset.Tables.Count > 0))
                    dt = dset.Tables[0];
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
            SqlConnection connection = null;
            DataTable dt = null;

            try
            {
                connection = GetConnection(sConnection, sDatabase);
                if (connection == null)
                    return dt;

                SqlCommand cmd = new SqlCommand("sp_tables", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@table_type", "'TABLE'");

                DataSet dset = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dset);

                if ((dset != null) && (dset.Tables.Count > 0))
                    dt = dset.Tables[0];
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
            SqlConnection connection = null;
            DataTable dt = null;

            try
            {
                connection = GetConnection(sConnection, sDatabase);
                if (connection == null)
                    return dt;

                SqlParameter[] sqlParms = new SqlParameter[] { new SqlParameter("@table_name", sTableName) };
                DataSet dset = SqlHelper.ExecuteDataset(connection, "sp_columns", sqlParms);

                if ((dset != null) && (dset.Tables.Count > 0))
                    dt = dset.Tables[0];
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
            SqlConnection connection = null;
            DataTable dt = null;

            try
            {
                connection = GetConnection(sConnection, sDatabase);
                if (connection == null)
                    return dt;

                DataSet dset = SqlHelper.ExecuteDataset(connection, CommandType.Text, sQuery);
                if ((dset != null) && (dset.Tables.Count > 0))
                    dt = dset.Tables[0];
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
    }
}
