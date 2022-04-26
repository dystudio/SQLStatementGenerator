using System;
using System.Xml;
using System.IO;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Text;
using SqlStatementGenerator.App_Code;

namespace SqlStatementGenerator
{
    public class SqlScriptGenerator
    {
        private const string insertSqlServerTemplate = @"INSERT INTO [{0}]({1}) ";
        private const string updateSqlServerTemplate = @"UPDATE [{0}] SET {1} WHERE {2};";
        private const string deleteSqlServerTemplate = @"DELETE FROM [{0}] WHERE {1};";

        private const string insertPostgresTemplate = @"INSERT INTO {0}({1}) ";
        private const string updatePostgresTemplate = @"UPDATE {0} SET {1} WHERE {2};";
        private const string deletePostgresTemplate = @"DELETE FROM {0} WHERE {1};";

        public static string GenerateSqlInserts(DatabaseType type, ArrayList aryColumns, DataTable dtTable, string sTargetTableName)
        {

            string sSqlInserts = string.Empty;
            StringBuilder sbSqlStatements = new StringBuilder(string.Empty);
            StringBuilder sbColumns = new StringBuilder(string.Empty);

            // create the columns portion of the INSERT statement						            
            foreach (string colname in aryColumns)
            {
                if (sbColumns.ToString() != string.Empty)
                    sbColumns.Append(", ");
                if (type.Equals(DatabaseType.Sqlserver))
                {
                    sbColumns.Append("[" + colname + "]");
                }
                else if (type.Equals(DatabaseType.Postgres))
                {
                    sbColumns.Append("" + colname + "");
                }
                
            }

            // loop thru each record of the datatable
            foreach (DataRow drow in dtTable.Rows)
            {
                // loop thru each column, and include the value if the column is in the array
                StringBuilder sbValues = new StringBuilder(string.Empty);
                foreach (string col in aryColumns)
                {
                    if (sbValues.ToString() != string.Empty)
                        sbValues.Append(", ");

                    // need to do a case to check the column-value types(quote strings(check for dups first), convert bools)
                    string sType = string.Empty;
                    try
                    {
                        sType = drow[col].GetType().ToString();
                        switch (sType.Trim().ToLower())
                        {
                            case "system.boolean":
                                sbValues.Append((Convert.ToBoolean(drow[col]) == true ? "1" : "0"));
                                break;

                            case "system.string":
                                sbValues.Append(string.Format("'{0}'", QuoteSQLString(drow[col])));
                                break;

                            case "system.datetime":
                                string sDateTime = QuoteSQLString(drow[col]);
                                if (Validation.IsDateTime(sDateTime) == true)
                                    sDateTime = System.DateTime.Parse(sDateTime).ToString("yyyy-MM-dd HH:mm:ss");
                                else
                                    sDateTime = string.Empty;
                                sbValues.Append(string.Format("'{0}'", sDateTime));
                                break;

                            case "system.byte[]":
                                sbValues.Append(string.Format("'{0}'", Convert.ToBase64String((byte[])drow[col])));
                                break;                                

                            default:
                                if (drow[col] == System.DBNull.Value)
                                    sbValues.Append("NULL");
                                else
                                    sbValues.Append(Convert.ToString(drow[col]));
                                break;
                        }
                    }
                    catch
                    {
                        sbValues.Append(string.Format("'{0}'", QuoteSQLString(drow[col])));
                    }
                }

                //   INSERT INTO Tabs(Name) 
                //      VALUES('Referrals')
                // write the insert line out to the stringbuilder
                var template = type == DatabaseType.Sqlserver ? insertSqlServerTemplate : insertPostgresTemplate;

                string snewsql = string.Format(template, sTargetTableName, sbColumns.ToString());
                sbSqlStatements.Append(snewsql);
                sbSqlStatements.AppendLine();
                sbSqlStatements.Append('\t');
                snewsql = string.Format("VALUES({0});", sbValues.ToString());
                sbSqlStatements.Append(snewsql);
                sbSqlStatements.AppendLine();
                sbSqlStatements.AppendLine();
            }

            sSqlInserts = sbSqlStatements.ToString();
            return sSqlInserts;
        }

