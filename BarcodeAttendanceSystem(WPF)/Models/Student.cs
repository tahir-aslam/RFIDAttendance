using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeAttendanceSystem_WPF_.Models
{
    public class Student
    {
        public string id { set; get; }
        public string std_name { set; get; }
        public string father_name { set; get; }        
        public DateTime dob { set; get; }        
        public string parmanent_adress { set; get; }        
        public string cell_no { set; get; }        
        public string class_name { set; get; }
        public string section_name { set; get; }
        public string roll_no { set; get; }
        public string adm_no { set; get; }        
        public string gender { set; get; }        
        public string class_id { set; get; }
        public string section_id { set; get; }
        public byte[] image { set; get; }
        public byte[] std_image { set; get; }
        public int count {get;set;}
    }
}
