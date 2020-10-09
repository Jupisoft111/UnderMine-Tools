using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnderMine_Tools
{
    internal static class Program
    {
        internal static readonly string Texto_Fecha = "2020_10_08_20_52_07_601"; // First created at: 2020_09_14_14_26_15_809.
        /// <summary>
        /// The UnderMine version that most tools of this application will support.
        /// </summary>
        internal static readonly string Texto_UnderMine_Versión = "UnderMine 1.0.1.26";
        internal static string Texto_Usuario = Environment.UserName;
        internal static string Texto_Título = "UnderMine Tools by Jupisoft";
        internal static string Texto_Programa = "UnderMine Tools";
        internal static readonly string Texto_Versión = "1.0";
        internal static readonly string Texto_Versión_Fecha = Texto_Versión + " (" + Texto_Fecha/*.Replace("_", null)*/ + ")";
        internal static string Texto_Título_Versión = Texto_Título + " " + Texto_Versión;

        /// <summary>
        /// Using this icon instead of adding it to the designer of each form saved almost 11 MB of space in my Minecraft Tools application.
        /// </summary>
        internal static Icon Icono_Jupisoft = null;

        internal static Random Rand = new Random();
        internal static readonly char Caracter_Coma_Decimal = (0.5d).ToString()[1];
        internal static readonly char Caracter_Coma_Decimal_Invertido = Caracter_Coma_Decimal == ',' ? '.' : ',';
        internal static Process Proceso = Process.GetCurrentProcess();
        internal static PerformanceCounter Rendimiento_Procesador = null;

        /// <summary>
        /// Obtains a rectangle with cut positions from any image trying to exclude from it the background color specified. Note: use "Color.Empty" instead of "Color.Transparent" or it will fail.
        /// </summary>
        /// <param name="Imagen">Any valid image. It should have alpha.</param>
        /// <param name="Color_Fondo">The background color to exclude. Note: use "Color.Empty" instead of "Color.Transparent" or it will fail.</param>
        /// <returns>Returns a rectangle with cut positions for the image, but check if it's out of bounds, which will mean the image needs no changes. Returns null on any error.</returns>
        internal static Rectangle Buscar_Zona_Recorte_Imagen(Bitmap Imagen, Color Color_Fondo)
        {
            if (Imagen != null)
            {
                int Ancho = Imagen.Width;
                int Alto = Imagen.Height;
                int Rectángulo_X = int.MaxValue;
                int Rectángulo_Y = int.MaxValue;
                int Rectángulo_Ancho = int.MinValue;
                int Rectángulo_Alto = int.MinValue;
                BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Imagen.Width, Imagen.Height), ImageLockMode.ReadOnly, Imagen.PixelFormat);
                int Ancho_Stride = Math.Abs(Bitmap_Data.Stride);
                int Bytes_Aumento = !Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 3 : 4;
                int Bytes_Diferencia = Ancho_Stride - ((Imagen.Width * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                byte[] Matriz_Bytes = new byte[Ancho_Stride * Imagen.Height];
                Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                Imagen.UnlockBits(Bitmap_Data);
                for (int Y = 0, Índice = 0; Y < Alto; Y++, Índice += Bytes_Diferencia)
                {
                    for (int X = 0; X < Ancho; X++, Índice += Bytes_Aumento)
                    {
                        if (((Color_Fondo == Color.Empty ||
                            Color_Fondo == Color.Transparent) &&
                            Matriz_Bytes[Índice + 3] > 0) ||
                            (Color_Fondo != Color.Empty &&
                            Color_Fondo != Color.Transparent &&
                            (Matriz_Bytes[Índice + 3] != Color_Fondo.A ||
                            Matriz_Bytes[Índice + 2] != Color_Fondo.R ||
                            Matriz_Bytes[Índice + 1] != Color_Fondo.G ||
                            Matriz_Bytes[Índice] != Color_Fondo.B)))
                        {
                            if (X < Rectángulo_X) Rectángulo_X = X;
                            if (X + 1 > Rectángulo_Ancho) Rectángulo_Ancho = X + 1;
                            if (Y < Rectángulo_Y) Rectángulo_Y = Y;
                            if (Y + 1 > Rectángulo_Alto) Rectángulo_Alto = Y + 1;
                        }
                    }
                }
                Matriz_Bytes = null;
                //Rectangle Rectángulo = Rectangle.FromLTRB(Rectángulo_X, Rectángulo_Y, Rectángulo_Ancho, Rectángulo_Alto);
                //if (Rectángulo.Width <= 0 || Rectángulo.Height <= 0) Rectángulo = new Rectangle(0, 0, Ancho, Alto);
                return Rectangle.FromLTRB(Rectángulo_X, Rectángulo_Y, Rectángulo_Ancho, Rectángulo_Alto);
            }
            return Rectangle.Empty;
        }

        /// <summary>
        /// Loads any image from disk into memory and redraws it in one of the supported pixel formats, so it will never give any error (in theory).
        /// </summary>
        /// <param name="Ruta">Any valid file path that contains an image inside.</param>
        /// <param name="Alfa">If it's Indeterminate the returned image will contain alpha (transparency) only it if had it before. If it's Checked the returned image will always have alpha. Otherwise it will never have alpha.</param>
        /// <returns>The redrawed image in one of the supported pixel formats.</returns>
        internal static Bitmap Cargar_Imagen_Ruta(string Ruta, CheckState Alfa)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                {
                    Image Imagen_Original = null;
                    FileStream Lector = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    try { Imagen_Original = Image.FromStream(Lector, false, false); }
                    catch { Imagen_Original = null; }
                    if (Imagen_Original != null)
                    {
                        int Ancho = Imagen_Original.Width;
                        int Alto = Imagen_Original.Height;
                        Bitmap Imagen = new Bitmap(Ancho, Alto, Alfa == CheckState.Unchecked ? PixelFormat.Format24bppRgb : Alfa == CheckState.Checked ? PixelFormat.Format32bppArgb : (!Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format24bppRgb : PixelFormat.Format32bppArgb));
                        Graphics Pintar = Graphics.FromImage(Imagen);
                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar.SmoothingMode = SmoothingMode.HighQuality;
                        Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                        Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho, Alto), GraphicsUnit.Pixel);
                        Pintar.Dispose();
                        Pintar = null;
                        Imagen_Original.Dispose();
                        Imagen_Original = null;
                        return Imagen;
                    }
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        /// <summary>
        /// Generates a copy of the specified file and saves it with the specified name.
        /// </summary>
        /// <param name="Ruta_Entrada">The original file to read.</param>
        /// <param name="Ruta_Salida">The new file to generate or overwrite without asking.</param>
        /// <returns>Returns true if the file was copied without errors. Returns false otherwise.</returns>
        internal static bool Copiar_Archivo(string Ruta_Entrada, string Ruta_Salida)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta_Entrada) && !string.IsNullOrEmpty(Ruta_Salida) && File.Exists(Ruta_Entrada))
                {
                    Crear_Carpetas(Path.GetDirectoryName(Ruta_Salida));
                    DateTime Fecha_Último_Acceso;
                    DateTime Fecha_Modificación;
                    DateTime Fecha_Creación;
                    try { Fecha_Último_Acceso = File.GetLastAccessTime(Ruta_Entrada); }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Fecha_Último_Acceso = DateTime.MinValue; }
                    try { Fecha_Modificación = File.GetLastWriteTime(Ruta_Entrada); }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Fecha_Modificación = DateTime.MinValue; }
                    try { Fecha_Creación = File.GetCreationTime(Ruta_Entrada); }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Fecha_Creación = DateTime.MinValue; }
                    FileStream Lector_Entrada = new FileStream(Ruta_Entrada, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    long Longitud_Total = Lector_Entrada.Length;
                    Lector_Entrada.Seek(0L, SeekOrigin.Begin);
                    if (File.Exists(Ruta_Salida)) Program.Quitar_Atributo_Sólo_Lectura(Ruta_Salida);
                    FileStream Lector_Salida = new FileStream(Ruta_Salida, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Lector_Salida.SetLength(0L); // Always try to overwrite the output file.
                    Lector_Salida.Seek(0L, SeekOrigin.Begin);
                    int Longitud_Búfer = 4096; // Buffer length of 4 KB.
                    byte[] Matriz_Bytes_Búfer = new byte[Longitud_Búfer];
                    long Longitud_Leída = 0L;
                    for (long Índice_Bloque = 0L; Índice_Bloque < Lector_Entrada.Length; Índice_Bloque += Longitud_Búfer)
                    {
                        int Longitud = Lector_Entrada.Read(Matriz_Bytes_Búfer, 0, Longitud_Búfer);
                        if (Longitud > 0)
                        {
                            Lector_Salida.Write(Matriz_Bytes_Búfer, 0, Longitud);
                            Lector_Salida.Flush();
                            Longitud_Leída += Longitud;
                        }
                    }
                    Lector_Salida.Close();
                    Lector_Salida.Dispose();
                    Lector_Salida = null;
                    Lector_Entrada.Close();
                    Lector_Entrada.Dispose();
                    Lector_Entrada = null;
                    // Try to copy all the dates of the original file also.
                    try { File.SetCreationTime(Ruta_Salida, Fecha_Creación); }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                    try { File.SetLastWriteTime(Ruta_Salida, Fecha_Modificación); }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                    try { File.SetLastAccessTime(Ruta_Salida, Fecha_Último_Acceso); }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                    if (Longitud_Leída == Longitud_Total) return true; // Perfect copy done (in theory).
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return false; // Something went wrong.
        }

        /// <summary>
        /// Creates all the directories is the specified path if they don't exist yet, without showing any exception.
        /// </summary>
        /// <param name="Ruta">Any valid directory path.</param>
        /// <returns>Returns true if the specified directories in the path now exist. Returns false on any exception, possibly indicating that the directories might not exist.</returns>
        internal static bool Crear_Carpetas(string Ruta)
        {
            try
            {
                if (!Directory.Exists(Ruta))
                {
                    Directory.CreateDirectory(Ruta);
                    return Directory.Exists(Ruta);
                }
                else return true;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return false;
        }

        /// <summary>
        /// Executes the specified file, directory or URL, with the specified window style.
        /// </summary>
        /// <param name="Ruta">Any valid file or directory path.</param>
        /// <param name="Estado">Any valid window style.</param>
        /// <returns>Returns true if the process can be executed. Returns false if it can't be executed.</returns>
        internal static bool Ejecutar_Ruta(string Ruta, ProcessWindowStyle Estado)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta))
                {
                    Process Proceso = new Process();
                    Proceso.StartInfo.Arguments = null;
                    Proceso.StartInfo.ErrorDialog = false;
                    Proceso.StartInfo.FileName = Ruta;
                    Proceso.StartInfo.UseShellExecute = true;
                    Proceso.StartInfo.Verb = "open";
                    Proceso.StartInfo.WindowStyle = Estado;
                    if (File.Exists(Ruta)) Proceso.StartInfo.WorkingDirectory = Ruta;
                    else if (Directory.Exists(Ruta)) Proceso.StartInfo.WorkingDirectory = Ruta;
                    bool Resultado;
                    try { Resultado = Proceso.Start(); }
                    catch { Resultado = false; }
                    Proceso.Close();
                    Proceso.Dispose();
                    Proceso = null;
                    return Resultado;
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return false;
        }

        /// <summary>
        /// Function that returns one of the 1.530 possible 24 bits RGB colors with full saturation and middle brightness.
        /// </summary>
        /// <param name="Índice">Any value between 0 and 1529. Red = 0, Yellow = 255, Green = 510, Cyan = 765, blue = 1020, purple = 1275. If the value is below 0 or above 1529, pure white will be returned instead.</param>
        /// <returns>Returns an ARGB color based on the selected index, or white if out of bounds.</returns>
        internal static Color Obtener_Color_Puro_1530(int Índice)
        {
            try
            {
                if (Índice >= 0 && Índice <= 1529)
                {
                    if (Índice < 255) return Color.FromArgb(255, Índice, 0);
                    else if (Índice < 510) return Color.FromArgb(510 - Índice, 255, 0);
                    else if (Índice < 765) return Color.FromArgb(0, 255, 255 - (765 - Índice));
                    else if (Índice < 1020) return Color.FromArgb(0, 1020 - Índice, 255);
                    else if (Índice < 1275) return Color.FromArgb(255 - (1275 - Índice), 0, 255);
                    else return Color.FromArgb(255, 0, 1530 - Índice);
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return Color.FromArgb(255, 255, 255);
        }

        /// <summary>
        /// Obtains a miniature (or any size really) from any valid image, keeping it's original aspect ratio if desired.
        /// </summary>
        /// <param name="Imagen_Original">Any valid image.</param>
        /// <param name="Ancho_Miniatura">The desired width of the miniature.</param>
        /// <param name="Alto_Miniatura">The desired height of the miniature.</param>
        /// <param name="Relación_Aspecto">If true the miniature will keep the original aspect ratio.</param>
        /// <param name="Antialiasing">If true the miniature will be drawn with high interpolation, reducing the alias effect, at the cost of getting a bit blurred.</param>
        /// <param name="Alfa">If it's Indeterminate the returned image will contain alpha (transparency) only it if had it before. If it's Checked the returned image will always have alpha. Otherwise it will never have alpha.</param>
        /// <returns>Returns the miniature drawn with the specified options. On any error it will return null.</returns>
        internal static Bitmap Obtener_Imagen_Miniatura(Image Imagen_Original, int Ancho_Miniatura, int Alto_Miniatura, bool Relación_Aspecto, bool Antialiasing, CheckState Alfa)
        {
            try
            {
                if (Imagen_Original != null)
                {
                    int Ancho_Original = Imagen_Original.Width;
                    int Alto_Original = Imagen_Original.Height;
                    int Ancho = Ancho_Miniatura;
                    int Alto = Alto_Miniatura;
                    if (Relación_Aspecto) // Keep the original aspect ratio.
                    {
                        Ancho = (Alto_Miniatura * Ancho_Original) / Alto_Original;
                        Alto = (Ancho_Miniatura * Alto_Original) / Ancho_Original;
                        if (Ancho <= Ancho_Miniatura) Alto = Alto_Miniatura;
                        else if (Alto <= Alto_Miniatura) Ancho = Ancho_Miniatura;
                    }
                    if (Ancho < 1) Ancho = 1;
                    if (Alto < 1) Alto = 1;
                    Bitmap Imagen = new Bitmap(Ancho, Alto, Alfa == CheckState.Indeterminate ? (Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb) : Alfa == CheckState.Checked ? PixelFormat.Format32bppArgb : PixelFormat.Format24bppRgb);
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    //Pintar.Clear(Color.Black);
                    Pintar.CompositingMode = CompositingMode.SourceCopy;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = !Antialiasing ? InterpolationMode.NearestNeighbor : InterpolationMode.HighQualityBicubic;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.None;
                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                    Pintar.DrawImage(Imagen_Original, new Rectangle(0, 0, Ancho, Alto), new Rectangle(0, 0, Ancho_Original, Alto_Original), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;
                    return Imagen;
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static Bitmap Obtener_Imagen_Miniatura_Recortada(Bitmap Imagen_Original, int Ancho_Alto, bool Antialiasing)
        {
            try
            {
                if (Imagen_Original != null)
                {
                    int Ancho = Imagen_Original.Width;
                    int Alto = Imagen_Original.Height;
                    Rectangle Rectángulo = Buscar_Zona_Recorte_Imagen(Imagen_Original, Color.Transparent);
                    if (Rectángulo.X > -1 && Rectángulo.Y > -1 &&
                        Rectángulo.X < int.MaxValue && Rectángulo.Y < int.MaxValue &&
                        Rectángulo.Width > 0 && Rectángulo.Height > 0)
                    {
                        Imagen_Original = Imagen_Original.Clone(Rectángulo, !Image.IsAlphaPixelFormat(Imagen_Original.PixelFormat) ? PixelFormat.Format24bppRgb : PixelFormat.Format32bppArgb);
                        int Máximo_Ancho_Alto = Math.Max(Rectángulo.Width, Rectángulo.Height);
                        Bitmap Imagen = new Bitmap(Máximo_Ancho_Alto, Máximo_Ancho_Alto, Imagen_Original.PixelFormat);
                        Graphics Pintar = Graphics.FromImage(Imagen);
                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                        Pintar.InterpolationMode = !Antialiasing ? InterpolationMode.NearestNeighbor : InterpolationMode.HighQualityBicubic;
                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        Pintar.SmoothingMode = SmoothingMode.None;
                        Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                        Pintar.DrawImage(Imagen_Original, new Rectangle((int)Math.Round((double)(Máximo_Ancho_Alto - Rectángulo.Width) / 2d, MidpointRounding.AwayFromZero), (int)Math.Round((double)(Máximo_Ancho_Alto - Rectángulo.Height) / 2d, MidpointRounding.AwayFromZero), Rectángulo.Width, Rectángulo.Height), new Rectangle(0, 0, Rectángulo.Width, Rectángulo.Height), GraphicsUnit.Pixel);
                        Pintar.Dispose();
                        Pintar = null;
                        //Imagen = Obtener_Imagen_Miniatura(Imagen, Ancho_Alto, Ancho_Alto, false, Antialiasing, CheckState.Indeterminate);
                        return Imagen;
                    }
                    else return null;
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return null;
        }

        internal static string Obtener_Nombre_Temporal()
        {
            try
            {
                DateTime Fecha = DateTime.Now;
                string Año = Fecha.Year.ToString();
                string Mes = Fecha.Month.ToString();
                string Día = Fecha.Day.ToString();
                string Hora = Fecha.Hour.ToString();
                string Minuto = Fecha.Minute.ToString();
                string Segundo = Fecha.Second.ToString();
                string Milisegundo = Fecha.Millisecond.ToString();
                while (Año.Length < 4) Año = '0' + Año;
                while (Mes.Length < 2) Mes = '0' + Mes;
                while (Día.Length < 2) Día = '0' + Día;
                while (Hora.Length < 2) Hora = '0' + Hora;
                while (Minuto.Length < 2) Minuto = '0' + Minuto;
                while (Segundo.Length < 2) Segundo = '0' + Segundo;
                while (Milisegundo.Length < 3) Milisegundo = '0' + Milisegundo;
                return Año + "_" + Mes + "_" + Día + "_" + Hora + "_" + Minuto + "_" + Segundo + "_" + Milisegundo;
            }
            catch { }
            return "0000_00_00_00_00_00_000";
        }

        /// <summary>
        /// This function makes sure that the selected file or directory doesn't have a read-only attribute, and if it does, tries to remove it automatically.
        /// </summary>
        /// <param name="Ruta">Any valid and existing file or directory path.</param>
        /// <returns>Returns the original attributes of the file or directory.</returns>
        internal static FileAttributes Quitar_Atributo_Sólo_Lectura(string Ruta)
        {
            try
            {
                if (!string.IsNullOrEmpty(Ruta) && (File.Exists(Ruta) || Directory.Exists(Ruta)))
                {
                    FileSystemInfo Info = File.Exists(Ruta) ? (FileSystemInfo)new FileInfo(Ruta) : (FileSystemInfo)new DirectoryInfo(Ruta);
                    FileAttributes Atributos_Originales = Info.Attributes;
                    FileAttributes Atributos = Atributos_Originales;
                    if ((Atributos & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                    {
                        Atributos -= FileAttributes.ReadOnly;
                        if (Atributos <= 0) Atributos = FileAttributes.Normal;
                        Info.Attributes = Atributos;
                    }
                    Info = null;
                    return Atributos_Originales;
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return FileAttributes.Normal;
        }

        internal static string Traducir_Fecha_Hora(DateTime Fecha)
        {
            try
            {
                if (Fecha != null && Fecha >= DateTime.MinValue && Fecha <= DateTime.MaxValue)
                {
                    string Año = Fecha.Year.ToString();
                    string Mes = Fecha.Month.ToString();
                    string Día = Fecha.Day.ToString();
                    string Hora = Fecha.Hour.ToString();
                    string Minuto = Fecha.Minute.ToString();
                    string Segundo = Fecha.Second.ToString();
                    string Milisegundo = Fecha.Millisecond.ToString();
                    while (Año.Length < 4) Año = "0" + Año;
                    while (Mes.Length < 2) Mes = "0" + Mes;
                    while (Día.Length < 2) Día = "0" + Día;
                    while (Hora.Length < 2) Hora = "0" + Hora;
                    while (Minuto.Length < 2) Minuto = "0" + Minuto;
                    while (Segundo.Length < 2) Segundo = "0" + Segundo;
                    while (Milisegundo.Length < 3) Milisegundo = "0" + Milisegundo;
                    return Día + "-" + Mes + "-" + Año + ", " + Hora + ":" + Minuto + ":" + Segundo + "." + Milisegundo;
                }
            }
            catch (Exception Excepción) { Application.OnThreadException(Excepción); }
            return "??-??-????, ??:??:??.???";
        }

        internal static string Traducir_Intervalo_Días_Horas_Minutos_Segundos(TimeSpan Intervalo)
        {
            try
            {
                string Días = Intervalo.Days.ToString();
                string Horas = Intervalo.Hours.ToString();
                string Minutos = Intervalo.Minutes.ToString();
                string Segundos = Intervalo.Seconds.ToString();
                string Milisegundos = Intervalo.Milliseconds.ToString();
                while (Horas.Length < 2) Horas = "0" + Horas;
                while (Minutos.Length < 2) Minutos = "0" + Minutos;
                while (Segundos.Length < 2) Segundos = "0" + Segundos;
                while (Milisegundos.Length < 3) Milisegundos = "0" + Milisegundos;
                return Días + ":" + Horas + ":" + Minutos + ":" + Segundos + "." + Milisegundos;
            }
            catch (Exception Excepción) { Application.OnThreadException(Excepción); }
            return "0:00:00:00.000";
        }

        internal static string Traducir_Número(sbyte Valor)
        {
            return Valor.ToString();
        }

        internal static string Traducir_Número(byte Valor)
        {
            return Valor.ToString();
        }

        internal static string Traducir_Número(short Valor)
        {
            return Valor > -1000 && Valor < 1000 ? Valor.ToString() : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(ushort Valor)
        {
            return Valor < 1000 ? Valor.ToString() : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(int Valor)
        {
            return Valor > -1000 && Valor < 1000 ? Valor.ToString() : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(uint Valor)
        {
            return Valor < 1000 ? Valor.ToString() : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(long Valor)
        {
            return Valor > -1000L && Valor < 1000L ? Valor.ToString() : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(ulong Valor)
        {
            return Valor < 1000UL ? Valor.ToString() : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(float Valor)
        {
            //if (Single.IsNegativeInfinity(Valor)) return "-?";
            //else if (Single.IsPositiveInfinity(Valor)) return "+?";
            //else if (Single.IsNaN(Valor)) return "?";
            if (float.IsInfinity(Valor) || float.IsNaN(Valor)) return "0";
            else return Valor > -1000f && Valor < 1000f ? Valor.ToString().Replace(Caracter_Coma_Decimal, ',') : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(double Valor)
        {
            //if (Double.IsNegativeInfinity(Valor)) return "-?";
            //else if (Double.IsPositiveInfinity(Valor)) return "+?";
            //else if (Double.IsNaN(Valor)) return "?";
            if (double.IsInfinity(Valor) || double.IsNaN(Valor)) return "0";
            else return Valor > -1000d && Valor < 1000d ? Valor.ToString().Replace(Caracter_Coma_Decimal, ',') : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(decimal Valor)
        {
            return Valor > -1000m && Valor < 1000m ? Valor.ToString().Replace(Caracter_Coma_Decimal, ',') : Traducir_Número(Valor.ToString());
        }

        internal static string Traducir_Número(string Texto)
        {
            Texto = Texto.Replace(Caracter_Coma_Decimal, ',').Replace(".", null);
            for (int Índice = !Texto.Contains(",") ? Texto.Length - 3 : Texto.IndexOf(',') - 3, Índice_Final = !Texto.StartsWith("-") ? 0 : 1; Índice > Índice_Final; Índice -= 3) Texto = Texto.Insert(Índice, ".");
            return Texto;
            /*Texto = Texto.Replace(Caracter_Coma_Decimal, ',');
            if (Texto.Contains(".")) Texto = Texto.Replace(".", null);
            int Índice = Texto.IndexOf(',');
            for (Índice = Índice < 0 ? Texto.Length - 3 : Índice - 3; Índice > (Texto[0] != '-' ? 0 : 1); Índice -= 3) Texto = Texto.Insert(Índice, ".");
            return Texto;*/
        }

        internal static string Traducir_Número_Decimales_Redondear(double Valor, int Decimales)
        {
            Valor = Math.Round(Valor, Decimales, MidpointRounding.AwayFromZero);
            string Texto = double.IsInfinity(Valor) || double.IsNaN(Valor) ? "0" : Valor > -1000d && Valor < 1000d ? Valor.ToString().Replace(Caracter_Coma_Decimal, ',') : Traducir_Número(Valor.ToString());
            if (Texto.Contains(",") == false) Texto += ',' + new string('0', Decimales);
            else
            {
                Decimales = Decimales - (Texto.Length - (Texto.IndexOf(',') + 1));
                if (Decimales > 0) Texto += new string('0', Decimales);
            }
            return Texto;
        }

        internal static string Traducir_Tamaño_Bytes_Automático(long Tamaño_Bytes, int Decimales, bool Decimales_Cero)
        {
            try
            {
                decimal Valor = (decimal)Tamaño_Bytes;
                int Índice = 0;
                for (; Índice < 7; Índice++)
                {
                    if (Valor < 1024m) break;
                    else Valor = Valor / 1024m;
                }
                string Texto = Traducir_Número(Math.Round(Valor, Decimales, MidpointRounding.AwayFromZero));
                if (Decimales_Cero)
                {
                    if (!Texto.Contains(Caracter_Coma_Decimal.ToString())) Texto += ',' + new string('0', Decimales);
                    else
                    {
                        Decimales = Decimales - (Texto.Length - (Texto.IndexOf(Caracter_Coma_Decimal) + 1));
                        if (Decimales > 0) Texto += new string('0', Decimales);
                    }
                }
                if (Índice == 0) Texto += Tamaño_Bytes == 1L ? " Byte" : " Bytes";
                else if (Índice == 1) Texto += " KB";
                else if (Índice == 2) Texto += " MB";
                else if (Índice == 3) Texto += " GB";
                else if (Índice == 4) Texto += " TB";
                else if (Índice == 5) Texto += " PB";
                else if (Índice == 6) Texto += " EB";
                return Texto;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
            return "? Bytes";
        }

        internal static class HSL
        {
            /// <summary>
            /// Convierte un color RGB en uno HSL.
            /// </summary>
            /// <param name="Rojo">Valor entre 0 y 255.</param>
            /// <param name="Verde">Valor entre 0 y 255.</param>
            /// <param name="Azul">Valor entre 0 y 255.</param>
            /// <param name="Matiz">Valor entre 0 y 360.</param>
            /// <param name="Saturación">Valor entre 0 y 100.</param>
            /// <param name="Luminosidad">Valor entre 0 y 100.</param>
            internal static void From_RGB(byte Rojo, byte Verde, byte Azul, out double Matiz, out double Saturación, out double Luminosidad)
            {
                Matiz = 0d;
                Saturación = 0d;
                Luminosidad = 0d;
                double Rojo_1 = Rojo / 255d;
                double Verde_1 = Verde / 255d;
                double Azul_1 = Azul / 255d;
                double Máximo, Mínimo, Diferencia;
                Máximo = Math.Max(Rojo_1, Math.Max(Verde_1, Azul_1));
                Mínimo = Math.Min(Rojo_1, Math.Min(Verde_1, Azul_1));
                Luminosidad = (Mínimo + Máximo) / 2d;
                if (Luminosidad <= 0d) return;
                Diferencia = Máximo - Mínimo;
                Saturación = Diferencia;
                if (Saturación > 0d) Saturación /= (Luminosidad <= 0.5d) ? (Máximo + Mínimo) : (2d - Máximo - Mínimo);
                else
                {
                    //Luminosidad = Math.Round(Luminosidad * 100d, 1, MidpointRounding.AwayFromZero);
                    Luminosidad *= Luminosidad * 100d;
                    return;
                }
                double Rojo_2 = (Máximo - Rojo_1) / Diferencia;
                double Verde_2 = (Máximo - Verde_1) / Diferencia;
                double Azul_2 = (Máximo - Azul_1) / Diferencia;
                if (Rojo_1 == Máximo) Matiz = (Verde_1 == Mínimo ? 5d + Azul_2 : 1d - Verde_2);
                else if (Verde_1 == Máximo) Matiz = (Azul_1 == Mínimo ? 1d + Rojo_2 : 3d - Azul_2);
                else Matiz = (Rojo_1 == Mínimo ? 3d + Verde_2 : 5d - Rojo_2);
                Matiz /= 6d;
                if (Matiz >= 1d) Matiz = 0d;
                Matiz *= 360d;
                Saturación *= 100d;
                Luminosidad *= 100d;
                //if (Matiz < 0d || Matiz >= 360d) MessageBox.Show("To Matiz", Matiz.ToString());
                //if (Saturación < 0d || Saturación > 100d) MessageBox.Show("To Saturación");
                //if (Luminosidad < 0d || Luminosidad > 100d) MessageBox.Show("To Luminosidad");
                //Matiz = Math.Round(Matiz * 360d, 1, MidpointRounding.AwayFromZero); // 0.0d ~ 360.0d
                //Saturación = Math.Round(Saturación * 100d, 1, MidpointRounding.AwayFromZero); // 0.0d ~ 100.0d
                //Luminosidad = Math.Round(Luminosidad * 100d, 1, MidpointRounding.AwayFromZero); // 0.0d ~ 100.0d
                //if (Matiz >= 360d) Matiz = 0d;
            }

            /// <summary>
            /// Convierte un color HSL en uno RGB.
            /// </summary>
            /// <param name="Matiz">Valor entre 0 y 360.</param>
            /// <param name="Saturación">Valor entre 0 y 100.</param>
            /// <param name="Luminosidad">Valor entre 0 y 100.</param>
            /// <param name="Rojo">Valor entre 0 y 255.</param>
            /// <param name="Verde">Valor entre 0 y 255.</param>
            /// <param name="Azul">Valor entre 0 y 255.</param>
            internal static void To_RGB(double Matiz, double Saturación, double Luminosidad, out byte Rojo, out byte Verde, out byte Azul)
            {
                if (Matiz >= 360d) Matiz = 0d;
                //Matiz = Math.Round(Matiz, 1, MidpointRounding.AwayFromZero);
                //Saturación = Math.Round(Saturación, 1, MidpointRounding.AwayFromZero);
                //Luminosidad = Math.Round(Luminosidad, 1, MidpointRounding.AwayFromZero);
                Matiz /= 360d; // 0.0d ~ 1.0d
                Saturación /= 100d; // 0.0d ~ 1.0d
                Luminosidad /= 100d; // 0.0d ~ 1.0d
                double Rojo_Temporal = Luminosidad; // Default to Gray
                double Verde_Temporal = Luminosidad;
                double Azul_Temporal = Luminosidad;
                double v = Luminosidad <= 0.5d ? (Luminosidad * (1d + Saturación)) : (Luminosidad + Saturación - Luminosidad * Saturación);
                if (v > 0d)
                {
                    double m, sv, Sextante, fract, vsf, mid1, mid2;
                    m = Luminosidad + Luminosidad - v;
                    sv = (v - m) / v;
                    Matiz *= 6d;
                    Sextante = Math.Floor(Matiz);
                    fract = Matiz - Sextante;
                    vsf = v * sv * fract;
                    mid1 = m + vsf;
                    mid2 = v - vsf;
                    if (Sextante == 0d)
                    {
                        Rojo_Temporal = v;
                        Verde_Temporal = mid1;
                        Azul_Temporal = m;
                    }
                    else if (Sextante == 1d)
                    {
                        Rojo_Temporal = mid2;
                        Verde_Temporal = v;
                        Azul_Temporal = m;
                    }
                    else if (Sextante == 2d)
                    {
                        Rojo_Temporal = m;
                        Verde_Temporal = v;
                        Azul_Temporal = mid1;
                    }
                    else if (Sextante == 3d)
                    {
                        Rojo_Temporal = m;
                        Verde_Temporal = mid2;
                        Azul_Temporal = v;
                    }
                    else if (Sextante == 4d)
                    {
                        Rojo_Temporal = mid1;
                        Verde_Temporal = m;
                        Azul_Temporal = v;
                    }
                    else if (Sextante == 5d)
                    {
                        Rojo_Temporal = v;
                        Verde_Temporal = m;
                        Azul_Temporal = mid2;
                    }
                }
                Rojo = (byte)Math.Round(Rojo_Temporal * 255d, MidpointRounding.AwayFromZero);
                Verde = (byte)Math.Round(Verde_Temporal * 255d, MidpointRounding.AwayFromZero);
                Azul = (byte)Math.Round(Azul_Temporal * 255d, MidpointRounding.AwayFromZero);
            }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Debugger.Iniciar_Depurador();
                try { Rendimiento_Procesador = new PerformanceCounter("Processor", "% Processor Time", "_Total", true); }
                catch { Rendimiento_Procesador = null; }
                Application.Run(new Form_Main());
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
        }
    }
}
