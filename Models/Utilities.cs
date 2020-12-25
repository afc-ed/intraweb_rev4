using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text.RegularExpressions;

namespace intraweb_rev3.Models
{
    public class Utilities
    {
        private static Random _random = new Random();
        public static int decimalDigit = 2;

        private static OleDbConnection ExcelConnection(string fileName)
        {
            try
            {
                return new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=YES;IMEX=1\"");
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Utilities.ExcelConnection()");
            }
        }

        public static DataTable GetExcelData(string fileName, string sheetName)
        {
            try
            {
                DataTable dataTable = new DataTable();
                using (OleDbConnection selectConnection = Utilities.ExcelConnection(fileName))
                {
                    selectConnection.Open();
                    new OleDbDataAdapter("select * from [" + sheetName + "]", selectConnection).Fill(dataTable);
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Utilities.GetExcelData()");
            }
        }

        public static void DeleteOldFiles(string path)
        {
            try
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    DateTime lastWriteTime = File.GetLastWriteTime(file);
                    DateTime dateTime = DateTime.Now;
                    dateTime = dateTime.AddDays(-1.0);
                    DateTime date = dateTime.Date;
                    if (lastWriteTime < date)
                        Utilities.DeleteFile(file);
                }
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Utilities.DeleteOldFiles()");
            }
        }

        public static void DeleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Utilities.DeleteFile()");
            }
        }

        public static int GetRandom()
        {
            try
            {
                return Utilities._random.Next();
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Utilities.GetRandom()");
            }
        }

        public static string cleanInput(string input)
        {
            try
            {
                return Regex.Replace(input, "[^0-9a-zA-Z\\s]+", "");
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Utilities.cleanInput()");
            }
        }

        public static bool isNull(object oValue)
        {
            try
            {
                return oValue == null || oValue == DBNull.Value;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Utilities.isNull()");
            }
        }

        public static Exception ErrHandler(Exception err, string sModule = "") => new Exception("Exception: " + sModule + " : " + err.Message.ToString());

        public static void WriteToLog(string sFile, string sMsg)
        {
            StreamWriter streamWriter = new StreamWriter(sFile, true);
            try
            {
                streamWriter.WriteLine(sMsg);
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error writing to {0}. Message = {1}", (object)sFile, (object)ex.Message);
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Close();
                    streamWriter.Dispose();
                }
            }
        }

        public static string FormatPhone(string value)
        {
            try
            {
                value = new Regex("\\D").Replace(value, string.Empty);
                value = value.TrimStart('1');
                if (value.Length == 7)
                    return Convert.ToInt64(value).ToString("###-####");
                if (value.Length == 10)
                    return Convert.ToInt64(value).ToString("###-###-####");
                return value.Length > 10 ? Convert.ToInt64(value).ToString("###-###-#### " + new string('#', value.Length - 10)) : value;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Utilities.FormatPhone()");
            }
        }

        public static string RemoveWhiteSpace(string sInput)
        {
            try
            {
                return Regex.Replace(sInput, "\\s+", "");
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Utilities.RemoveWhiteSpace()");
            }
        }

        public static string CleanForCSV(string sInput)
        {
            try
            {
                return Regex.Replace(sInput, "\\r|\\t|\\n|\\,", " ");
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Utilities.CleanForCSV()");
            }
        }
    }
}