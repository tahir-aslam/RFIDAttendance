using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeAttendanceSystem_WPF_.Models
{
    public class Institute
    {
        public string institute_name { set; get; }
        public byte[] institute_logo { set; get; }
        public string institute_cell { set; get; }
        public string institute_phone { set; get; }      
    }
}
