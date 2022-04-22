using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;


namespace SqlStatementGenerator
{    

    class FileUtilities
    {       

        /// <summary>
        /// Returns a guaranteed unique filename in the temp directory
        /// </summary>
        /// <param name="sExtension"></param>
        /// <returns></returns>
        public static string GetUniqueTempFileName(string sExtension)
        {
            string sUniqueName = System.Guid.NewGuid().ToString() + sExtension;

            return Path.Combine(Path.GetTempPath(), sUniqueName);
        }

        public static string GetUniqueTempFileName(string sPath, string sExtension)
        {
            string sUniqueName = System.Guid.NewGuid().ToString() + sExtension;

            return Path.Combine(sPath, sUniqueName);
        }

        /// <summary>
        /// Returns a guaranteed unique path in the temp directory
        /// </summary>
        /// <param name="sExtension"></param>
        /// <returns></returns>
        public static string GetUniqueTempPath()
        {
            string sUniqueName = System.Guid.NewGuid().ToString();

            return Path.Combine(Path.GetTempPath(), sUniqueName);
        }

        public static string GetUniqueTempPath(string sPath)
        {
            string sUniqueName = System.Guid.NewGuid().ToString();

            return Path.Combine(sPath, sUniqueName);
        }

        /// <summary>
        /// Reads and returns the contents of a file into a string
        /// </summary>
        /// <param name="sFilePath"></param>
        /// <returns></returns>
        public static string ReadFileContents(string sFilePath)
        {
            FileStream file = null;
            StreamReader sr = null;
            string sContents = string.Empty;

            try
            {
                // make sure we're allowed to overwrite the file if it exists
                if (File.Exists(sFilePath) == false)
                {
                    throw new Exception("Cannot read the file '" + sFilePath + "' because it does not exist!");
                }

                // Specify file, instructions, and priveledges
                file = new FileStream(sFilePath, FileMode.OpenOrCreate, FileAccess.Read);

                // Create a new stream to read from a file
                sr = new StreamReader(file);

                // Read contents of file into a string
                sContents = sr.ReadToEnd();
            }
            catch (Exception ex)
            {
                throw new Exception("ReadFileContents() failed with error: " + ex.Message);
            }
            finally
            {
                // Close StreamReader
                if (sr != null)
                    sr.Close();

                // Close file
                if (file != null)
                    file.Close();
            }

            return sContents;
        }

        /// <summary>
        /// Writes a string/contents to a given filepath
        /// </summary>
        /// <param name="sFilePath"></param>
        /// <param name="sContents"></param>
        /// <param name="bOverwrite"></param>
        public static void WriteFileContents(string sFilePath, string sContents, bool bOverwrite)
        {
            FileStream file = null;
            StreamWriter sw = null;

            try
            {
                // make sure we're allowed to overwrite the file if it exists
                if (bOverwrite == false)
                {
                    if (File.Exists(sFilePath) == true)
                    {
                        throw new Exception("Cannot write the file '" + sFilePath + "' because it already exists!");
                    }
                }

                // Specify file, instructions, and privelegdes
                file = new FileStream(sFilePath, FileMode.Create, FileAccess.Write);

                // Create a new stream to write to the file
                sw = new StreamWriter(file);

                // Write a string to the file
                sw.Write(sContents);
            }
            catch (Exception ex)
            {
                throw new Exception("WriteFileContents() failed with error: " + ex.Message);
            }
            finally
            {
                // Close StreamWriter
                if (sw != null)
                    sw.Close();

                // Close file
                if (file != null)
                    file.Close();
            }
        }


    }
}
