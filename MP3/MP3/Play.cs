using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP3
{
    class Play
    {
        string nombredecan;
        string direccion;

        public string Nombredecan
        {
            get
            {
                return nombredecan;
            }

            set
            {
                nombredecan = value;
            }
        }

        public string Direccion
        {
            get
            {
                return direccion;
            }

            set
            {
                direccion = value;
            }
        }
    }
}
