using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BarcodeAttendanceSystem_WPF_.Models;
using MySql.Data.MySqlClient;

namespace BarcodeAttendanceSystem_WPF_.DAL
{
    public class StudentDAL
    {
        
        public StudentDAL() 
        {
        }

        public Student getStudentInfo(int id)
        {
            Student std = new Student();

            using (MySqlConnection con = new MySqlConnection(ConnectionString.con_string))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    cmd.CommandText = "SELECT* FROM sms_admission where is_active='Y' && id = @id && session_id=(select session_id from sms_admission where id=@id order by session_id DESC Limit 1)";
                    cmd.Parameters.Add("@id",MySqlDbType.Int32).Value = id;
                    cmd.Parameters.Add("@session_id", MySqlDbType.VarChar).Value = MainWindow.m_SessionID;
                    cmd.Connection = con;                   
                   
                    try
                    {
                        con.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            std = new Student()
                            {
                                id = Convert.ToString(reader["id"].ToString()),
                                std_name = Convert.ToString(reader["std_name"].ToString()),
                                father_name = Convert.ToString(reader["father_name"].ToString()),
                                class_id = Convert.ToString(reader["class_id"].ToString()),
                                class_name = Convert.ToString(reader["class_name"].ToString()),
                                section_id = Convert.ToString(reader["section_id"].ToString()),
                                section_name = Convert.ToString(reader["section_name"].ToString()),
                                cell_no = Convert.ToString(reader["cell_no"].ToString()),
                                roll_no = Convert.ToString(reader["roll_no"].ToString()),
                                adm_no = Convert.ToString(reader["adm_no"].ToString()),
                                std_image = (byte[])(reader["image"]),
                            };
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }
            return std;
        }
        public int insertStudentAttendance(StudentAttendance sa)
        {
            int i =0 ;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConnectionString.con_string))
                    {
                        con.Open();

                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.CommandText = "Delete from sms_student_attendence where std_id=@std_id && attendence_date= @dt && session_id=@session_id";
                            cmd.Connection = con;
                            cmd.Parameters.Add("@dt", MySqlDbType.Date).Value = sa.attendence_date;
                            cmd.Parameters.Add("@std_id", MySqlDbType.Int32).Value = sa.std_id;
                            cmd.Parameters.Add("@session_id", MySqlDbType.Int32).Value = MainWindow.m_SessionID;                            
                            
                            cmd.ExecuteNonQuery();                     
                        }
                        

                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.CommandText = "INSERT INTO sms_student_attendence(total_presents,total_abs,total_days,att_percentage,std_id,section_id,attendence_date,class_id,roll_no,attendence,std_name,created_by,date_time,session_id)Values(@total_presents,@total_abs,@total_days,@att_percentage,@std_id,@section_id,@attendence_date,@class_id,@roll_no,@attendence,@std_name,@created_by,@date_time,@session_id)";
                            cmd.Connection = con;
                            //cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.Add("@session_id", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = MainWindow.m_SessionID;
                            cmd.Parameters.Add("@class_id", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = sa.class_id;
                            cmd.Parameters.Add("@section_id", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = sa.section_id;
                            cmd.Parameters.Add("@std_id", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = sa.std_id;
                            cmd.Parameters.Add("@std_name", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = sa.std_name;
                            cmd.Parameters.Add("@roll_no", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = sa.roll_no;
                            cmd.Parameters.Add("@attendence_date", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = sa.attendence_date;
                            cmd.Parameters.Add("@attendence", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = sa.attendence;
                            cmd.Parameters.Add("@att_percentage", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = "0";
                            cmd.Parameters.Add("@total_days", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = 0;
                            cmd.Parameters.Add("@total_abs", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = 0;
                            cmd.Parameters.Add("@total_presents", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = 0;

                            cmd.Parameters.Add("@created_by", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = sa.created_by;
                            cmd.Parameters.Add("@date_time", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = sa.date_time;
                            
                            i = Convert.ToInt32(cmd.ExecuteNonQuery());
                            con.Close();
                        }
                    }
            }catch(Exception ex)
            {
                throw ex;
            }
            return i;
        }
        public List<StudentAttendance> getAttendanceByDate(DateTime dt) 
        {
            List<StudentAttendance> list = new List<StudentAttendance>();
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConnectionString.con_string))
                {
                    using (MySqlCommand cmd = new MySqlCommand())
                    {
                        cmd.CommandText = "SELECT std_id FROM sms_student_attendence  where attendence_date=@attendence_date";
                        cmd.Connection = con;
                        cmd.Parameters.Add("@attendence_date", MySqlDbType.Date).Value = dt;
                        cmd.Parameters.Add("@session_id", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = MainWindow.m_SessionID;
                        //cmd.CommandType = System.Data.CommandType.StoredProcedure;                    
                        con.Open();
                        MySqlDataReader reader = cmd.ExecuteReader();
                        while (reader.Read())
                        {
                            StudentAttendance att = new StudentAttendance()
                            {                                
                                std_id = Convert.ToString(reader["std_id"].ToString()),                             
                            };
                            list.Add(att);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        public int insertAllStudentAttendance(List<StudentAttendance> list)
        {
            int i = 0;
            try
            {
                using (MySqlConnection con = new MySqlConnection(ConnectionString.con_string))
                {
                    con.Open();

                    foreach (var sa in list)
                    {
                        using (MySqlCommand cmd = new MySqlCommand())
                        {
                            cmd.CommandText = "INSERT INTO sms_student_attendence(total_presents,total_abs,total_days,att_percentage,std_id,section_id,attendence_date,class_id,roll_no,attendence,std_name,created_by,date_time,session_id)Values(@total_presents,@total_abs,@total_days,@att_percentage,@std_id,@section_id,@attendence_date,@class_id,@roll_no,@attendence,@std_name,@created_by,@date_time,@session_id)";
                            cmd.Connection = con;
                            //cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            cmd.Parameters.Add("@session_id", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = MainWindow.m_SessionID;
                            cmd.Parameters.Add("@class_id", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = sa.class_id;
                            cmd.Parameters.Add("@section_id", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = sa.section_id;
                            cmd.Parameters.Add("@std_id", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = sa.std_id;
                            cmd.Parameters.Add("@std_name", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = sa.std_name;
                            cmd.Parameters.Add("@roll_no", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = "0";
                            cmd.Parameters.Add("@attendence_date", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = sa.attendence_date;
                            cmd.Parameters.Add("@attendence", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = sa.attendence;
                            cmd.Parameters.Add("@att_percentage", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = "0";
                            cmd.Parameters.Add("@total_days", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = 0;
                            cmd.Parameters.Add("@total_abs", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = 0;
                            cmd.Parameters.Add("@total_presents", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = 0;

                            cmd.Parameters.Add("@created_by", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = sa.created_by;
                            cmd.Parameters.Add("@date_time", MySql.Data.MySqlClient.MySqlDbType.DateTime).Value = sa.date_time;

                            i = Convert.ToInt32(cmd.ExecuteNonQuery());                     
                        }
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
        public List<Student> get_all_admissions()
        {
            List<Student> adm_list = new List<Student>();

            using (MySqlConnection con = new MySqlConnection(ConnectionString.con_string))
            using (MySqlCommand cmd = new MySqlCommand())
            {
                cmd.CommandText = "SELECT id, std_name, class_id, section_id FROM sms_admission where is_active='Y' && session_id=@session_id";
                cmd.Connection = con;
                cmd.Parameters.Add("@session_id", MySql.Data.MySqlClient.MySqlDbType.VarChar).Value = MainWindow.m_SessionID;
                //cmd.CommandType = System.Data.CommandType.StoredProcedure;                    
                try
                {
                    con.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Student adm = new Student()
                        {
                            id = Convert.ToString(reader["id"].ToString()),
                            std_name = Convert.ToString(reader["std_name"].ToString()),                            
                            class_id = Convert.ToString(reader["class_id"].ToString()),                            
                            section_id = Convert.ToString(reader["section_id"].ToString()),                            
                        };
                        adm_list.Add(adm);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return adm_list;
        }
    }
}