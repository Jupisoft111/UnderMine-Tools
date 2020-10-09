using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UnderMine_Tools
{
    internal static class PInvoke
    {
        internal static class Shell32
        {
            /// <summary>
            /// this enum used on conjunction with getobjectproperties
            /// </summary>
            public enum SHObjectPropertiesFlags
            {
                PrinterName = 1, // lpObject points to a printer friendly name
                FilePath = 2, // lpObject points to a fully qualified path + file name
                VolumeGuid = 4 // lpObject points to a Volume GUID
            }

            /// <summary>
            /// This function invokes the Properties context menu command on a Shell object.
            /// Ejemplo: "CallPropDialog(this.Handle, GetProperties.SHOP_FILEPATH, Ruta, null)";
            /// </summary>
            /// <param name="hwnd">[in] The HWND of the window that will be the parent of the dialog box.</param>
            /// <param name="dwType">enum to what to call</param>
            /// <param name="szObject">[in] A NULL-terminated Unicode string that contains the object name.
            /// The contents of the string are determinated by which of
            /// the first three flags are set in dwType.</param>
            /// <param name="szPage">[in] A NULL-terminated Unicode string that contains the name of
            /// the property sheet page to be initally opened.
            /// Set this parameter to NULL to specifiy the default page.
            /// General, Summary, Security, etc. Pero en Español: "Resumen" (09-01-2010)...</param>
            /// <returns>Returns TRUE if the Properties command is successfully invoked, or FALSE otherwise.</returns>
            [DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            internal static extern bool SHObjectProperties(IntPtr Handle_Ventana, SHObjectPropertiesFlags Flags, string Ruta, string Página);
        }
    }
}
