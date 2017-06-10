using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Linq;
using System.Linq;


namespace MP3
{
    public partial class Form1 : Form
    {
        List<Play> listareproduci = new List<Play>();
        List<Datos> listadatosmp3 = new List<Datos>();
        List<Lista> listabiblio = new List<Lista>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }
    }
}
