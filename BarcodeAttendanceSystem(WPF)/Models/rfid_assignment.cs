using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeAttendanceSystem_WPF_.Models
{
    public class rfid_assignment
    {
        public Int32 id { get; set; }
        public Int32 session_id { get; set; }
        public Int32 card_holder_id { get; set; }
        public string card_no { get; set; }
        public string is_std { get; set; }
        public string created_by { get; set; }
        public DateTime date_time { get; set; }
        public Int32 emp_id { get; set; }



    }
}
