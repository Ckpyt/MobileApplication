using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobileApplication
{
    class Customer
    {
        public int ID;
        //rights used bite system.
        int Rights;
        public string Name;
        public string phone;

        /// <summary>
        /// Convert right from bite system to string
        /// </summary>
        public string GetStringRights()
        {
            string result = "";
            result += (Rights & 1) != 0 ? "I" : "";
            result += (Rights & 2) != 0 ? "D" : "";
            result += (Rights & 4) != 0 ? "P" : "";
            result += (Rights & 8) != 0 ? "C" : "";
            result += (Rights & 16) != 0 ? "L" : "";
            return result;
        }
        /// <summary>
        /// Convert right from string to byte system
        /// </summary>
        public void SetStringRights(string rights)
        {
            foreach (char c in rights)
            {
                switch (c)
                {
                    case 'I':
                        Rights += 1;
                        break;
                    case 'D':
                        Rights += 2;
                        break;
                    case 'P':
                        Rights += 4;
                        break;
                    case 'C':
                        Rights += 8;
                        break;
                    case 'L':
                        Rights += 16;
                        break;
                    case ' ': break;
                    default:
                        throw new Exception("unexpected char " + c + " founded");
                }
            }
        }
    }
}
