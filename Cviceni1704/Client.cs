using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cviceni1704
{
    internal class Client
    {
        private string jmeno;
        private string heslo;

        public string Jmeno
        {
            get { return jmeno; }
            set { jmeno = value; }
        }

        public string Heslo
        {
            get { return heslo; }
            set { heslo = value; }
        }

        public Client(string j, string h)
        {
            Jmeno = j;
            Heslo = h;
        }

        public string HashingSimulator()
        {
            string hash = "";
            for (int i = 0; i < Heslo.Length; i++)
            {
                hash += "*";
            }
            return hash;
        }

        public override string ToString()
        {
            return Jmeno + ", " + Heslo;
        }


    }
}
