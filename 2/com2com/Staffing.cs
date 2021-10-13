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
        private const char ESCChange = (char)43;
        private const char flagChange = '@';

        public string byteStaffing(string message)
        {
            string staffMessage = "";
            foreach (char symbol in message.Substring(1))
            {
                if (symbol == flag)
                {
                    staffMessage = staffMessage+flagChange;
                    staffMessage = staffMessage+ESC;
                }
                else staffMessage = staffMessage+symbol;
                if (symbol == ESC) staffMessage += ESCChange;
            }
            return staffMessage;
        }

        public string unByteStaffing(string staffMessage)
        {
            string message = "";
            bool esc = false;
            foreach (char symbol in staffMessage.Substring(1))
            {
                if (symbol == flag) break;
                if (esc)
                {
                    switch (symbol)
                    {
                        case flagChange:
                            message += flag;
                            break;
                        case ESCChange:
                            message += ESC;
                            break;
                        default:
                            throw new Exception("Invalid staffing");
                    }
                    esc = false;
                }
                else if (symbol == ESC)
                    esc = true;
                else message += symbol;
            }
            return message;
        }
    }
}



