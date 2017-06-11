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
namespace MP3
{
    public partial class listas : Form
    {
        Form1 frm = new Form1();
        static XmlDocument documento = new XmlDocument();
        static string ruta = @"Listas.xml";        
        List<Play> listareproduci = new List<Play>();
        List<Datos> listadatosmp3 = new List<Datos>();
        List<Lista> listabiblioteca = new List<Lista>();
        public listas()
        {
            InitializeComponent();
        }

        private void listas_Load(object sender, EventArgs e)
        {

        }
    }
}
