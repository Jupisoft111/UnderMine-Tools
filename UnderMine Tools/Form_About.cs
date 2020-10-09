using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnderMine_Tools.Properties;

namespace UnderMine_Tools
{
    public partial class Form_About : Form
    {
        public Form_About()
        {
            InitializeComponent();
        }

        internal string Texto_Programa = Program.Texto_Programa + " " + Program.Texto_Versión_Fecha;
        internal string Texto_Desarrollador = "Jupisoft (Júpiter Mauro Cámara, of " + Program.Traducir_Número_Decimales_Redondear(DateTime.Now.Date.Subtract(new DateTime(1986, 4, 21, 17, 40, 0, 0)).TotalDays / 365.25d, 2) + " years, from Spain, Jupitermauro@gmail.com, http://jupisoft.x10host.com/)";
        internal string Texto_Copyright = "© 2004 - " + Math.Max(2020, DateTime.Now.Year).ToString() + " by Jupisoft, all rights reserved. Some images are copyright from Mojang Studios and others";
        internal string Texto_Licencia = "Released under the GNU General Public License v3.0 (http://www.gnu.org/licenses/gpl-3.0.html/)";
        internal string Texto_Librerías = "Newtonsoft.Json";
        internal string Texto_Comentarios = "\"Only for the Glory of God\", from Johann Sebastian Bach and also read Gulliver's Travels by Jonathan Swift and \"Laputa\"";
        internal string Texto_Advertencia = "I can't afford internet, so I may take a bit to answer back, sorry... donations will help me to make more and better content";
        internal bool Variable_Siempre_Visible = false;
        internal int Modo_Color = 0;
        internal int Índice_Matiz = 0;

        private void Ventana_Acerca_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Program.Icono_Jupisoft.Clone() as Icon;
                this.Text = "About " + Program.Texto_Título_Versión + " - [" + Program.Texto_UnderMine_Versión + "]";
                TextBox_Programa.Text = Texto_Programa;
                TextBox_Desarrollador.Text = Texto_Desarrollador;
                TextBox_Copyright.Text = Texto_Copyright;
                TextBox_Licencia.Text = Texto_Licencia;
                TextBox_Librerías.Text = Texto_Librerías;
                TextBox_Comentarios.Text = Texto_Comentarios;
                TextBox_Advertencia.Text = Texto_Advertencia;
                TextBox_Gracias.Text = Environment.UserName + " and anyone else that has ever helped me, collaborated with my multiple works or posted comments";
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Acerca_Shown(object sender, EventArgs e)
        {
            try
            {
                this.Activate();
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Acerca_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Acerca_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Acerca_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Ventana_Acerca_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt && !e.Control && !e.Shift)
                {
                    if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                    }
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Picture_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                /*if (e.Button == MouseButtons.Left)
                {
                    Temporizador_Principal.Enabled = !Temporizador_Principal.Enabled;
                    if (!Temporizador_Principal.Enabled)
                    {
                        Índice_Matiz = 0;
                        Picture.Image = null;
                        Picture.Invalidate();
                        Picture.Update();
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (!Temporizador_Principal.Enabled)
                    {
                        Picture.Image = Filtrar_Imagen(Program.Rand.Next(0, 1529));
                        Picture.Invalidate();
                        Picture.Update();
                    }
                }
                else *///if (e.Button == MouseButtons.Middle)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Sitio_David_Maeso_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.davidmaeso.com/index.html", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Fratelli_Stellari_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.messaggidallestelle.altervista.org/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Xisumavoid_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.xisumavoid.com/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Hermitcraft_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://hermitcraft.com/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Jupisoft_GitHub_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://github.com/Jupisoft111/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Jupisoft_x10host_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://jupisoft.x10host.com/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Jupisoft_YouTube_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.youtube.com/channel/UCQkgl08FknXRlP1zVBYSw2A/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Jupisoft_GitHub_Proyecto_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://github.com/Jupisoft111/UnderMine-Tools/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Propiedades_Click(object sender, EventArgs e)
        {
            try
            {
                if (!PInvoke.Shell32.SHObjectProperties(this.Handle, PInvoke.Shell32.SHObjectPropertiesFlags.FilePath, Application.ExecutablePath, null)) SystemSounds.Beep.Play();
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Jupisoft_Twitter_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.twitter.com/jupisoft/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Jupisoft_SoundClick_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.soundclick.com/jupisoft/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Jupisoft_SoundCloud_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("http://www.soundcloud.com/jupisoft/", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Correo_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("mailto:jupitermauro@gmail.com?subject=" + Program.Texto_Programa + " " + Program.Texto_Versión_Fecha, ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Donar_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=KSMZ3XNG2R9P6", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        private void Botón_Aceptar_Click(object sender, EventArgs e)
        {
            try
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }

        internal Bitmap Filtrar_Imagen(int Índice_Matiz)
        {
            Bitmap Imagen = Resources.Jupisoft_256.Clone() as Bitmap;
            BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, 256, 256), ImageLockMode.ReadWrite, Imagen.PixelFormat);
            byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * 256];
            Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
            double Matiz, Saturación, Luminosidad;
            byte Rojo, Verde, Azul;
            for (int Y = 0, Índice = 0; Y < 256; Y++)
            {
                for (int X = 0; X < 256; X++, Índice += 4)
                {
                    if (Matriz_Bytes[Índice] != Matriz_Bytes[Índice + 1] || Matriz_Bytes[Índice] != Matriz_Bytes[Índice + 2]) // Not a gray pixel.
                    {
                        Program.HSL.From_RGB(Matriz_Bytes[Índice + 2], Matriz_Bytes[Índice + 1], Matriz_Bytes[Índice], out Matiz, out Saturación, out Luminosidad);
                        Matiz += (Índice_Matiz * 360d) / 1529d;
                        while (Matiz < 0d) Matiz += 360d;
                        while (Matiz >= 360d) Matiz -= 360d;
                        Program.HSL.To_RGB(Matiz, Saturación, Luminosidad, out Rojo, out Verde, out Azul);
                        Matriz_Bytes[Índice + 2] = Rojo;
                        Matriz_Bytes[Índice + 1] = Verde;
                        Matriz_Bytes[Índice] = Azul;
                    }
                }
            }
            Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
            Imagen.UnlockBits(Bitmap_Data);
            Bitmap_Data = null;
            Matriz_Bytes = null;
            return Imagen;
        }

        private void Temporizador_Principal_Tick(object sender, EventArgs e)
        {
            try
            {
                Picture.Image = Filtrar_Imagen(Índice_Matiz);
                Picture.Invalidate();
                Picture.Update();
                if (Modo_Color == 0)
                {
                    Índice_Matiz++;
                    if (Índice_Matiz >= 1530) Índice_Matiz = 0;
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }
    }
}
