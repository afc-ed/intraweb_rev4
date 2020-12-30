using System;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace intraweb_rev3.Models
{
    public class Utilities
    {
        private static Random _random = new Random();
        public static int decimalDigit = 2;

        public class MessagePart
        {
            public string File1 { get; set; } = "";
            public string File2 { get; set; } = "";
            public string From { get; set; } = "";
            public string To { get; set; } = "";
            public string Cc { get; set; } = "";
            public string Bc { get; set; } = "";
            public string Subject { get; set; } = "";
            public string Body { get; set; } = "";
        }

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
                // if directory not found then create it, otherwise an exception occurs.
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
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
                if (File.Exists(filePath))
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
        // returns false if an exception is thrown when calling MailAddress if the email format is invalid.
        public static bool isValidEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    return false;
                MailAddress mailAddress = new MailAddress(email);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void SendMessage(MessagePart message)
        {
            try
            {
                MailMessage message1 = new MailMessage();
                SmtpClient smtpClient = new SmtpClient();
                smtpClient.Host = "smtp.gmail.com";
                smtpClient.Port = 587;
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential("afcgosushi@gmail.com", "vfayxsbgejycjazl");
                message1.From = new MailAddress("afcgosushi@gmail.com");
                message1.To.Add(message.To);
                if (!string.IsNullOrEmpty(message.Cc))
                    message1.CC.Add(message.Cc);
                if (!string.IsNullOrEmpty(message.Bc))
                    message1.Bcc.Add(message.Bc);
                message1.Subject = message.Subject;
                message1.Body = message.Body;
                message1.IsBodyHtml = true;
                if (!string.IsNullOrEmpty(message.File1))
                    message1.Attachments.Add(new Attachment(message.File1));
                if (!string.IsNullOrEmpty(message.File2))
                    message1.Attachments.Add(new Attachment(message.File2));
                smtpClient.Send(message1);
            }
            catch (Exception ex)
            {
                throw ErrHandler(ex, "Utilities.SendMessage");
            }
        }


    }
}