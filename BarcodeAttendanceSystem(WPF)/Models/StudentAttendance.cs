using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeAttendanceSystem_WPF_.Models
{
    public class StudentAttendance
    {
        public string id { set; get; }
        public string std_id { set; get; }
        public string std_name { set; get; }
        public string father_name { set; get; }
        public string cell_no { set; get; }
        public string roll_no { set; get; }
        public string adm_no { set; get; }
        public string class_id { set; get; }
        public string class_name { set; get; }
        public string section_name { set; get; }
        public string section_id { set; get; }
        public string att_percentage { set; get; }
        public char attendence { set; get; }                
        public DateTime date_time { set; get; }
        public DateTime attendence_date { set; get; }
        public string created_by { set; get; }
        public string total_days { set; get; }
        public string total_abs { set; get; }
        public string total_presents { set; get; }
    }
}
