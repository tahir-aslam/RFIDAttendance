using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarcodeAttendanceSystem_WPF_
{
   public class ConnectionString

    {
        //public static String con_string = @"Server=localhost; port=3306; Database=sms; Uid=root; Pwd=7120020@123; SslMode=None;";
        public static String con_string = @"Server=" + MainWindow.Server + "; port=" + MainWindow.Port + "; Database=" + MainWindow.Database + "; Uid=" + MainWindow.Uid + "; Pwd=7120020@123; default command timeout=99999;CHARSET=utf8; SslMode=None;";
    }
}
