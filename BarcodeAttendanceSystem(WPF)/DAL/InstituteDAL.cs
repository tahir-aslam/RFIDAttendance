using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcodeAttendanceSystem_WPF_.Models;
using MySql.Data.MySqlClient;

namespace BarcodeAttendanceSystem_WPF_.DAL
{
    public class InstituteDAL
    {
        public InstituteDAL() 
        {
        }

        public Institute GetInstitute()
        {           
            Institute ins;
            using (MySqlConnection con = new MySqlConnection(ConnectionString.con_string))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.CommandText = "SELECT* FROM sms_institute";
                    cmd.Connection = con;                    
                    try
                    {
                        con.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        reader.Read();
                        ins = new Institute()
                        {
                            institute_name = Convert.ToString(reader["institute_name"].ToString()),
                            institute_cell = Convert.ToString(reader["institute_cell"].ToString()),
                            institute_phone = Convert.ToString(reader["institute_phone"].ToString()),
                            institute_logo = (byte[])(reader["institute_logo"]),
                        };
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return ins;
        }
    }
}