        public static string GenerateSqlUpdates(DatabaseType type, ArrayList aryColumns, ArrayList aryWhereColumns, DataTable dtTable, string sTargetTableName)
        {
            string sSqlUpdates = string.Empty;
            StringBuilder sbSqlStatements = new StringBuilder(string.Empty);
            StringBuilder sbColumns = new StringBuilder(string.Empty);

            // UPDATE table SET col1 = 3, col2 = 4 WHERE (select cols)
            // loop thru each record of the datatable
            foreach (DataRow drow in dtTable.Rows)
            {
                // VALUES clause:  loop thru each column, and include the value if the column is in the array
                StringBuilder sbValues = new StringBuilder(string.Empty);                

                foreach (string col in aryColumns)
                {
                    StringBuilder sbNewValue = new StringBuilder();
                    if (type.Equals(DatabaseType.Sqlserver)) {
                        sbNewValue.Append("[" + col + "] = ");
                    }
                    else if(type.Equals(DatabaseType.Postgres))
                    {
                        sbNewValue.Append("" + col + " = ");
                    }
                    if (sbValues.ToString() != string.Empty)
                        sbValues.Append(", ");

                    // need to do a case to check the column-value types(quote strings(check for dups first), convert bools)
                    string sType = string.Empty;
                    try
                    {
                        sType = drow[col].GetType().ToString();
                        switch (sType.Trim().ToLower())
                        {
                            case "system.boolean":
                                sbNewValue.Append((Convert.ToBoolean(drow[col]) == true ? "1" : "0"));
                                break;

                            case "system.string":
                                sbNewValue.Append(string.Format("'{0}'", QuoteSQLString(drow[col])));
                                break;

                            case "system.datetime":
                                string sDateTime = QuoteSQLString(drow[col]);
                                if (Validation.IsDateTime(sDateTime) == true)
                                    sDateTime = System.DateTime.Parse(sDateTime).ToString("yyyy-MM-dd HH:mm:ss");
                                else
                                    sDateTime = string.Empty;
                                sbNewValue.Append(string.Format("'{0}'", sDateTime));                                
                                break;

                            case "system.byte[]":
                                sbNewValue.Append(string.Format("'{0}'", Convert.ToBase64String((byte[])drow[col])));
                                break;   

                            default:
                                if (drow[col] == System.DBNull.Value)
                                    sbNewValue.Append("NULL");
                                else
                                    sbNewValue.Append(Convert.ToString(drow[col]));
                                break;
                        }
                    }
                    catch
                    {
                        sbNewValue.Append(string.Format("'{0}'", QuoteSQLString(drow[col])));
                    }

                    sbValues.Append(sbNewValue.ToString());
                }

                // WHERE clause:  loop thru each column, and include the value if the column is in the array
                StringBuilder sbWhereValues = new StringBuilder(string.Empty);       
                foreach (string col in aryWhereColumns)
                {
                    StringBuilder sbNewValue = new StringBuilder();
                    if (type.Equals(DatabaseType.Sqlserver))
                    {
                        sbNewValue.Append("[" + col + "] = ");
                    }
                    else if (type.Equals(DatabaseType.Postgres))
                    {
                        sbNewValue.Append("" + col + " = ");
                    }
                    if (sbWhereValues.ToString() != string.Empty)
                        sbWhereValues.Append(" AND ");    

                    // need to do a case to check the column-value types(quote strings(check for dups first), convert bools)
                    string sType = string.Empty;
                    try
                    {
                        sType = drow[col].GetType().ToString();
                        switch (sType.Trim().ToLower())
                        {
                            case "system.boolean":
                                sbNewValue.Append((Convert.ToBoolean(drow[col]) == true ? "1" : "0"));
                                break;

                            case "system.string":
                                sbNewValue.Append(string.Format("'{0}'", QuoteSQLString(drow[col])));
                                break;

                            case "system.datetime":
                                string sDateTime = QuoteSQLString(drow[col]);
                                if (Validation.IsDateTime(sDateTime) == true)
                                    sDateTime = System.DateTime.Parse(sDateTime).ToString("yyyy-MM-dd HH:mm:ss");
                                else
                                    sDateTime = string.Empty;
                                sbNewValue.Append(string.Format("'{0}'", sDateTime));
                                break;

                            case "system.byte[]":
                                sbNewValue.Append(string.Format("'{0}'", Convert.ToBase64String((byte[])drow[col])));
                                break;  

                            default:
                                if (drow[col] == System.DBNull.Value)
                                    sbNewValue.Append("NULL");
                                else
                                    sbNewValue.Append(Convert.ToString(drow[col]));
                                break;
                        }
                    }
                    catch
                    {
                        sbNewValue.Append(string.Format("'{0}'", QuoteSQLString(drow[col])));
                    }

                    sbWhereValues.Append(sbNewValue.ToString());
                }

                // UPDATE table SET col1 = 3, col2 = 4 WHERE (select cols)
                // write the line out to the stringbuilder
                var template = type == DatabaseType.Sqlserver ? updateSqlServerTemplate : updatePostgresTemplate;
                string snewsql = string.Format(template, sTargetTableName, sbValues.ToString(), sbWhereValues.ToString());
                sbSqlStatements.Append(snewsql);
                sbSqlStatements.AppendLine();
                sbSqlStatements.AppendLine();
            }

            sSqlUpdates = sbSqlStatements.ToString();
            return sSqlUpdates;
        }

