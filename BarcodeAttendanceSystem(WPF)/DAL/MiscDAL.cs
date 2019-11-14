using BarcodeAttendanceSystem_WPF_.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeAttendanceSystem_WPF_.DAL
{
    public class MiscDAL
    {
        public int insertIntoQueue(SMSQueue obj)
        {
            int i = 0;

            try
            {
                using (MySqlConnection con = new MySqlConnection(ConnectionString.con_string))
                {
                    con.Open();


                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "INSERT INTO sms_sms_queue(receiver_id,receiver_type_id, receiver_cell_no, receiver_name, sms_message, sms_type, sms_type_id, sort_order, created_by, date_time, emp_id) Values(@receiver_id, @receiver_type_id, @receiver_cell_no, @receiver_name, @sms_message, @sms_type, @sms_type_id, @sort_order, @created_by, @date_time, @emp_id)";
                        cmd.Connection = con;

                        cmd.Parameters.Add("@receiver_id", MySqlDbType.Int32).Value = obj.receiver_id;
                        cmd.Parameters.Add("@receiver_type_id", MySqlDbType.Int32).Value = obj.receiver_type_id;
                        cmd.Parameters.Add("@receiver_cell_no", MySqlDbType.VarChar).Value = obj.receiver_cell_no;
                        cmd.Parameters.Add("@receiver_name", MySqlDbType.VarChar).Value = obj.receiver_name;
                        cmd.Parameters.Add("@sms_message", MySqlDbType.VarChar).Value = obj.sms_message;
                        cmd.Parameters.Add("@sms_type", MySqlDbType.VarChar).Value = obj.sms_type;
                        cmd.Parameters.Add("@sms_type_id", MySqlDbType.Int32).Value = obj.sms_type_id;
                        cmd.Parameters.Add("@sort_order", MySqlDbType.Int32).Value = obj.sort_order;
                        cmd.Parameters.Add("@created_by", MySqlDbType.VarChar).Value = obj.created_by;
                        cmd.Parameters.Add("@date_time", MySqlDbType.DateTime).Value = obj.date_time;
                        cmd.Parameters.Add("@emp_id", MySqlDbType.Int32).Value = obj.emp_id;

                        i = Convert.ToInt32(cmd.ExecuteNonQuery());
                        cmd.Parameters.Clear();
                    }
                    con.Close();

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
