using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UnderMine_Tools
{
    internal static class Debugger
    {
        internal static bool Depurador_Iniciado = false;
        internal static FileStream Lector_Depurador = null;
        internal static BinaryWriter Escritor_Binario_Depurador = null;
        internal static BinaryReader Lector_Binario_Depurador = null;
        internal static long Depurador_Errores = 0L;
        internal static long Depurador_Errores_Únicos = 0L;

        /// <summary>
        /// The values of this array are used to calculate the CRC32 like WinRar or 7Zip do.
        /// </summary>
        internal static readonly uint[] Matriz_CRC32 = new uint[256]
        {
            0x00000000, 0x77073096, 0xEE0E612C, 0x990951BA,
            0x076DC419, 0x706AF48F, 0xE963A535, 0x9E6495A3,
            0x0EDB8832, 0x79DCB8A4, 0xE0D5E91E, 0x97D2D988,
            0x09B64C2B, 0x7EB17CBD, 0xE7B82D07, 0x90BF1D91,
            0x1DB71064, 0x6AB020F2, 0xF3B97148, 0x84BE41DE,
            0x1ADAD47D, 0x6DDDE4EB, 0xF4D4B551, 0x83D385C7,
            0x136C9856, 0x646BA8C0, 0xFD62F97A, 0x8A65C9EC,
            0x14015C4F, 0x63066CD9, 0xFA0F3D63, 0x8D080DF5,
            0x3B6E20C8, 0x4C69105E, 0xD56041E4, 0xA2677172,
            0x3C03E4D1, 0x4B04D447, 0xD20D85FD, 0xA50AB56B,
            0x35B5A8FA, 0x42B2986C, 0xDBBBC9D6, 0xACBCF940,
            0x32D86CE3, 0x45DF5C75, 0xDCD60DCF, 0xABD13D59,
            0x26D930AC, 0x51DE003A, 0xC8D75180, 0xBFD06116,
            0x21B4F4B5, 0x56B3C423, 0xCFBA9599, 0xB8BDA50F,
            0x2802B89E, 0x5F058808, 0xC60CD9B2, 0xB10BE924,
            0x2F6F7C87, 0x58684C11, 0xC1611DAB, 0xB6662D3D,
            0x76DC4190, 0x01DB7106, 0x98D220BC, 0xEFD5102A,
            0x71B18589, 0x06B6B51F, 0x9FBFE4A5, 0xE8B8D433,
            0x7807C9A2, 0x0F00F934, 0x9609A88E, 0xE10E9818,
            0x7F6A0DBB, 0x086D3D2D, 0x91646C97, 0xE6635C01,
            0x6B6B51F4, 0x1C6C6162, 0x856530D8, 0xF262004E,
            0x6C0695ED, 0x1B01A57B, 0x8208F4C1, 0xF50FC457,
            0x65B0D9C6, 0x12B7E950, 0x8BBEB8EA, 0xFCB9887C,
            0x62DD1DDF, 0x15DA2D49, 0x8CD37CF3, 0xFBD44C65,
            0x4DB26158, 0x3AB551CE, 0xA3BC0074, 0xD4BB30E2,
            0x4ADFA541, 0x3DD895D7, 0xA4D1C46D, 0xD3D6F4FB,
            0x4369E96A, 0x346ED9FC, 0xAD678846, 0xDA60B8D0,
            0x44042D73, 0x33031DE5, 0xAA0A4C5F, 0xDD0D7CC9,
            0x5005713C, 0x270241AA, 0xBE0B1010, 0xC90C2086,
            0x5768B525, 0x206F85B3, 0xB966D409, 0xCE61E49F,
            0x5EDEF90E, 0x29D9C998, 0xB0D09822, 0xC7D7A8B4,
            0x59B33D17, 0x2EB40D81, 0xB7BD5C3B, 0xC0BA6CAD,
            0xEDB88320, 0x9ABFB3B6, 0x03B6E20C, 0x74B1D29A,
            0xEAD54739, 0x9DD277AF, 0x04DB2615, 0x73DC1683,
            0xE3630B12, 0x94643B84, 0x0D6D6A3E, 0x7A6A5AA8,
            0xE40ECF0B, 0x9309FF9D, 0x0A00AE27, 0x7D079EB1,
            0xF00F9344, 0x8708A3D2, 0x1E01F268, 0x6906C2FE,
            0xF762575D, 0x806567CB, 0x196C3671, 0x6E6B06E7,
            0xFED41B76, 0x89D32BE0, 0x10DA7A5A, 0x67DD4ACC,
            0xF9B9DF6F, 0x8EBEEFF9, 0x17B7BE43, 0x60B08ED5,
            0xD6D6A3E8, 0xA1D1937E, 0x38D8C2C4, 0x4FDFF252,
            0xD1BB67F1, 0xA6BC5767, 0x3FB506DD, 0x48B2364B,
            0xD80D2BDA, 0xAF0A1B4C, 0x36034AF6, 0x41047A60,
            0xDF60EFC3, 0xA867DF55, 0x316E8EEF, 0x4669BE79,
            0xCB61B38C, 0xBC66831A, 0x256FD2A0, 0x5268E236,
            0xCC0C7795, 0xBB0B4703, 0x220216B9, 0x5505262F,
            0xC5BA3BBE, 0xB2BD0B28, 0x2BB45A92, 0x5CB36A04,
            0xC2D7FFA7, 0xB5D0CF31, 0x2CD99E8B, 0x5BDEAE1D,
            0x9B64C2B0, 0xEC63F226, 0x756AA39C, 0x026D930A,
            0x9C0906A9, 0xEB0E363F, 0x72076785, 0x05005713,
            0x95BF4A82, 0xE2B87A14, 0x7BB12BAE, 0x0CB61B38,
            0x92D28E9B, 0xE5D5BE0D, 0x7CDCEFB7, 0x0BDBDF21,
            0x86D3D2D4, 0xF1D4E242, 0x68DDB3F8, 0x1FDA836E,
            0x81BE16CD, 0xF6B9265B, 0x6FB077E1, 0x18B74777,
            0x88085AE6, 0xFF0F6A70, 0x66063BCA, 0x11010B5C,
            0x8F659EFF, 0xF862AE69, 0x616BFFD3, 0x166CCF45,
            0xA00AE278, 0xD70DD2EE, 0x4E048354, 0x3903B3C2,
            0xA7672661, 0xD06016F7, 0x4969474D, 0x3E6E77DB,
            0xAED16A4A, 0xD9D65ADC, 0x40DF0B66, 0x37D83BF0,
            0xA9BCAE53, 0xDEBB9EC5, 0x47B2CF7F, 0x30B5FFE9,
            0xBDBDF21C, 0xCABAC28A, 0x53B39330, 0x24B4A3A6,
            0xBAD03605, 0xCDD70693, 0x54DE5729, 0x23D967BF,
            0xB3667A2E, 0xC4614AB8, 0x5D681B02, 0x2A6F2B94,
            0xB40BBE37, 0xC30C8EA1, 0x5A05DF1B, 0x2D02EF8D
        };

        /// <summary>
        /// Calculates the CRC32 from the specified byte array.
        /// </summary>
        internal static uint Calcular_CRC32(byte[] Matriz_Bytes)
        {
            if (Matriz_Bytes == null || Matriz_Bytes.Length <= 0) return 0; // If the array is null or empty return 0.
            uint CRC_32_Bits = 0xFFFFFFFF; // Start at the maximum possible value.
            for (int Índice = 0; Índice < Matriz_Bytes.Length; Índice++) CRC_32_Bits = Matriz_CRC32[(byte)(CRC_32_Bits ^ Matriz_Bytes[Índice])] ^ (CRC_32_Bits >> 8); // For each byte in the array keep calculating it's CRC32.
            return ~CRC_32_Bits; // Return the calculated value with it's bits inverted.
        }

        /// <summary>
        /// Starts the necessary resources to register and store any exception that might occur while running the application.
        /// </summary>
        internal static void Iniciar_Depurador()
        {
            try
            {
                Lector_Depurador = new FileStream(Application.StartupPath + "\\Debugger", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                Lector_Depurador.Seek(0L, SeekOrigin.Begin);
                Lector_Depurador.Write(new byte[8] { (byte)'J', (byte)'U', (byte)'P', (byte)'I', (byte)'S', (byte)'O', (byte)'F', (byte)'T' }, 0, 8); // Always write "JUPISOFT" at the start of the file.
                Lector_Depurador.Flush();
                Lector_Depurador.Seek(8L, SeekOrigin.Begin);
                Escritor_Binario_Depurador = new BinaryWriter(Lector_Depurador, Encoding.ASCII);
                Lector_Binario_Depurador = new BinaryReader(Lector_Depurador, Encoding.ASCII);
                for (long Índice = 8L; Índice < Lector_Depurador.Length;)
                {
                    Lector_Binario_Depurador.ReadBytes(4); // CRC-32 of the message.
                    Lector_Binario_Depurador.ReadBytes(8); // First date.
                    Lector_Binario_Depurador.ReadBytes(8); // Last date.
                    Depurador_Errores += Lector_Binario_Depurador.ReadInt64(); // Times it has happened.
                    int Longitud = Lector_Binario_Depurador.ReadInt32(); // Message length.
                    Lector_Binario_Depurador.ReadBytes(Longitud); // Message.
                    Depurador_Errores_Únicos++;
                    Índice += 32 + Longitud;
                }
                Lector_Depurador.Seek(Lector_Depurador.Length, SeekOrigin.Begin);
                Depurador_Iniciado = true;
                return;
            }
            catch (Exception Excepción)
            {
                try
                {
                    Lector_Depurador = null;
                    Escritor_Binario_Depurador = null;
                    Lector_Binario_Depurador = null;
                }
                catch { }
                try { MessageBox.Show(Excepción.ToString(), Program.Texto_Título, MessageBoxButtons.OK, MessageBoxIcon.Error); }
                catch { }
                MessageBox.Show("The application won't be able to register any error.\r\nIf it's located at a read only folder or an administrator privileges only folder, please exit the application, move or copy it's folder to another valid location, and restart it from there.", Program.Texto_Título, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Depurador_Iniciado = false;
        }

        /// <summary>
        /// Writes an exception message inside the "Debugger" file, along with it's date and CRC32 to save disk space. Note: calling "Exception.ToString()" inside this function, while easier, might return a different exception message, so to keep the source code line where the exception happened, it's recommended to use the default calling, which might also fail if the option "Optimize code" is enabled in Microsoft Visual Studio, under the "Project" menu, in "Properties", and then in the tab "Build".
        /// </summary>
        /// <param name="Mensaje">Any text message</param>
        internal static void Escribir_Excepción(string Mensaje)
        {
            try
            {
                if (Depurador_Iniciado)
                {
                    //if (Emitir_Sonido) SystemSounds.Hand.Play();
                    if (string.IsNullOrEmpty(Mensaje)) Mensaje = "Unknown error.";
                    uint CRC32 = 0;
                    long Fecha = 0L;
                    byte[] Matriz_Bytes = null;
                    try { Fecha = DateTime.Now.ToBinary(); }
                    catch { Fecha = 0L; }
                    try { Matriz_Bytes = Encoding.Unicode.GetBytes(Mensaje); }
                    catch { Matriz_Bytes = new byte[] { 69, 114, 114, 111, 114, 32, 100, 101, 115, 99, 111, 110, 111, 99, 105, 100, 111, 46 }; } // Error desconocido.
                    try { CRC32 = Calcular_CRC32(Matriz_Bytes); }
                    catch { CRC32 = 0; }
                    if (Lector_Depurador != null && Escritor_Binario_Depurador != null && Lector_Binario_Depurador != null)
                    {
                        bool Mensaje_Repetido = false;
                        Lector_Depurador.Seek(8L, SeekOrigin.Begin);
                        for (long Índice = 8L; Índice < Lector_Depurador.Length;)
                        {
                            uint CRC32_Temporal = Lector_Binario_Depurador.ReadUInt32(); // CRC-32 of the message.
                            Lector_Binario_Depurador.ReadBytes(8); // First date.
                            if (CRC32_Temporal == CRC32)
                            {
                                Mensaje_Repetido = true;
                                break;
                            }
                            Lector_Binario_Depurador.ReadBytes(8); // Last date.
                            Lector_Binario_Depurador.ReadBytes(8); // Times it has happened.
                            int Longitud = Lector_Binario_Depurador.ReadInt32(); // Message length.
                            Lector_Binario_Depurador.ReadBytes(Longitud); // Message.
                            Índice += 32 + Longitud;
                        }
                        if (!Mensaje_Repetido)
                        {
                            Escritor_Binario_Depurador.Write(CRC32);
                            Escritor_Binario_Depurador.Write(Fecha);
                            Escritor_Binario_Depurador.Write(Fecha);
                            Escritor_Binario_Depurador.Write(1L);
                            Escritor_Binario_Depurador.Write(Matriz_Bytes.Length);
                            Escritor_Binario_Depurador.Write(Matriz_Bytes);
                            Depurador_Errores_Únicos++;
                        }
                        else
                        {
                            Escritor_Binario_Depurador.Write(Fecha); // Last date.
                            long Repeticiones = Lector_Binario_Depurador.ReadInt64() + 1L; // Times it has happened.
                            Lector_Depurador.Seek(Lector_Depurador.Position - 8L, SeekOrigin.Begin);
                            Escritor_Binario_Depurador.Write(Repeticiones); // Times it has happened + 1.
                        }
                        Depurador_Errores++;
                        Escritor_Binario_Depurador.Flush();
                        Lector_Depurador.Seek(Lector_Depurador.Length, SeekOrigin.Begin);
                    }
                    else Depurador_Errores++;
                }
            }
            catch (Exception Excepción) { MessageBox.Show(Excepción.ToString(), Program.Texto_Título, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
