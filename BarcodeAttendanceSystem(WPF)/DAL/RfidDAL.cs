using BarcodeAttendanceSystem_WPF_.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeAttendanceSystem_WPF_.DAL
{
    public class RfidDAL
    {
        public rfid_assignment GetIDFromRfidCArdNo(string card_no)
        {
            int i = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConnectionString.con_string))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "select * from sms_rfid_assignment where card_no="+card_no;
                        cmd.Connection = con;
                        con.Open();

                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read()) 
                        {
                            rfid_assignment obj = new rfid_assignment()
                            {
                                card_holder_id = Convert.ToInt32(reader["card_holder_id"]),
                                is_std = Convert.ToString(reader["is_std"]),
                            };
                            return obj;
                        }                  
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }

         public int InserRfidCard(rfid_assignment obj)
        {
            int i = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConnectionString.con_string))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "INSERT INTO sms_rfid_assignment(session_id,card_holder_id,card_no,is_std,created_by,date_time,emp_id) Values(@session_id,@card_holder_id,@card_no,@is_std,@created_by,@date_time,@emp_id)";
                        cmd.Connection = con;
                        //cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.Add("@session_id", MySqlDbType.Int32).Value = obj.session_id;
                        cmd.Parameters.Add("@card_holder_id", MySqlDbType.Int32).Value = obj.card_holder_id;
                        cmd.Parameters.Add("@card_no", MySqlDbType.VarChar).Value = obj.card_no;
                        cmd.Parameters.Add("@is_std", MySqlDbType.VarChar).Value = obj.is_std;
                        cmd.Parameters.Add("@created_by", MySqlDbType.VarChar).Value = obj.created_by;
                        cmd.Parameters.Add("@date_time", MySqlDbType.DateTime).Value = obj.date_time;
                        cmd.Parameters.Add("@emp_id", MySqlDbType.Int32).Value = obj.emp_id;

                        con.Open();
                        i = Convert.ToInt32(cmd.ExecuteNonQuery());
                        con.Close();
                    }
                }
            }

            catch (Exception ex)
            {
                throw ex;
            }
            return i;
        }
    }
    
}
