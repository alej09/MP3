using System;
using System.Runtime.InteropServices;
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
    public partial class Form1 : Form
    {
        List<Play> listareproduci = new List<Play>();
        List<Datos> listadatos = new List<Datos>();
        List<Lista> listabibliogra = new List<Lista>();
        public Form1()
        {
            InitializeComponent();
        }      
        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (player.URL == "")
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    player.URL = openFileDialog1.FileName;
                }

                player.Ctlcontrols.play();
                tag(openFileDialog1.FileName);
                label1.Text = openFileDialog1.Title;


            }


            else
            {
                player.Ctlcontrols.play();
            }
        }
    
        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                player.URL = openFileDialog1.FileName;
            }
            listadatos.RemoveRange(0, listadatos.Count);
            player.Ctlcontrols.play();
            tag(openFileDialog1.FileName);
            label1.Text = openFileDialog1.Title;
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            eliminarlisre();
            Application.ExitThread();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //this.Hide(); //cerrar formulario actual
            player.Ctlcontrols.stop();
            this.Hide();
            listas frm = new listas();
            frm.Hide();
            frm.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.stop();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            player.Ctlcontrols.pause();
        }
        public void refresh1()
        {
            if (listabibliogra.Count == 0)
            {
                leerListas();
            }

        }
        public void refresh()
        {
            if (listareproduci.Count == 0)
            {
                leerxml();
            }

        }
        public void leerxml()
        {
            XDocument documento = XDocument.Load(@"Lectura.xml");
            var listar = from lis in documento.Descendants("Lista_Favoritos") select lis;
            foreach (XElement u in listar.Elements("Cancion"))
            {
                Play tmp = new Play();
                tmp.Nombredecan = u.Element("Titulo").Value;
                tmp.Direccion = u.Element("Url").Value;
                listareproduci.Add(tmp);

            }
        }
        public void leerListas()
        {
            XDocument documento = XDocument.Load(@"Listas.xml");
            var listar = from lis in documento.Descendants("Lista") select lis;
            foreach (XElement u in listar.Elements("Cancion"))
            {
                Lista tmp = new Lista();
                tmp.Nombre = u.Element("Titulo").Value;
                tmp.Url = u.Element("Url").Value;
                tmp.Num = u.Element("No").Value;
                tmp.Album = u.Element("Album").Value;
                tmp.Duracion = u.Element("Duracion").Value;
                tmp.Calidad = u.Element("Calidad").Value;

                listabibliogra.Add(tmp);
            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            if (listareproduci.Count == 0)
            {
                listabibliogra.RemoveRange(0, listabibliogra.Count);
                refresh();
                int max2 = listabibliogra.Count;
                for (int i = 0; i < listabibliogra.Count; i++)
                {
                    if (label1.Text == listabibliogra[i].Nombre)
                    {
                        if (i == max2 - 1)
                        {
                            player.URL = listabibliogra[0].Url;
                            label1.Text = listabibliogra[0].Nombre;
                            break;
                        }
                        else
                        {
                            player.URL = listabibliogra[i + 1].Url;
                            label1.Text = listabibliogra[i + 1].Nombre;
                            break;
                        }

                    }
                }
            }
            else
            {
                listareproduci.RemoveRange(0, listareproduci.Count);
                //refreshactualizar();
                int max = listareproduci.Count;
                for (int i = 0; i < listareproduci.Count; i++)
                {
                    if (label1.Text == listareproduci[i].Nombredecan)
                    {
                        if (i == max - 1)
                        {
                            player.URL = listareproduci[0].Nombredecan;
                            label1.Text = listareproduci[0].Nombredecan;
                            break;
                        }
                        else
                        {
                            player.URL = listareproduci[i + 1].Nombredecan;
                            label1.Text = listareproduci[i + 1].Nombredecan;
                            break;
                        }

                    }
                }

            }
            player.Ctlcontrols.play();
            listadatos.RemoveRange(0, listadatos.Count);
            tag(player.URL);
        }

        private void macTrackBar1_ValueChanged(object sender, decimal value)
        {
            player.settings.volume = macTrackBar1.Value;
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            macTrackBar1.Value = player.settings.volume;
            refresh();

        }

        private void button9_Click(object sender, EventArgs e)
        {
                listareproduci.RemoveRange(0, listareproduci.Count);
                refresh();
                var myPlayList = player.playlistCollection.newPlaylist("MyPlayList");

                for (int i = 0; i < listareproduci.Count; i++)
                {
                    var mediaItem = player.newMedia(listareproduci[i].Direccion);
                    myPlayList.appendItem(mediaItem);
                }
                player.currentPlaylist = myPlayList;

            }
        private void caratula_Click(object sender, EventArgs e)
        {

        }
        private void abrirListaFavoritosToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }
        public void eliminarlisre()
        {
            XmlDocument documento = new XmlDocument();
            string ruta = @"Lectura.xml";
            //Cargamos el documento XML.
            documento = new XmlDocument();
            documento.Load(ruta);
            //Obtenemos el nodo raiz del documento.
            XmlElement bibliot = documento.DocumentElement;

            //Obtenemos la lista de todos los empleados.
            XmlNodeList listacancion = documento.SelectNodes("Lista_Favoritos/Cancion");

            foreach (XmlNode item in listacancion)
            {
                for (int i = 0; i < listareproduci.Count; i++)
                {
                    //Determinamos el nodo a modificar por medio del id de empleado.
                    if (item.FirstChild.InnerText == listareproduci[i].Nombredecan)
                    {
                        //Nodo sustituido.
                        XmlNode nodoOld = item;
                        bibliot.RemoveChild(nodoOld);
                    }
                }
                documento.Save(ruta);
            }
}
        public void tag(string dato)
        {
            string dat = dato;
            TagLib.File file = TagLib.File.Create(dat);
            System.Drawing.Image currentImage = null;

            // In method onclick of the listbox showing all mp3's

            if (file.Tag.Pictures.Length > 0)
            {
                TagLib.IPicture pic = file.Tag.Pictures[0];
                MemoryStream ms = new MemoryStream(pic.Data.Data);
                if (ms != null && ms.Length > 4096)
                {
                    currentImage = System.Drawing.Image.FromStream(ms);
                    // Load thumbnail into PictureBox
                    pictureBox1.Image = currentImage.GetThumbnailImage(200, 200, null, System.IntPtr.Zero);
                }
                ms.Close();
            }

            Datos datmp = new Datos();
            datmp.Titulo = file.Tag.Title;
            datmp.Num = Convert.ToString(file.Tag.Track);
            datmp.Album = file.Tag.Album;
            datmp.Año = Convert.ToString(file.Tag.Year);
            datmp.Genero = file.Tag.FirstGenre;
            datmp.Duracion = file.Properties.Duration.ToString();
            datmp.Artista = file.Tag.FirstArtist;
            datmp.Comentario = file.Tag.Comment;
            listadatos.Add(datmp);
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = listadatos;
            dataGridView1.Refresh();
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            {
                listBox1.Visible = true;
                //pictureBox1.Visible = false;
                this.listBox1.Items.Clear();
                OpenFileDialog open = new OpenFileDialog();
                open.Filter = "Archivo txt (*.txt)|*.txt|All(*,*)|*,*";
                try
                {
                    open.ShowDialog();
                    StreamReader import = new StreamReader(Convert.ToString(open.FileName));
                    while (import.Peek() >= 0)
                    {
                        listBox1.Items.Add(Convert.ToString(import.ReadLine()));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Convert.ToString(ex.Message));
                    return;
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //listBox1.Visible = false;
            pictureBox1.Visible = true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
            {
                listareproduci.RemoveRange(0, listareproduci.Count);
                refresh();
                int max = listareproduci.Count;
                for (int i = 0; i < listareproduci.Count; i++)
                {
                    if (label1.Text == listareproduci[i].Nombredecan)
                    {
                        if (i == 0)
                        {
                            player.URL = listareproduci[max - 1].Direccion;
                            label1.Text = listareproduci[max - 1].Nombredecan;
                            break;
                        }
                        else
                        {
                            player.URL = listareproduci[i - 1].Direccion;
                            label1.Text = listareproduci[i - 1].Nombredecan;
                            break;
                        }

                    }
                }
            listadatos.RemoveRange(0, listadatos.Count);
                tag(player.URL);
            
        }
        public void sigycargar()
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}


