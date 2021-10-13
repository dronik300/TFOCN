using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lr_1
{
    public class Staffing
    {
        public const char flag = 'g';
        private const char ESC = '+';
        private const char ESCChange = '$';

        public static string byteStaffing(string message)
        {
            message = message.Replace("+", "?");
            message = message.Replace("g", "@+");
            return message;
        }

        public static string unByteStaffing(string message)
        {
            message = message.Replace("@+", "g");
            message = message.Replace("?", "+");
            return message;
        }
    }
}



