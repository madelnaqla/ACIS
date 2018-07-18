using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace ACIS
{
    public static class Library
    {
        public  delegate void dgEventRaiser(ref string Text);
        public static event dgEventRaiser SetText;
        public static void WriteErrorLog(Exception ex)
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + ex.Source.ToString().Trim() + "; " + ex.Message.ToString().Trim());
                sw.Flush();
                sw.Close();
                //SetText(ref ex.Message.ToString ());
            }
            catch
            {
            }
        }

        public static void WriteErrorLog(string Message)
        {
            StreamWriter sw = null;
            try
            {
                Console.WriteLine(Message );
                sw = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\LogFile.txt", true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + Message);
                sw.Flush();
                sw.Close();
                SetText(ref Message);
            }
            catch
            {
            }
        }
        // Date time formate 
        static public string dtFormat = "yyyy-mm-dd HH:mm:ss";
        static public string ConnString = "Data Source=10.1.201.129;User Id=sa;Password=Zewaid78744;Initial Catalog=PHPC";
        //static public string oradb = "Data Source=10.1.201.129;User Id=sa;Password=Zewaid78744;Initial Catalog=PHPC";

         // Load a CSV file into an array of rows and columns.
        // Assume there may be blank lines but every line has
        // the same number of fields.
        static public  string[][] LoadCsv(string data, char ch)
        {
            // Get the file's text.
          

            // Split into lines.
            data = data.Replace('\n', '\r');
            string[] lines = data.Split(new char[] { '\r' },
                StringSplitOptions.RemoveEmptyEntries);

            // See how many rows and columns there are.
            int num_rows = lines.Length;
            int num_cols = lines[0].Split(ch).Length;

    
            // Allocate the data array.
            string[][] values = new string[num_rows][];

            // Load the array. 
            for (int r = 0; r < num_rows; r++)
            {
                string[] line_r = lines[r].Split(ch);
                values[r] = new string[num_cols];
                for (int c = 0; c < num_cols; c++)
                {
                    values[r][c] = line_r[c];
                }
            }

            // Return the values.
            return values;
        }
        static public void insert(string UserID, string CHECKTIME, string CHECKTYPE, string VERIFYCODE, string SENSORID, string Memoinfo, string WorkCode, string sn, string UserExtFmt)
        {
            string oradb = "Data Source=.;User Id=sa;Password=Zewaid78744;Initial Catalog=PHPC";

            SqlConnection conn = new SqlConnection(oradb); // C#
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO [PHPC].[dbo].[CHECKINOUT_TEST]([USERID],[CHECKTIME],[CHECKTYPE],[VERIFYCODE],[SENSORID],[Memoinfo],[WorkCode],[sn],[UserExtFmt]) VALUES (" + UserID + "," + CHECKTIME + "," + CHECKTYPE + "," + VERIFYCODE + "," + SENSORID + "," + Memoinfo + "," + WorkCode + "," + sn + "," + UserExtFmt + ")";
            int rowsUpdated = cmd.ExecuteNonQuery();
            if (rowsUpdated == 0)
                Library.WriteErrorLog("Record not inserted");
            else
                Library.WriteErrorLog("Success!");
            conn.Dispose();
        }
        static public  DateTime GetLastFromDB(string SerialNumber)
        {
            try
            {
                //string connectionString = "Data Source=.;Initial Catalog=PHPC;User ID=sa;Password=Mm123456789*";
                string sql = "SELECT max ([CHECKTIME] ) FROM [PHPC].[dbo].[CHECKINOUT_TEST] where sn=@sn";
                SqlCommand cmd;
                SqlConnection cnn = new SqlConnection(ConnString);

                cnn.Open();
                cmd = new SqlCommand(sql, cnn);
                cmd.Parameters.AddWithValue("@sn", SerialNumber);
                DateTime dt = Convert.ToDateTime(cmd.ExecuteScalar());
                cmd.Dispose();
                cnn.Close();
                return dt;
            }
            catch (Exception)
            {
                return new DateTime(0, DateTimeKind.Local);
            }
        }
        static public List<string[]> GetUsers()
        {   
            List<string[]> data = new List<string[]>();
            try
            {
                //string ConnString = "Data Source=.;Initial Catalog=PHPC;User ID=sa;Password=Mm123456789*";
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    conn.Open();
                    
                    using (SqlCommand command = new SqlCommand("SELECT [Badgenumber],[Name],[CardNo] FROM [dbo].[USERINFO]", conn))
                    {
                        // int result = command.ExecuteNonQuery();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {

                            while (reader.Read())
                            {
                                string[] DataTemp = new string[3];
                                DataTemp[0] = reader[0].ToString();
                                DataTemp[1] = reader[1].ToString();
                                DataTemp[2] = reader[2].ToString();
                                data.Add(DataTemp);
                                //Console.WriteLine(String.Format("{0}", reader["id"]));
                            }
                        }
                    }
                    conn.Close();
                }
               
            }
            catch (Exception)
            {
                string[] DataTemp = new string[3];
                DataTemp[0] = "No Data Base Connection ";
                data.Add (DataTemp);
                return data;
            }
            return data;
        }
        static public void insertByParameter(int UserID, DateTime  CHECKTIME, string CHECKTYPE, string VERIFYCODE, int SENSORID, string Memoinfo, int WorkCode, string sn, string UserExtFmt, int NOfRetry)
        {
            
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "INSERT INTO [PHPC].[dbo].[CHECKINOUT_TEST]([USERID],[CHECKTIME],[CHECKTYPE],[VERIFYCODE],[SENSORID],[Memoinfo],[WorkCode],[sn],[UserExtFmt]) VALUES (@UserID,@CHECKTIME,@CHECKTYPE,@VERIFYCODE,@SENSORID,@Memoinfo,@WorkCode,@sn,@UserExtFmt)";
                        cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;
                        cmd.Parameters.Add("@CHECKTIME", SqlDbType.DateTime).Value = CHECKTIME;
                        cmd.Parameters.Add("@CHECKTYPE", SqlDbType.VarChar).Value = CHECKTYPE;
                        cmd.Parameters.Add("@VERIFYCODE", SqlDbType.VarChar).Value = 1;
                        cmd.Parameters.Add("@SENSORID", SqlDbType.VarChar).Value = SENSORID;
                        cmd.Parameters.Add("@Memoinfo", SqlDbType.VarChar).Value = Memoinfo;
                        cmd.Parameters.Add("@WorkCode", SqlDbType.Int).Value = WorkCode;
                        cmd.Parameters.Add("@sn", SqlDbType.VarChar).Value = sn;
                        cmd.Parameters.Add("@UserExtFmt", SqlDbType.VarChar).Value = UserExtFmt;
                        int rowsUpdated = cmd.ExecuteNonQuery();
                        if (rowsUpdated == 0)
                            Library.WriteErrorLog("Record not inserted");
                        else
                            Library.WriteErrorLog("Success!");
                    }
                }
            }
            catch (SqlException ex)
            {
                if (NOfRetry == 5)
                {
                    Library.WriteErrorLog("Max Of Dead Loack Reached");
                    throw new Exception();
                }
                if (ex.Number == 1205)   // DeadLock Error Code 
                {
                    Library.WriteErrorLog("Record not inserted Due to DeadLoack - try to insert");
                    System.Threading.Thread.Sleep(100);
                    insertByParameter(UserID, CHECKTIME, CHECKTYPE, VERIFYCODE, SENSORID, Memoinfo, WorkCode, sn, UserExtFmt, ++NOfRetry );
                }
            }
            catch (Exception ex)
            {
                Library.WriteErrorLog(ex.Message);
            }
        }

        static public void insertFinger(int UserID, int FingerIndex, string Tempalate, int Lenght)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "delete [PHPC].[dbo].[template_test] where badgenumber =@badgenumber;INSERT INTO [PHPC].[dbo].[TEMPLATE_test] ([badgenumber],[lenght] ,[finger_index],[finger_TEMPLATE]) VALUES (@badgenumber,@lenght,@face_index,@face_TEMPLATE)";
                        cmd.Parameters.Add("@badgenumber", SqlDbType.Int).Value = UserID;
                        cmd.Parameters.Add("@lenght", SqlDbType.Int).Value = Lenght;
                        cmd.Parameters.Add("@face_index", SqlDbType.Int).Value = FingerIndex;
                        cmd.Parameters.Add("@face_TEMPLATE", SqlDbType.Image).Value = System.Text.Encoding.ASCII.GetBytes(Tempalate); ;

              
                        int rowsUpdated = cmd.ExecuteNonQuery();
                        if (rowsUpdated == 0)
                            Library.WriteErrorLog("Record not inserted");
                        else
                            Library.WriteErrorLog("Success!");
                    }
                }
            }
            catch (Exception ex)
            {
                Library.WriteErrorLog(ex.Message);
            }
        }

        static public void insertFace(int UserID, int FaceIndex, string Face, int Lenght)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "delete [PHPC].[dbo].[template_face_test] where badgenumber =@badgenumber;INSERT INTO [PHPC].[dbo].[FaceTemp_test]  ([badgenumber],[lenght] ,[face_index],[face_TEMPLATE]) VALUES (@badgenumber,@lenght,@face_index,@face_TEMPLATE)";
                        cmd.Parameters.Add("@badgenumber", SqlDbType.Int).Value = UserID;
                        cmd.Parameters.Add("@lenght", SqlDbType.Int).Value = Lenght;
                        cmd.Parameters.Add("@face_index", SqlDbType.Int).Value = FaceIndex;
                        cmd.Parameters.Add("@face_TEMPLATE", SqlDbType.Image).Value = System.Text.Encoding.ASCII.GetBytes(Face); ;


                        int rowsUpdated = cmd.ExecuteNonQuery();
                        if (rowsUpdated == 0)
                            Library.WriteErrorLog("Record not inserted");
                        else
                            Library.WriteErrorLog("Success!");
                    }
                }
            }
            catch (Exception ex)
            {
                Library.WriteErrorLog(ex.Message);
            }
        }

        static public void UpdateCard(int UserID,string CardNo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE [PHPC].[dbo].[USERINFO]  SET  [CardNo] = @CardNo WHERE [BADGENUMBER] =@BADGENUMBER";
                        cmd.Parameters.Add("@badgenumber", SqlDbType.Int).Value = UserID;
                        cmd.Parameters.Add("@CardNo", SqlDbType.Int).Value = CardNo;
                        
                        

                        int rowsUpdated = cmd.ExecuteNonQuery();
                        if (rowsUpdated == 0)
                            Library.WriteErrorLog("Card not updated");
                        else
                            Library.WriteErrorLog("Success!");
                    }
                }
            }
            catch (Exception ex)
            {
                Library.WriteErrorLog(ex.Message);
            }
        }
        static public void EnableUser(int UserID, int Status)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(ConnString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "UPDATE [PHPC].[dbo].[USERINFO]  SET  [Privilege] = @CardNo WHERE [BADGENUMBER] =@BADGENUMBER";
                        cmd.Parameters.Add("@badgenumber", SqlDbType.Int).Value = UserID;
                        cmd.Parameters.Add("@Status", SqlDbType.Int).Value = Status;


                        int rowsUpdated = cmd.ExecuteNonQuery();
                        if (rowsUpdated == 0)
                            Library.WriteErrorLog("Card not updated");
                        else
                            Library.WriteErrorLog("Success!");
                    }
                }
            }
            catch (Exception ex)
            {
                Library.WriteErrorLog(ex.Message);
            }
        }
        public static T[] RemoveAt<T>(this T[] array, int startIndex, int length)
        {
            if (array == null)
                throw new ArgumentNullException("array");

            if (length < 0)
            {
                startIndex += 1 + length;
                length = -length;
            }

            if (startIndex < 0)
                throw new ArgumentOutOfRangeException("startIndex");
            if (startIndex + length > array.Length)
                throw new ArgumentOutOfRangeException("length");

            T[] newArray = new T[array.Length - length];

            Array.Copy(array, 0, newArray, 0, startIndex);
            Array.Copy(array, startIndex + length, newArray, startIndex, array.Length - startIndex - length);

            return newArray;
        }
        static public DateTime   ConvertToDate(string time)
        {
            long t = long.Parse (time);
            int second = int.Parse((t % 60).ToString ());
            t /= 60;
            int minute = int.Parse((t % 60).ToString());
            t /= 60;
            int hour = int.Parse((t % 24).ToString());
            t /= 24;
            int day = int.Parse((t % 31 + 1).ToString());
            t /= 31;
            int month = int.Parse((t % 12 + 1).ToString());
            t /= 12;
            int year = int.Parse((t + 2000).ToString());

            DateTime dt = new DateTime(year,month ,day , hour,minute,second );
            return dt ;
        }
        static public long ConvertToLong(DateTime  dt)
        {

            long temp  = ((dt.Year - 2000) * 12 * 31 + (dt.Month - 1) * 31 + (dt.Day - 1)) * (24 * 60 * 60) + dt.Hour * 60 * 60 + dt.Minute * 60 + dt.Second;

            return temp;
        }
    }
}
