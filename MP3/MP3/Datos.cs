using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP3
{
    class Datos
    {
        string artista;
        string comentario;
        string album;
        string año;
        string duracion;
        string num;
        string titulo;
        string genero;
        List<string> Compositor;

        public string Artista
        {
            get
            {
                return artista;
            }

            set
            {
                artista = value;
            }
        }

        public string Comentario
        {
            get
            {
                return comentario;
            }

            set
            {
                comentario = value;
            }
        }

        public string Album
        {
            get
            {
                return album;
            }

            set
            {
                album = value;
            }
        }

        public string Año
        {
            get
            {
                return año;
            }

            set
            {
                año = value;
            }
        }

        public string Duracion
        {
            get
            {
                return duracion;
            }

            set
            {
                duracion = value;
            }
        }

        public string Num
        {
            get
            {
                return num;
            }

            set
            {
                num = value;
            }
        }

        public string Titulo
        {
            get
            {
                return titulo;
            }

            set
            {
                titulo = value;
            }
        }

        public string Genero
        {
            get
            {
                return genero;
            }

            set
            {
                genero = value;
            }
        }

        public List<string> Compositor1
        {
            get
            {
                return Compositor;
            }

            set
            {
                Compositor = value;
            }
        }
    }
}
