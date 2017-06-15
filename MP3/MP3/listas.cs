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
        List<Lista> listabiblio = new List<Lista>();
        public listas()
        {
            InitializeComponent();
        }

        private void listas_Load(object sender, EventArgs e)
        {
            dataGridView2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            Play reprotemp = new Play();
            reprotemp.Direccion = openFileDialog1.FileName;
            reprotemp.Nombredecan = openFileDialog1.SafeFileName.ToString();
            listareproduci.Add(reprotemp);
            cargar();
            xml();
        }
        public void cargar()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            dataGridView1.DataSource = listareproduci;
            dataGridView1.Columns["direccion"].Visible = false;
            dataGridView1.Refresh();
        }
        public void xml()
        {
            XmlDocument doc = new XmlDocument();
            XmlElement raiz = doc.CreateElement("Lista_Favoritos");
            doc.AppendChild(raiz);

            XmlElement cancion = doc.CreateElement("Cancion");


            XmlElement titulo = doc.CreateElement("Titulo");


            XmlElement url = doc.CreateElement("Url");

            for (int i = 0; i < listareproduci.Count(); i++)
            {
                //nuevo documento
                cancion = doc.CreateElement("Cancion");
                raiz.AppendChild(cancion);

                titulo = doc.CreateElement("Titulo");
                titulo.AppendChild(doc.CreateTextNode(listareproduci[i].Nombredecan));
                cancion.AppendChild(titulo);

                url = doc.CreateElement("Url");
                url.AppendChild(doc.CreateTextNode(listareproduci[i].Direccion));
                cancion.AppendChild(url);

                doc.Save(@"Lectura.xml");
            }


        }
        public void EscribirXml()
        {
            for (int i = 0; i < listabiblio.Count(); i++)
            {
                if (label1.Text == listabiblio[i].Nombre)
                {
                    //Creamos el escritor.
                    using (XmlTextWriter Writer = new XmlTextWriter(@"biblio.xml", Encoding.UTF8))
                    {
                        //Declaración inicial del Xml.
                        Writer.WriteStartDocument();

                        //Configuración.
                        Writer.Formatting = Formatting.Indented;
                        Writer.Indentation = 5;

                        //Escribimos el nodo principal.
                        Writer.WriteStartElement("Blibioteca");

                        //Escribimos un nodo empleado.
                        Writer.WriteStartElement("Cancion");

                        //Escribimos cada uno de los elementos del nodo empleado.
                        Writer.WriteElementString("nombre", listabiblio[i].Nombre);
                        Writer.WriteElementString("url", listabiblio[i].Url);
                        Writer.WriteElementString("num", listabiblio[i].Num);
                        Writer.WriteElementString("album", listabiblio[i].Album);
                        //Escribimos el subnodo teléfono.
                        Writer.WriteElementString("duracion", listabiblio[i].Duracion);
                        Writer.WriteElementString("calidad", listabiblio[i].Calidad);

                        //Cerramos el nodo y el documento.
                        Writer.WriteEndElement();
                        Writer.WriteEndDocument();
                        Writer.Flush();
                    }
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string nom = dataGridView1.CurrentRow.Cells["nombredecan"].Value.ToString();
            label1.Text = nom;
        }
        public void limn(int c)
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

                //Salvamos el documento.
                documento.Save(ruta);
            }

        }
        public void refre()
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
        private XmlNode CrearNodoXml(string nom1, string url1, string num1, string album1, string dura1, string cali1)
        {
            //Creamos el nodo que deseamos insertar.
            XmlElement Cancion = documento.CreateElement("Cancion");

            //Creamos el elemento idEmpleado.
            XmlElement nombre = documento.CreateElement("Titulo");
            nombre.InnerText = nom1;
            Cancion.AppendChild(nombre);

            //Creamos el elemento nombre.
            XmlElement Url = documento.CreateElement("Url");
            Url.InnerText = url1;
            Cancion.AppendChild(Url);

            //Creamos el elemento apellidos.
            XmlElement num = documento.CreateElement("No");
            num.InnerText = num1;
            Cancion.AppendChild(num);

            //Creamos el elemento numeroSS.
            XmlElement album = documento.CreateElement("Album");
            album.InnerText = album1;
            Cancion.AppendChild(album);

            //Creamos el elemento fijo.
            XmlElement duracion = documento.CreateElement("Duracion");
            duracion.InnerText = dura1;
            Cancion.AppendChild(duracion);
            //Creamos el elemento movil.
            XmlElement calidad = documento.CreateElement("Calidad");
            calidad.InnerText = cali1;
            Cancion.AppendChild(calidad);

            return Cancion;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            button3.Visible = true;
            button4.Visible = false;
            dataGridView1.Visible = false;
            dataGridView2.Visible = true;
            listareproduci.RemoveRange(0, listareproduci.Count);
            refre();
            eliminarlisre();
            listabiblio.RemoveRange(0, listabiblio.Count);
            listareproduci.RemoveRange(0, listareproduci.Count);

            button5.Visible = false;
            button6.Visible = false;
            button7.Visible = false;
            button8.Visible = true;
            leerbiblio();
            dataGridView2.DataSource = null;
            dataGridView2.Refresh();
            dataGridView2.DataSource = listabiblio;
            dataGridView2.Refresh();
        }
        string nombre1, url, num, album, dura, cali;
        private void InsertarXml()
        {
            //Cargamos el documento XML.
            documento = new XmlDocument();
            documento.Load(ruta);

            for (int i = 0; i < listabiblio.Count(); i++)
            {

                if (label1.Text == listabiblio[i].Nombre)
                {

                    nombre1 = listabiblio[i].Nombre;
                    url = listabiblio[i].Url;
                    num = listabiblio[i].Num;
                    album = listabiblio[i].Album;
                    dura = listabiblio[i].Duracion;
                    cali = listabiblio[i].Calidad;

                }
            }
            //Creamos el nodo que deseamos insertar.
            XmlNode empleado = this.CrearNodoXml(nombre1, url, num, album, dura, cali);
            //Obtenemos el nodo raiz del documento.
            XmlNode nodoRaiz = documento.DocumentElement;

            //Insertamos el nodo empleado al final del archivo
            nodoRaiz.InsertAfter(empleado, nodoRaiz.LastChild);   //***

            documento.Save(ruta);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                urla = openFileDialog1.FileName;
            }
            listadatosmp3.RemoveRange(0, listadatosmp3.Count);



            Lista blitmp = new Lista();
            TagLib.File file = TagLib.File.Create(urla);
            blitmp.Url = urla;
            blitmp.Nombre = file.Tag.Title;
            blitmp.Titulo = file.Tag.Title;
            label1.Text = file.Tag.Title;
            blitmp.Año = Convert.ToString(file.Tag.Year);
            blitmp.Duracion = file.Properties.Duration.ToString();
            blitmp.Num = Convert.ToString(file.Tag.Track);
            blitmp.Album = file.Tag.Album;
            blitmp.Calidad = Convert.ToString(file.Properties.AudioBitrate);

            listabiblio.Add(blitmp);

            string archivo = @"biblio.xml";
            if (File.Exists(archivo) == true)
            {
                InsertarXml();
            }
            else { EscribirXml(); }
            listabiblio.RemoveRange(0, listabiblio.Count);
            leerbiblio();
            dataGridView2.DataSource = null;
            dataGridView2.Refresh();
            dataGridView2.DataSource = listabiblio;
            dataGridView2.Columns["Url"].Visible = false;
            dataGridView2.Refresh();
        }
        public void leerbiblio()
        {
            XDocument documento = XDocument.Load(@"biblio.xml");
            var listar = from lis in documento.Descendants("Blibioteca") select lis;
            foreach (XElement u in listar.Elements("Cancion"))
            {
                Lista tmp = new Lista();
                tmp.Nombre = u.Element("Titulo").Value;
                tmp.Url = u.Element("Url").Value;
                tmp.Num = u.Element("No").Value;
                tmp.Album = u.Element("Album").Value;
                tmp.Duracion = u.Element("Duracion").Value;
                tmp.Calidad = u.Element("Calidad").Value;

                listabiblio.Add(tmp);

            }
        }
        public void cargarima(string dat)
        {
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
                    frm.pictureBox1.Image = currentImage.GetThumbnailImage(200, 200, null, System.IntPtr.Zero);
                }
                ms.Close();
            }
        }
        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string nom = dataGridView2.CurrentRow.Cells["Nombre"].Value.ToString();
            label1.Text = nom;
        }
        string urla;
        private void dataGridView2_DoubleClick(object sender, EventArgs e)
        {
            frm.Hide();
            frm.label1.Text = " ";

            frm.label1.Text = dataGridView2.CurrentRow.Cells["nombre"].Value.ToString();
            frm.player.URL = dataGridView2.CurrentRow.Cells["url"].Value.ToString();

            WMPLib.IWMPPlaylist playlist = frm.player.playlistCollection.newPlaylist("myplaylist");
            WMPLib.IWMPMedia media;

            media = frm.player.newMedia(dataGridView2.CurrentRow.Cells["url"].Value.ToString());
            playlist.appendItem(media);

            frm.player.currentPlaylist = playlist;
            listadatosmp3.RemoveRange(0, listadatosmp3.Count);
            string dat = dataGridView2.CurrentRow.Cells["url"].Value.ToString();
            cargarima(dat);
            tagcan(dat);
            frm.dataGridView1.DataSource = null;
            frm.dataGridView1.Refresh();
            frm.dataGridView1.DataSource = listadatosmp3;
            frm.dataGridView1.Refresh();
            frm.Show();
        }
        public void tagcan(string car)
        {
            TagLib.File file = TagLib.File.Create(car);
            Datos datmp = new Datos();
            datmp.Titulo = file.Tag.Title;
            datmp.Año = Convert.ToString(file.Tag.Year);
            datmp.Genero = file.Tag.FirstGenre;
            datmp.Duracion = file.Properties.Duration.ToString();
            datmp.Num = Convert.ToString(file.Tag.Track);
            datmp.Artista = file.Tag.TitleSort;
            datmp.Album = file.Tag.Album;
            datmp.Comentario = file.Tag.Comment;
            listadatosmp3.Add(datmp);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string nomb = label1.Text;
            for (int i = 0; i < listareproduci.Count; i++)
            {
                if (nomb == listareproduci[i].Nombredecan)
                {
                    listareproduci.RemoveAt(i);
                }
            }
            cargar();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            Play reprotemp = new Play();
            reprotemp.Direccion = openFileDialog1.FileName;
            reprotemp.Nombredecan = openFileDialog1.SafeFileName.ToString();
            listareproduci.Add(reprotemp);
            cargar();
            xml();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            frm.Hide();
            frm.label1.Text = " ";

            frm.label1.Text = dataGridView1.CurrentRow.Cells["nombredecan"].Value.ToString();
            frm.player.URL = dataGridView1.CurrentRow.Cells["direccion"].Value.ToString();

            WMPLib.IWMPPlaylist playlist = frm.player.playlistCollection.newPlaylist("myplaylist");
            WMPLib.IWMPMedia media;

            media = frm.player.newMedia(dataGridView1.CurrentRow.Cells["direccion"].Value.ToString());
            playlist.appendItem(media);

            frm.player.currentPlaylist = playlist;
            listadatosmp3.RemoveRange(0, listadatosmp3.Count);
            string dat = dataGridView1.CurrentRow.Cells["direccion"].Value.ToString();
            cargarima(dat);
            tagcan(dat);
            frm.dataGridView1.DataSource = null;
            frm.dataGridView1.Refresh();
            frm.dataGridView1.DataSource = listadatosmp3;
            frm.dataGridView1.Refresh();
            frm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int contar = listareproduci.Count();
            int inicio = 0;
            listareproduci.RemoveRange(inicio, contar);
            cargar();
        }
        public void ModificarDatosXml(string url)
        {
            //Cargamos el documento XML.
            documento = new XmlDocument();
            documento.Load(ruta);
            //Obtenemos el nodo raiz del documento.
            XmlElement bibliot = documento.DocumentElement;

            //Obtenemos la lista de todos los empleados.
            XmlNodeList listacancion = documento.SelectNodes("Blibioteca/Cancion");

            foreach (XmlNode item in listacancion)
            {
                //Determinamos el nodo a modificar por medio del id de empleado.
                if (item.FirstChild.InnerText == url)
                {
                    //Nodo sustituido.
                    XmlNode nodoOld = item;
                    bibliot.RemoveChild(nodoOld);
                }
            }

            //Salvamos el documento.
            documento.Save(ruta);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string nomb = label1.Text;
            for (int i = 0; i < listabiblio.Count; i++)
            {
                if (nomb == listabiblio[i].Nombre)
                {
                    listabiblio.RemoveAt(i);
                }
            }

            ModificarDatosXml(nomb);
            listabiblio.RemoveRange(0, listabiblio.Count);
            leerbiblio();
            dataGridView2.DataSource = null;
            dataGridView2.Refresh();
            dataGridView2.DataSource = listabiblio;
            dataGridView2.Refresh();

        }

        private void button9_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string nomb = label1.Text;
            for (int i = 0; i < listareproduci.Count; i++)
            {
                Lista blitmp = new Lista();
                if (nomb == listareproduci[i].Nombredecan)
                {
                    blitmp.Url = listareproduci[i].Direccion;
                    blitmp.Nombre = listareproduci[i].Nombredecan;
                    TagLib.File file = TagLib.File.Create(listareproduci[i].Direccion);
                    blitmp.Titulo = file.Tag.Title;
                    blitmp.Año = Convert.ToString(file.Tag.Year);
                    blitmp.Duracion = file.Properties.Duration.ToString();
                    blitmp.Num = Convert.ToString(file.Tag.Track);
                    blitmp.Album = file.Tag.Album;
                    blitmp.Calidad = Convert.ToString(file.Properties.AudioBitrate);

                }
                listabiblio.Add(blitmp);
            }
            string archivo = @"biblio.xml";
            if (File.Exists(archivo) == true)
            {
                InsertarXml();
            }
            else { EscribirXml(); }
        }
    }
}