        public static string GenerateSqlDeletes(DatabaseType type, ArrayList aryColumns, DataTable dtTable, string sTargetTableName)
        {
            string sSqlDeletes = string.Empty;
            StringBuilder sbSqlStatements = new StringBuilder(string.Empty);

            // loop thru each record of the datatable
            foreach (DataRow drow in dtTable.Rows)
            {
                // loop thru each column, and include the value if the column is in the array
                StringBuilder sbValues = new StringBuilder(string.Empty);                
                foreach (string col in aryColumns)
                {
                    StringBuilder sbNewValue = new StringBuilder();
                    if (type.Equals(DatabaseType.Sqlserver))
                    {
                        sbNewValue.Append("[" + col + "] = ");
                    }
                    else if (type.Equals(DatabaseType.Postgres))
                    {
                        sbNewValue.Append("" + col + " = ");
                    }

                    if (sbValues.ToString() != string.Empty)
                        sbValues.Append(" AND ");    

                    // need to do a case to check the column-value types(quote strings(check for dups first), convert bools)
                    string sType = string.Empty;
                    try
                    {
                        sType = drow[col].GetType().ToString();
                        switch (sType.Trim().ToLower())
                        {
                            case "system.boolean":
                                sbNewValue.Append((Convert.ToBoolean(drow[col]) == true ? "1" : "0"));
                                break;

                            case "system.string":
                                sbNewValue.Append(string.Format("'{0}'", QuoteSQLString(drow[col])));
                                break;

                            case "system.datetime":
                                string sDateTime = QuoteSQLString(drow[col]);
                                if (Validation.IsDateTime(sDateTime) == true)
                                    sDateTime = System.DateTime.Parse(sDateTime).ToString("yyyy-MM-dd HH:mm:ss");
                                else
                                    sDateTime = string.Empty;
                                sbNewValue.Append(string.Format("'{0}'", sDateTime));
                                break;

                            default:
                                if (drow[col] == System.DBNull.Value)
                                    sbNewValue.Append("NULL");
                                else
                                    sbNewValue.Append(Convert.ToString(drow[col]));
                                break;
                        }
                    }
                    catch
                    {
                        sbNewValue.Append(string.Format("'{0}'", QuoteSQLString(drow[col])));
                    }

                    sbValues.Append(sbNewValue.ToString());
                }

                // DELETE FROM table WHERE col1 = 3 AND col2 = '4'
                // write the line out to the stringbuilder
                var template = type == DatabaseType.Sqlserver ? deleteSqlServerTemplate : deletePostgresTemplate;

                string snewsql = string.Format(template, sTargetTableName, sbValues.ToString());
                sbSqlStatements.Append(snewsql);
                sbSqlStatements.AppendLine();
                sbSqlStatements.AppendLine();
            }

            sSqlDeletes = sbSqlStatements.ToString();
            return sSqlDeletes;
        }

        public static string QuoteSQLString(string str)
        {
            return str.Replace("'", "''");
        }

        public static string QuoteSQLString(object ostr)
        {
            return ostr.ToString().Replace("'", "''");
        }
    }
}
