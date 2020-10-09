using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UnderMine_Tools.Properties;

namespace UnderMine_Tools
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Array of the first part of a name for a male peasant.
        /// List decoded by Jupisoft from the file "PeonNames-resources.assets-3249".
        /// </summary>
        internal static readonly string[] Matriz_Nombres_Hombre_Inicio = new string[]
        {
            "Althal", "Arth", "Ash", "Baggle", "Bar", "Ben", "Berin", "Bor",
            "Brom", "Bry", "Car", "Cass", "Ced", "Clif", "Dain", "Destr",
            "Don", "Dor", "Ear", "Fav", "Fen", "Fend", "Forth", "Fran",
            "Freder", "Gav", "Gav", "Geoff", "Gorv", "Greg", "Had", "Hen",
            "Jansh", "Jar", "Jos", "Jose", "Just", "Leo", "Lethold", "Lief",
            "Mer", "Oliv", "Pet", "Peyt", "Quin", "Rob", "Ronal", "Row",
            "Rulf", "Sad", "Sim", "Terr", "Terro", "Thom", "Tris", "Tyb",
            "Ul", "Wal", "Walt", "Will", "Xal", "Zan"
        };

        /// <summary>
        /// Array of the last part of a name for a male peasant.
        /// List decoded by Jupisoft from the file "PeonNames-resources.assets-3249".
        /// </summary>
        internal static readonly string[] Matriz_Nombres_Hombre_Fin = new string[]
        {
            "an", "ard", "arke", "blood", "cher", "den", "down", "eb",
            "ence", "es", "frey", "ish", "ker", "ker", "man", "olfe",
            "ood", "ory", "owne", "ring", "tan", "ter", "ter", "ver",
            "y", "ylor", "yne"
        };

        /// <summary>
        /// Array of the first part of a name for a female peasant.
        /// List decoded by Jupisoft from the file "PeonNames-resources.assets-3249".
        /// </summary>
        internal static readonly string[] Matriz_Nombres_Mujer_Inicio = new string[]
        {
            "Aman", "An", "Ari", "Au", "Bern", "Bri", "Can", "Car",
            "Caro", "Ela", "Ele", "Eliz", "Em", "Em", "En", "Ev",
            "Gabri", "Gi", "Hen", "Jan", "Jas", "Jess", "Jos", "Julli",
            "Kay", "Ken", "Keri", "Kins", "Kris", "La", "Lay", "Le",
            "Li", "Liz", "Lu", "Lu", "Ma", "Mad", "Mor", "Nao",
            "Oli", "Penel", "Ri", "Ru", "Sar", "Ser", "Sky", "So",
            "Sor", "Vio", "Wan", "Wen"
        };

        /// <summary>
        /// Array of the last part of a name for a female peasant.
        /// List decoded by Jupisoft from the file "PeonNames-resources.assets-3249".
        /// </summary>
        internal static readonly string[] Matriz_Nombres_Mujer_Fin = new string[]
        {
            "a", "ah", "an", "anna", "beth", "by", "da", "dice",
            "drey", "dy", "eline", "ella", "elle", "en", "ette", "gan",
            "ica", "ice", "la", "lee", "let", "ley", "lia", "line",
            "lyn", "ma", "mi", "na", "ne", "ope", "phia", "phie",
            "phine", "ta", "via", "ya"
        };

        /// <summary>
        /// Array of the first random part of a peasant name.
        /// Based on this wiki page: https://undermine.gamepedia.com/Peasant_Names.
        /// But after further research, the current version of the game can give more names.
        /// </summary>
        [Obsolete]
        internal static readonly string[] Matriz_Nombres_Aleatorios_Inicio = new string[61]
        {
            "Althal", "Arth", "Ash", "Baggle", "Bar", "Ben", "Berin", "Bor",
            "Brom", "Bry", "Car", "Cass", "Cedr", "Clif", "Dain", "Destr",
            "Dor", "Don", "Ear", "Fav", "Fen", "Fend", "Forth", "Fran",
            "Freder", "Gav", "Geoff", "Gorv", "Greg", "Had", "Hen", "Jansh",
            "Jar", "Jos", "Jose", "Just", "Leo", "Lethold", "Lief", "Mer",
            "Oliv", "Peyt", "Pet", "Quin", "Rob", "Ronal", "Row", "Rulf",
            "Sad", "Sim", "Terr", "Terro", "Thom", "Trist", "Tyb", "Ul",
            "Walt", "Will", "Wal", "Xal", "Zan"
        };

        /// <summary>
        /// Array of the last random part of a peasant name.
        /// Based on this wiki page: https://undermine.gamepedia.com/Peasant_Names.
        /// But after further research, the current version of the game can give more names.
        /// </summary>
        [Obsolete]
        internal static readonly string[] Matriz_Nombres_Aleatorios_Fin = new string[22]
        {
            "ard", "arke", "blood", "cher", "den", "down", "ebb", "es",
            "frey", "ish", "ker", "man", "nett", "olfe", "ood", "ory",
            "owne", "ring", "ter", "ver", "ylor", "yne"
        };

        // Code discovered in the UnderMine Wiki after decoding by myself all the colors.
        // Untested and otdated.
        // https://undermine.gamepedia.com/Module:Profile
        /*local skin_range = 2^9
        local eye_range = 2^16
        local hat_colors = {
            {light = "#394552", dark = "#212029"},
            {light = "#313031", dark = "#181818"},
            {light = "#5A555A", dark = "#292829"},
            {light = "#523C31", dark = "#312021"},
            {light = "#5A4D39", dark = "#392821"},
            {light = "#737984", dark = "#394142"},
            {light = "#4A5542", dark = "#212821"},
            {light = "#523442", dark = "#291C29"},
            {light = "#B55921", dark = "#7B3018"},
            {light = "#5A1C10", dark = "#310C08"},
            {light = "#B58239", dark = "#734918"},
            {light = "#395952", dark = "#293839"},
            {light = "#485643", dark = "#CDCDCD"}
        }
        local skin_colors = {
            {light = "#945531", dark = "#632C18"},
            {light = "#BD7942", dark = "#8C4521"},
            {light = "#E7B28C", dark = "#D67D52"},
            {light = "#DD9B5D", dark = "#B65B2A"}
        }
        local eye_colors = {
            "#298493",
            "#781F14",
            "#4B5C5F",
            "#359200",
            "#8C2992",
            "#552E8A",
            "#A3CBD1",
            "#718D92"
        }
        local p = {}
        function p.run(code)
            local eye = math.floor(code/eye_range)
            code=code-eye_range*eye
            local skin = math.floor(code/skin_range)
            code=code-skin_range*skin
            local hat=code
            return hat_colors[hat+1].light, hat_colors[hat+1].dark, skin_colors[skin+1].light, skin_colors[skin+1].dark, eye_colors[eye+1]
        end
        return p*/

        internal static readonly Color[] Matriz_Colores_Eyes = new Color[9]
        {
            Color.FromArgb(255, 41, 132, 147),
            Color.FromArgb(255, 120, 31, 20),
            Color.FromArgb(255, 75, 92, 95),
            Color.FromArgb(255, 53, 146, 0),
            Color.FromArgb(255, 140, 41, 146),
            Color.FromArgb(255, 85, 46, 138),
            Color.FromArgb(255, 163, 203, 209),
            Color.FromArgb(255, 113, 141, 146),
            Color.FromArgb(255, 205, 205, 205) //Color.FromArgb(255, 41, 132, 147)
        };

        /// <summary>
        /// Array that holds all the known hair colors in UnderMine.
        /// The five (most left) bits seems to turn the mouth white.
        /// The 8th bit seems to alternate between 2 hair colors.
        /// The 7th bit seems alternate between 2 skin colors.
        /// The 6th bit seems to activate even brighter base colors.
        /// </summary>
        internal static readonly Color[] Matriz_Colores_Hair = new Color[9]
        {
            Color.FromArgb(255, 82, 44, 41),
            Color.FromArgb(255, 49, 44, 41),
            Color.FromArgb(255, 82, 44, 24),
            Color.FromArgb(255, 49, 44, 41),
            Color.FromArgb(255, 181, 81, 33),
            Color.FromArgb(255, 82, 44, 41),
            Color.FromArgb(255, 115, 69, 24),
            Color.FromArgb(255, 173, 113, 41),
            Color.FromArgb(255, 205, 205, 205) //Color.FromArgb(255, 112, 68, 26). // Overflowed white mouth.
        };

        internal static readonly Color[] Matriz_Colores_Skin = new Color[]
        {
            Color.FromArgb(255, 148, 85, 49),
            //Color.FromArgb(255, 148, 85, 49),
            Color.FromArgb(255, 189, 121, 66),
            //Color.FromArgb(255, 189, 121, 66),
            Color.FromArgb(255, 231, 178, 140),
            //Color.FromArgb(255, 231, 178, 140),
            Color.FromArgb(255, 224, 155, 93),
            //Color.FromArgb(255, 224, 155, 93),
            Color.FromArgb(255, 205, 205, 205) //Color.FromArgb(255, 224, 155, 93) // Overflowed white mouth.
        };

        internal static readonly Color[] Matriz_Colores_Clothes = new Color[13]
        {
            Color.FromArgb(255, 57, 69, 82),
            Color.FromArgb(255, 49, 48, 49),
            Color.FromArgb(255, 90, 85, 90),
            Color.FromArgb(255, 82, 60, 49),
            Color.FromArgb(255, 90, 77, 57),
            Color.FromArgb(255, 115, 121, 132),
            Color.FromArgb(255, 74, 85, 66),
            Color.FromArgb(255, 82, 52, 66),
            Color.FromArgb(255, 181, 89, 33),
            Color.FromArgb(255, 90, 28, 16),
            Color.FromArgb(255, 181, 130, 57),
            Color.FromArgb(255, 57, 89, 82),
            Color.FromArgb(255, 205, 205, 205) // Color.FromArgb(255, 72, 86, 67). // Overflowed white clothes.
        };

        /// <summary>
        /// Sorter of status effects based on names.
        /// </summary>
        internal class Comparador_Efectos : IComparer<Efectos>
        {
            public int Compare(Efectos X, Efectos Y)
            {
                int Comparación = string.Compare(X.Tipo.ToString(), Y.Tipo.ToString(), false);
                if (Comparación != 0) return Comparación;
                else return string.Compare(X.Nombre, Y.Nombre, false);
            }
        }

        /// <summary>
        /// Sorter of familiars based on names.
        /// </summary>
        internal class Comparador_Familiares : IComparer<Familiares>
        {
            public int Compare(Familiares X, Familiares Y)
            {
                return string.Compare(X.Nombre, Y.Nombre, false);
            }
        }

        /// <summary>
        /// Sorter of unique bomb upgrades based on names.
        /// </summary>
        internal class Comparador_Mejoras_Bomba : IComparer<Mejoras_Bomba>
        {
            public int Compare(Mejoras_Bomba X, Mejoras_Bomba Y)
            {
                return string.Compare(X.Nombre, Y.Nombre, false);
            }
        }

        /// <summary>
        /// Sorter of unique gloves upgrades based on names.
        /// </summary>
        internal class Comparador_Mejoras_Guantes : IComparer<Mejoras_Guantes>
        {
            public int Compare(Mejoras_Guantes X, Mejoras_Guantes Y)
            {
                return string.Compare(X.Nombre, Y.Nombre, false);
            }
        }

        /// <summary>
        /// Sorter of unique weapon upgrades based on names.
        /// </summary>
        internal class Comparador_Mejoras_Arma : IComparer<Mejoras_Arma>
        {
            public int Compare(Mejoras_Arma X, Mejoras_Arma Y)
            {
                return string.Compare(X.Nombre, Y.Nombre, false);
            }
        }

        /// <summary>
        /// Sorter of potions based on names.
        /// </summary>
        internal class Comparador_Pociones : IComparer<Pociones>
        {
            public int Compare(Pociones X, Pociones Y)
            {
                return string.Compare(X.Nombre, Y.Nombre, false);
            }
        }

        /// <summary>
        /// Structure that holds up all the information about an artifact.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Artefactos
        {
            internal Bitmap Imagen;
            internal string Id;
            internal string Nombre;
            internal string Descripción;

            internal Artefactos(Bitmap Imagen,
                string Id,
                string Nombre,
                string Descripción)
            {
                this.Imagen = Imagen;
                this.Id = Id;
                this.Nombre = Nombre;
                this.Descripción = Descripción;
            }

            internal static int Buscar_Índice(string ID)
            {
                try
                {
                    if (ID != null)
                    {
                        for (int Índice = 0; Índice < Matriz.Length; Índice++)
                        {
                            try
                            {
                                if (Matriz[Índice].Id != null &&
                                    string.Compare(ID, Matriz[Índice].Id, true) == 0)
                                {
                                    return Índice;
                                }
                            }
                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                        }
                    }
                }
                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                return 0;
            }

            internal static Artefactos[] Matriz = new Artefactos[]
            {
                new Artefactos(Resources.Artifact_Journal, "038c0066e235445a84153e8c9c450522", "Journal", "An enchanted journal"),
                new Artefactos(Resources.Artifact_Selt_s_Fragment, "038c0066e235445a84153e8c9c450522", "Selt's Fragment", "A stone fragment depicting the Sand Queen"),
                new Artefactos(Resources.Artifact_Mortar_s_Fragment, "ff7bebf7fd3c4504922e69d4573c9268", "Mortar's Fragment", "A stone fragment depicting the Stone Lord"),
                new Artefactos(Resources.Artifact_Noori_s_Fragment, "4201016017924b249dace995484afd57", "Noori's Fragment", "A stone fragment depicting the Shadow Lord"),
                new Artefactos(Resources.Artifact_Ponzu_s_Fragment, "a408291029c24aa3b723842af215836d", "Ponzu's Fragment", "A stone fragment depicting the Crystal Lord"),
                new Artefactos(Resources.Artifact_Seer_s_Fragment, "65d587c6a62c4908affda8651a72a5de", "Seer's Fragment", "A stone fragment depicting the Fire Queen"),

                new Artefactos(Resources.Artifact_Prisoner_Key, "b70e6c76523f42c7a0f2394fe8b015ef", "Prisoner Key", "A simple key, covered in dirt"),
                new Artefactos(Resources.Artifact_Library_Key, "3a2eea11831e483587794f82632a4da5", "Library Key", "A key to Auduin's library"),
                new Artefactos(Resources.Artifact_Dungeon_Key, "838f688c94ff4655abae5a22b2d293f9", "Dungeon Key", "A key to the Delvemore Dungeon"),
                new Artefactos(Resources.Artifact_Master_s_Key, "01cd8d8f74ec41b5923238097784ec3d", "Master's Key", "A key to a hidden prison cell"),
                new Artefactos(Resources.Artifact_Key_to_the_Halls, "e253e01a1b514aeb89de7976b4d28faa", "Key to the Halls", "A key fashioned from bones"),
                new Artefactos(Resources.Artifact_Keystone, "2d577bfdd17e414b98e23e1a399e19c5", "Keystone", "A smooth stone resembling a key"),

                new Artefactos(Resources.Artifact_Core_Key, "2061bb05fb0f4e388a0f2ffde2cfd781", "Core Key", "A key forged from molten rock"),
                new Artefactos(Resources.Artifact_Nightshade, "fdb82b8b71314508a5f74f923691f1c5", "Nightshade", "An evening fungus"),
                new Artefactos(Resources.Artifact_Shiitake, "7d08db7563ff4dc09c68202222e3caa7", "Shiitake", "The spawn of Matsutake"),
                new Artefactos(Resources.Artifact_Blastcap, "4c14bf5fc0054790a90bc0e0c60948d7", "Blastcap", "A spicy mushroom with a kick"),
                new Artefactos(Resources.Artifact_Dusty_Book, "b1b125655e2c49d0bb911b5b85144918", "Dusty Book", "A well worn and dirty book"),
                new Artefactos(Resources.Artifact_Dungeon_Map, "427ec4504c1f460696ced20c4fe48152", "Dungeon Map", "A map to the Delvemore Dungeon"),

                new Artefactos(Resources.Artifact_Halls_Map, "f251ba3c7e1b467b857d04b1135812a4", "Halls Map", "A map to the Halls of Din"),
                new Artefactos(Resources.Artifact_Caverns_Map, "ee77be7cfb224130a3874ebc9393ed3e", "Caverns Map", "A map to the Shimmering Caverns"),
                new Artefactos(Resources.Artifact_Core_Map, "ef252f4fa97a46ecbb7f85ec341df560", "Core Map", "A map to the Golden Core"),
                new Artefactos(Resources.Artifact_Summoning_Stone, "f8986aae9dc240acbd1a8625f91356e7", "Summoning Stone", "Resets and strengthens the denizens of the Undermine"),
                new Artefactos(Resources.Artifact_Stone_of_the_Recreant, "fc644210617d4cdead7023f82aeca359", "Stone of the Recreant", "Sets one back at the beginning"),
            };
        }

        /// <summary>
        /// Structure that holds up all the information about a familiar.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Familiares
        {
            internal Bitmap Imagen;
            internal string Id;
            internal int Xp;
            internal string Nombre;
            internal string Descripción;

            internal Familiares(Bitmap Imagen,
                string Id,
                int Xp,
                string Nombre,
                string Descripción)
            {
                this.Imagen = Imagen;
                this.Id = Id;
                this.Xp = Xp;
                this.Nombre = Nombre;
                this.Descripción = Descripción;
            }

            internal static int Buscar_Índice(string ID)
            {
                try
                {
                    if (ID != null)
                    {
                        for (int Índice = 0; Índice < Matriz.Length; Índice++)
                        {
                            try
                            {
                                if (Matriz[Índice].Id != null &&
                                    string.Compare(ID, Matriz[Índice].Id, true) == 0)
                                {
                                    return Índice + 1;
                                }
                            }
                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                        }
                    }
                }
                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                return 0;
            }

            internal static Familiares[] Matriz = new Familiares[]
            {
                new Familiares(Resources.Familiar_Canary, "4f2ebf67c2ab4103bba07ae33f8fce10", 800, "Canary", "A miner's best friend"),
                new Familiares(Resources.Familiar_Djinn, "42c02406d29049eba350c408a44bd699", 1100, "Djinn", "A cunning ally that sees through deception"),
                new Familiares(Resources.Familiar_Firebird, "31d2663fef3248c8af2d6727453a83ef", 700, "Firebird", "A hot tempered bird"),
                new Familiares(Resources.Familiar_Nikko, "e6bc0264d4e14cdc866c93bfe318195e", 700, "Nikko", "A hard punching apeian"),
                new Familiares(Resources.Familiar_Sol_s_Phoenix, "f85e0301401b414b811f17c6a84506d2", 1300, "Sol's Phoenix", "A guardian of the weak"),
                new Familiares(Resources.Familiar_Sylph, "966adf72347c4f35bf9f3789d6b7ee18", 1600, "Sylph", "A blessed creature"),
                new Familiares(Resources.Familiar_Spirit, "25cb114d9617483a8be3c9c7ddb8ee39", 1500, "Spirit", "Aged and fermented for eons"),
                new Familiares(Resources.Familiar_Thunderbird, "839ad2905954481cb294f4b6cd01ff34", 700, "Thunderbird", "A shockingly short tempered bird"),
                new Familiares(Resources.Familiar_Chaos_Spawn, "9d1c4ac6c24f4dfdb29a48457c1c49e6", 520, "Chaos Spawn", "A child of the god Arengee"),
                new Familiares(Resources.Familiar_Eidolon, "97b65bd7fb0e420e87e88345312624c5", 700, "Eidolon", "A haunted suit of armor"),
                new Familiares(Resources.Familiar_Lesser_Demon, "157bcafd3635443485e1676d182197b9", 800, "Lesser Demon", "Looks up to greater demons")
            };
        }

        /// <summary>
        /// Structure that holds up all the information about a unique bomb upgrade.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Mejoras_Bomba
        {
            internal Bitmap Imagen;
            internal string Id;
            internal string Nombre;
            internal string Descripción;

            internal Mejoras_Bomba(Bitmap Imagen,
                string Id,
                string Nombre,
                string Descripción)
            {
                this.Imagen = Imagen;
                this.Id = Id;
                this.Nombre = Nombre;
                this.Descripción = Descripción;
            }

            internal static int Buscar_Índice(string ID)
            {
                try
                {
                    if (ID != null)
                    {
                        for (int Índice = 0; Índice < Matriz.Length; Índice++)
                        {
                            try
                            {
                                if (Matriz[Índice].Id != null &&
                                    string.Compare(ID, Matriz[Índice].Id, true) == 0)
                                {
                                    return Índice;
                                }
                            }
                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                        }
                    }
                }
                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                return 0;
            }

            internal static Mejoras_Bomba[] Matriz = new Mejoras_Bomba[]
            {
                new Mejoras_Bomba(Resources.Bombs, "d7ea51b97c4d410e86eb87bb594976c0", "Bomb", "Deals damage and destroys rocks as well as other objects"),
                new Mejoras_Bomba(Resources.Bomb_Upgrade_Bishop_s_Bomb, "833d6ac4c9df415bb85875b32d189a6a", "Bishop's Bomb", "Death at forty five degrees"),
                new Mejoras_Bomba(Resources.Bomb_Upgrade_Blood_Bomb, "64bd42db7c7e4267b8107d4d022d2e69", "Blood Bomb", "Leeches life from enemies slain by bombs"),
                new Mejoras_Bomba(Resources.Bomb_Upgrade_Bombushka, "b013d56f64d24c91acbc136781b8ccb4", "Bombushka", "Bombs, in bombs, in bombs"),
                new Mejoras_Bomba(Resources.Bomb_Upgrade_Branding_Bomb, "5f30d1c616bb4dcfa6754e8f60437992", "Branding Bomb", "Bombs brand enemies for sacrifice, +10 bombs"),
                new Mejoras_Bomba(Resources.Bomb_Upgrade_Golden_Powder, "34a0fc86fcb6436d970b1e15f67d6b68", "Golden Powder", "Turns rock into gold"),
                new Mejoras_Bomba(Resources.Bomb_Upgrade_Lightning_Bomb, "b218393482d64155861953f95b8667f6", "Lightning Bomb", "Bomb explosions chain lightning"),
                new Mejoras_Bomba(Resources.Bomb_Upgrade_Queen_s_Bomb, "1cd16e5483c547ac924ca9482fedadbf", "Queen's Bomb", "Death in all directions"),
                new Mejoras_Bomba(Resources.Bomb_Upgrade_Rook_s_Bomb, "354c2f284b3f4420acdcdcb73e759168", "Rook's Bomb", "Death at ninety degrees"),
                new Mejoras_Bomba(Resources.Bomb_Upgrade_Seer_s_Blood, "d8541923ed754682bedfe3bd90df18b2", "Seer's Blood", "Bombs explode in a shower of fire"),
                new Mejoras_Bomba(Resources.Bomb_Upgrade_Transmutagen_Blast, "ae90e4f32247408fa170c85404944492", "Transmutagen Blast", "Transforms items"),
                new Mejoras_Bomba(Resources.Bomb_Upgrade_Tsar_Bomba, "d88203223f544ea6bfbdbbd16d4cb7e8", "Tsar Bomba", "Killing enemies with a bomb spawns a new bomb. Carried bombs decrease swing and throw damage."),
            };
        }

        /// <summary>
        /// Structure that holds up all the information about a unique gloves upgrade.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Mejoras_Guantes
        {
            internal Bitmap Imagen;
            internal string Id;
            internal string Nombre;
            internal string Descripción;

            internal Mejoras_Guantes(Bitmap Imagen,
                string Id,
                string Nombre,
                string Descripción)
            {
                this.Imagen = Imagen;
                this.Id = Id;
                this.Nombre = Nombre;
                this.Descripción = Descripción;
            }

            internal static int Buscar_Índice(string ID)
            {
                try
                {
                    if (ID != null)
                    {
                        for (int Índice = 0; Índice < Matriz.Length; Índice++)
                        {
                            try
                            {
                                if (Matriz[Índice].Id != null &&
                                    string.Compare(ID, Matriz[Índice].Id, true) == 0)
                                {
                                    return Índice;
                                }
                            }
                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                        }
                    }
                }
                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                return 0;
            }

            internal static Mejoras_Guantes[] Matriz = new Mejoras_Guantes[]
            {
                new Mejoras_Guantes(null, "0d6832c57637415da663b892eeb3fc09", "None", "Default gloves"),
                new Mejoras_Guantes(Resources.Gloves_Upgrade_Fork, "81e4afd743374d50b0186ef6d221cc70", "Fork", "Splits your ranged attacks"),
                new Mejoras_Guantes(Resources.Gloves_Upgrade_Guidance, "c1217e8a43e6494da60aff69e11cc152", "Guidance", "Ricochet the thrown pickaxe"),
                new Mejoras_Guantes(Resources.Gloves_Upgrade_Sequence_Breaker, "db237b7f272948d288a6f2df44450105", "Sequence Breaker", "Teleport to your pickaxe"),
            };
        }

        /// <summary>
        /// Structure that holds up all the information about a unique weapon upgrade.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Mejoras_Arma
        {
            internal Bitmap Imagen;
            internal string Id;
            internal string Nombre;
            internal string Descripción;

            internal Mejoras_Arma(Bitmap Imagen,
                string Id,
                string Nombre,
                string Descripción)
            {
                this.Imagen = Imagen;
                this.Id = Id;
                this.Nombre = Nombre;
                this.Descripción = Descripción;
            }

            internal static int Buscar_Índice(string ID)
            {
                try
                {
                    if (ID != null)
                    {
                        for (int Índice = 0; Índice < Matriz.Length; Índice++)
                        {
                            try
                            {
                                if (Matriz[Índice].Id != null &&
                                    string.Compare(ID, Matriz[Índice].Id, true) == 0)
                                {
                                    return Índice;
                                }
                            }
                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                        }
                    }
                }
                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                return 0;
            }

            internal static Mejoras_Arma[] Matriz = new Mejoras_Arma[]
            {
                new Mejoras_Arma(null, "be55653f933e4d6dbfe860f3cd875400", "None", "Default weapon"),
                new Mejoras_Arma(Resources.Weapon_Upgrade_Chakram, "eaf4191a4c554bb3adf786947b6d7941", "Chakram", "Throw a whirling blade of death"),
                new Mejoras_Arma(Resources.Weapon_Upgrade_Vorpal_Blade, "e05e3ce278a24c18899a38a7bcc27b49", "Vorpal Blade", "Attack quickly without stopping"),
            };
        }

        /// <summary>
        /// Structure that holds up all the information about a unique hat upgrade.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Mejoras_Sombrero
        {
            internal Bitmap Imagen;
            internal string Id;
            internal string Nombre;
            internal string Descripción;

            internal Mejoras_Sombrero(Bitmap Imagen,
                string Id,
                string Nombre,
                string Descripción)
            {
                this.Imagen = Imagen;
                this.Id = Id;
                this.Nombre = Nombre;
                this.Descripción = Descripción;
            }

            internal static int Buscar_Índice(string ID)
            {
                try
                {
                    if (ID != null)
                    {
                        for (int Índice = 0; Índice < Matriz.Length; Índice++)
                        {
                            try
                            {
                                if (Matriz[Índice].Id != null &&
                                    string.Compare(ID, Matriz[Índice].Id, true) == 0)
                                {
                                    return Índice;
                                }
                            }
                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                        }
                    }
                }
                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                return 0;
            }

            internal static Mejoras_Sombrero[] Matriz = new Mejoras_Sombrero[]
            {
                new Mejoras_Sombrero(null, "", "None", "Undermine default hat"), // The ID can't be null, but it can be "".
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Halo, "401fc943d5e043a9bff5260528aef8ed", "Othermine Crown 0", "Othermine default hat"),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_1, "9e2295a17ae744c898ff3f9e9c30c025", "Othermine Crown 1", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."), // Artifact Discovered!
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_2, "5753de48c1f34c97806c2dadbbd7e890", "Othermine Crown 2", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_3, "8963fd86e19647b3ab300879b6bce83a", "Othermine Crown 3", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_4, "781445f23a6a47c98ce76588440fc1e7", "Othermine Crown 4", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_5, "3aaff92a13ed47b596010546abe7d395", "Othermine Crown 5", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_6, "fcaf852964374a18a13b828d7c1a2b39", "Othermine Crown 6", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_7, "d90b2f0a44274f1abe87d30b02499fb7", "Othermine Crown 7", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_8, "7ed9955782974889b5e8a75796086d5c", "Othermine Crown 8", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_9, "eafdb379ab3e427c95282d21838d8a10", "Othermine Crown 9", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_10, "c982ab82d09149bb80225b5e967d5245", "Othermine Crown 10", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_11, "4111480791624c4c8918df1db4b7ba5e", "Othermine Crown 11", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_12, "d0c5438c476c489dac3bbf16bf530ece", "Othermine Crown 12", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_13, "795649df2c5346fa81bc58515879c6b5", "Othermine Crown 13", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_14, "f633a5af1a374bd99b5fd29fb6e06e3b", "Othermine Crown 14", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_15, "48ccacf934014a61ac4f3a1306f464e1", "Othermine Crown 15", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_16, "429db66640394d91a8ff79326d97f966", "Othermine Crown 16", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_17, "c40589d160644e48b398439cbe1be933", "Othermine Crown 17", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_18, "c57dafa396dd486b9e5a5ef36779a575", "Othermine Crown 18", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_19, "99e439748d35440c93f74de2143ff46a", "Othermine Crown 19", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_20, "3485774e517d4092b1b69190648a811a", "Othermine Crown 20", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_21, "828932003ce24d5da914e601ee86c97d", "Othermine Crown 21", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_22, "b5dbbaf47dbe4c8f9145b1b5b4156e22", "Othermine Crown 22", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_23, "293176316d774244934c9c1e87e0cbf1", "Othermine Crown 23", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_24, "5ba6812f6b484b2f8caef8e675b73fed", "Othermine Crown 24", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_25, "56e8195015f14d8285e425ba7dc50142", "Othermine Crown 25", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_26, "cdb372e98852424aa407e2e3c7977232", "Othermine Crown 26", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_27, "cf0d662816a84bbcb05f5a2c56f55f1f", "Othermine Crown 27", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_28, "a0d7c8a39d5a4eeb91eda7f5a17f2ac9", "Othermine Crown 28", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_29, "356415e282f04dc1a17f75054da1738b", "Othermine Crown 29", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
                new Mejoras_Sombrero(Resources.Hat_Upgrade_Othermine_Crown_30, "88b78b960803421d8c048173ab5b8a9f", "Othermine Crown 30", "A crown bestowed to those that meet the challenge of the Othermine. Reverts back to its default state upon death."),
            };
        }

        /// <summary>
        /// Structure that holds up all the information about a potion (the item, not the effect).
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Pociones
        {
            internal Bitmap Imagen;
            internal string Id;
            internal int CurrentHP;
            internal string Nombre;
            internal string Descripción;

            internal Pociones(Bitmap Imagen,
                string Id,
                int CurrentHP,
                string Nombre,
                string Descripción)
            {
                this.Imagen = Imagen;
                this.Id = Id;
                this.CurrentHP = CurrentHP;
                this.Nombre = Nombre;
                this.Descripción = Descripción;
            }

            internal static int Buscar_Índice(string ID)
            {
                try
                {
                    if (ID != null)
                    {
                        for (int Índice = 0; Índice < Matriz.Length; Índice++)
                        {
                            try
                            {
                                if (Matriz[Índice].Id != null &&
                                    string.Compare(ID, Matriz[Índice].Id, true) == 0)
                                {
                                    return Índice + 1;
                                }
                            }
                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                        }
                    }
                }
                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                return 0;
            }

            internal static Pociones[] Matriz = new Pociones[]
            {
                new Pociones(Resources.Potion_Tincture, "f88f1626a156460c9d793a790f3eadbc", 5, "Tincture", "Recovers health"),
                new Pociones(Resources.Potion_Apprentice_s_Tincture, "6633b8cd44ac4d729b381691ccc0d159", 5, "Apprentice's Tincture", "Recovers a random amount of health"),
                new Pociones(Resources.Potion_Salvaging_Sludge, "34dd71878c07441ea02fd8f0cbe2e813", 5, "Salvaging Sludge", "Recover health for each carried curse"),
                new Pociones(Resources.Potion_Troll_Sweat, "bb69fd18c1f4477c98c2c499d4192c62", 5, "Troll Sweat", "Regenerate health over time"),
                new Pociones(Resources.Potion_Ghostly_Ichor, "0ff810e1fe8c45b0a5facf59cfa6b934", 5, "Ghostly Ichor", "Heal over time for each curse"),
                new Pociones(Resources.Potion_Cure_All, "e7be129bf8754747b40619a2b2708e78", 5, "Cure All", "Create a circle of healing"),
                new Pociones(Resources.Potion_Coffee, "4a1995bed1ec403692082d1dd839cc31", 5, "Coffee", "Recover health proportional to missing health"),
                new Pociones(Resources.Potion_Elixir, "6cfb55f11bc74fdea6690b19608c377a", 5, "Elixir", "Restores all missing health"),
                new Pociones(Resources.Potion_Rainbow_Kernels, "0f00680feaf340518a0185d4cf9038e4", 5, "Rainbow Kernels", "Duplicate almost everything in the room"),
                new Pociones(Resources.Potion_Golden_Kernels, "9aa1438f3c1b4e6196f402d7aeb4cb6c", 5, "Golden Kernels", "Duplicate all gold in the room"),
                new Pociones(Resources.Potion_Popcorn_Kernels, "fd7752c4994c45619c031d90235718ad", 5, "Popcorn Kernels", "Duplicate all basic items in the room"),
                new Pociones(Resources.Potion_Seasoned_Kernels, "1b76796f7c414aa59ab6b68409db7f04", 5, "Seasoned Kernels", "Duplicate all food in the room"),
                new Pociones(Resources.Potion_Whiplash_Serum, "a7de979e8de04cb1b40cf3892d819531", 5, "Whiplash Serum", "Temporarily increases throw damage"),
                new Pociones(Resources.Potion_Strength_Serum, "39e6d812b36b4463b469a3f90a1bc784", 5, "Strength Serum", "Temporarily increases swing damage"),
                new Pociones(Resources.Potion_Savagery_Serum, "875f7b3bf865424ab40e4fe2106ec755", 5, "Savagery Serum", "Temporarily increases critical chance"),
                new Pociones(Resources.Potion_Alacrity_Serum, "a96427afe6e644e7a892051a426fa200", 5, "Alacrity Serum", "Temporarily increases attack speed"),
                new Pociones(Resources.Potion_Sundering_Serum, "aa9b6541ab374dd3961a86178fda49b7", 5, "Sundering Serum", "Temporarily increases swing size"),
                new Pociones(Resources.Potion_Cyclonic_Serum, "0cb32dceb9ce4011960f1253e6797aaf", 5, "Cyclonic Serum", "Temporarily increases throw size"),
                new Pociones(Resources.Potion_Durability_Serum, "c4c4a425d8b2403d80d82f0af642b682", 5, "Durability Serum", "Temporarily increases health"),
                new Pociones(Resources.Potion_Holy_Water, "60a5a77f8af54d00b1d982fe2a204000", 5, "Holy Water", "Remove a curse"),
                new Pociones(Resources.Potion_Purge_Potion, "3c15a213161d4953a1d349547efdeaf0", 5, "Purge Potion", "Removes a curse and deals 75 damage"),
                new Pociones(Resources.Potion_Purification_Potion, "2606cf0ab8434449950613f159043109", 5, "Purification Potion", "Removes all curses, bombs, keys, and sets health to 1"),
                new Pociones(Resources.Potion_Aether, "f4bed4addab44aa1a3d83e12caa7be28", 5, "Aether", "Remove a specific curse"),
                new Pociones(Resources.Potion_Absolution, "a07b25919d62486c9455501a06488ff9", 5, "Absolution", "If you have exactly 5 curses, removes 5 curses"),
                new Pociones(Resources.Potion_Doubling_Saison, "4afff03f79404bb2982406e5336e5806", 5, "Doubling Saison", "Double your bombs"),
                new Pociones(Resources.Potion_Impish_Key_Bomb, "48b58600a508424c936037336889cdf0", 5, "Impish Key Bomb", "Swap your items around"),
                new Pociones(Resources.Potion_Iron_Glaze, "3f464ee7b0af4f96af31dd602136ea95", 5, "Iron Glaze", "Average the number of held keys and bombs"),
                new Pociones(Resources.Potion_Holy_Glaze, "4e9a0816109743aba853ce4e9c977ea9", 5, "Holy Glaze", "Average the levels of all blessings"),
                new Pociones(Resources.Potion_Ambrosia, "5f024f76f7dc46d29147cc366a439fba", 5, "Ambrosia", "Double the level of a random blessing"),
                new Pociones(Resources.Potion_Nitroglycerin, "cccd866f2bcb482191c10dfb553864f8", 5, "Nitroglycerin", "Drop bombs continuously"),
                new Pociones(Resources.Potion_Auglycerin, "aa208069905d445f91fe4cce2e256d69", 5, "Auglycerin", "Drop gold continuously"),
                new Pociones(Resources.Potion_Immolation_Potion, "27afc8bec02b409a9171d0c909d5055f", 5, "Immolation Potion", "Burns nearby enemies"),
                new Pociones(Resources.Potion_Float_Potion, "117d584949fa458a98a3892da901608e", 5, "Float Potion", "Avoid falling into holes"),
                new Pociones(Resources.Potion_Selt_s_Blood, "88bb14b45afc465da693b1f5a3b65e24", 5, "Selt's Blood", "Spawn larvae"),
                new Pociones(Resources.Potion_Blood_Chalice, "2d3b1658f27b44b783c292b8b3f3853a", 5, "Blood Chalice", "Consumes 25% health, drops items and sometimes another Blood Chalice"),
                new Pociones(Resources.Potion_Fury_Potion, "658b2e66edb44bccb589c45d3df68e94", 5, "Fury Potion", "Fire some fireballs"),
                new Pociones(Resources.Potion_Antimatter, "343d6182d08e467e8834b8c666227bca", 5, "Antimatter", "The next time you would take damage, gain that much health instead"),
                new Pociones(Resources.Potion_Bottled_Pilfer, "e10e9968a7294d168a4563a6415b045c", 5, "Bottled Pilfer", "Releases a Hoarding Pilfer"),
                new Pociones(Resources.Potion_Shop_in_a_Bottle, "d13669b1ac7b45838746a8b6bf89867c", 5, "Shop in a Bottle", "Discover the secret shop"),
                new Pociones(Resources.Potion_Bottles_In_a_Bottle, "383721ef682c4c8cbe6a5f7e2a6b4873", 5, "Bottles In a Bottle", "Drop two potions"),
                new Pociones(Resources.Potion_Chest_in_a_Bottle, "5d63c4ad96d74c929963a04aee6a22b2", 5, "Chest in a Bottle", "Drop a random chest"),
                new Pociones(Resources.Potion_TRANSMUT3_In_a_Bottle, "b6739892a5f9444eb76a96b80870143c", 5, "TRANSMUT3 In a Bottle", "Add a TRANSMUT3 to the room"),
                new Pociones(Resources.Potion_Altar_In_A_Bottle, "222727a1ac6c442bbef0635162cff95b", 5, "Altar In A Bottle", "Spawn an altar"),
                new Pociones(Resources.Potion_Transmutagen, "3f3c01172dd845898f00cac3ae8c224e", 5, "Transmutagen", "Transforms all relics in the room"),
                new Pociones(Resources.Potion_Metamorphim, "c54185b068ad4dd5a99ed6ca6687b658", 5, "Metamorphim", "Transmute a carried relic"),
                new Pociones(Resources.Potion_Mighty_Metamorphim, "72289e0235014c3db6964048fdc3a611", 5, "Mighty Metamorphim", "Transmute all carried relics"),
                new Pociones(Resources.Potion_Circle_of_Transmutation, "1ec33c9a63e6451f8ab6b59d85b95087", 5, "Circle of Transmutation", "Transmutes items in a small circle, but makes them fragile"),
                new Pociones(Resources.Potion_Berserker_s_Brew, "b5485d90404d4d9a90918fdb9863ce41", 5, "Berserker's Brew", "Deal and take more damage"),
                new Pociones(Resources.Potion_Ghost_Pepper_Sauce, "a26fc3f4320c481cb69561631885e5a9", 5, "Ghost Pepper Sauce", "Become immune to fire damage and ignite yourself"),
                new Pociones(Resources.Potion_Numbing_Cream, "7b2aca5ce33443918a07d54241ec9838", 5, "Numbing Cream", "Reduce incoming damage"),
                new Pociones(Resources.Potion_Potion_of_True_Sight, "59167c96b01748c1a8272f14b76e044d", 5, "Potion of True Sight", "Discover nearby secrets"),
                new Pociones(Resources.Potion_Potion_of_Plenty, "e9f869553031481faf86e6b3ada7e75e", 5, "Potion of Plenty", "Drop some useful things"),
                new Pociones(Resources.Potion_Protein_Shake, "ca789a9a294d4ed9b83c997071cae608", 5, "Protein Shake", "Drop some protein"),
                new Pociones(Resources.Potion_Blessed_Blend, "8e75ca40d7eb460c92f99829ffa94f88", 5, "Blessed Blend", "Drops a blessing"),
                new Pociones(Resources.Potion_Toxin, "859e28cc7aaa4cc48928a71d334f5bbd", 5, "Toxin", "Coat your weapon with poison"),
                new Pociones(Resources.Potion_Witch_s_Brew, "b059c170ca1c438782536ec456704c52", 5, "Witch's Brew", "Become cursed"),
                new Pociones(Resources.Potion_Freeloader_Draught, "f6956009d6504b7384eebfa9a3198290", 5, "Freeloader Draught", "Get something for nothing"),
                new Pociones(Resources.Potion_Biscuits, "c37dedd5213547bfa3e6d68086baa104", 5, "Biscuits", "Increases experience gain for a familiar"),
                new Pociones(Resources.Potion_Midas_Touch, "174e5d81496d4f128eb2dd6c738aa991", 5, "Midas Touch", "All enemies are turned to gold"),
                new Pociones(Resources.Potion_Kiss_of_the_Succubus, "0dd6ce41255748ccab851155aa142a48", 5, "Kiss of the Succubus", "Kill all enemies and steal their health"),
                new Pociones(Resources.Potion_All_Potion, "5829b0cc3f6149da9649aa302cf7ca57", 5, "All-Potion", "Is whatever you need it to be"),
                new Pociones(Resources.Potion_Starlight_Sip, "f1958e6f5d4e416497dc7d40b5489bce", 5, "Starlight Sip", "Pull the heavens down onto your foes"),
                new Pociones(Resources.Potion_Churchbell_Nectar, "997f751df83546eb9c17daee9a6718de", 5, "Churchbell Nectar", "Creates a temporary Churchbell shield"),
                new Pociones(Resources.Potion_Pangolin_Potion, "2dfc460581634d6f8213760584e8d21c", 5, "Pangolin Potion", "Refills four armor points")
            };
        }

        /// <summary>
        /// Structure that holds up all the information about a status effect.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Efectos
        {
            internal enum Tipos : int
            {
                Blessing,
                Crystal,
                Demon_relic,
                Effect,
                Familiar,
                Hex,
                Major_curse,
                Minor_curse,
                Othermine_major_curse,
                Othermine_demon_relic,
                Othermine_relic,
                Potion,
                Relic,
                UnderMod_relic
            }

            internal Bitmap Imagen;
            internal string Id;
            internal int Level;
            internal double Duration;
            internal double DurationRatio;
            internal int UserData;
            internal string UserString;
            internal bool Sticky;
            internal Tipos Tipo;
            internal string Nombre;
            internal string Descripción;

            internal Efectos(Bitmap Imagen,
                string Id,
                int Level,
                double Duration,
                double DurationRatio,
                int UserData,
                string UserString,
                bool Sticky,
                Tipos Tipo,
                string Nombre,
                string Descripción)
            {
                this.Imagen = Imagen;
                this.Id = Id;
                this.Level = Level;
                this.Duration = Duration;
                this.DurationRatio = DurationRatio;
                this.UserData = UserData;
                this.UserString = UserString;
                this.Sticky = Sticky;
                this.Tipo = Tipo;
                this.Nombre = Nombre;
                this.Descripción = Descripción;
                /*// Check the names to see if any word starts with lower case instead of upper case.
                try
                {
                    if (!string.IsNullOrEmpty(Nombre))
                    {
                        for (int Índice_Caracter = 1; Índice_Caracter < Nombre.Length; Índice_Caracter++)
                        {
                            try
                            {
                                if (char.IsWhiteSpace(Nombre[Índice_Caracter - 1]) &&
                                    char.IsLower(Nombre[Índice_Caracter]) &&
                                    string.Compare(Nombre, "Pocket of Holding", false) != 0 &&
                                    string.Compare(Nombre, "Rain of Fire", false) != 0 &&
                                    string.Compare(Nombre, "Totem of Life", false) != 0)
                                {
                                    MessageBox.Show("This status effect has a word that starts in lower case:\r\n\"" + Nombre + "\".", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    break;
                                }
                            }
                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                        }
                    }
                }
                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }*/
            }

            internal static int Buscar_Índice(string ID)
            {
                try
                {
                    if (ID != null)
                    {
                        for (int Índice = 0; Índice < Matriz.Length; Índice++)
                        {
                            try
                            {
                                if (Matriz[Índice].Id != null &&
                                    string.Compare(ID, Matriz[Índice].Id, true) == 0)
                                {
                                    return Índice;
                                }
                            }
                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                        }
                    }
                }
                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                return -1;
            }

            internal static Efectos[] Matriz = new Efectos[]
            {
                new Efectos(Resources.Status_Effect_Balance, "3f3b511e1dfd4615ab8e6c43b850a2cb", 1, -1d, 0d, 0, "", false, Tipos.Blessing, "Balance", "Deal more throw damage"),
                new Efectos(Resources.Status_Effect_Vigor, "c37c453e2f244ac594e1d7769e1d4f92", 1, -1d, 0d, 0, "", false, Tipos.Blessing, "Vigor", "Increase throw speed"),
                new Efectos(Resources.Status_Effect_Mighty_Hurl, "86a01a1197324d5b9bb5c19ba3fa1e27", 1, -1d, 0d, 0, "", false, Tipos.Blessing, "Mighty Hurl", "Increases throw range"),
                new Efectos(Resources.Status_Effect_Strength, "93e3fbafa9144c9596fd3367e3727b5e", 1, -1d, 0d, 0, "", false, Tipos.Blessing, "Strength", "Deal more swing damage"),
                new Efectos(Resources.Status_Effect_Exuberance, "736530a63d3c497f9b17c5973d283dff", 2, -1d, 0d, 0, "", false, Tipos.Blessing, "Exuberance", "Faster swing speed"),
                new Efectos(Resources.Status_Effect_Cleave, "f1d67473ea454647b68d7c783f489e7a", 3, -1d, 0d, 0, "", false, Tipos.Blessing, "Cleave", "Increase swing attack size"),
                new Efectos(Resources.Status_Effect_Savagery, "665a9c6b163c4485a420a6ec1602c91d", 1, -1d, 0d, 0, "", false, Tipos.Blessing, "Savagery", "Increase chance to critical strike"),
                new Efectos(Resources.Status_Effect_Ferocity, "833209971f7f4b3291da91bb53942abd", 1, -1d, 0d, 0, "", false, Tipos.Blessing, "Ferocity", "Increase critical strike damage"),
                new Efectos(Resources.Status_Effect_Demolition, "dc706512085644d4a5a847dac77eb4a7", 2, -1d, 0d, 0, "", false, Tipos.Blessing, "Demolition", "Increases bomb size"),
                new Efectos(Resources.Status_Effect_Explosiveness, "4baf318bf4954a0cb7ce05784751b7df", 1, -1d, 0d, 0, "", false, Tipos.Blessing, "Explosiveness", "Deal more bomb damage"),
                new Efectos(Resources.Status_Effect_Fortitude, "16c7fa0de6704699b4555a253f0fbba5", 1, -1d, 0d, 0, "", false, Tipos.Blessing, "Fortitude", "Receive less damage"),
                new Efectos(Resources.Status_Effect_Toughness, "151226f9fc374aeebc0c11b4e76594cd", 4, -1d, 0d, 0, "", false, Tipos.Blessing, "Toughness", "Increased maximum health"), // Supports up to level int.MaxValue and more than 30.000.000 of health = immortal.
                new Efectos(Resources.Status_Effect_Heartiness, "950599879b8d48f8abc3b17d9af2dd33", 1, -1d, 0d, 0, "", false, Tipos.Blessing, "Heartiness", "Healing is more effective"),
                new Efectos(Resources.Status_Effect_Slow_Metabolism, "5cb92c80d82843be86d4d9e59559bd62", 1, -1d, 0d, 0, "", false, Tipos.Blessing, "Slow Metabolism", "Increase potion duration"),
                new Efectos(Resources.Status_Effect_Regeneration, "b507880795d2471fa8f6e8a0f4ab7de1", 1, -1d, 0d, 0, "", false, Tipos.Blessing, "Regeneration", "Heal through adventure"),
                new Efectos(Resources.Status_Effect_Craftsmanship, "f73176ee0d764246b2ef69baf213e596", 1, -1d, 0d, 0, "", false, Tipos.Blessing, "Craftsmanship", "Lose less gold on death"),
                new Efectos(Resources.Status_Effect_Wealth, "390f070028984bff8f003fb517103711", 3, -1d, 0d, 0, "", false, Tipos.Blessing, "Wealth", "Increase gold income"),
                new Efectos(Resources.Status_Effect_Loyalty, "fc2e43b5896a4e2a9d831a7d0aea18ca", 1, -1d, 0d, 0, "", false, Tipos.Blessing, "Loyalty", "Reduces the cost of shop items"),
                
                new Efectos(Resources.Status_Effect_Electrorock, "9e83a3496e2343a08291eef30c95fff2", 0, 54.860050201416d, 0.914334177970886d, 0, "", false, Tipos.Crystal, "Electrorock", "Imbue your weapon with lightning"),
                new Efectos(Resources.Status_Effect_Seertooth, "974a46c3399b4eec86c23cbdd1fcde9c", 0, 12.5942993164063d, 0.209904983639717d, 0, "", false, Tipos.Crystal, "Seertooth", "Imbue your weapon with fire"),

                new Efectos(Resources.Status_Effect_Chaotic_Offering, "1a2a4e79b6834973b87a62c056b55b1a", 0, -1d, 0d, 0, "", false, Tipos.Hex, "Chaotic Offering", "All items are hidden"),
                new Efectos(Resources.Status_Effect_Dreadful_Fog, "186668076c6e4532a6451703d17a83c7", 0, -1d, 0d, 0, "", false, Tipos.Hex, "Dreadful Fog", "Enemies have double health and damage"),
                new Efectos(Resources.Status_Effect_Red_Hot_Nuggets, "6873f352cbeb489eb050bd6fb47a1372", 0, -1d, 0d, 0, "", false, Tipos.Hex, "Red-Hot Nuggets", "Take damage when picking up gold"),
                new Efectos(Resources.Status_Effect_Rogue_s_Ultimatum, "7a78e1580ae34f9c908fc2cb74cc5e81", 0, -1d, 0d, 0, "", false, Tipos.Hex, "Rogue's Ultimatum", "Disables permanent upgrades"),
                new Efectos(Resources.Status_Effect_Siegfried_s_Torment, "ae2c9f624b104e1b807bb002eeafec9a", 0, -1d, 0d, 0, "", false, Tipos.Hex, "Siegfried's Torment", "Adds an indestructible curse on every floor"),
                new Efectos(Resources.Status_Effect_Frostbite, "91517ccfe5f94e0386f4c40fa30938d5", 0, -1d, 0d, 0, "", false, Tipos.Hex, "Frostbite", "Cannot heal"),
                new Efectos(Resources.Status_Effect_Adventurer_s_Peril, "319cab181142408cae02ade562c5ff14", 0, -1d, 0d, 0, "", false, Tipos.Hex, "Adventurer's Peril", "One health point"),
                new Efectos(Resources.Status_Effect_Unending_Desolation, "15a5a17ac6824e32aa375755f3119c7c", 0, -1d, 0d, 0, "", false, Tipos.Hex, "Unending Desolation", "Floors no longer have reilc rooms"),

                new Efectos(Resources.Status_Effect_Arachnophobia, "650d6e533504428b8076fdf9fbccbff9", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Arachnophobia", "Enemies release spiders on death"),
                new Efectos(Resources.Status_Effect_Sweaty_Palms, "4e1049ea39144c578bc0916671bb28b1", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Sweaty Palms", "Chance to lose a bomb when you take damage"),
                new Efectos(Resources.Status_Effect_Sweaty_Fingers, "7832d137109c4710b2a1d50c7ac54eb5", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Sweaty Fingers", "Chance to lose a key when you take damage"),
                new Efectos(Resources.Status_Effect_Pilfer_s_Nightmare, "8a3fb50ea6d941cf94b4c2b96b8b3161", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Pilfer's Nightmare", "New and more dangerous Pilfers"),
                new Efectos(Resources.Status_Effect_Metamfiezomaiophobia, "674c74f67e99474aafba25b424be2e8a", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Metamfiezomaiophobia", "More mimics than you would like"),
                new Efectos(Resources.Status_Effect_Relic_Eater, "b15f297d2be44a8ba25d03a1841f8eb5", 0, -1d, 0d, 29, "", false, Tipos.Major_curse, "Relic Eater", "Killing enemies destroys a relic"),
                new Efectos(Resources.Status_Effect_Blessing_Eater, "ae4ceb9fe5e14f9f91dcc576cae24a06", 0, -1d, 0d, 19, "", false, Tipos.Major_curse, "Blessing Eater", "Killing enemies destroys a blessing"),
                new Efectos(Resources.Status_Effect_Item_Eater, "1f7b5d274546445e88897129cb42b33c", 0, -1d, 0d, 17, "", false, Tipos.Major_curse, "Item Eater", "Killing enemies destroys items"),
                new Efectos(Resources.Status_Effect_Tariffs, "9c143a082f204e0eb6220fc3314b77a1", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Tariffs", "Double all costs"),
                new Efectos(Resources.Status_Effect_Market_Crash, "15fc0bacc9a04d9db15883beec141760", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Market Crash", "Increases the cost of shop items"),
                new Efectos(Resources.Status_Effect_Low_Stock, "524dc2e94cb3435baa50038dd48c321e", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Low Stock", "Reduced shop options"),
                new Efectos(Resources.Status_Effect_Blood_Pact, "3745356bd92e4ef791d2d0ae0fc8efbd", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Blood Pact", "Costs health to plant bombs"),
                new Efectos(Resources.Status_Effect_Powder_Shortage, "08ab291ae510459fb2f1b5e44904ffb0", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Powder Shortage", "Dropping a bomb costs 2 bombs"),
                new Efectos(Resources.Status_Effect_Rain_of_Fire, "3b26ef08b08b420891b410526d668ebe", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Rain of Fire", "Droplets of fire fall from the sky"),
                new Efectos(Resources.Status_Effect_Waking_Light, "bf5f922ae4e24356b29a2aea8d1cb4a6", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Waking Light", "Torches and lanters fire fireballs"),
                new Efectos(Resources.Status_Effect_Salamander_s_Wrath, "2909d776ac2243148c7990f474c83102", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Salamander's Wrath", "All enemies ignite the peasant"),
                new Efectos(Resources.Status_Effect_Fast_Metabolism, "72b15e34f352461c89ce554d2806d559", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Fast Metabolism", "Decreases potion duration"),
                new Efectos(Resources.Status_Effect_Venomous, "a04fae1442e34960baced60906b95ec8", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Venomous", "All enemies inflict poison"),
                new Efectos(Resources.Status_Effect_Plague, "cead863547fb479e87a6f10a87155db1", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Plague", "Uncooked food is less effective and has a chance to poison"),
                new Efectos(Resources.Status_Effect_Tenderfoot, "59a8224ae2d24270b43e281fbf45aed6", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Tenderfoot", "Take damage when you jump"),
                new Efectos(Resources.Status_Effect_Empty_Coffers, "1ddf8b30a2b14192a93c9034950319c3", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Empty Coffers", "Chests are sometimes empty"),
                new Efectos(Resources.Status_Effect_Rigged, "c975aff56bfd400c900d5c84cf170250", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Rigged", "Chests are trapped more often"),
                new Efectos(Resources.Status_Effect_The_Crumbles, "c2c574fbc30144789da581b3a2d53610", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "The Crumbles", "Items become extra fragile"),
                new Efectos(Resources.Status_Effect_Poison_Mushroom, "ba6ac42caecb4193bfab61120b2cbc23", 0, -1d, 0d, -7, "", false, Tipos.Major_curse, "Poison Mushroom", "Lose maximum health when killing enemies"),
                new Efectos(Resources.Status_Effect_Haunted_Locks, "8a807e86856b4d22b5c4425f3c3630fc", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Haunted Locks", "Costs health to use a key"),
                new Efectos(Resources.Status_Effect_Weakness, "2fc65faad4a642e697ae54d8f026bee2", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Weakness", "Deal less swing damage"),
                new Efectos(Resources.Status_Effect_Wobbly, "10308dab939c404da05f7d6a85987bd1", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Wobbly", "Deal less throw damage"),
                new Efectos(Resources.Status_Effect_Mediocrity, "2d6085afbf5b4e948768f42a1b28a3ca", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Mediocrity", "Can no longer critical strike"),
                new Efectos(Resources.Status_Effect_Enfeebled, "9ddcfe0f4562461c9ffcdd1f47878f16", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Enfeebled", "Reduced maximum health"),
                new Efectos(Resources.Status_Effect_Fever, "4e8b9293d54348da98464840629df9ca", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Fever", "Healing is less effective"),
                new Efectos(Resources.Status_Effect_Ineptitude, "5e4de46088364e4d95ce10f93e89d93c", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Ineptitude", "Lose more gold on death"),
                new Efectos(Resources.Status_Effect_Vulnerable, "79524a425882498ca70761121f434eaa", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Vulnerable", "Receive more damage"),
                new Efectos(Resources.Status_Effect_Miner_s_Shoulder, "f734c4d062cc4610839714515aea90c3", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Miner's Shoulder", "Decreases throw speed"),
                new Efectos(Resources.Status_Effect_Arthritis, "102f828aaba7466a8016dd9754de7a25", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Arthritis", "Decreases swing speed"),
                new Efectos(Resources.Status_Effect_Spatial_Sickness, "179e0fb393a641a98a878a39de94f6a1", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Spatial Sickness", "Teleport when hit"),
                new Efectos(Resources.Status_Effect_Bottle_Blight, "3c53be2da36248b4b6afc124725fbba5", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Bottle Blight", "Take damage for each of your potions, each room"),
                new Efectos(Resources.Status_Effect_Compulsion, "4d72bf084ef1497ca2e1759a1adf8747", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Compulsion", "All potions are automatically drink on pickup"),
                new Efectos(Resources.Status_Effect_Bottle_Stopper, "5e543ddf07fd4511b1614b45008aa27a", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Bottle Stopper", "Carry fewer potions"),
                new Efectos(Resources.Status_Effect_Mutation, "f1675c52530e404d949eccbb9d83b3ad", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Mutation", "Relics have a chance to be turned into potions"),
                new Efectos(Resources.Status_Effect_Secrecy, "cf3e89d9e77b41589562def9af731222", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Secrecy", "Secret rooms are even more secret"),
                new Efectos(Resources.Status_Effect_Security_Theater, "fe4e9ca092d640a2ad2a1372219a3b13", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Security Theater", "Locked shops are extra locked"),
                new Efectos(Resources.Status_Effect_Explosive_Decompression, "3676c755578340b0a8d80c2938bffc23", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Explosive Decompression", "All ore is mined when entering a room"),
                new Efectos(Resources.Status_Effect_Vertigo, "e1767d1b69ee47c59559201aa8763444", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Vertigo", "Receive increased fall damage"),
                new Efectos(Resources.Status_Effect_Nullification, "09aecbb2a36c47e9b3c89bcd9253343e", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Nullification", "Disables 2 relics"),
                new Efectos(Resources.Status_Effect_Dark_Cloud, "be6d615df5dd4f308201aee1211428b2", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Dark Cloud", "Disables 2 blessings"),
                new Efectos(Resources.Status_Effect_Uncreative, "75c7974d263844e5893bce93583850a5", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Uncreative", "Only one blessing is available at altars"),
                new Efectos(Resources.Status_Effect_Malicious_Intent, "f3b68b4235a84381858500754fd3dcf5", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Malicious Intent", "The cost of Penance is doubled"),
                new Efectos(Resources.Status_Effect_Bloodied_Locks, "9728683655fd4f0094bd1fec56e4fbf1", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Bloodied Locks", "Lock costs are converted to health"),
                new Efectos(Resources.Status_Effect_Blood_Offering, "9354e282de7d43a9884c9a3b7f32f807", 0, -1d, 0d, 0, "", false, Tipos.Major_curse, "Blood Offering", "Shop costs are converted to health"),
                new Efectos(Resources.Status_Effect_Debt, "3908f81afc6d4e12a16d27fdbe05758d", 0, -1d, 0d, 0, "", false, Tipos.Othermine_major_curse, "Debt", "?"),
                
                new Efectos(Resources.Status_Effect_Inflation, "1ef2ded4c7284589a2b54c9ae1fb54e6", 1, -1d, 0d, 0, "", false, Tipos.Minor_curse, "Inflation", "Increases the cost of shop items"),
                new Efectos(Resources.Status_Effect_Poverty, "1060b8aae45c457db34c9e882a7cf89e", 1, -1d, 0d, 0, "", false, Tipos.Minor_curse, "Poverty", "Decrease the amount of gold received"),
                new Efectos(Resources.Status_Effect_Lethargy, "aa3c4135f42848ecb93429f22ff749f4", 2, -1d, 0d, 0, "", false, Tipos.Minor_curse, "Lethargy", "Deal less swing damage"),
                new Efectos(Resources.Status_Effect_Imbalance, "33fdcb7e9b71475eb8ba55748a430516", 1, -1d, 0d, 0, "", false, Tipos.Minor_curse, "Imbalance", "Deal less throw damage"),
                new Efectos(Resources.Status_Effect_Frailty, "ebca0710179f4382ad7a70a78aa14980", 1, -1d, 0d, 0, "", false, Tipos.Minor_curse, "Frailty", "Reduced maximum health"),
                new Efectos(Resources.Status_Effect_Illness, "107606f718064785af45bd81cb91c1e7", 1, -1d, 0d, 0, "", false, Tipos.Minor_curse, "Illness", "Healing is less effective"),
                new Efectos(Resources.Status_Effect_Clumsiness, "883577bb502e4ba69ad88eff54306b19", 1, -1d, 0d, 0, "", false, Tipos.Minor_curse, "Clumsiness", "Lose more gold on death"),
                new Efectos(Resources.Status_Effect_Brittleness, "7d64fc2bf3d542ec8638fde4d5aa64d4", 1, -1d, 0d, 0, "", false, Tipos.Minor_curse, "Brittleness", "Receive more damage"),
                new Efectos(Resources.Status_Effect_Blisters, "506489bb36ce432ca656e1a73f3b246e", 1, -1d, 0d, 0, "", false, Tipos.Minor_curse, "Blisters", "Decreases throw speed"),
                new Efectos(Resources.Status_Effect_Joint_Pain, "bf0c699b238042aa9f5f9b35f2a5b43e", 1, -1d, 0d, 0, "", false, Tipos.Minor_curse, "Joint Pain", "Decreases swing speed"),

                /*//new Efectos("ed66841526f14a6ead318cea8b8eebc5", 11, 3.70541572570801d, 0.247027710080147d, 0, "", false, Tipos.Effect, "Canis Major", "Crits temporarily increase crit chance"),
                //new Efectos("356a5ae30e334f57bd235a3f7076ec7d", 1, 100.824592590332d, 0.840204954147339d, 20, "", false, Tipos.Effect, "Ursa Major", "Increases max health after eating food"),
                new Efectos(Resources.Status_Effect_, "ed66841526f14a6ead318cea8b8eebc5", 17, 11.8789186477661d, 0.791927933692932d, 0, "", false, Tipos_Efecto.Effect, "Canis Major", "Crits temporarily increase crit chance"),
                new Efectos(Resources.Status_Effect_, "934774b7cdf44d56a456014ae903d42a", 0, 23.7454471588135d, 0.263838291168213d, 0, "", false, Tipos_Efecto.Effect, "Circinus", "Temporarily reveals secret rooms, secret rooms can have secret rooms"),
                new Efectos(Resources.Status_Effect_, "d0478aece0e44ffa853430b4c1ac1e79", 0, 3.44131970405579d, 0.430164963006973d, 0, "", false, Tipos_Efecto.Effect, "Invigorated", "Increased move, attack, and throw speed"),
                new Efectos(Resources.Status_Effect_, "9c136a49f65c44a690de4eda21441e11", 0, 4.92379713058472d, 0.61547464132309d, 0, "", false, Tipos_Efecto.Effect, "Sagitta", "Enemies explode in arrows after a critical strike"),
                new Efectos(Resources.Status_Effect_, "356a5ae30e334f57bd235a3f7076ec7d", 6, 81.7585754394531d, 0.681321442127228d, 120, "", false, Tipos_Efecto.Effect, "Ursa Major", "Increases max health after eating food"),
                */
                new Efectos(Resources.Familiar_Sol_s_Phoenix, "fce5502d0bc44ee8abb056af3aa6f8de", -3, 3.75412750244141d, 0.53630393743515d, 0, "", false, Tipos.Familiar, "Radiance", "Whenever the Phoenix heals the peasant it applies an immolation effect that burns nearby enemies"),
                new Efectos(Resources.Familiar_Sol_s_Phoenix, "eb7aa0f0238744378420bf267ccb3bdd", 0, -1d, 0d, 0, "", false, Tipos.Familiar, "Rebirth", "The Phoenix will resurrect the peasant once when they fall"),
                /*
                new Efectos(Resources.Status_Effect_, "abb7917aa86c47859f94c02d5c45e329", 0, 185.200088500977d, 0.771667063236237d, 0, "", false, Tipos_Efecto.Potion, "Alacrity Serum", "Temporarily increases attack speed"),
                new Efectos(Resources.Status_Effect_, "2815818d7294484b94f058b3b80f196f", 0, 156.324996948242d, 0.361863404512405d, 0, "", false, Tipos_Efecto.Potion, "Biscuits", "Increases experience gain for a familiar"),
                new Efectos(Resources.Status_Effect_, "2a4a2334a1c94bfeba9fc15cd08002fe", 0, 86.4858703613281d, 0.360357791185379d, 0, "", false, Tipos_Efecto.Potion, "Cyclonic Serum", "Temporarily increases throw size"),
                new Efectos(Resources.Status_Effect_, "b1542b673dfd46d3a74959cf7a9d79a7", 0, 68.7548828125d, 0.63661926984787d, 0, "", false, Tipos_Efecto.Potion, "Durability Serum", "Temporarily increases health"),
                new Efectos(Resources.Status_Effect_, "1085fd1dddf84b439caa3385ee2f509c", 0, 134.782852172852d, 0.56159520149231d, 0, "", false, Tipos_Efecto.Potion, "Floating Potion", "Avoid falling into holes"),
                new Efectos(Resources.Status_Effect_, "6f1eb18169a74919abd65987af934c7c", 0, 0.831142127513886d, 0.0173154603689909d, 0, "", false, Tipos_Efecto.Potion, "Ghostly Ichor", "Heal over time for each curse"),
                new Efectos(Resources.Status_Effect_, "4702446347384be998a8c2acf00b0352", 0, 181.453277587891d, 0.756055295467377d, 0, "", false, Tipos_Efecto.Potion, "Savagery Serum", "Temporarily increases critical chance"),
                new Efectos(Resources.Status_Effect_, "f3eca92397214278b215dc02376e82cf", 0, 88.7787094116211d, 0.369911283254623d, 0, "", false, Tipos_Efecto.Potion, "Sundering Serum", "Temporarily increases swing size"),
                new Efectos(Resources.Status_Effect_, "ccee2ff659234eb2a0b0235b4ed9e5b8", 0, 57.4096641540527d, 0.354380637407303d, 0, "", false, Tipos_Efecto.Potion, "Toxin", "Coat your weapon with poison"),
                new Efectos(Resources.Status_Effect_, "65f38453c5354e04804b629fcad00dd9", 0, 202.595397949219d, 0.844147503376007d, 0, "", false, Tipos_Efecto.Potion, "Whiplash Serum", "Temporarily increases throw damage"),
                */
                new Efectos(Resources.Status_Effect_Wayland_s_Boots, "df4ddf16dd4a45ef8695cdf3e7679ce2", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Wayland's Boots", "Break spikes, take names"),
                new Efectos(Resources.Status_Effect_Galoshes, "a4a29157f46c4a539e3bbddd81cde3e9", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Galoshes", "Walk and jump on oil"),
                new Efectos(Resources.Status_Effect_Float_Boots, "2a1fa10af49a47d28b24f56077eee57e", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Float Boots", "Walk on air"),
                new Efectos(Resources.Status_Effect_Lava_Walkers, "43eff4cddbd24b36931720f418920d5d", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Lava Walkers", "Walk on fire"),
                new Efectos(Resources.Status_Effect_Helios_Boots, "c05ef86ba2054882959c51b24350e72b", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Helios Boots", "Fire patches last for much longer and deal more damage"),
                new Efectos(Resources.Status_Effect_Butcher_s_Cleaver, "1210bacc5fcb460ca07e95896df87cf6", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Butcher's Cleaver", "Sometimes drop meat from your enemies"),
                new Efectos(Resources.Status_Effect_Key_Blade, "951e2808ae0d4c42b1a29cd857f881e9", 0, -1d, 0d, 12, "", false, Tipos.Relic, "Key Blade", "Increases swing damage for each key you have"),
                new Efectos(Resources.Status_Effect_Master_Pickaxe, "befda1eb80d645f9bb12e17ce56e6a28", 0, -1d, 0d, 0, "", false, Tipos.Othermine_relic, "Master Pickaxe", "Fire projectiles at full health"),
                new Efectos(Resources.Status_Effect_Mjölnir, "fe10d95ade194b0f86ecaec29fc92466", 0, -1d, 0d, 0, "", false, Tipos.Demon_relic, "Mjölnir", "Summon lightning when the pickaxe is caught"),
                new Efectos(Resources.Status_Effect_Battle_Axe, "e0ce88d289d4461896c3ef9fd2157692", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Battle Axe", "Increases swing size, but decreases swing damage"),
                new Efectos(Resources.Status_Effect_Masa, "8144d7dba6454abf84d594f2c1f6974c", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Masa", "Reduces throw damage and increases swing damage"),
                new Efectos(Resources.Status_Effect_Suneater, "290689aa82ec4d7db053f7494cbbece8", 0, -1d, 0d, 424, "", false, Tipos.Demon_relic, "Suneater", "Consumes all current and future blessings and converts them to swing damage"),
                new Efectos(Resources.Status_Effect_Twisted_Blade, "11bcd163ae2c4a0ba19237600b123a7d", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Twisted Blade", "Increases critical strike chance for each carried curse"),
                new Efectos(Resources.Status_Effect_Obsidian_Knife, "e5f374c38c33493ba6e2bc82b75c9a67", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Obsidian Knife", "Dramatically increases damage, but breaks when hit"),
                new Efectos(Resources.Status_Effect_Dirk_s_Hammer, "c77a03a88f214e5dba0b92e48669d2ad", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Dirk's Hammer", "Transmute an item by hitting it"),
                new Efectos(Resources.Status_Effect_Resurrection, "440e515219864ce6943973c411308094", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Resurrection", "Resurrect on death"),
                new Efectos(Resources.Status_Effect_Gordon_s_Tunic, "c89eae5774314d36b855d3ea12bbc527", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Gordon's Tunic", "Reduces elemental damage"),
                new Efectos(Resources.Status_Effect_Breastplate, "571d369d5dd44c24b7783e914a0800d1", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Breastplate", "Adds a point of armor to the health bar"),
                new Efectos(Resources.Status_Effect_Pauldron, "2a74269a1ec84ab984621031ec063f1f", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Pauldron", "Adds a point of armor to the health bar"),
                new Efectos(Resources.Status_Effect_Gauntlets, "5324ddcb6a4148eeb0d2d9dbacf7f7e7", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Gauntlets", "Adds a point of armor to the health bar"),
                new Efectos(Resources.Status_Effect_Greaves, "36721d531f51454e9ca97f99f11ee172", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Greaves", "Adds a point of armor to the health bar"),
                new Efectos(Resources.Status_Effect_Blast_Suit, "181beab6d4fc4bb4b1725575b8c5d4e2", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Blast Suit", "Invulnerability to friendly bomb blasts and increases bomb damage"),
                new Efectos(Resources.Status_Effect_Wet_Blanket, "bc336eefb70b414e9a7f381ea9f83272", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Wet Blanket", "Puts out fires immediately, but requires water charges"),
                new Efectos(Resources.Status_Effect_War_Paint, "049357f4cc6348468368da680e267ebd", 0, -1d, 0d, 0, "", false, Tipos.Relic, "War Paint", "Increase attack damage and speed when killing enemies"),
                new Efectos(Resources.Status_Effect_Battle_Standard, "01835ae333dd4b2e863c1cab8c6cf831", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Battle Standard", "Increases move, attack, and throw speed at the beginning of battle"),
                new Efectos(Resources.Status_Effect_Catalyst, "16db3491f36a4f6899a90e1caeded685", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Catalyst", "When healed, heal again"),
                new Efectos(Resources.Status_Effect_Selt_s_Egg, "e368e9e9601a4e53b93c855916fa4b1a", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Selt's Egg", "Spawn larvae on entrance"),
                new Efectos(Resources.Status_Effect_Electrified_Orb, "89718ac2e2394449a20f3ca35f399195", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Electrified Orb", "Orbits the peasant and shocks enemies on contact"),
                new Efectos(Resources.Status_Effect_Othermine_Conduit, "54f81f94543d43d99302effce88021db", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Othermine Conduit", "Ghastly hands will attack the enemy"),
                new Efectos(Resources.Status_Effect_Doll, "2a8b4896a0e347ffa6f96331f65bd32c", 0, -1d, 0d, 4, "", false, Tipos.Relic, "Doll", "Blocks the next few curses"),
                new Efectos(Resources.Status_Effect_Devotion, "f3a1c9d032734882a2b94b2f3ee0245a", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Devotion", "Pray twice at altars"),
                new Efectos(Resources.Status_Effect_108_Beads, "459b909d53d14c6c8ada4a3a45e9cb05", 0, -1d, 0d, 0, "", false, Tipos.Relic, "108 Beads", "Heal when praying at an altar"),
                new Efectos(Resources.Status_Effect_Holy_Guacamole, "f43b6cd8c56547d09275b48db5846189", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Holy Guacamole", "Find more altar rooms"),
                new Efectos(Resources.Status_Effect_Sonic_Boom, "999c33fe95794e7da53ef286e3854967", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Sonic Boom", "Throw really fast"),
                new Efectos(Resources.Status_Effect_Sewing_Kit, "8110d78251e94611b35e7b673b0f970e", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Sewing Kit", "Keep all your gold when you die"),
                new Efectos(Resources.Status_Effect_Simple_Chest, "f120a5a06673456c9db702c654ac1a6a", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Simple Chest", "Overstocks the shop"),
                new Efectos(Resources.Status_Effect_Meal_Ticket, "399970a794994a668f8fb342af95425d", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Meal Ticket", "Free food at the shop, right now!"),
                new Efectos(Resources.Status_Effect_Adventurer_s_Hat, "90ee14233cfe4249818d5ca2eb8ec122", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Adventurer's Hat", "Discover mroe secret rooms"),
                new Efectos(Resources.Status_Effect_Adventurer_s_Whip, "0e7e31f44491456bb5f6268f2c2686c2", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Adventurer's Whip", "Discover more treasure rooms"),
                new Efectos(Resources.Status_Effect_Golden_Idol, "8d05bd4c82d04ecb92f252721bb1dc01", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Golden Idol", "Discover more rooms and get rich"),
                new Efectos(Resources.Status_Effect_Totem_of_Life, "d67d79e1a05e4b4786762cd3f0134bd0", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Totem of Life", "Sustain yourself through adventure"),
                new Efectos(Resources.Status_Effect_Aphotic_Charm, "28919928e5bb401291440ef927b80b6b", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Aphotic Charm", "Heal in each new room for each curse"),
                new Efectos(Resources.Status_Effect_Dillon_s_Claw, "6ad637fd31d14b0890ace63d7502c003", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Dillon's Claw", "Deal even more damage when you critical strike"),
                new Efectos(Resources.Status_Effect_Shadow_s_Fang, "17c69c40ca934368ba046afbdb3f6592", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Shadow's Fang", "Higher chance to critical strike"),
                new Efectos(Resources.Status_Effect_Bramble_Vest, "426d6734b50d42839957c9193684feeb", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Bramble Vest", "Return damage but amplified"),
                new Efectos(Resources.Status_Effect_Duplicator, "7ec4995423b04e5ab3d5a346d84bfd9d", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Duplicator", "Choose between reilcs"),
                new Efectos(Resources.Status_Effect_Miner_s_Flask, "007263063c92486b919015f9c9acaae4", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Miner's Flask", "feel the effects of a potion for longer"),
                new Efectos(Resources.Status_Effect_Lunchbox, "79c17ac6ec1b4a578101c435bc982fad", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Lunchbox", "Store a piece of food for later"),
                new Efectos(Resources.Status_Effect_Large_Ember, "d46d62559d084690b7732d3ba29487d4", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Large Ember", "Radiate heat"),
                new Efectos(Resources.Status_Effect_Popcorn, "c000b0766bf14e15be15c8b0388f39cd", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Popcorn", "Items will sometimes duplicate themselves"),
                new Efectos(Resources.Status_Effect_Golden_Popcorn, "3476bec1a63c4fffb9699dc50c43046d", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Golden Popcorn", "Gold will sometimes duplicate itself"),
                new Efectos(Resources.Status_Effect_Seasoned_Popcorn, "429ef0c052544c9c9442e323dc9f213c", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Seasoned Popcorn", "Food will sometimes duplicate itself"),
                new Efectos(Resources.Status_Effect_Caramel_Popcorn, "1c2449de28a34fda9fe5c990045cb81b", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Caramel Popcorn", "Duplicated food packs a surprise"),
                new Efectos(Resources.Status_Effect_Pocket_Grill, "0407348b3edc45c0a0d3803fea4afd46", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Pocket Grill", "Cooks all your food"),
                new Efectos(Resources.Status_Effect_Leftovers, "323b849ba6144a67a3bdf03ce434e16f", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Leftovers", "Find old, gross food in chests"),
                new Efectos(Resources.Status_Effect_Spare_Ordnance, "5ffaae53faf94dc2b9c2c08efa2d6636", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Spare Ordnance", "Discover a bomb in every chest"),
                new Efectos(Resources.Status_Effect_Key_Doubler, "3ccf0b428250435a9e7de14be1ce5bcb", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Key Doubler", "Sometimes drop a new key when using an old key"),
                new Efectos(Resources.Status_Effect_Bomb_Doubler, "c08a901c2f3646e5834cfe3a0e965ca8", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Bomb Doubler", "Sometimes drop a new bomb when using an old bomb"),
                new Efectos(Resources.Status_Effect_Double_Doubler, "94ce9288476e4d77a6ac9a5906ddcd0d", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Double Doubler", "Chance to drop a key and bomb when using either"),
                new Efectos(Resources.Status_Effect_U_235, "bc720d3ef33e40de87fc9cc3278522a1", 0, -1d, 0d, 75, "", false, Tipos.Relic, "U-235", "Bomb damage is proportional to the number of carried bombs, +5 bombs"),
                new Efectos(Resources.Status_Effect_Gecko_Blast, "24d2d4296fb546739f8dec5a708a766a", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Gecko Blast", "Bomb blasts attract items"),
                new Efectos(Resources.Status_Effect_Capture_Sphere, "a5c75abdd69e4551bb75b9eb826ea5fc", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Capture Sphere", "Bomb kills permanently increase bomb damage"),
                new Efectos(Resources.Status_Effect_Short_Wicks, "99576d2061ce4c4fafc0e2d063c774ac", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Short Wicks", "Reduces the bomb cooldown"),
                new Efectos(Resources.Status_Effect_Guantes, "987e35491ec54376b57a907882b2796e", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Guantes", "Increase throw damage"),
                new Efectos(Resources.Status_Effect_Throwing_Star, "fc255814eb7648559a3aec7b0f4ea2cb", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Throwing Star", "Thrown pickaxe penetrates enemies and objects"),
                new Efectos(Resources.Status_Effect_Phantasmal_Axe, "45456369fa044e2abb115898f060f7be", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Phantasmal Axe", "Thrown pickaxes duplicate themselves"),
                new Efectos(Resources.Status_Effect_Bottled_Lightning, "bd762e07df9a4f39a012ebb9636696fb", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Bottled Lightning", "Chance on hit to chain lightning"),
                new Efectos(Resources.Status_Effect_Salamander_Tail, "2fbcef2fb8174cccae5c29c44ad3259a", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Salamander Tail", "Chance on hit to ignite your enemies"),
                new Efectos(Resources.Status_Effect_Crippling_Poison, "04065cb0940445b9912492f2b13b90f2", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Crippling Poison", "Chance on hit to poison your enemies"),
                new Efectos(Resources.Status_Effect_Caustic_Vial, "7717c6fd23084fba93cd27a78ff25265", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Caustic Vial", "Enemies hit with the thrown pickaxe explode on death"),
                new Efectos(Resources.Status_Effect_Cracked_Orb, "652ca70944e048d6b96c3f6d8c2d52f3", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Cracked Orb", "Enemies hit with the thrown pickaxe take more damage"),
                new Efectos(Resources.Status_Effect_Ursine_Ring, "89e440d42a2f44ef8d4bf99f8fd6bcf7", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Ursine Ring", "Increase health"),
                new Efectos(Resources.Status_Effect_Demon_Ring, "327964741a6449fda89b95efc756b68a", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Demon Ring", "Increase swing damage"),
                new Efectos(Resources.Status_Effect_Mediocre_Ring, "49e3d5e06fbb452a99f239d9cfbfb017", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Mediocre Ring", "Increases health and damage"),
                new Efectos(Resources.Status_Effect_Berserker_s_Pendant, "d7aa255768cc4c37ab24ec06a33eafcd", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Berserker's Pendant", "Deal more swing damage at low health"),
                new Efectos(Resources.Status_Effect_Axe_Thrower_s_Pendant, "7ae45ec37d694a3aa2879323e95cd120", 0, -1d, 0d, 22, "", false, Tipos.Relic, "Axe Thrower's Pendant", "Deal more throw damage at low health"),
                new Efectos(Resources.Status_Effect_Knight_s_Pendant, "238da59625f047b2b8e6e145b00352d1", 0, -1d, 0d, 1, "", false, Tipos.Relic, "Knight's Pendant", "Deal more swing damage at high health"),
                new Efectos(Resources.Status_Effect_Archer_s_Pendant, "b18a27c35366424a9a8ad87ddbafbce4", 0, -1d, 0d, 8, "", false, Tipos.Relic, "Archer's Pendant", "Deal more throw damage at high health"),
                new Efectos(Resources.Status_Effect_Iron_Branch, "73efa79907614717bf14bc6a211a9fee", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Iron Branch", "Increase health, swing damage, and attack speed"),
                new Efectos(Resources.Status_Effect_Queen_s_Crown, "0d5fcfc5097e451bb557c422c428b68e", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Queen's Crown", "Increase throw damage, swing size, and swing speed"),
                new Efectos(Resources.Status_Effect_King_s_Crown, "b6201c6ad35f4744ad35061cb4280261", 0, -1d, 0d, 0, "", false, Tipos.Relic, "King's Crown", "Increase swing damage, throw size, and throw speed"),
                new Efectos(Resources.Status_Effect_Emperor_s_Crown, "46b55b2a7e3f4b9aabcbd60b84ea4efd", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Emperor's Crown", "Increases stats and receive a blessing"),
                new Efectos(Resources.Status_Effect_Pilfer_Ring, "45f79621ee194b0e96bf9ab0546e1700", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Pilfer Ring", "Soak up gold and get a discount at the shop"),
                new Efectos(Resources.Status_Effect_Unstable_Concoction, "19c4b7a2790c443685db5dcf0a13d519", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Unstable Concoction", "Gold hits the floor with explosive force"),
                new Efectos(Resources.Status_Effect_Gold_Tooth, "8224a7c9424b4507bbc53ff780d8a6f2", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Gold Tooth", "Sustain yourself on gold"),
                new Efectos(Resources.Status_Effect_Golden_Delicious, "ab10cd3daa364654b75227b35b543a9c", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Golden Delicious", "All food is golden and extra delicious"),
                new Efectos(Resources.Status_Effect_Conductor, "54fbbe48844b4acdb0d4d1f73baef293", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Conductor", "Electrify your enemies when you pick up gold"),
                new Efectos(Resources.Status_Effect_Gold_Frenzy, "2c17d50b674043d192024669af343b57", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Gold Frenzy", "Gain temporary damage when picking up gold"),
                new Efectos(Resources.Status_Effect_Intensifier, "b171647c3ba540309b6b64371c3a5519", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Intensifier", "Increases damage when killing enemies"),
                new Efectos(Resources.Status_Effect_Floating_Skull, "66c0fc8f90654bc988b8604d9b72c18a", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Floating Skull", "A shiled that blocks projectiles, most of the time"),
                new Efectos(Resources.Status_Effect_Grimhilde_s_Mirror, "ec2ed98f555049e68d8268e0ed10de73", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Grimhilde's Mirror", "Return projectiles with an attack"),
                new Efectos(Resources.Status_Effect_Mirror_Shield, "b63fbdf8d12b4c99bdb3753cf011be10", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Mirror Shield", "Automatically reflect projectiles"),
                new Efectos(Resources.Status_Effect_Hungry_Ghost, "afd75994728e4730b45063a8835ea07b", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Hungry Ghost", "Leach the life of your enemies"),
                new Efectos(Resources.Status_Effect_Nullstone, "472e2993550c48698025988aeb648f45", 0, -1d, 0d, 0, "", false, Tipos.Demon_relic, "Nullstone", "Block a hit once in a while"),
                new Efectos(Resources.Status_Effect_Mushroom, "dc0312a890fd4b2393e7e4db91be8240", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Mushroom", "Gain maximum health when killing enemies"),
                new Efectos(Resources.Status_Effect_Four_Leaf_Clover, "b4ed3b0c8b2f448382d52ca31c37f41b", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Four Leaf Clover", "Enemies drop gold when killed"),
                new Efectos(Resources.Status_Effect_Tent, "3396650b106648d98750b5ca54c3222a", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Tent", "Adds a tent to the starting room of a floor, one use only"),
                new Efectos(Resources.Status_Effect_Aegis, "b8a22809f913430cbcfd179d9e2cf9dc", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Aegis", "Increase defense at critical health"),
                new Efectos(Resources.Status_Effect_Map, "4bad89d3c3554d42859efa3c0b8d23e9", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Map", "Reveals secret rooms"),
                new Efectos(Resources.Status_Effect_Petrified_Rock, "507d1a7cbf21475cb3f480b9ef8c58bc", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Petrified Rock", "Increase drop rate of items from rocks"),
                new Efectos(Resources.Status_Effect_Cosmic_Egg, "34877701997b45e98173ff5bd40862f7", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Cosmic Egg", "Increases experience gain for a familiar"),
                new Efectos(Resources.Status_Effect_Birthing_Pod, "c1a1ef3e9e9e4856b4df14b2662d56dd", 0, -1d, 0d, 500, "", false, Tipos.Relic, "Birthing Pod", "Consumes all healing until birth"),
                new Efectos(Resources.Status_Effect_Karmic_Scale, "d874ff07c3bb4feba128dc6e44f677ad", 0, -1d, 0d, 0, "", false, Tipos.Demon_relic, "Karmic Scale", "Health, damage, and healing become small and even"),
                new Efectos(Resources.Status_Effect_Pocket_of_Holding, "22b6e3deaece497ba1ea1e8233516878", 0, -1d, 0d, 0, "", false, Tipos.Demon_relic, "Pocket of Holding", "Get some temporary bombs each room"),
                new Efectos(Resources.Status_Effect_Lockpick, "5e209720547a49d38cbdbfe623b9af06", 0, -1d, 0d, 0, "", false, Tipos.Demon_relic, "Lockpick", "Open locks for free, but for how long?"),
                new Efectos(Resources.Status_Effect_Lucky_Charm, "3fe862f303f84124a3ca55f026398e50", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Lucky Charm", "A chance at avoiding death"),
                new Efectos(Resources.Status_Effect_Lucky_Lockpick, "5459c455b9ec49d8a04b071a8f172756", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Lucky Lockpick", "A chance to not die, and no chance to break"),
                new Efectos(Resources.Status_Effect_Inverter, "de0689dc68194f00bfb324e0d39e3590", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Inverter", "Lost curses become blessings"),
                new Efectos(Resources.Status_Effect_Recycler, "7865ea1123c44dfeb47c60038795bf6a", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Recycler", "Destroy empty chests for items"),
                new Efectos(Resources.Status_Effect_Fan_of_Knives, "c23a5b7a7dd34f8f9b075ce13f6a155b", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Fan of Knives", "Sometimes release a spray of daggers when you throw"),
                new Efectos(Resources.Status_Effect_Kurtz__Stache, "26eb8914c8cd43b39d1f5bad52b4a0f8", 0, -1d, 0d, 8, "", false, Tipos.Relic, "Kurtz' Stache", "A mysterious box that invites calamity"),
                new Efectos(Resources.Status_Effect_Glass_Cannon, "35de3a889d7e48e3b5c6789afa1d526b", 0, -1d, 0d, 0, "", false, Tipos.Demon_relic, "Glass Cannon", "Increases damage, but decreases maximum health"),
                new Efectos(Resources.Status_Effect_Soul_Cannon, "23552eed8db8460bb556e721d13a17c3", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Soul Cannon", "Fire a projectile while swinging"),
                new Efectos(Resources.Status_Effect_Ara, "14dfd3eea8ea4e2cbddb9c9bfacc35b8", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Ara", "Consumes a future curse after praying"),
                new Efectos(Resources.Status_Effect_Ursa_Major, "328225531e2f43a286177d85fd946512", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Ursa Major", "Increases max health after eating food"),
                new Efectos(Resources.Status_Effect_Canis_Major, "26d64f3c478e45e0bdb8173ff705b37d", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Canis Major", "Crits temporarily increase crit chance"),
                new Efectos(Resources.Status_Effect_Sagitta, "abc8856bc0b64c61973c0f4eb18160e4", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Sagitta", "Enemies explode in arrows after a critical strike"),
                new Efectos(Resources.Status_Effect_Circinus, "633c1aeec47b4378a56f48ea7d965ea0", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Circinus", "Temporarily reveals secret rooms, secret rooms can have secret rooms"),
                new Efectos(Resources.Status_Effect_Hot_Cross_Bun, "3104fc5c2b104138bf0f745111187928", 3, -1d, 0d, 0, "", false, Tipos.Effect, "Hot Cross Bun", "Increases maximum health, very slightly"), // Will only appear this relic from now on.
                // Space for the still unknown status effects IDs... soon I'll find them.
                new Efectos(Resources.Status_Effect_Glaive, "daa208c4561c4a5abd52be778e9dbe31", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Glaive", "Increases throw size"),
                new Efectos(Resources.Status_Effect_Mune, "3e07f594a331417b88fcbbe5637bcdcf", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Mune", "Reduces swing damage and increases throw damage"),
                new Efectos(Resources.Status_Effect_Masamune, "c88199b42a0b4b03b3887b1d453cb1fa", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Masamune", "Instantly kill enemies sometimes"),
                new Efectos(Resources.Status_Effect_Shield_of_Quills, "ea4d3f4689664e8e9729ce8f910c649d", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Shield of Quills", "Gain two points of armor, armor increases damage"),
                new Efectos(Resources.Status_Effect_Hyperstone, "e871507b2b434cde8aaff305d5547daf", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Hyperstone", "Increase attack speed"),
                new Efectos(Resources.Status_Effect_Remote_Detonator, "9174205fec034bcd95a9d2240b8c3ec6", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Remote Detonator", "Detonate bombs when you feel like it"),
                new Efectos(Resources.Status_Effect_Pilfer_Credit_Card_Silver, "21dc94cf49994b75a7965178a19d67dd", 0, -1d, 0d, 0, "", false, Tipos.Othermine_relic, "Pilfer Credit Card Silver", "A 2,500 limit at 0% interest!"),
                new Efectos(Resources.Status_Effect_Pilfer_Credit_Card_Gold, "17f014a742844fd69d56031bdce0ba70", 0, -1d, 0d, 0, "", false, Tipos.Othermine_relic, "Pilfer Credit Card Gold", "A 5,000 limit at 0% interest!"),
                new Efectos(Resources.Status_Effect_Pilfer_Credit_Card_Black_Edition, "814e5f1f467448b8ac2882a85592c888", 0, -1d, 0d, 0, "", false, Tipos.Othermine_relic, "Pilfer Credit Card Black Edition", "7,500 limit and 5% cash back!"),
                new Efectos(Resources.Status_Effect_Four_Leaf_Cleaver, "852a50adacfa447cbc704080ddbec16f", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Four Leaf Cleaver", "Sometimes drop golden meat from your enemies"),
                new Efectos(Resources.Status_Effect_Rat_Bond, "dd02f72712f648e58728305c3ab4dabc", 0, -1d, 0d, 0, "", false, Tipos.Relic, "Rat Bond", "Become charming to rats"),
                new Efectos(Resources.Status_Effect_Siegfried_s_Aegis, "8697d8f359de429e88b743f8b07e973e", 0, -1d, 0d, 0, "", false, Tipos.Othermine_relic, "Siegfried's Aegis", "Inflicts pain on the bearer"),
                new Efectos(Resources.Status_Effect_Paladin_Shield, "7409d82802cb4b7d8d65d1dbe5d1f1b7", 0, -1d, 0d, 0, "", false, Tipos.Othermine_relic, "Paladin Shield", "Increases stats"),
                //new Efectos("", 0, -1d, 0d, 0, "", false, Tipos_Efecto.Relic, "", ""),
                new Efectos(Resources.Status_Effect_Warp_Pipe, "fb73f3e50c7e4bc797f66f25b4a4dc03", 0, -1d, 0d, 0, "", false, Tipos.UnderMod_relic, "Warp Pipe", "Warps you back to the beginning"), // "Warps you to back to the beginning"
                new Efectos(Resources.Status_Effect_Time_Capsule, "fb645e7e74564d2aa0e57e7ec8205436", 0, -1d, 0d, 0, "", false, Tipos.UnderMod_relic, "Time Capsule", "Leave a random item behind for a future adventurer"),
                new Efectos(Resources.Status_Effect_Bag_Lunch, "b9cf8da2c0ba40ec81c8ed7693d39ca7", 0, -1d, 0d, 0, "", false, Tipos.UnderMod_relic, "Bag Lunch", "Refill your health on entering a new zone"),
                new Efectos(Resources.Status_Effect_Blank_Map, "4b50fe29c8ed487e9bfb5097a85364e2", 0, -1d, 0d, 0, "", false, Tipos.UnderMod_relic, "Blank Map", "Get paid for each new room you discover"),
                new Efectos(Resources.Status_Effect_Cursed_Wax, "cdceafb210174007b37e408165932804", 0, -1d, 0d, 0, "", false, Tipos.UnderMod_relic, "Cursed Wax", "Heals you significantly on gaining a curse"),
                new Efectos(Resources.Status_Effect_Cursifier, "fa8e0d3c0e4940e2bf055f71c1eefbd9", 0, -1d, 0d, 0, "", false, Tipos.UnderMod_relic, "Cursifier", "Whenever you gain a curse, gain a second curse!"),
                new Efectos(Resources.Status_Effect_Last_Will, "934de68da02c4810b97b6b005727340f", 0, -1d, 0d, 0, "", false, Tipos.UnderMod_relic, "Last Will", "Choose a relic to leave to your next of kin"),
                new Efectos(Resources.Status_Effect_Pocket_Furnace, "527315f36aeb4187a3490c487584ead4", 0, -1d, 0d, 0, "", false, Tipos.UnderMod_relic, "Pocket Furnace", "Chance of smelting gold drops into bars which are worth more"),
                new Efectos(Resources.Status_Effect_Relic_Catalog, "77b17849cce447a89aed6800e2de217f", 0, -1d, 0d, 0, "", false, Tipos.UnderMod_relic, "Relic Catalog", "Choose from up to 10 relics instantly!"),
                new Efectos(Resources.Status_Effect_Antique_Coupon, "4cb3572d3b3943e4a30c2721b45e7a5c", 0, -1d, 0d, 0, "", false, Tipos.UnderMod_relic, "Antique Coupon", "Shops will try to stock more relics"),
                //new Efectos(Resources.Status_Effect_, "", 0, -1d, 0d, 0, "", false, Tipos_Efecto.UnderMod_relic, "", ""),
            };
        }

        /// <summary>
        /// Structure that holds up all the information about a peasant sex.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Sexos
        {
            internal string Id;
            internal string Nombre;

            internal Sexos(string Id,
                string Nombre)
            {
                this.Id = Id;
                this.Nombre = Nombre;
            }

            internal static int Buscar_Índice(string ID)
            {
                try
                {
                    if (ID != null)
                    {
                        for (int Índice = 0; Índice < Matriz.Length; Índice++)
                        {
                            try
                            {
                                if (Matriz[Índice].Id != null &&
                                    string.Compare(ID, Matriz[Índice].Id, true) == 0)
                                {
                                    return Índice;
                                }
                            }
                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                        }
                    }
                }
                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                return 0;
            }

            internal static Sexos[] Matriz = new Sexos[]
            {
                new Sexos("470fadeafb964c6f99ed7edb3cbcfa50", "Man"),
                new Sexos("12978e83e73548c89a2aba4d008ad5d0", "Woman"),
            };
        }

        /// <summary>
        /// Structure that holds up all the information about a zone.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        internal struct Zonas
        {
            internal Bitmap Imagen;
            internal string Id;
            internal string Nombre;

            internal Zonas(Bitmap Imagen,
                string Id,
                string Nombre)
            {
                this.Imagen = Imagen;
                this.Id = Id;
                this.Nombre = Nombre;
            }

            internal static int Buscar_Índice(string ID)
            {
                try
                {
                    if (ID != null)
                    {
                        for (int Índice = 0; Índice < Matriz.Length; Índice++)
                        {
                            try
                            {
                                if (Matriz[Índice].Id != null &&
                                    string.Compare(ID, Matriz[Índice].Id, true) == 0)
                                {
                                    return Índice + 1;
                                }
                            }
                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                        }
                    }
                }
                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); }
                return 0;
            }

            internal static Zonas[] Matriz = new Zonas[]
            {
                // UnderMine zones.
                new Zonas(Resources.Artifact_Journal, "219d813ae07049b39d1bf35f1863c2b1", "The Hub"),
                //new Zonas(Resources.Status_Effect_Blank_Map, "66b7a475397c4b4bba63b7f358b96544", "Tutorial level"),
                new Zonas(Resources.Status_Effect_Map, "140bfdfffc514544a0ec4f26bc794ebd", "The Goldmine 1"),
                new Zonas(Resources.Status_Effect_Map, "9884c68e242845de834006fac97fb6f5", "The Goldmine 2"), // ed75700775b84d79a57f419e2fea0901.
                new Zonas(Resources.Status_Effect_Map, "5d7520790973458bb9787fdee63f46c7", "The Goldmine 3"),
                new Zonas(Resources.Status_Effect_Map, "4be6ef1124eb4143af8d82340c94c1ab", "The Goldmine 4"),
                new Zonas(Resources.Artifact_Dungeon_Map, "1d7996a9e48c4dca8eae173e5a813a8a", "Delvemore Dungeon 1"),
                new Zonas(Resources.Artifact_Dungeon_Map, "c5526cf2734b44c0a94c76b4aed1ca5c", "Delvemore Dungeon 2"),
                new Zonas(Resources.Artifact_Dungeon_Map, "ebb7ead9b7c44bb492b127f313384abf", "Delvemore Dungeon 3"),
                new Zonas(Resources.Artifact_Dungeon_Map, "ca942964baaf4706804811b72564fab4", "Delvemore Dungeon 4"),
                new Zonas(Resources.Artifact_Halls_Map, "1fc45e2294dd48d687dfdad097e45079", "Halls of Din 1"),
                new Zonas(Resources.Artifact_Halls_Map, "9784537ad4df4360b339b1ec975784c2", "Halls of Din 2"),
                new Zonas(Resources.Artifact_Halls_Map, "e5a535a84cc2426bb7df652a4e0387af", "Halls of Din 3"),
                new Zonas(Resources.Artifact_Halls_Map, "81d8ae6de9d342aea90bdf02290f7d6c", "Halls of Din 4"),
                new Zonas(Resources.Artifact_Caverns_Map, "0fc8f9f6d16a4ba1ad3c302b8d441612", "Shimmering Caverns 1"),
                new Zonas(Resources.Artifact_Caverns_Map, "eeb127c8af8c420e8efb95b2829a6b44", "Shimmering Caverns 2"),
                new Zonas(Resources.Artifact_Caverns_Map, "49fa99a921f34a5eba3a0cea512123d2", "Shimmering Caverns 3"),
                new Zonas(Resources.Artifact_Caverns_Map, "7501f821dbac445086a6cff9fc3be938", "Shimmering Caverns 4"),
                new Zonas(Resources.Artifact_Core_Map, "52391b95eb1c4383827502e8dcf85162", "Golden Core 1"),
                new Zonas(Resources.Artifact_Core_Map, "7d2b512fb4464322a2dc55a998c1416c", "Golden Core 2"),
                new Zonas(Resources.Artifact_Core_Map, "7acab685bef24dffbab75293671bd647", "Golden Core 3"),
                new Zonas(Resources.Artifact_Core_Map, "ee56edd8a02e4988aeb7fad9e14e675f", "Golden Core 4"),
                new Zonas(Resources.Artifact_Dusty_Book, "6ee542375a4b413a8117dc3a6dcec330", "Nowhere, Death's Hand"),
                // Othermine zones.
                new Zonas(Resources.Artifact_Journal, "39e587ef6fc647a298c8db8c958c03f6", "Othermine Antechamber"),
                new Zonas(Resources.Status_Effect_Last_Will, "04649afbd838476b9cf9f1a31ffebb78", "Othermine 1"), // Delvemore Dungeon = a6752d54e0ae401885fce21de127fb63.
                new Zonas(Resources.Status_Effect_Last_Will, "c21e0ccc666d46b982b59b8dc80d1a43", "Othermine 2"),
                new Zonas(Resources.Status_Effect_Last_Will, "40920041408e4325a37e8e753d1e8af9", "Othermine 3"),
                new Zonas(Resources.Status_Effect_Uncreative, "f916a4f98137452da344cbad29fe73f1", "Othermine Antechamber 3"),
                new Zonas(Resources.Status_Effect_Last_Will, "06c400ee8cfa454ea0257de9f5645c97", "Othermine 4"),
                new Zonas(Resources.Status_Effect_Last_Will, "fc2ab1e374344c6998f8d86ae10297c7", "Othermine 5"),
                new Zonas(Resources.Status_Effect_Last_Will, "e62c54f06a454461905f068843e295e6", "Othermine 6"),
                new Zonas(Resources.Status_Effect_Uncreative, "785c30040e6947a3a3df387ea3b95f98", "Othermine Antechamber 6"),
                new Zonas(Resources.Status_Effect_Last_Will, "cedb9bc2987d4d8cb8851439267fdfa5", "Othermine 7"),
                new Zonas(Resources.Status_Effect_Last_Will, "0b911a24a97047888d58a3aea7be4e46", "Othermine 8"),
                new Zonas(Resources.Status_Effect_Last_Will, "15efc9b7db5143988cee32f0558419f8", "Othermine 9"),
                new Zonas(Resources.Status_Effect_Uncreative, "f2cfd35612c74feba4d9b48eaddc73fd", "Othermine Antechamber 9"),
                new Zonas(Resources.Status_Effect_Last_Will, "5f7f88b1c4cd40eb867b1932bacd5494", "Othermine 10"),
                new Zonas(Resources.Status_Effect_Last_Will, "0bd63d985f6f4580aa9dd9e940efa6d2", "Othermine 11"),
                new Zonas(Resources.Status_Effect_Last_Will, "b30244515fff4cbda3d1d4a73e195202", "Othermine 12"),
                new Zonas(Resources.Artifact_Dusty_Book, "9a85b2fffc684e00b2f6c26f9dd92dde", "Othermine Nowhere, Death's Hand")
            };
        }

        internal readonly string Texto_Título = "UnderMine Tools by Jupisoft for " + Program.Texto_Usuario;
        internal bool Variable_Excepción = false;
        internal bool Variable_Excepción_Imagen = false;
        internal int Variable_Excepción_Total = 0;
        internal bool Variable_Memoria = false;
        internal static Stopwatch FPS_Cronómetro = Stopwatch.StartNew();
        internal long FPS_Segundo_Anterior = 0L;
        internal long FPS_Temporal = 0L;
        internal long FPS_Real = 0L;
        /// <summary>
        /// Variable that if it's true will always show the main window on top of others.
        /// </summary>
        internal static bool Variable_Siempre_Visible = false;
        internal static readonly string Ruta_Backups = Application.StartupPath + "\\Backups";
        internal static readonly string Ruta_Saves = Application.StartupPath + "\\Saves";
        internal Dictionary<int, int[]> Diccionario_Índices_Combinaciones = null;
        internal static List<int> Lista_Colores = null;
        internal byte[] Matriz_Bytes_Base_64 = null;
        internal bool Othermine_Cargado = false;

        /// <summary>
        /// Function designed to store all the unique "PeonColor" found.
        /// So far it seems that the first byte (most left) is unused.
        /// The second byte only has it's last 3 bits used (0 to 7).
        /// The third byte also only has it's last 3 bits used (0 to 7).
        /// And the fourth byte only has it's last 4 bits used (0 to 15).
        /// Eyes colors found so far: from 0 to 7.
        /// Hair colors found so far: 0 to 7.
        /// Clothes colors found so far: 0 to 11.
        /// </summary>
        /// <param name="Valor_Nuevo">The new "PeonColor" found. The function will look if it's already stored or not.</param>
        internal void Actualizar_Lista_Colores(int Valor_Nuevo)
        {
            try
            {
                // First byte (most left): Unused?
                // Second byte: Eyes color.
                // Third byte: Hair color.
                // Fourth byte (most right): Clothes color.
                Lista_Colores = new List<int>();
                string Ruta_Color = Application.StartupPath + "\\z Color.txt";
                FileStream Lector_Entrada = new FileStream(Ruta_Color, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                Lector_Entrada.Seek(0L, SeekOrigin.Begin);
                StreamReader Lector_Texto_Entrada = new StreamReader(Lector_Entrada, Encoding.UTF8);
                while (!Lector_Texto_Entrada.EndOfStream)
                {
                    try
                    {
                        string Línea = Lector_Texto_Entrada.ReadLine();
                        if (!string.IsNullOrEmpty(Línea))
                        {
                            int Valor = int.Parse(Línea.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0]);
                            if (!Lista_Colores.Contains(Valor))
                            {
                                Lista_Colores.Add(Valor);
                            }
                        }
                    }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
                Lector_Texto_Entrada.Close();
                Lector_Texto_Entrada.Dispose();
                Lector_Texto_Entrada = null;
                Lector_Entrada.Close();
                Lector_Entrada.Dispose();
                Lector_Entrada = null;
                if (!Lista_Colores.Contains(Valor_Nuevo))
                {
                    Lista_Colores.Add(Valor_Nuevo);
                    SystemSounds.Asterisk.Play(); // New color value found.
                }
                if (Lista_Colores.Count > 1) Lista_Colores.Sort();
                FileStream Lector_Salida = new FileStream(Ruta_Color, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                Lector_Salida.SetLength(0L);
                Lector_Salida.Seek(0L, SeekOrigin.Begin);
                StreamWriter Lector_Texto_Salida = new StreamWriter(Lector_Salida, Encoding.UTF8);
                foreach (int Valor in Lista_Colores)
                {
                    try
                    {
                        int Eyes = (Valor >> 16) & 255;
                        int Hair = (Valor >> 8) & 255;
                        int Clothes = Valor & 255;

                        string Texto_Valor = Valor.ToString();
                        while (Texto_Valor.Length < 10) Texto_Valor = '0' + Texto_Valor;

                        string Texto_Binario = Convert.ToString(Valor, 2);
                        while (Texto_Binario.Length < 32) Texto_Binario = '0' + Texto_Binario;
                        List<char> Lista_Caracteres = new List<char>(Texto_Binario.ToCharArray());
                        for (int Índice = 28; Índice >= 4; Índice -= 4)
                        {
                            try
                            {
                                Lista_Caracteres.Insert(Índice, ' ');
                            }
                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                        Lector_Texto_Salida.WriteLine(Texto_Valor + " = " + new string(Lista_Caracteres.ToArray()) + " = " + Eyes.ToString() + ", " + Hair.ToString() + ", " + Clothes.ToString());
                        Lector_Texto_Salida.Flush();
                    }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
                Lector_Texto_Salida.Close();
                Lector_Texto_Salida.Dispose();
                Lector_Texto_Salida = null;
                Lector_Salida.Close();
                Lector_Salida.Dispose();
                Lector_Salida = null;
                Ruta_Color = null;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Recortar_Imágenes_Peasant()
        {
            try
            {
                /*string Ruta_64x = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\64x";
                string[] Matriz_Rutas_64x = Directory.GetFiles(Ruta_64x, "*.png", SearchOption.TopDirectoryOnly);
                foreach (string Ruta in Matriz_Rutas_64x)
                {
                    Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                    int Ancho = Imagen.Width;
                    int Alto = Imagen.Height;
                    Bitmap Imagen_64x = new Bitmap(64, 64, PixelFormat.Format32bppArgb);
                    Graphics Pintar_64x = Graphics.FromImage(Imagen_64x);
                    Pintar_64x.CompositingMode = CompositingMode.SourceCopy;
                    Pintar_64x.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar_64x.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Pintar_64x.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar_64x.SmoothingMode = SmoothingMode.None;
                    Pintar_64x.TextRenderingHint = TextRenderingHint.AntiAlias;
                    Pintar_64x.DrawImage(Imagen, new Rectangle(1, 1, 62, 62), new Rectangle(0, 0, 62, 62), GraphicsUnit.Pixel);
                    Pintar_64x.Dispose();
                    Pintar_64x = null;
                    Imagen_64x.Save(Ruta, ImageFormat.Png);
                }
                return;*/
                string Ruta_Peasant = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\UnderMine\\Peasant";

                Bitmap Imagen_Man_Clothes = Program.Cargar_Imagen_Ruta(Path.GetDirectoryName(Ruta_Peasant) + "\\Peasant_Man_Clothes.png", CheckState.Checked);
                Bitmap Imagen_Man_Eyes = Program.Cargar_Imagen_Ruta(Path.GetDirectoryName(Ruta_Peasant) + "\\Peasant_Man_Eyes.png", CheckState.Checked);
                Bitmap Imagen_Man_Hair = Program.Cargar_Imagen_Ruta(Path.GetDirectoryName(Ruta_Peasant) + "\\Peasant_Man_Hair.png", CheckState.Checked);

                //Bitmap Imagen_Woman_Clothes = Program.Cargar_Imagen_Ruta(Path.GetDirectoryName(Ruta_Peasant) + "\\Peasant_Woman_Clothes.png", CheckState.Checked);
                //Bitmap Imagen_Woman_Eyes = Program.Cargar_Imagen_Ruta(Path.GetDirectoryName(Ruta_Peasant) + "\\Peasant_Woman_Eyes.png", CheckState.Checked);
                //Bitmap Imagen_Woman_Hair = Program.Cargar_Imagen_Ruta(Path.GetDirectoryName(Ruta_Peasant) + "\\Peasant_Woman_Hair.png", CheckState.Checked);

                Bitmap Imagen_Máscara = Imagen_Man_Hair;
                int Ancho_Máscara = Imagen_Máscara.Width;
                int Alto_Máscara = Imagen_Máscara.Height;
                BitmapData Bitmap_Data_Máscara = Imagen_Máscara.LockBits(new Rectangle(0, 0, Ancho_Máscara, Alto_Máscara), ImageLockMode.ReadOnly, Imagen_Máscara.PixelFormat);
                byte[] Matriz_Bytes_Máscara = new byte[Math.Abs(Bitmap_Data_Máscara.Stride) * Alto_Máscara];
                Marshal.Copy(Bitmap_Data_Máscara.Scan0, Matriz_Bytes_Máscara, 0, Matriz_Bytes_Máscara.Length);
                Imagen_Máscara.UnlockBits(Bitmap_Data_Máscara);
                Bitmap_Data_Máscara = null;

                string[] Matriz_Rutas = Directory.GetFiles(Ruta_Peasant, "*.png", SearchOption.TopDirectoryOnly);
                foreach (string Ruta in Matriz_Rutas)
                {
                    try
                    {
                        if (Ruta.Contains("_Man_Hair"))
                        {
                            Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                            int Ancho = Imagen.Width;
                            int Alto = Imagen.Height;
                            BitmapData Bitmap_Data = Imagen.LockBits(new Rectangle(0, 0, Ancho, Alto), ImageLockMode.ReadWrite, Imagen.PixelFormat);
                            byte[] Matriz_Bytes = new byte[Math.Abs(Bitmap_Data.Stride) * Alto];
                            Marshal.Copy(Bitmap_Data.Scan0, Matriz_Bytes, 0, Matriz_Bytes.Length);
                            int Bytes_Aumento = Image.IsAlphaPixelFormat(Imagen.PixelFormat) ? 4 : 3;
                            int Bytes_Diferencia = Math.Abs(Bitmap_Data.Stride) - ((Ancho * Image.GetPixelFormatSize(Imagen.PixelFormat)) / 8);
                            for (int Y = 0, Índice_Byte = 0; Y < Alto; Y++, Índice_Byte += Bytes_Diferencia)
                            {
                                for (int X = 0; X < Ancho; X++, Índice_Byte += Bytes_Aumento)
                                {
                                    if (Matriz_Bytes_Máscara[Índice_Byte + 3] <= 0) // Clear this pixel.
                                    {
                                        Matriz_Bytes[Índice_Byte + 3] = 0;
                                        Matriz_Bytes[Índice_Byte + 2] = 0;
                                        Matriz_Bytes[Índice_Byte + 1] = 0;
                                        Matriz_Bytes[Índice_Byte + 0] = 0;
                                    }
                                }
                            }
                            Marshal.Copy(Matriz_Bytes, 0, Bitmap_Data.Scan0, Matriz_Bytes.Length);
                            Imagen.UnlockBits(Bitmap_Data);
                            Bitmap_Data = null;
                            Imagen.Save(Application.StartupPath + "\\Saves\\" + Path.GetFileNameWithoutExtension(Ruta) + ".png", ImageFormat.Png);
                            Matriz_Bytes = null;
                            Imagen.Dispose();
                            Imagen = null;
                        }
                    }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal Bitmap Crear_Imagen_Peasant(bool Recortar)
        {
            try
            {
                Bitmap Imagen = null;
                Bitmap Imagen_Clothes = null;
                Bitmap Imagen_Eyes = null;
                Bitmap Imagen_Hair = null;
                Bitmap Imagen_Skin = null;
                if (!CheckBox_Peasant.Checked)
                {
                    if (ComboBox_Sexo.SelectedIndex == 1)
                    {
                        Imagen = Resources.Peasant_Woman_Background;
                        Imagen_Clothes = Resources.ResourceManager.GetObject("Peasant_Woman_Clothes_" + Math.Min((int)NumericUpDown_Clothes.Value, 12).ToString()) as Bitmap;
                        Imagen_Eyes = Resources.ResourceManager.GetObject("Peasant_Woman_Eyes_" + Math.Min((int)NumericUpDown_Eyes.Value, 8).ToString()) as Bitmap;
                        Imagen_Hair = Resources.ResourceManager.GetObject("Peasant_Woman_Hair_" + Math.Min(((int)NumericUpDown_Skin.Value << 1) + (int)NumericUpDown_Hair.Value, 8).ToString()) as Bitmap;
                        Imagen_Skin = Resources.ResourceManager.GetObject("Peasant_Woman_Skin_" + Math.Min((int)NumericUpDown_Skin.Value, 4).ToString()) as Bitmap;
                    }
                    else
                    {
                        Imagen = Resources.Peasant_Man_Background;
                        Imagen_Clothes = Resources.ResourceManager.GetObject("Peasant_Man_Clothes_" + Math.Min((int)NumericUpDown_Clothes.Value, 12).ToString()) as Bitmap;
                        Imagen_Eyes = Resources.ResourceManager.GetObject("Peasant_Man_Eyes_" + Math.Min((int)NumericUpDown_Eyes.Value, 8).ToString()) as Bitmap;
                        Imagen_Hair = Resources.ResourceManager.GetObject("Peasant_Man_Hair_" + Math.Min(((int)NumericUpDown_Skin.Value << 1) + (int)NumericUpDown_Hair.Value, 8).ToString()) as Bitmap;
                        Imagen_Skin = Resources.ResourceManager.GetObject("Peasant_Man_Skin_" + Math.Min((int)NumericUpDown_Skin.Value, 4).ToString()) as Bitmap;
                    }
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceOver;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.HighQuality;
                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                    if (Imagen_Clothes != null)
                    {
                        PictureBox_Clothes.Image = Program.Obtener_Imagen_Miniatura(Imagen_Clothes.Clone(new Rectangle(3, 3, 58, 58), PixelFormat.Format32bppArgb), 18, 18, true, false, CheckState.Checked);
                        Pintar.DrawImage(Imagen_Clothes, new Rectangle(0, 0, 62, 62), new Rectangle(0, 0, 62, 62), GraphicsUnit.Pixel);
                        Imagen_Clothes.Dispose();
                        Imagen_Clothes = null;
                    }
                    else PictureBox_Clothes.Image = null;
                    if (Imagen_Eyes != null)
                    {
                        PictureBox_Eyes.Image = Program.Obtener_Imagen_Miniatura(Imagen_Eyes.Clone(new Rectangle(3, 3, 58, 58), PixelFormat.Format32bppArgb), 18, 18, true, false, CheckState.Checked);
                        Pintar.DrawImage(Imagen_Eyes, new Rectangle(0, 0, 62, 62), new Rectangle(0, 0, 62, 62), GraphicsUnit.Pixel);
                        Imagen_Eyes.Dispose();
                        Imagen_Eyes = null;
                    }
                    else PictureBox_Eyes.Image = null;
                    if (Imagen_Hair != null)
                    {
                        PictureBox_Hair.Image = Program.Obtener_Imagen_Miniatura(Imagen_Hair.Clone(new Rectangle(3, 3, 58, 58), PixelFormat.Format32bppArgb), 18, 18, true, false, CheckState.Checked);
                        Pintar.DrawImage(Imagen_Hair, new Rectangle(0, 0, 62, 62), new Rectangle(0, 0, 62, 62), GraphicsUnit.Pixel);
                        Imagen_Hair.Dispose();
                        Imagen_Hair = null;
                    }
                    else PictureBox_Hair.Image = null;
                    if (Imagen_Skin != null)
                    {
                        PictureBox_Skin.Image = Program.Obtener_Imagen_Miniatura(Imagen_Skin.Clone(new Rectangle(3, 3, 58, 58), PixelFormat.Format32bppArgb), 18, 18, true, false, CheckState.Checked);
                        Pintar.DrawImage(Imagen_Skin, new Rectangle(0, 0, 62, 62), new Rectangle(0, 0, 62, 62), GraphicsUnit.Pixel);
                        Imagen_Skin.Dispose();
                        Imagen_Skin = null;
                    }
                    else PictureBox_Skin.Image = null;
                    Pintar.Dispose();
                    Pintar = null;
                    if (Recortar)
                    {
                        Imagen = Program.Obtener_Imagen_Miniatura(Imagen.Clone(new Rectangle(1, 1, 62, 62), Imagen.PixelFormat), 124, 124, true, false, CheckState.Checked);
                    }
                }
                else
                {
                    if (ComboBox_Sexo.SelectedIndex == 1)
                    {
                        Imagen = Resources.Full_Peasant_Woman_Background;
                        Imagen_Clothes = Resources.ResourceManager.GetObject("Full_Peasant_Woman_Clothes_" + Math.Min((int)NumericUpDown_Clothes.Value, 12).ToString()) as Bitmap;
                        Imagen_Eyes = Resources.ResourceManager.GetObject("Full_Peasant_Woman_Eyes_" + Math.Min((int)NumericUpDown_Eyes.Value, 8).ToString()) as Bitmap;
                        Imagen_Hair = Resources.ResourceManager.GetObject("Full_Peasant_Woman_Hair_" + Math.Min(((int)NumericUpDown_Skin.Value << 1) + (int)NumericUpDown_Hair.Value, 8).ToString()) as Bitmap;
                        Imagen_Skin = Resources.ResourceManager.GetObject("Full_Peasant_Woman_Skin_" + Math.Min((int)NumericUpDown_Skin.Value, 4).ToString()) as Bitmap;
                    }
                    else
                    {
                        Imagen = Resources.Full_Peasant_Man_Background;
                        Imagen_Clothes = Resources.ResourceManager.GetObject("Full_Peasant_Man_Clothes_" + Math.Min((int)NumericUpDown_Clothes.Value, 12).ToString()) as Bitmap;
                        Imagen_Eyes = Resources.ResourceManager.GetObject("Full_Peasant_Man_Eyes_" + Math.Min((int)NumericUpDown_Eyes.Value, 8).ToString()) as Bitmap;
                        Imagen_Hair = Resources.ResourceManager.GetObject("Full_Peasant_Man_Hair_" + Math.Min(((int)NumericUpDown_Skin.Value << 1) + (int)NumericUpDown_Hair.Value, 8).ToString()) as Bitmap;
                        Imagen_Skin = Resources.ResourceManager.GetObject("Full_Peasant_Man_Skin_" + Math.Min((int)NumericUpDown_Skin.Value, 4).ToString()) as Bitmap;
                    }
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceOver;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.HighQuality;
                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                    if (Imagen_Clothes != null)
                    {
                        PictureBox_Clothes.Image = Program.Obtener_Imagen_Miniatura(Imagen_Clothes, 18, 18, true, false, CheckState.Checked);
                        Pintar.DrawImage(Imagen_Clothes, new Rectangle(0, 0, 130, 130), new Rectangle(0, 0, 130, 130), GraphicsUnit.Pixel);
                        Imagen_Clothes.Dispose();
                        Imagen_Clothes = null;
                    }
                    else PictureBox_Clothes.Image = null;
                    if (Imagen_Eyes != null)
                    {
                        PictureBox_Eyes.Image = Program.Obtener_Imagen_Miniatura(Imagen_Eyes, 18, 18, true, false, CheckState.Checked);
                        Pintar.DrawImage(Imagen_Eyes, new Rectangle(0, 0, 130, 130), new Rectangle(0, 0, 130, 130), GraphicsUnit.Pixel);
                        Imagen_Eyes.Dispose();
                        Imagen_Eyes = null;
                    }
                    else PictureBox_Eyes.Image = null;
                    if (Imagen_Hair != null)
                    {
                        PictureBox_Hair.Image = Program.Obtener_Imagen_Miniatura(Imagen_Hair, 18, 18, true, false, CheckState.Checked);
                        Pintar.DrawImage(Imagen_Hair, new Rectangle(0, 0, 130, 130), new Rectangle(0, 0, 130, 130), GraphicsUnit.Pixel);
                        Imagen_Hair.Dispose();
                        Imagen_Hair = null;
                    }
                    else PictureBox_Hair.Image = null;
                    if (Imagen_Skin != null)
                    {
                        PictureBox_Skin.Image = Program.Obtener_Imagen_Miniatura(Imagen_Skin, 18, 18, true, false, CheckState.Checked);
                        Pintar.DrawImage(Imagen_Skin, new Rectangle(0, 0, 130, 130), new Rectangle(0, 0, 130, 130), GraphicsUnit.Pixel);
                        Imagen_Skin.Dispose();
                        Imagen_Skin = null;
                    }
                    else PictureBox_Skin.Image = null;
                    Pintar.Dispose();
                    Pintar = null;
                    if (Recortar)
                    {
                        //Imagen = Imagen.Clone(new Rectangle(1, 0, 128, 128), Imagen.PixelFormat);
                        Imagen = Imagen.Clone(new Rectangle(3, 0, 124, 124), Imagen.PixelFormat);
                    }
                }
                return Imagen;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return null;
        }

        private void Form_Main_Load(object sender, EventArgs e)
        {
            try
            {
                //Recortar_Imágenes_Peasant();
                // Sorts by type and name the arrays of status effects, familiars and potions.
                // If these 3 sorters aren't called then the arrays will be sorted like in game.
                Array.Sort(Efectos.Matriz, new Comparador_Efectos());
                Array.Sort(Familiares.Matriz, new Comparador_Familiares());
                //Array.Sort(Mejoras_Bomba.Matriz_Mejoras_Bomba, new Comparador_Mejoras_Bomba());
                //Array.Sort(Mejoras_Guantes.Matriz_Mejoras_Guantes, new Comparador_Mejoras_Guantes());
                //Array.Sort(Mejoras_Arma.Matriz_Mejoras_Arma, new Comparador_Mejoras_Arma());
                Array.Sort(Pociones.Matriz, new Comparador_Pociones());

                if (Program.Icono_Jupisoft == null) Program.Icono_Jupisoft = this.Icon.Clone() as Icon;
                this.Text = Texto_Título + " - [" + Program.Texto_UnderMine_Versión + "+, Peasants: 2.106, Man names: " + Program.Traducir_Número(Matriz_Nombres_Hombre_Inicio.Length * Matriz_Nombres_Hombre_Fin.Length) + ", Woman names: " + Program.Traducir_Número(Matriz_Nombres_Mujer_Inicio.Length * Matriz_Nombres_Mujer_Fin.Length) + ", Status effects known: " + Program.Traducir_Número(Efectos.Matriz.Length) + "]";
                Menú_Contextual_Acerca.Text = "About " + Program.Texto_Programa + " " + Program.Texto_Versión + "...";
                this.WindowState = FormWindowState.Maximized;

                // Main settings.
                NumericUpDown_Version.Minimum = int.MinValue;
                NumericUpDown_Version.Maximum = int.MaxValue;
                NumericUpDown_PlayTime.Minimum = -1m;
                NumericUpDown_PlayTime.Maximum = 922337203685m;
                NumericUpDown_PlayTime_ValueChanged(NumericUpDown_PlayTime, EventArgs.Empty);

                // Peasant settings.
                NumericUpDown_PeonColor.Minimum = decimal.MinValue;
                NumericUpDown_PeonColor.Maximum = decimal.MaxValue;
                NumericUpDown_Clothes.Minimum = 0;
                NumericUpDown_Clothes.Maximum = 255;
                NumericUpDown_Eyes.Minimum = 0;
                NumericUpDown_Eyes.Maximum = 255;
                NumericUpDown_Hair.Minimum = 0;
                NumericUpDown_Hair.Maximum = 1;
                NumericUpDown_Skin.Minimum = 0;
                NumericUpDown_Skin.Maximum = 127;
                NumericUpDown_Clothes_ValueChanged(NumericUpDown_Clothes, EventArgs.Empty);
                NumericUpDown_Eyes_ValueChanged(NumericUpDown_Eyes, EventArgs.Empty);
                NumericUpDown_Hair_ValueChanged(NumericUpDown_Hair, EventArgs.Empty);
                NumericUpDown_Skin_ValueChanged(NumericUpDown_Skin, EventArgs.Empty);

                // Familiar settings.
                ComboBox_Familiar_1_ID.Items.Add("");
                ComboBox_Familiar_2_ID.Items.Add("");
                foreach (Familiares Familiar in Familiares.Matriz)
                {
                    try
                    {
                        ComboBox_Familiar_1_ID.Items.Add(Familiar.Nombre + ": " + Familiar.Descripción + " [Maximum XP: " + Program.Traducir_Número(Familiar.Xp) + "]");
                        ComboBox_Familiar_2_ID.Items.Add(Familiar.Nombre + ": " + Familiar.Descripción + " [Maximum XP: " + Program.Traducir_Número(Familiar.Xp) + "]");
                    }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
                NumericUpDown_Familiar_1_XP.Minimum = int.MinValue;
                NumericUpDown_Familiar_1_XP.Maximum = int.MaxValue;
                NumericUpDown_Familiar_2_XP.Minimum = int.MinValue;
                NumericUpDown_Familiar_2_XP.Maximum = int.MaxValue;

                // Unique relic upgrades.
                foreach (Mejoras_Bomba Mejora_Bomba in Mejoras_Bomba.Matriz)
                {
                    try
                    {
                        ComboBox_Bomb.Items.Add(Mejora_Bomba.Nombre + ": " + Mejora_Bomba.Descripción);
                    }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
                foreach (Mejoras_Guantes Mejora_Guantes in Mejoras_Guantes.Matriz)
                {
                    try
                    {
                        ComboBox_Gloves.Items.Add(Mejora_Guantes.Nombre + ": " + Mejora_Guantes.Descripción);
                    }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
                foreach (Mejoras_Arma Mejora_Arma in Mejoras_Arma.Matriz)
                {
                    try
                    {
                        ComboBox_Weapon.Items.Add(Mejora_Arma.Nombre + ": " + Mejora_Arma.Descripción);
                    }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
                foreach (Mejoras_Sombrero Mejora_Sombrero in Mejoras_Sombrero.Matriz)
                {
                    try
                    {
                        ComboBox_Hat.Items.Add(Mejora_Sombrero.Nombre + ": " + Mejora_Sombrero.Descripción);
                    }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
                ComboBox_Bomb.SelectedIndex = 0;
                ComboBox_Gloves.SelectedIndex = 0;
                ComboBox_Weapon.SelectedIndex = 0;
                ComboBox_Hat.SelectedIndex = 0;

                // Level settings.
                NumericUpDown_Seed.Minimum = int.MinValue;
                NumericUpDown_Seed.Maximum = int.MaxValue;
                NumericUpDown_Timer.Minimum = -1m;
                NumericUpDown_Timer.Maximum = 922337203685m;
                NumericUpDown_Timer_ValueChanged(NumericUpDown_Timer, EventArgs.Empty);
                ComboBox_Zone.Items.Add("Keep the current zone");
                foreach (Zonas Zona in Zonas.Matriz)
                {
                    try
                    {
                        ComboBox_Zone.Items.Add(Zona.Nombre);
                    }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
                NumericUpDown_HealthPoints.Minimum = int.MinValue;
                NumericUpDown_HealthPoints.Maximum = int.MaxValue;
                NumericUpDown_Gold.Minimum = int.MinValue;
                NumericUpDown_Gold.Maximum = int.MaxValue;
                NumericUpDown_Thorium.Minimum = int.MinValue;
                NumericUpDown_Thorium.Maximum = int.MaxValue;
                NumericUpDown_Nether.Minimum = int.MinValue;
                NumericUpDown_Nether.Maximum = int.MaxValue;
                NumericUpDown_Bombs.Minimum = int.MinValue;
                NumericUpDown_Bombs.Maximum = int.MaxValue;
                NumericUpDown_Keys.Minimum = int.MinValue;
                NumericUpDown_Keys.Maximum = int.MaxValue;

                // Summoning stone.
                NumericUpDown_Summoning_Stone.Minimum = 0;
                NumericUpDown_Summoning_Stone.Maximum = 65536;
                NumericUpDown_Summoning_Stone_Máximo.Minimum = 0;
                NumericUpDown_Summoning_Stone_Máximo.Maximum = 65536;

                // Potions.
                ComboBox_Poción_1.Items.Add("None");
                ComboBox_Poción_2.Items.Add("None");
                ComboBox_Poción_3.Items.Add("None");
                ComboBox_Poción_4.Items.Add("None");
                foreach (Pociones Poción in Pociones.Matriz)
                {
                    try
                    {
                        ComboBox_Poción_1.Items.Add(Poción.Nombre + ": " + Poción.Descripción);
                        ComboBox_Poción_2.Items.Add(Poción.Nombre + ": " + Poción.Descripción);
                        ComboBox_Poción_3.Items.Add(Poción.Nombre + ": " + Poción.Descripción);
                        ComboBox_Poción_4.Items.Add(Poción.Nombre + ": " + Poción.Descripción);
                    }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
                ComboBox_Poción_1.SelectedIndex = 0;
                ComboBox_Poción_2.SelectedIndex = 0;
                ComboBox_Poción_3.SelectedIndex = 0;
                ComboBox_Poción_4.SelectedIndex = 0;

                // Progress settings.
                ComboBox_Artifacts.SelectedIndex = 0;
                ComboBox_Doors.SelectedIndex = 0;
                ComboBox_Bosses.SelectedIndex = 0;
                ComboBox_Discovered.SelectedIndex = 0;
                ComboBox_Unlocked.SelectedIndex = 0;
                ComboBox_Upgrades.SelectedIndex = 0;

                // Status effects.
                ListViewGroup ListViewGroup_Blessings = new ListViewGroup("Blessings", HorizontalAlignment.Left);
                ListViewGroup ListViewGroup_Crystals = new ListViewGroup("Crystals", HorizontalAlignment.Left);
                ListViewGroup ListViewGroup_Demon_relics = new ListViewGroup("Demon relics", HorizontalAlignment.Left);
                ListViewGroup ListViewGroup_Effects = new ListViewGroup("Effects - [Only appear after all relics]", HorizontalAlignment.Left);
                ListViewGroup ListViewGroup_Familiars = new ListViewGroup("Familiars - [Might cause bugs]", HorizontalAlignment.Left);
                ListViewGroup ListViewGroup_Hexes = new ListViewGroup("Hexes", HorizontalAlignment.Left);
                ListViewGroup ListViewGroup_Major_Curses = new ListViewGroup("Major curses", HorizontalAlignment.Left);
                ListViewGroup ListViewGroup_Minor_curses = new ListViewGroup("Minor curses", HorizontalAlignment.Left);
                ListViewGroup ListViewGroup_Othermine_major_curses = new ListViewGroup("Othermine major curses", HorizontalAlignment.Left);
                ListViewGroup ListViewGroup_Othermine_demon_relics = new ListViewGroup("Othermine demon relics", HorizontalAlignment.Left);
                ListViewGroup ListViewGroup_Othermine_relics = new ListViewGroup("Othermine relics", HorizontalAlignment.Left);
                ListViewGroup ListViewGroup_Potions = new ListViewGroup("Potions", HorizontalAlignment.Left);
                ListViewGroup ListViewGroup_Relics = new ListViewGroup("Relics", HorizontalAlignment.Left);
                ListViewGroup ListViewGroup_UnderMod_relics = new ListViewGroup("UnderMod relics - [Outdated mod]", HorizontalAlignment.Left);
                ListView_Efectos.Groups.Add(ListViewGroup_Blessings);
                ListView_Efectos.Groups.Add(ListViewGroup_Crystals);
                ListView_Efectos.Groups.Add(ListViewGroup_Demon_relics);
                ListView_Efectos.Groups.Add(ListViewGroup_Effects);
                ListView_Efectos.Groups.Add(ListViewGroup_Familiars);
                ListView_Efectos.Groups.Add(ListViewGroup_Hexes);
                ListView_Efectos.Groups.Add(ListViewGroup_Major_Curses);
                ListView_Efectos.Groups.Add(ListViewGroup_Minor_curses);
                ListView_Efectos.Groups.Add(ListViewGroup_Othermine_major_curses);
                ListView_Efectos.Groups.Add(ListViewGroup_Othermine_demon_relics);
                ListView_Efectos.Groups.Add(ListViewGroup_Othermine_relics);
                ListView_Efectos.Groups.Add(ListViewGroup_Potions);
                ListView_Efectos.Groups.Add(ListViewGroup_Relics);
                ListView_Efectos.Groups.Add(ListViewGroup_UnderMod_relics);
                ImageList Lista_Imágenes_18 = new ImageList();
                Lista_Imágenes_18.ColorDepth = ColorDepth.Depth32Bit;
                Lista_Imágenes_18.ImageSize = new Size(18, 18);
                Lista_Imágenes_18.TransparentColor = Color.Empty;
                for (int Índice_Efecto = 0; Índice_Efecto < Efectos.Matriz.Length; Índice_Efecto++)
                {
                    try
                    {
                        Efectos Efecto = Efectos.Matriz[Índice_Efecto];
                        Lista_Imágenes_18.Images.Add(Efecto.Imagen);
                        ListViewItem Objeto = new ListViewItem(Efecto.Nombre + ": " + Efecto.Descripción, Índice_Efecto, ListViewGroup_Blessings);
                        if (Efecto.Tipo == Efectos.Tipos.Blessing) Objeto.Group = ListViewGroup_Blessings;
                        else if (Efecto.Tipo == Efectos.Tipos.Crystal) Objeto.Group = ListViewGroup_Crystals;
                        else if (Efecto.Tipo == Efectos.Tipos.Demon_relic) Objeto.Group = ListViewGroup_Demon_relics;
                        else if (Efecto.Tipo == Efectos.Tipos.Effect) Objeto.Group = ListViewGroup_Effects;
                        else if (Efecto.Tipo == Efectos.Tipos.Familiar) Objeto.Group = ListViewGroup_Familiars;
                        else if (Efecto.Tipo == Efectos.Tipos.Hex) Objeto.Group = ListViewGroup_Hexes;
                        else if (Efecto.Tipo == Efectos.Tipos.Major_curse) Objeto.Group = ListViewGroup_Major_Curses;
                        else if (Efecto.Tipo == Efectos.Tipos.Minor_curse) Objeto.Group = ListViewGroup_Minor_curses;
                        else if (Efecto.Tipo == Efectos.Tipos.Othermine_demon_relic) Objeto.Group = ListViewGroup_Othermine_demon_relics;
                        else if (Efecto.Tipo == Efectos.Tipos.Othermine_major_curse) Objeto.Group = ListViewGroup_Othermine_major_curses;
                        else if (Efecto.Tipo == Efectos.Tipos.Othermine_relic) Objeto.Group = ListViewGroup_Othermine_relics;
                        else if (Efecto.Tipo == Efectos.Tipos.Potion) Objeto.Group = ListViewGroup_Potions;
                        else if (Efecto.Tipo == Efectos.Tipos.Relic) Objeto.Group = ListViewGroup_Relics;
                        else if (Efecto.Tipo == Efectos.Tipos.UnderMod_relic) Objeto.Group = ListViewGroup_UnderMod_relics;
                        ListView_Efectos.Items.Add(Objeto);
                    }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
                ListView_Efectos.SmallImageList = Lista_Imágenes_18;
                ListView_Efectos.LargeImageList = Lista_Imágenes_18;
                NumericUpDown_Nivel_Bendiciones.Minimum = 1;
                NumericUpDown_Nivel_Bendiciones.Maximum = int.MaxValue;
                NumericUpDown_Nivel_Maldiciones.Minimum = 1;
                NumericUpDown_Nivel_Maldiciones.Maximum = int.MaxValue;

                // Start the dictionary here to avoid start exceptions.
                // Note: I had to add this code to avoid the "Masamune" with it's parts at
                // the same time or the game will crash, at least if it wasn't discovered yet.
                // The rest of combined relcs alongside with their 2 parts for the rest seem
                // to work without problems, but I haven't tested the "Pilfer Credit Cards" yet,
                // so just in case I've also added them as an exception to avoid at the same time.
                Diccionario_Índices_Combinaciones = new Dictionary<int, int[]>();
                Diccionario_Índices_Combinaciones.Add(Efectos.Buscar_Índice("8d05bd4c82d04ecb92f252721bb1dc01"), new int[2] { Efectos.Buscar_Índice("90ee14233cfe4249818d5ca2eb8ec122"), Efectos.Buscar_Índice("0e7e31f44491456bb5f6268f2c2686c2") }); // Golden Idol.
                Diccionario_Índices_Combinaciones.Add(Efectos.Buscar_Índice("b63fbdf8d12b4c99bdb3753cf011be10"), new int[2] { Efectos.Buscar_Índice("66c0fc8f90654bc988b8604d9b72c18a"), Efectos.Buscar_Índice("ec2ed98f555049e68d8268e0ed10de73") }); // Mirror Shield.
                Diccionario_Índices_Combinaciones.Add(Efectos.Buscar_Índice("46b55b2a7e3f4b9aabcbd60b84ea4efd"), new int[2] { Efectos.Buscar_Índice("0d5fcfc5097e451bb557c422c428b68e"), Efectos.Buscar_Índice("b6201c6ad35f4744ad35061cb4280261") }); // Emperor's Crown.
                Diccionario_Índices_Combinaciones.Add(Efectos.Buscar_Índice("852a50adacfa447cbc704080ddbec16f"), new int[2] { Efectos.Buscar_Índice("1210bacc5fcb460ca07e95896df87cf6"), Efectos.Buscar_Índice("b4ed3b0c8b2f448382d52ca31c37f41b") }); // Four Leaf Cleaver.
                Diccionario_Índices_Combinaciones.Add(Efectos.Buscar_Índice("5459c455b9ec49d8a04b071a8f172756"), new int[2] { Efectos.Buscar_Índice("5e209720547a49d38cbdbfe623b9af06"), Efectos.Buscar_Índice("3fe862f303f84124a3ca55f026398e50") }); // Lucky Lockpick.
                Diccionario_Índices_Combinaciones.Add(Efectos.Buscar_Índice("c05ef86ba2054882959c51b24350e72b"), new int[2] { Efectos.Buscar_Índice("a4a29157f46c4a539e3bbddd81cde3e9"), Efectos.Buscar_Índice("43eff4cddbd24b36931720f418920d5d") }); // Helios Boots.
                Diccionario_Índices_Combinaciones.Add(Efectos.Buscar_Índice("1c2449de28a34fda9fe5c990045cb81b"), new int[2] { Efectos.Buscar_Índice("3476bec1a63c4fffb9699dc50c43046d"), Efectos.Buscar_Índice("429ef0c052544c9c9442e323dc9f213c") }); // Caramel Popcorn.
                Diccionario_Índices_Combinaciones.Add(Efectos.Buscar_Índice("c88199b42a0b4b03b3887b1d453cb1fa"), new int[2] { Efectos.Buscar_Índice("8144d7dba6454abf84d594f2c1f6974c"), Efectos.Buscar_Índice("3e07f594a331417b88fcbbe5637bcdcf") }); // Masamune.
                Diccionario_Índices_Combinaciones.Add(Efectos.Buscar_Índice("814e5f1f467448b8ac2882a85592c888"), new int[2] { Efectos.Buscar_Índice("21dc94cf49994b75a7965178a19d67dd"), Efectos.Buscar_Índice("17f014a742844fd69d56031bdce0ba70") }); // Pilfer Credit Card Black Edition.
                Diccionario_Índices_Combinaciones.Add(Efectos.Buscar_Índice("94ce9288476e4d77a6ac9a5906ddcd0d"), new int[2] { Efectos.Buscar_Índice("3ccf0b428250435a9e7de14be1ce5bcb"), Efectos.Buscar_Índice("c08a901c2f3646e5834cfe3a0e965ca8") }); // Double Doubler.
                try
                {
                    if (string.Compare(Environment.UserName, "Jupisoft", true) == 0)
                    {
                        ToolStripSeparator Menú_Contextual_Separador_6 = new ToolStripSeparator();
                        Menú_Contextual.Items.Add(Menú_Contextual_Separador_6);
                        ToolStripMenuItem Menú_Contextual_Convertir_Imágenes_UnderMine = new ToolStripMenuItem("Convert to 18 x 18 the UnderMine images...", Resources.Jupisoft_16, Menú_Contextual_Convertir_Imágenes_UnderMine_Click, Keys.Control | Keys.J);
                        Menú_Contextual.Items.Add(Menú_Contextual_Convertir_Imágenes_UnderMine);
                    }
                }
                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Form_Main_Shown(object sender, EventArgs e)
        {
            try
            {
                TextBox_Ruta.Text = Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)) + "\\LocalLow\\Thorium Entertainment\\UnderMine\\Saves";
                this.Activate();
                Temporizador_Principal.Start();
                // Function used to resize the images of the game.
                /*bool Convertir = false; // false // true
                if (Convertir)
                {
                    string[] Matriz_Rutas = Directory.GetFiles(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\UnderMine\\Potions", "*.png", SearchOption.TopDirectoryOnly);
                    foreach (string Ruta in Matriz_Rutas)
                    {
                        Bitmap Imagen = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                        Imagen = Program.Obtener_Imagen_Miniatura_Recortada(Imagen, 16, true);
                        Imagen.Save(Application.StartupPath + "\\Saves\\" + Path.GetFileNameWithoutExtension(Ruta) + ".png", ImageFormat.Png);
                        Imagen.Dispose();
                        Imagen = null;
                    }
                    Matriz_Rutas = null;
                }*/
                /*// Try a real time filter.
                Bitmap Imagen = Program.Cargar_Imagen_Ruta(@"C:\Users\Jupisoft\Desktop\UnderMine Wikisok\OK\HeatRamp-resources.assets-2304.png", CheckState.Checked);
                string q = null;
                for (int i = 0; i < 256; i++)
                {
                    Color Color_ARGB = Imagen.GetPixel(i, 0);
                    q += "Color.FromArgb(" + Color_ARGB.A.ToString() + ", " + Color_ARGB.R.ToString() + ", " + Color_ARGB.G.ToString() + ", " + Color_ARGB.B.ToString() + "),\r\n";
                }
                Clipboard.SetText(q);*/
                /*Bitmap Imagen = Program.Cargar_Imagen_Ruta(@"C:\Users\Jupisoft\Desktop\UnderMine Wiki\OK\Crown\CrownSprites-resources.assets-1551.png", CheckState.Checked);
                for (int y = 0, i = 1; y < 5; y++)
                {
                    for (int x = 0; x < 10; x++, i++)
                    {
                        Bitmap Imagen_1 = Imagen.Clone(new Rectangle(x * 112, y * 112, 112, 112), PixelFormat.Format32bppArgb);
                        Imagen_1.Save(@"C:\Users\Jupisoft\Desktop\UnderMine Wikisok\OK\Crown\0\" + i.ToString() + ".png", ImageFormat.Png);
                        Imagen_1.Dispose();
                        Imagen_1 = null;
                    }
                }
                Imagen.Dispose();
                Imagen = null;*/
                /*MessageBox.Show(Matriz_Nombres_Mujer_Fin.Length.ToString());
                string q = null;
                Array.Sort(Matriz_Nombres_Mujer_Fin);
                for (int i = 0; i < Matriz_Nombres_Mujer_Fin.Length; )
                {
                    for (int x = 0; x < 8; x++, i++)
                    {
                        if (i < Matriz_Nombres_Mujer_Fin.Length)
                        {
                            q += "\"" + Matriz_Nombres_Mujer_Fin[i] + "\", ";
                        }
                    }
                    q += "\r\n";
                }
                Clipboard.SetText(q);*/
                //MessageBox.Show(Matriz_Nombres_Hombre_Inicio.Length.ToString() + "\r\n" + Matriz_Nombres_Hombre_Fin.Length.ToString(), "Man");
                //MessageBox.Show(Matriz_Nombres_Mujer_Inicio.Length.ToString() + "\r\n" + Matriz_Nombres_Mujer_Fin.Length.ToString(), "Woman");
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                Temporizador_Principal.Stop();
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Form_Main_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {

            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Form_Main_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                //if (this.WindowState == FormWindowState.Maximized) this.WindowState = FormWindowState.Normal;
                Columna_Efecto.Width = ListView_Efectos.ClientSize.Width - SystemInformation.VerticalScrollBarWidth;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Form_Main_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (!e.Alt && !e.Control && !e.Shift)
                {
                    if (e.KeyCode == Keys.Escape)
                    {
                        e.Handled = true;
                        e.SuppressKeyPress = true;
                        this.Close();
                    }
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Barra_Estado_Botón_Excepción_Click(object sender, EventArgs e)
        {
            try
            {
                Variable_Excepción = false;
                Variable_Excepción_Imagen = false;
                Variable_Excepción_Total = 0;
                Barra_Estado_Botón_Excepción.Visible = false;
                Barra_Estado_Separador_1.Visible = false;
                Barra_Estado_Botón_Excepción.Image = Resources.Excepción_Gris;
                Barra_Estado_Botón_Excepción.ForeColor = Color.Black;
                Barra_Estado_Botón_Excepción.Text = "Exceptions: 0";
                Ventana_Depurador_Excepciones Ventana = new Ventana_Depurador_Excepciones();
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Opening(object sender, CancelEventArgs e)
        {
            try
            {
                Menú_Contextual_Depurador_Excepciones.Text = "Exception debugger - [" + Program.Traducir_Número(Variable_Excepción_Total) + (Variable_Excepción_Total != 1 ? " exceptions" : " exception") + "]...";
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Donar_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=KSMZ3XNG2R9P6", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Visor_Ayuda_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show(this, "Here are some tips I learned while playing this game:\r\n\r\n" +
                    "- At the start of each new level the game says \"Autosaving\", which means that after the message is gone you can quit the game to it's main menu without fully closing it and edit your desired save game with this application, and even fully delete the saves folder to then add any backup you want. This trick saved me a lot of time while making this tool and doing tests.\r\n\r\n" +
                    "- There is a potion that spawns a random chest. These chests can be \"blue\", which gives thorium and new blueprints for blessings, potions or relics, so giving yourself 4 chest potions at the start of each level with this tool is the quickest way to unlock everything in game (aside from fully cheating). And the game saves at the start of each level, so this means that if after spawning the 4 chests none of them was blue, you can quit and reload the same save to try again with the 4 potions without even editing again your save. But just remember to finish the level so the next autosaving can occur or your new discoveries will be lost forever. The chests can also be \"green\" with a lot of gold, \"purple\" with a random curse each time (useful for unlocking new curses) or \"brown\" (with or without a lock) with just regular stuff and random potions.\r\n\r\n" +
                    "But the best guide will always be the official UnderMine wiki:\r\n\r\nhttps://undermine.gamepedia.com/UnderMine_Wiki", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Acerca_Click(object sender, EventArgs e)
        {
            try
            {
                Form_About Ventana = new Form_About();
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Depurador_Excepciones_Click(object sender, EventArgs e)
        {
            try
            {
                Variable_Excepción = false;
                Variable_Excepción_Imagen = false;
                Variable_Excepción_Total = 0;
                Barra_Estado_Botón_Excepción.Visible = false;
                Barra_Estado_Separador_1.Visible = false;
                Barra_Estado_Botón_Excepción.Image = Resources.Excepción_Gris;
                Barra_Estado_Botón_Excepción.ForeColor = Color.Black;
                Barra_Estado_Botón_Excepción.Text = "Exceptions: 0";
                Ventana_Depurador_Excepciones Ventana = new Ventana_Depurador_Excepciones();
                Ventana.ShowDialog(this);
                Ventana.Dispose();
                Ventana = null;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Siempre_Visible_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Variable_Siempre_Visible = Menú_Contextual_Siempre_Visible.Checked;
                this.TopMost = Variable_Siempre_Visible;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox_Ruta_TextChanged(TextBox_Ruta, EventArgs.Empty);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Temporizador_Principal_Tick(object sender, EventArgs e)
        {
            try
            {
                int Tick = Environment.TickCount; // Used in the next calculations.

                try // If there are new exceptions, flash in red text every 500 milliseconds.
                {
                    if (Variable_Excepción)
                    {
                        if ((Tick / 500) % 2 == 0)
                        {
                            if (!Variable_Excepción_Imagen)
                            {
                                Variable_Excepción_Imagen = true;
                                Barra_Estado_Botón_Excepción.Image = Resources.Excepción;
                                Barra_Estado_Botón_Excepción.ForeColor = Color.Red;
                                Barra_Estado_Botón_Excepción.Text = "Exceptions: " + Program.Traducir_Número(Variable_Excepción_Total);
                            }
                        }
                        else
                        {
                            if (Variable_Excepción_Imagen)
                            {
                                Variable_Excepción_Imagen = false;
                                Barra_Estado_Botón_Excepción.Image = Resources.Excepción_Gris;
                                Barra_Estado_Botón_Excepción.ForeColor = Color.Black;
                                Barra_Estado_Botón_Excepción.Text = "Exceptions: " + Program.Traducir_Número(Variable_Excepción_Total);
                            }
                        }
                        if (!Barra_Estado_Botón_Excepción.Visible) Barra_Estado_Botón_Excepción.Visible = true;
                        if (!Barra_Estado_Separador_1.Visible) Barra_Estado_Separador_1.Visible = true;
                    }
                }
                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                try // CPU and RAM use calculations.
                {
                    try
                    {
                        if (Tick % 250 == 0) // Update every 250 milliseconds.
                        {
                            if (Program.Rendimiento_Procesador != null)
                            {
                                double CPU = (double)Program.Rendimiento_Procesador.NextValue();
                                if (CPU < 0d) CPU = 0d;
                                else if (CPU > 100d) CPU = 100d;
                                Barra_Estado_Etiqueta_CPU.Text = "CPU: " + Program.Traducir_Número_Decimales_Redondear(CPU, 2) + " %";
                            }
                            Program.Proceso.Refresh();
                            long Memoria_Bytes = Program.Proceso.PagedMemorySize64;
                            Barra_Estado_Etiqueta_Memoria.Text = "RAM: " + Program.Traducir_Tamaño_Bytes_Automático(Memoria_Bytes, 2, true);
                            if (Memoria_Bytes < 4294967296L) // < 4 GB, default black text.
                            {
                                if (Variable_Memoria)
                                {
                                    Variable_Memoria = false;
                                    Barra_Estado_Etiqueta_Memoria.ForeColor = Color.Black;
                                }
                            }
                            else // >= 4 GB, flash in red text every 500 milliseconds.
                            {
                                if ((Tick / 500) % 2 == 0)
                                {
                                    if (!Variable_Memoria)
                                    {
                                        Variable_Memoria = true;
                                        Barra_Estado_Etiqueta_Memoria.ForeColor = Color.Red;
                                    }
                                }
                                else
                                {
                                    if (Variable_Memoria)
                                    {
                                        Variable_Memoria = false;
                                        Barra_Estado_Etiqueta_Memoria.ForeColor = Color.Black;
                                    }
                                }
                            }
                        }
                    }
                    catch { Barra_Estado_Etiqueta_Memoria.Text = "RAM: ? MB (? GB)"; }
                }
                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }

                try // FPS calculation and drawing.
                {
                    long FPS_Milisegundo = FPS_Cronómetro.ElapsedMilliseconds;
                    long FPS_Segundo = FPS_Milisegundo / 1000L;
                    int Milisegundo_Actual = FPS_Cronómetro.Elapsed.Milliseconds;
                    if (FPS_Segundo != FPS_Segundo_Anterior)
                    {
                        FPS_Segundo_Anterior = FPS_Segundo;
                        FPS_Real = FPS_Temporal;
                        Barra_Estado_Etiqueta_FPS.Text = FPS_Real.ToString() + " FPS";
                        FPS_Temporal = 0L;
                    }
                    FPS_Temporal++;
                }
                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void TextBox_Ruta_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string Texto_Guardado = ComboBox_Guardado.Text;
                ComboBox_Guardado.Items.Clear();
                string Path_Save = TextBox_Ruta.Text;
                if (!string.IsNullOrEmpty(Path_Save) && Directory.Exists(Path_Save))
                {
                    string[] Matriz_Rutas = Directory.GetFiles(Path_Save, "*.json", SearchOption.TopDirectoryOnly);
                    if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                    {
                        if (Matriz_Rutas.Length > 1) Array.Sort(Matriz_Rutas);
                        foreach (string Ruta in Matriz_Rutas)
                        {
                            try
                            {
                                ComboBox_Guardado.Items.Add(Path.GetFileNameWithoutExtension(Ruta));
                            }
                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                        }
                        if (!string.IsNullOrEmpty(Texto_Guardado) &&
                            ComboBox_Guardado.Items.Contains(Texto_Guardado))
                        {
                            ComboBox_Guardado.Text = Texto_Guardado;
                        }
                        else ComboBox_Guardado.SelectedIndex = 0;
                    }
                    Matriz_Rutas = null;
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void ComboBox_Guardado_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cargar_Partida(Othermine_Cargado);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_PlayTime_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_PlayTime.Refresh();
                TextBox_PlayTime.Text = Program.Traducir_Intervalo_Días_Horas_Minutos_Segundos(TimeSpan.FromSeconds((double)NumericUpDown_PlayTime.Value));
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Timer_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_Timer.Refresh();
                TextBox_Timer.Text = Program.Traducir_Intervalo_Días_Horas_Minutos_Segundos(TimeSpan.FromSeconds((double)NumericUpDown_Timer.Value));
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Guardar_Othermine_Click(object sender, EventArgs e)
        {
            try
            {
                Guardar_Partida(true);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Botón_Guardar_UnderMine_Click(object sender, EventArgs e)
        {
            try
            {
                Guardar_Partida(false);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_PeonName_Mínimo_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox_Nombre.Text = Environment.UserName;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_PeonName_Máximo_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox_Nombre.Text = Environment.UserName;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_PeonName_Aleatorio_Click(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Sexo.SelectedIndex = Program.Rand.Next(0, 2);
                NumericUpDown_Clothes.Value = Program.Rand.Next(0, 12);
                NumericUpDown_Eyes.Value = Program.Rand.Next(0, 8);
                NumericUpDown_Hair.Value = Program.Rand.Next(0, 2);
                NumericUpDown_Skin.Value = Program.Rand.Next(0, 4);
                // Update: I found and decoded the file "PeonNames-resources.assets-3249", with
                // all the new names for male and female peasants, so the wiki can be updated now.
                if (ComboBox_Sexo.SelectedIndex == 1)
                {
                    TextBox_Nombre.Text = Matriz_Nombres_Mujer_Inicio[Program.Rand.Next(0, Matriz_Nombres_Mujer_Inicio.Length)] + Matriz_Nombres_Mujer_Fin[Program.Rand.Next(0, Matriz_Nombres_Mujer_Fin.Length)];
                }
                else
                {
                    TextBox_Nombre.Text = Matriz_Nombres_Hombre_Inicio[Program.Rand.Next(0, Matriz_Nombres_Hombre_Inicio.Length)] + Matriz_Nombres_Hombre_Fin[Program.Rand.Next(0, Matriz_Nombres_Hombre_Fin.Length)];
                }
                /*if (Program.Rand.Next(0, 100) < 25)
                {
                    // Exact copy of the possible random names UnderMine can give.
                    // But I also found "Jessne" as name and it doesn't appear on the wiki...
                    // so most likely it's outdated.
                    TextBox_Nombre.Text = Matriz_Nombres_Aleatorios_Inicio[Program.Rand.Next(0, Matriz_Nombres_Aleatorios_Inicio.Length)] + Matriz_Nombres_Aleatorios_Fin[Program.Rand.Next(0, Matriz_Nombres_Aleatorios_Fin.Length)];
                }
                else
                {
                    // Alternative made up code to simulate random names with some sense.
                    List<char> Lista_Consonantes = new List<char>();
                    List<char> Lista_Vocales = new List<char>();
                    for (char Caracter = 'a'; Caracter < 'z'; Caracter++)
                    {
                        if (Caracter != 'a' &&
                            Caracter != 'e' &&
                            Caracter != 'i' &&
                            Caracter != 'o' &&
                            Caracter != 'u')
                        {
                            Lista_Consonantes.Add(Caracter);
                        }
                        else Lista_Vocales.Add(Caracter);
                    }
                    string Nombre = null;
                    // Pick a random length between 1 and 5 and double it.
                    //int Longitud = Program.Rand.Next(1, 6) * 2;
                    // Pick a random length between 2 and 10.
                    int Longitud = Program.Rand.Next(2, 11);
                    // Add the first character in upper casing and alternate the two types of letters.
                    for (int Índice = 0; Índice < Longitud; Índice++)
                    {
                        if (Índice % 2 == 0)
                        {
                            if (Índice != 0) Nombre += Lista_Consonantes[Program.Rand.Next(0, Lista_Consonantes.Count)];
                            else Nombre += char.ToUpperInvariant(Lista_Consonantes[Program.Rand.Next(0, Lista_Consonantes.Count)]);
                        }
                        else Nombre += Lista_Vocales[Program.Rand.Next(0, Lista_Vocales.Count)];
                    }
                    Lista_Consonantes = null;
                    Lista_Vocales = null;
                    // Now we should have a name that almost makes sense in most cases.
                    TextBox_Nombre.Text = Nombre;
                }*/
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Nombre_Usuario_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox_Nombre.Text = Environment.UserName;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Seed_Aleatorio_Click(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_Seed.Value = Program.Rand.Next();
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Zone_Aleatorio_Click(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Zone.SelectedIndex = Program.Rand.Next(0, ComboBox_Zone.Items.Count);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_HealthPoints_Aleatorio_Click(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_HealthPoints.Value = Program.Rand.Next();
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Gold_Aleatorio_Click(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_Gold.Value = Program.Rand.Next(0, 1000000);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Thorium_Aleatorio_Click(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_Thorium.Value = Program.Rand.Next(0, 1000);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Nether_Aleatorio_Click(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_Nether.Value = Program.Rand.Next(0, 100);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Bombs_Aleatorio_Click(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_Bombs.Value = Program.Rand.Next(0, 100);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Keys_Aleatorio_Click(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_Keys.Value = Program.Rand.Next(0, 100);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Clothes_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int Clothes = (int)NumericUpDown_Clothes.Value;
                int Eyes = (int)NumericUpDown_Eyes.Value;
                int Hair = (int)NumericUpDown_Hair.Value;
                int Skin = (int)NumericUpDown_Skin.Value;
                int Valor = (Eyes << 16) | (Skin << 9) | (Hair << 8) | Clothes;
                if (NumericUpDown_PeonColor.Value != Valor)
                {
                    NumericUpDown_PeonColor.Value = Valor;
                }
                PictureBox_Color_Clothes.BackColor = Clothes < Matriz_Colores_Clothes.Length ? Matriz_Colores_Clothes[Clothes] : Color.FromArgb(255, 205, 205, 205);
                PictureBox_Color_Eyes.BackColor = Eyes < Matriz_Colores_Eyes.Length ? Matriz_Colores_Eyes[Eyes] : Color.FromArgb(255, 205, 205, 205);
                PictureBox_Color_Hair.BackColor = (Skin << 1) + Hair < Matriz_Colores_Hair.Length ? Matriz_Colores_Hair[(Skin << 1) + Hair] : Color.FromArgb(255, 205, 205, 205);
                PictureBox_Color_Skin.BackColor = Skin < Matriz_Colores_Skin.Length ? Matriz_Colores_Skin[Skin] : Color.FromArgb(255, 205, 205, 205);
                PictureBox_Peasant.Image = Crear_Imagen_Peasant(true);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Eyes_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int Clothes = (int)NumericUpDown_Clothes.Value;
                int Eyes = (int)NumericUpDown_Eyes.Value;
                int Hair = (int)NumericUpDown_Hair.Value;
                int Skin = (int)NumericUpDown_Skin.Value;
                int Valor = (Eyes << 16) | (Skin << 9) | (Hair << 8) | Clothes;
                if (NumericUpDown_PeonColor.Value != Valor)
                {
                    NumericUpDown_PeonColor.Value = Valor;
                }
                PictureBox_Color_Clothes.BackColor = Clothes < Matriz_Colores_Clothes.Length ? Matriz_Colores_Clothes[Clothes] : Color.FromArgb(255, 205, 205, 205);
                PictureBox_Color_Eyes.BackColor = Eyes < Matriz_Colores_Eyes.Length ? Matriz_Colores_Eyes[Eyes] : Color.FromArgb(255, 205, 205, 205);
                PictureBox_Color_Hair.BackColor = (Skin << 1) + Hair < Matriz_Colores_Hair.Length ? Matriz_Colores_Hair[(Skin << 1) + Hair] : Color.FromArgb(255, 205, 205, 205);
                PictureBox_Color_Skin.BackColor = Skin < Matriz_Colores_Skin.Length ? Matriz_Colores_Skin[Skin] : Color.FromArgb(255, 205, 205, 205);
                PictureBox_Peasant.Image = Crear_Imagen_Peasant(true);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Hair_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int Clothes = (int)NumericUpDown_Clothes.Value;
                int Eyes = (int)NumericUpDown_Eyes.Value;
                int Hair = (int)NumericUpDown_Hair.Value;
                int Skin = (int)NumericUpDown_Skin.Value;
                int Valor = (Eyes << 16) | (Skin << 9) | (Hair << 8) | Clothes;
                if (NumericUpDown_PeonColor.Value != Valor)
                {
                    NumericUpDown_PeonColor.Value = Valor;
                }
                PictureBox_Color_Clothes.BackColor = Clothes < Matriz_Colores_Clothes.Length ? Matriz_Colores_Clothes[Clothes] : Color.FromArgb(255, 205, 205, 205);
                PictureBox_Color_Eyes.BackColor = Eyes < Matriz_Colores_Eyes.Length ? Matriz_Colores_Eyes[Eyes] : Color.FromArgb(255, 205, 205, 205);
                PictureBox_Color_Hair.BackColor = (Skin << 1) + Hair < Matriz_Colores_Hair.Length ? Matriz_Colores_Hair[(Skin << 1) + Hair] : Color.FromArgb(255, 205, 205, 205);
                PictureBox_Color_Skin.BackColor = Skin < Matriz_Colores_Skin.Length ? Matriz_Colores_Skin[Skin] : Color.FromArgb(255, 205, 205, 205);
                PictureBox_Peasant.Image = Crear_Imagen_Peasant(true);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Skin_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int Clothes = (int)NumericUpDown_Clothes.Value;
                int Eyes = (int)NumericUpDown_Eyes.Value;
                int Hair = (int)NumericUpDown_Hair.Value;
                int Skin = (int)NumericUpDown_Skin.Value;
                int Valor = (Eyes << 16) | (Skin << 9) | (Hair << 8) | Clothes;
                if (NumericUpDown_PeonColor.Value != Valor)
                {
                    NumericUpDown_PeonColor.Value = Valor;
                }
                PictureBox_Color_Clothes.BackColor = Clothes < Matriz_Colores_Clothes.Length ? Matriz_Colores_Clothes[Clothes] : Color.FromArgb(255, 205, 205, 205);
                PictureBox_Color_Eyes.BackColor = Eyes < Matriz_Colores_Eyes.Length ? Matriz_Colores_Eyes[Eyes] : Color.FromArgb(255, 205, 205, 205);
                PictureBox_Color_Hair.BackColor = (Skin << 1) + Hair < Matriz_Colores_Hair.Length ? Matriz_Colores_Hair[(Skin << 1) + Hair] : Color.FromArgb(255, 205, 205, 205);
                PictureBox_Color_Skin.BackColor = Skin < Matriz_Colores_Skin.Length ? Matriz_Colores_Skin[Skin] : Color.FromArgb(255, 205, 205, 205);
                PictureBox_Peasant.Image = Crear_Imagen_Peasant(true);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Sexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PictureBox_Peasant.Image = Crear_Imagen_Peasant(true);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Version_Mínimo_Click(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_Version.Value = 0m;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Version_Máximo_Click(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_Version.Value = (decimal)int.MaxValue;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Version_Aleatorio_Click(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_Version.Value = (decimal)Program.Rand.Next();
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Guid_Mínimo_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox_Guid.Text = new string('0', 32);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Guid_Máximo_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox_Guid.Text = new string('f', 32);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Guid_Aleatorio_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox_Guid.Text = Guid.NewGuid().ToString().Replace("-", null).ToLowerInvariant();
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_PlayTime_Mínimo_Click(object sender, EventArgs e)
        {

        }

        private void Button_PlayTime_Máximo_Click(object sender, EventArgs e)
        {

        }

        private void Button_PlayTime_Aleatorio_Click(object sender, EventArgs e)
        {
            try
            {
                NumericUpDown_PlayTime.Value = (decimal)Program.Rand.NextDouble() * (decimal)Program.Rand.Next();
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Copiar_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap Imagen = Crear_Imagen_Peasant(false);
                if (Imagen != null)
                {
                    Clipboard.SetImage(PictureBox_Peasant.Image);
                    SystemSounds.Asterisk.Play();
                    Imagen.Dispose();
                    Imagen = null;
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                Bitmap Imagen = Crear_Imagen_Peasant(false);
                if (Imagen != null)
                {
                    Program.Crear_Carpetas(Ruta_Saves);
                    Imagen.Save(Ruta_Saves + "\\" + Program.Obtener_Nombre_Temporal() + " Peasant " + ((int)NumericUpDown_PeonColor.Value).ToString() + ".png");
                    SystemSounds.Asterisk.Play();
                    Imagen.Dispose();
                    Imagen = null;
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Ejecutar_Ruta_Backups_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Program.Crear_Carpetas(Ruta_Backups);
                Program.Ejecutar_Ruta(Ruta_Backups, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Ejecutar_Ruta_Guardado_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(TextBox_Ruta.Text) &&
                    Directory.Exists(TextBox_Ruta.Text))
                {
                    Program.Ejecutar_Ruta(TextBox_Ruta.Text, ProcessWindowStyle.Maximized);
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Menú_Contextual_Ejecutar_Archivo_Guardado_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (!string.IsNullOrEmpty(TextBox_Ruta.Text) &&
                    Directory.Exists(TextBox_Ruta.Text) &&
                    !string.IsNullOrEmpty(ComboBox_Guardado.Text))
                {
                    Program.Ejecutar_Ruta(TextBox_Ruta.Text + "\\" + ComboBox_Guardado.Text + ".json", ProcessWindowStyle.Maximized);
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Button_Nada_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                NumericUpDown_HealthPoints.Value = 200m;
                NumericUpDown_Gold.Value = 0m;
                NumericUpDown_Thorium.Value = 0m;
                NumericUpDown_Nether.Value = 0m;
                NumericUpDown_Bombs.Value = 0m;
                NumericUpDown_Keys.Value = 0m;
                ComboBox_Bomb.SelectedIndex = 0;
                ComboBox_Gloves.SelectedIndex = 0;
                ComboBox_Weapon.SelectedIndex = 0;
                ComboBox_Hat.SelectedIndex = 0;
                ComboBox_Familiar_1_ID.SelectedIndex = 1;
                NumericUpDown_Familiar_1_XP.Value = 0m;
                ComboBox_Familiar_2_ID.SelectedIndex = 0;
                NumericUpDown_Familiar_2_XP.Value = 0m;
                ComboBox_Poción_1.SelectedIndex = 0;
                ComboBox_Poción_2.SelectedIndex = 0;
                ComboBox_Poción_3.SelectedIndex = 0;
                ComboBox_Poción_4.SelectedIndex = 0;
                CheckBox_Mantener_Efectos_Originales.Checked = false;
                for (int Índice_Efecto = 0; Índice_Efecto < Efectos.Matriz.Length; Índice_Efecto++)
                {
                    try
                    {
                        ListView_Efectos.Items[Índice_Efecto].Checked = false;
                    }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
                NumericUpDown_Nivel_Bendiciones.Value = 1m;
                NumericUpDown_Nivel_Maldiciones.Value = 1m;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Button_Aleatorio_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                NumericUpDown_HealthPoints.Value = Program.Rand.Next(1, 201);
                NumericUpDown_Gold.Value = Program.Rand.Next(0, 1000000);
                NumericUpDown_Thorium.Value = Program.Rand.Next(0, 1000);
                NumericUpDown_Nether.Value = Program.Rand.Next(0, 100);
                NumericUpDown_Bombs.Value = Program.Rand.Next(0, 100);
                NumericUpDown_Keys.Value = Program.Rand.Next(0, 100);
                ComboBox_Bomb.SelectedIndex = Program.Rand.Next(0, ComboBox_Bomb.Items.Count);
                ComboBox_Gloves.SelectedIndex = Program.Rand.Next(0, ComboBox_Gloves.Items.Count);
                ComboBox_Weapon.SelectedIndex = Program.Rand.Next(0, ComboBox_Weapon.Items.Count);
                ComboBox_Hat.SelectedIndex = Program.Rand.Next(0, ComboBox_Hat.Items.Count);
                ComboBox_Familiar_1_ID.SelectedIndex = Program.Rand.Next(0, ComboBox_Familiar_1_ID.Items.Count);
                NumericUpDown_Familiar_1_XP.Value = Program.Rand.Next(0, 1601);
                ComboBox_Familiar_2_ID.SelectedIndex = Program.Rand.Next(0, 100) >= 50 ? Program.Rand.Next(1, ComboBox_Familiar_2_ID.Items.Count) : 0;
                NumericUpDown_Familiar_2_XP.Value = Program.Rand.Next(0, 1601);
                int Pociones_Aleatorias = Program.Rand.Next(0, 100);
                ComboBox_Poción_1.SelectedIndex = Pociones_Aleatorias >= 10 ? Program.Rand.Next(1, ComboBox_Poción_1.Items.Count) : 0;
                ComboBox_Poción_2.SelectedIndex = Pociones_Aleatorias >= 35 ? Program.Rand.Next(1, ComboBox_Poción_2.Items.Count) : 0;
                ComboBox_Poción_3.SelectedIndex = Pociones_Aleatorias >= 60 ? Program.Rand.Next(1, ComboBox_Poción_3.Items.Count) : 0;
                ComboBox_Poción_4.SelectedIndex = Pociones_Aleatorias >= 85 ? Program.Rand.Next(1, ComboBox_Poción_4.Items.Count) : 0;
                CheckBox_Mantener_Efectos_Originales.Checked = false;
                int Total_Efectos = Program.Rand.Next(0, Efectos.Matriz.Length);
                List<int> Lista_Temporal = new List<int>();
                for (int Índice_Efecto = 0; Índice_Efecto < Efectos.Matriz.Length; Índice_Efecto++)
                {
                    try
                    {
                        Lista_Temporal.Add(Índice_Efecto);
                    }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
                List<int> Lista_Efectos = new List<int>();
                for (int Índice_Efecto = Total_Efectos - 1; Índice_Efecto >= 0; Índice_Efecto--)
                {
                    try
                    {
                        int Índice_Aleatorio = Program.Rand.Next(0, Lista_Temporal.Count);
                        Lista_Efectos.Add(Lista_Temporal[Índice_Aleatorio]);
                        Lista_Temporal.RemoveAt(Índice_Aleatorio);
                    }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
                Lista_Temporal = null;
                for (int Índice_Efecto = 0; Índice_Efecto < Efectos.Matriz.Length; Índice_Efecto++)
                {
                    try
                    {
                        ListView_Efectos.Items[Índice_Efecto].Checked = Lista_Efectos.Contains(Índice_Efecto) ? true : false;
                    }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
                Lista_Efectos = null;
                NumericUpDown_Nivel_Bendiciones.Value = Program.Rand.Next(1, 1000);
                NumericUpDown_Nivel_Maldiciones.Value = Program.Rand.Next(1, 1000);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Button_Malo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                NumericUpDown_HealthPoints.Value = 1m;
                NumericUpDown_Gold.Value = 0m;
                NumericUpDown_Thorium.Value = 0m;
                NumericUpDown_Nether.Value = 0m;
                NumericUpDown_Bombs.Value = 0m;
                NumericUpDown_Keys.Value = 0m;
                ComboBox_Bomb.SelectedIndex = 0;
                ComboBox_Gloves.SelectedIndex = 0;
                ComboBox_Weapon.SelectedIndex = 0;
                ComboBox_Hat.SelectedIndex = 1;
                ComboBox_Familiar_1_ID.SelectedIndex = 0;
                NumericUpDown_Familiar_1_XP.Value = 0m;
                ComboBox_Familiar_2_ID.SelectedIndex = 0;
                NumericUpDown_Familiar_2_XP.Value = 0m;
                ComboBox_Poción_1.SelectedIndex = 0;
                ComboBox_Poción_2.SelectedIndex = 0;
                ComboBox_Poción_3.SelectedIndex = 0;
                ComboBox_Poción_4.SelectedIndex = 0;
                CheckBox_Mantener_Efectos_Originales.Checked = false;
                for (int Índice_Efecto = 0; Índice_Efecto < Efectos.Matriz.Length; Índice_Efecto++)
                {
                    try
                    {
                        Efectos Efecto = Efectos.Matriz[Índice_Efecto];
                        if (Efecto.Tipo == Efectos.Tipos.Major_curse ||
                            Efecto.Tipo == Efectos.Tipos.Minor_curse)
                        {
                            ListView_Efectos.Items[Índice_Efecto].Checked = true;
                        }
                        else ListView_Efectos.Items[Índice_Efecto].Checked = false;
                    }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
                NumericUpDown_Nivel_Bendiciones.Value = 1m;
                NumericUpDown_Nivel_Maldiciones.Value = 999m;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Button_Todo_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                NumericUpDown_HealthPoints.Value = 999999m;
                NumericUpDown_Gold.Value = 999999m;
                NumericUpDown_Thorium.Value = 999m;
                NumericUpDown_Nether.Value = 99m;
                NumericUpDown_Bombs.Value = 99m;
                NumericUpDown_Keys.Value = 99m;
                ComboBox_Bomb.SelectedIndex = 10;
                ComboBox_Gloves.SelectedIndex = 1;
                ComboBox_Weapon.SelectedIndex = 2;
                ComboBox_Hat.SelectedIndex = ComboBox_Hat.Items.Count - 1;
                ComboBox_Familiar_1_ID.SelectedIndex = 10;
                NumericUpDown_Familiar_1_XP.Value = 9999m;
                ComboBox_Familiar_2_ID.SelectedIndex = 1;
                NumericUpDown_Familiar_2_XP.Value = 9999m;
                ComboBox_Poción_2.SelectedIndex = 16;
                ComboBox_Poción_1.SelectedIndex = 16;
                ComboBox_Poción_3.SelectedIndex = 16;
                ComboBox_Poción_4.SelectedIndex = 16;
                CheckBox_Mantener_Efectos_Originales.Checked = false;
                for (int Índice_Efecto = 0; Índice_Efecto < Efectos.Matriz.Length; Índice_Efecto++)
                {
                    try
                    {
                        Efectos Efecto = Efectos.Matriz[Índice_Efecto];
                        if (Efecto.Tipo == Efectos.Tipos.Blessing ||
                            Efecto.Tipo == Efectos.Tipos.Crystal ||
                            Efecto.Tipo == Efectos.Tipos.Relic)
                        {
                            ListView_Efectos.Items[Índice_Efecto].Checked = true;
                        }
                        else ListView_Efectos.Items[Índice_Efecto].Checked = false;
                    }
                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                }
                NumericUpDown_Nivel_Bendiciones.Value = 999m;
                NumericUpDown_Nivel_Maldiciones.Value = 1m;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Button_Peasant_Cargar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string Ruta = Application.StartupPath + "\\Peasant.txt";
                FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite);
                if (Lector != null && Lector.Length > 0L)
                {
                    Lector.Seek(0L, SeekOrigin.Begin);
                    StreamReader Lector_Texto = new StreamReader(Lector, Encoding.UTF8, true);
                    string Nombre = null;
                    int Ropa = -1;
                    int Ojos = -1;
                    int Pelo = -1;
                    int Piel = -1;
                    int Sexo = -1;
                    while (!Lector_Texto.EndOfStream)
                    {
                        try
                        {
                            string Línea = Lector_Texto.ReadLine();
                            if (!string.IsNullOrEmpty(Línea) && !Línea.StartsWith("//"))
                            {
                                if (string.IsNullOrEmpty(Nombre))
                                {
                                    Nombre = Línea;
                                }
                                else if (Sexo <= -1)
                                {
                                    Sexo = int.Parse(Línea);
                                    if (Sexo < 0) Sexo = 0;
                                    else if (Sexo > 1) Sexo = 1;
                                }
                                else if (Ropa <= -1)
                                {
                                    Ropa = int.Parse(Línea);
                                    if (Ropa < 0) Ropa = 0;
                                    else if (Ropa > 12) Ropa = 12;
                                }
                                else if (Ojos <= -1)
                                {
                                    Ojos = int.Parse(Línea);
                                    if (Ojos < 0) Ojos = 0;
                                    else if (Ojos > 8) Ojos = 8;
                                }
                                else if (Pelo <= -1)
                                {
                                    Pelo = int.Parse(Línea);
                                    if (Pelo < 0) Pelo = 0;
                                    else if (Pelo > 8) Pelo = 8;
                                }
                                else if (Piel <= -1)
                                {
                                    Piel = int.Parse(Línea);
                                    if (Piel < 0) Piel = 0;
                                    else if (Piel > 8) Piel = 8;
                                }
                                else // No more options are expected...
                                {
                                    break;
                                }
                            }
                        }
                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                    }
                    if (!string.IsNullOrEmpty(Nombre) &&
                        Sexo > -1 &&
                        Ropa > -1 &&
                        Ojos > -1 &&
                        Pelo > -1 &&
                        Piel > -1)
                    {
                        TextBox_Nombre.Text = Nombre;
                        ComboBox_Sexo.SelectedIndex = Sexo;
                        NumericUpDown_Clothes.Value = (decimal)Ropa;
                        NumericUpDown_Eyes.Value = (decimal)Ojos;
                        NumericUpDown_Hair.Value = (decimal)Pelo;
                        NumericUpDown_Skin.Value = (decimal)Piel;
                    }
                    Lector_Texto.Close();
                    Lector_Texto.Dispose();
                    Lector_Texto = null;
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                }
                Ruta = null;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Button_Peasant_Guardar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string Ruta = Application.StartupPath + "\\Peasant.txt";
                FileStream Lector = new FileStream(Ruta, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);
                if (Lector != null)
                {
                    if (Lector.Length > 0L) Lector.SetLength(0L);
                    Lector.Seek(0L, SeekOrigin.Begin);
                    StreamWriter Lector_Texto = new StreamWriter(Lector, Encoding.UTF8);

                    string Nombre = TextBox_Nombre.Text;
                    if (string.IsNullOrEmpty(Nombre)) Nombre = Environment.UserName;

                    int Sexo = ComboBox_Sexo.SelectedIndex;
                    if (Sexo < 0) Sexo = 0;
                    else if (Sexo > 1) Sexo = 1;

                    int Ropa = (int)NumericUpDown_Clothes.Value;
                    if (Ropa < 0) Ropa = 0;
                    else if (Ropa > 12) Ropa = 12;

                    int Ojos = (int)NumericUpDown_Eyes.Value;
                    if (Ojos < 0) Ojos = 0;
                    else if (Ojos > 8) Ojos = 8;

                    int Pelo = (int)NumericUpDown_Hair.Value;
                    if (Pelo < 0) Pelo = 0;
                    else if (Pelo > 1) Pelo = 1;

                    int Piel = (int)NumericUpDown_Skin.Value;
                    if (Piel < 0) Piel = 0;
                    else if (Piel > 4) Piel = 4;

                    Lector_Texto.WriteLine("// Lines that start with \"//\" or are empty will be ignored.");

                    Lector_Texto.WriteLine("");
                    Lector_Texto.WriteLine("// Peasant name:");
                    Lector_Texto.WriteLine(Nombre);

                    Lector_Texto.WriteLine("");
                    Lector_Texto.WriteLine("// Peasant sex (0 = Man, 1 = Woman):");
                    Lector_Texto.WriteLine(Sexo.ToString());

                    Lector_Texto.WriteLine("");
                    Lector_Texto.WriteLine("// Peasant clothes color (0 to 12):");
                    Lector_Texto.WriteLine(Ropa.ToString());

                    Lector_Texto.WriteLine("");
                    Lector_Texto.WriteLine("// Peasant eyes color (0 to 8):");
                    Lector_Texto.WriteLine(Ojos.ToString());

                    Lector_Texto.WriteLine("");
                    Lector_Texto.WriteLine("// Peasant hair color (0 to 1):");
                    Lector_Texto.WriteLine(Pelo.ToString());

                    Lector_Texto.WriteLine("");
                    Lector_Texto.WriteLine("// Peasant skin color (0 to 4):");
                    Lector_Texto.Write(Piel.ToString());

                    Lector_Texto.Flush();
                    Lector_Texto.Close();
                    Lector_Texto.Dispose();
                    Lector_Texto = null;
                    Lector.Close();
                    Lector.Dispose();
                    Lector = null;
                    SystemSounds.Asterisk.Play();
                }
                Ruta = null;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Button_Ruta_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                TextBox_Ruta_TextChanged(TextBox_Ruta, EventArgs.Empty);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Guardado_Actualizar_Click(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Guardado_SelectedIndexChanged(ComboBox_Guardado, EventArgs.Empty);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Familiar_1_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Familiar_1_ID.Refresh();
                PictureBox_Familiar_1_ID.Image = ComboBox_Familiar_1_ID.SelectedIndex > 0 ? Familiares.Matriz[ComboBox_Familiar_1_ID.SelectedIndex - 1].Imagen : null;
                NumericUpDown_Familiar_1_XP_ValueChanged(NumericUpDown_Familiar_1_XP, EventArgs.Empty);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Familiar_2_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Familiar_2_ID.Refresh();
                PictureBox_Familiar_2_ID.Image = ComboBox_Familiar_2_ID.SelectedIndex > 0 ? Familiares.Matriz[ComboBox_Familiar_2_ID.SelectedIndex - 1].Imagen : null;
                NumericUpDown_Familiar_2_XP_ValueChanged(NumericUpDown_Familiar_2_XP, EventArgs.Empty);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Poción_1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Poción_1.Refresh();
                PictureBox_Poción_1.Image = ComboBox_Poción_1.SelectedIndex > 0 ? Pociones.Matriz[ComboBox_Poción_1.SelectedIndex - 1].Imagen : null;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Poción_2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Poción_2.Refresh();
                PictureBox_Poción_2.Image = ComboBox_Poción_2.SelectedIndex > 0 ? Pociones.Matriz[ComboBox_Poción_2.SelectedIndex - 1].Imagen : null;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Poción_3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Poción_3.Refresh();
                PictureBox_Poción_3.Image = ComboBox_Poción_3.SelectedIndex > 0 ? Pociones.Matriz[ComboBox_Poción_3.SelectedIndex - 1].Imagen : null;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Poción_4_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Poción_4.Refresh();
                PictureBox_Poción_4.Image = ComboBox_Poción_4.SelectedIndex > 0 ? Pociones.Matriz[ComboBox_Poción_4.SelectedIndex - 1].Imagen : null;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        internal void Recortar_Bordes_Imágenes(string Ruta_Entrada, bool Antialiasing)
        {
            try
            {
                int Conversiones = 0;
                int Total = 0;
                if (!string.IsNullOrEmpty(Ruta_Entrada) && Directory.Exists(Ruta_Entrada))
                {
                    string[] Matriz_Rutas = Directory.GetFiles(Ruta_Entrada, "*.png", SearchOption.AllDirectories);
                    if (Matriz_Rutas != null && Matriz_Rutas.Length > 0)
                    {
                        foreach (string Ruta in Matriz_Rutas)
                        {
                            try
                            {
                                Bitmap Imagen_Original = Program.Cargar_Imagen_Ruta(Ruta, CheckState.Checked);
                                if (Imagen_Original != null)
                                {
                                    Total++;
                                    int Ancho = Imagen_Original.Width;
                                    int Alto = Imagen_Original.Height;
                                    Rectangle Rectángulo = Program.Buscar_Zona_Recorte_Imagen(Imagen_Original, Color.Transparent);
                                    if (Rectángulo.X > -1 && Rectángulo.Y > -1 &&
                                        Rectángulo.X < int.MaxValue && Rectángulo.Y < int.MaxValue &&
                                        Rectángulo.Width > 0 && Rectángulo.Height > 0)
                                    {
                                        Imagen_Original = Imagen_Original.Clone(Rectángulo, PixelFormat.Format32bppArgb);
                                        int Máximo_Ancho_Alto = Math.Max(Rectángulo.Width, Rectángulo.Height);
                                        Bitmap Imagen = new Bitmap(Máximo_Ancho_Alto, Máximo_Ancho_Alto, PixelFormat.Format32bppArgb);
                                        Graphics Pintar = Graphics.FromImage(Imagen);
                                        Pintar.CompositingMode = CompositingMode.SourceCopy;
                                        Pintar.CompositingQuality = CompositingQuality.HighQuality;
                                        Pintar.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                        Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                                        Pintar.SmoothingMode = SmoothingMode.None;
                                        Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                                        Pintar.DrawImage(Imagen_Original, new Rectangle((int)Math.Round((double)(Máximo_Ancho_Alto - Rectángulo.Width) / 2d, MidpointRounding.AwayFromZero), (int)Math.Round((double)(Máximo_Ancho_Alto - Rectángulo.Height) / 2d, MidpointRounding.AwayFromZero), Rectángulo.Width, Rectángulo.Height), new Rectangle(0, 0, Rectángulo.Width, Rectángulo.Height), GraphicsUnit.Pixel);
                                        Pintar.Dispose();
                                        Pintar = null;
                                        Imagen = Program.Obtener_Imagen_Miniatura(Imagen, 18, 18, true, Antialiasing, CheckState.Checked);
                                        Imagen.Save(Ruta, ImageFormat.Png);
                                        Imagen.Dispose();
                                        Imagen = null;
                                        Imagen_Original.Dispose();
                                        Imagen_Original = null;
                                        Conversiones++;
                                    }
                                }
                            }
                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                        }
                    }
                    Matriz_Rutas = null;
                }
                if (Total > 0)
                {
                    MessageBox.Show(this, "Images successfully converted: " + Program.Traducir_Número(Conversiones) + " of " + Program.Traducir_Número(Total) + ".", Program.Texto_Título_Versión, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Bomb_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Bomb.Refresh();
                PictureBox_Bomb.Image = ComboBox_Bomb.SelectedIndex > -1 ? Mejoras_Bomba.Matriz[ComboBox_Bomb.SelectedIndex].Imagen : null;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Gloves_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Gloves.Refresh();
                PictureBox_Gloves.Image = ComboBox_Gloves.SelectedIndex > 0 ? Mejoras_Guantes.Matriz[ComboBox_Gloves.SelectedIndex].Imagen : null;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Weapon_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Weapon.Refresh();
                PictureBox_Weapon.Image = ComboBox_Weapon.SelectedIndex > 0 ? Mejoras_Arma.Matriz[ComboBox_Weapon.SelectedIndex].Imagen : null;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Hat_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox_Hat.Refresh();
                PictureBox_Hat.Image = ComboBox_Hat.SelectedIndex > 0 ? Mejoras_Sombrero.Matriz[ComboBox_Hat.SelectedIndex].Imagen : null;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ListView_Efectos_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            try
            {
                if (Diccionario_Índices_Combinaciones != null &&
                    Diccionario_Índices_Combinaciones.Count > 0)
                {
                    foreach (KeyValuePair<int, int[]> Entrada in Diccionario_Índices_Combinaciones)
                    {
                        try
                        {
                            if (ListView_Efectos.Items[Entrada.Key].Checked &&
                                Entrada.Value != null &&
                                Entrada.Value.Length > 0)
                            {
                                foreach (int Índice_Efecto in Entrada.Value)
                                {
                                    try
                                    {
                                        if (Índice_Efecto > -1 &&
                                            ListView_Efectos.Items[Índice_Efecto].Checked)
                                        {
                                            ListView_Efectos.Items[Índice_Efecto].Checked = false;
                                        }
                                        else if (Índice_Efecto < 0)
                                        {
                                            this.Text = Índice_Efecto.ToString();
                                        }
                                    }
                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                }
                            }
                        }
                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                    }
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        /// <summary>
        /// Loads a full UnderMine save file.
        /// </summary>
        /// <param name="Cargar_Othermine">False to load the Undermine data. True to load the Othermine data.</param>
        private void Cargar_Partida(bool Cargar_Othermine)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ComboBox_Guardado.Refresh();
                if (ComboBox_Guardado.SelectedIndex > -1)
                {
                    string Texto_Portapapeles = null;
                    string Ruta = TextBox_Ruta.Text + "\\" + ComboBox_Guardado.Text + ".json";
                    if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                    {
                        FileStream Lector = new FileStream(Ruta, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        Lector.Seek(0L, SeekOrigin.Begin);
                        if (Lector.Length > 0L)
                        {
                            StreamReader Lector_Texto = new StreamReader(Lector, Encoding.UTF8, true);
                            JsonTextReader Lector_JSON = new JsonTextReader(Lector_Texto);
                            while (Lector_JSON.Read())
                            {
                                try
                                {
                                    string Línea = null;
                                    if (Lector_JSON.TokenType == JsonToken.PropertyName)
                                    {
                                        try { Línea = Lector_JSON.Value.ToString(); }
                                        catch { Línea = null; }
                                        if (!string.IsNullOrEmpty(Línea))
                                        {
                                            if (string.Compare(Línea, "version", true) == 0) // Version of the save data.
                                            {
                                                try
                                                {
                                                    if (Lector_JSON.Read())
                                                    {
                                                        NumericUpDown_Version.Value = int.Parse(Lector_JSON.Value.ToString());
                                                    }
                                                }
                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                            }
                                            else if (string.Compare(Línea, "guid", true) == 0) // Current random guid.
                                            {
                                                try
                                                {
                                                    if (Lector_JSON.Read())
                                                    {
                                                        TextBox_Guid.Text = Lector_JSON.Value.ToString();
                                                    }
                                                }
                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                            }
                                            else if (string.Compare(Línea, "playTime", true) == 0) // Total play time in seconds.
                                            {
                                                try
                                                {
                                                    if (Lector_JSON.Read())
                                                    {
                                                        NumericUpDown_PlayTime.Value = decimal.Parse(Lector_JSON.Value.ToString().Replace(Program.Caracter_Coma_Decimal_Invertido, Program.Caracter_Coma_Decimal));
                                                    }
                                                }
                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                            }
                                            else if (string.Compare(Línea, "peonName", true) == 0) // Peasant name.
                                            {
                                                try
                                                {
                                                    if (Lector_JSON.Read())
                                                    {
                                                        TextBox_Nombre.Text = Lector_JSON.Value.ToString();
                                                    }
                                                }
                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                            }
                                            else if (string.Compare(Línea, "peonColor", true) == 0) // Peasant clothes, eyes, hair and skin colors.
                                            {
                                                try
                                                {
                                                    if (Lector_JSON.Read())
                                                    {
                                                        NumericUpDown_PeonColor.Value = int.Parse(Lector_JSON.Value.ToString());
                                                    }
                                                }
                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                            }
                                            else if (string.Compare(Línea, "peonID", true) == 0) // Peasant sex.
                                            {
                                                try
                                                {
                                                    if (Lector_JSON.Read())
                                                    {
                                                        ComboBox_Sexo.SelectedIndex = Sexos.Buscar_Índice(Lector_JSON.Value.ToString());
                                                    }
                                                }
                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                            }
                                            else if (string.Compare(Línea, "familiar", true) == 0) // First familiar ID.
                                            {
                                                try
                                                {
                                                    // Ignored.
                                                }
                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                            }
                                            else if (string.Compare(Línea, "altarItemID", true) == 0) // The ID of the item stored for later runs.
                                            {
                                                try
                                                {
                                                    if (Lector_JSON.Read())
                                                    {
                                                        ComboBox_Altar_Item.Text = Lector_JSON.Value.ToString();
                                                    }
                                                }
                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                            }
                                            else if (string.Compare(Línea, "unlocked", true) == 0) // The IDs of the unlocked items.
                                            {
                                                try
                                                {
                                                    // Coming soon...
                                                }
                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                            }
                                            else if (string.Compare(Línea, "discovered", true) == 0) // The IDs of the discovered items.
                                            {
                                                try
                                                {
                                                    // Coming soon...
                                                }
                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                            }
                                            else if (string.Compare(Línea, "logEntries", true) == 0) // Unknown. Always was empty so far.
                                            {
                                                try
                                                {
                                                    // Ignored.
                                                }
                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                            }
                                            else if (string.Compare(Línea, "upgradeString", true) == 0) // Massive string with all the upgraded items, etc.
                                            {
                                                try
                                                {
                                                    if (Lector_JSON.Read())
                                                    {
                                                        Línea = Lector_JSON.Value.ToString();
                                                        int Summon_Count = 0;
                                                        int Max_Summon_Count = 0;
                                                        if (!string.IsNullOrEmpty(Línea))
                                                        {
                                                            string[] Matriz_Líneas = Línea.TrimEnd(",".ToCharArray()).Split(",".ToCharArray(), StringSplitOptions.None);
                                                            if (Matriz_Líneas != null && Matriz_Líneas.Length > 0)
                                                            {
                                                                Línea = null;
                                                                foreach (string Línea_Mejora in Matriz_Líneas)
                                                                {
                                                                    try
                                                                    {
                                                                        if (!string.IsNullOrEmpty(Línea_Mejora) &&
                                                                            Línea_Mejora.Contains(':'))
                                                                        {
                                                                            // Now split each new string by the ":" separator.
                                                                            string[] Matriz_Mejora = Línea_Mejora.Split(":".ToCharArray(), StringSplitOptions.None);
                                                                            if (Matriz_Mejora != null &&
                                                                                Matriz_Mejora.Length >= 2 &&
                                                                                !string.IsNullOrEmpty(Matriz_Mejora[0]) &&
                                                                                !string.IsNullOrEmpty(Matriz_Mejora[1]))
                                                                            {
                                                                                if (string.Compare(Matriz_Mejora[0], "summon_count", true) == 0)
                                                                                {
                                                                                    Summon_Count = int.Parse(Matriz_Mejora[1]);
                                                                                }
                                                                                else if (string.Compare(Matriz_Mejora[0], "max_summon_count", true) == 0)
                                                                                {
                                                                                    Max_Summon_Count = int.Parse(Matriz_Mejora[1]);
                                                                                }
                                                                            }
                                                                            Matriz_Mejora = null;
                                                                        }
                                                                    }
                                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                }
                                                            }
                                                            Matriz_Líneas = null;
                                                        }
                                                        NumericUpDown_Summoning_Stone.Value = Math.Min(Summon_Count, Max_Summon_Count);
                                                        NumericUpDown_Summoning_Stone_Máximo.Value = Math.Max(Summon_Count, Max_Summon_Count);
                                                    }
                                                }
                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                            }
                                            else if (string.Compare(Línea, "foundCountString", true) == 0) // Massive string with a counter for each found item, etc.
                                            {
                                                try
                                                {
                                                    // Ignored.
                                                }
                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                            }
                                            else if (string.Compare(Línea, "killCountString", true) == 0) // Massive string with a counter for each killed enemy, etc.
                                            {
                                                try
                                                {
                                                    // Ignored.
                                                }
                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                            }
                                            else if (string.Compare(Línea, "autoSaveData", true) == 0) // Main header for UnderMine save data.
                                            {
                                                try
                                                {
                                                    int Objetos_Abiertos = 0; // Keep track of the openings and closings of JSON objects.
                                                    while (Lector_JSON.Read())
                                                    {
                                                        try
                                                        {
                                                            if (Lector_JSON.TokenType == JsonToken.StartObject)
                                                            {
                                                                Objetos_Abiertos++; // "{".
                                                            }
                                                            else if (Lector_JSON.TokenType == JsonToken.EndObject)
                                                            {
                                                                Objetos_Abiertos--; // "}".
                                                                if (Objetos_Abiertos <= 0) break;
                                                            }
                                                            else if (Lector_JSON.TokenType == JsonToken.PropertyName)
                                                            {
                                                                if (!Cargar_Othermine) // Undermine settings.
                                                                {
                                                                    try { Línea = Lector_JSON.Value.ToString(); }
                                                                    catch { Línea = null; }
                                                                    if (!string.IsNullOrEmpty(Línea))
                                                                    {
                                                                        if (string.Compare(Línea, "seed", true) == 0) // The random seed for the current level.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    NumericUpDown_Seed.Value = int.Parse(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "timer", true) == 0) // Current play time in seconds.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    NumericUpDown_Timer.Value = decimal.Parse(Lector_JSON.Value.ToString().Replace(Program.Caracter_Coma_Decimal_Invertido, Program.Caracter_Coma_Decimal));
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "zone", true) == 0) // The ID of the current level.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    ComboBox_Zone.SelectedIndex = Zonas.Buscar_Índice(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "hp", true) == 0) // The remaining health points.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    NumericUpDown_HealthPoints.Value = int.Parse(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "padding", true) == 0) // Unknown, but is 1 when the player is on the Othermine.
                                                                        {
                                                                            try
                                                                            {
                                                                                // Coming soon...
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "gold", true) == 0) // The current gold.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    NumericUpDown_Gold.Value = int.Parse(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "thorium", true) == 0) // The current thorium.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    NumericUpDown_Thorium.Value = int.Parse(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "nether", true) == 0) // The current nether.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    NumericUpDown_Nether.Value = int.Parse(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "bombs", true) == 0) // The current bombs.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    NumericUpDown_Bombs.Value = int.Parse(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "keys", true) == 0) // The current keys.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    NumericUpDown_Keys.Value = int.Parse(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "bomb", true) == 0) // The current unique bomb upgrade.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    ComboBox_Bomb.SelectedIndex = Mejoras_Bomba.Buscar_Índice(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "gloves", true) == 0) // The current unique gloves upgrade.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    ComboBox_Gloves.SelectedIndex = Mejoras_Guantes.Buscar_Índice(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "weapon", true) == 0) // The current unique weapon upgrade.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    ComboBox_Weapon.SelectedIndex = Mejoras_Arma.Buscar_Índice(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "hat", true) == 0) // The current unique hat upgrade.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    ComboBox_Hat.SelectedIndex = Mejoras_Sombrero.Buscar_Índice(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "potions", true) == 0) // Unknown. Always was empty so far.
                                                                        {
                                                                            try
                                                                            {
                                                                                // Ignored.
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "potionDatas", true) == 0) // The data of the current potions.
                                                                        {
                                                                            try
                                                                            {
                                                                                List<int> Lista_Índices_Pociones = new List<int>();
                                                                                int Matrices_Abiertas_Pociones = 0; // Keep track of the openings and closings of JSON "arrays".
                                                                                while (Lector_JSON.Read())
                                                                                {
                                                                                    try
                                                                                    {
                                                                                        if (Lector_JSON.TokenType == JsonToken.StartArray)
                                                                                        {
                                                                                            Matrices_Abiertas_Pociones++; // "[".
                                                                                        }
                                                                                        else if (Lector_JSON.TokenType == JsonToken.EndArray)
                                                                                        {
                                                                                            Matrices_Abiertas_Pociones--; // "]".
                                                                                            if (Matrices_Abiertas_Pociones <= 0) break;
                                                                                        }
                                                                                        else if (Lector_JSON.TokenType == JsonToken.PropertyName)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                try { Línea = Lector_JSON.Value.ToString(); }
                                                                                                catch { Línea = null; }
                                                                                                if (!string.IsNullOrEmpty(Línea))
                                                                                                {
                                                                                                    if (string.Compare(Línea, "id", true) == 0) // The random seed for the current level.
                                                                                                    {
                                                                                                        try
                                                                                                        {
                                                                                                            if (Lector_JSON.Read())
                                                                                                            {
                                                                                                                Lista_Índices_Pociones.Add(Pociones.Buscar_Índice(Lector_JSON.Value.ToString()));
                                                                                                            }
                                                                                                        }
                                                                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                        }
                                                                                    }
                                                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                                }
                                                                                while (Lista_Índices_Pociones.Count < 4) Lista_Índices_Pociones.Add(0);
                                                                                ComboBox_Poción_1.SelectedIndex = Lista_Índices_Pociones[0];
                                                                                ComboBox_Poción_2.SelectedIndex = Lista_Índices_Pociones[1];
                                                                                ComboBox_Poción_3.SelectedIndex = Lista_Índices_Pociones[2];
                                                                                ComboBox_Poción_4.SelectedIndex = Lista_Índices_Pociones[3];
                                                                                Lista_Índices_Pociones = null;
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "statusEffects", true) == 0) // The data of the status effects like blessings, curses, relics, etc.
                                                                        {
                                                                            try
                                                                            {
                                                                                bool Cargar_Efectos = !CheckBox_Mantener_Efectos_Originales.Checked;
                                                                                if (Cargar_Efectos)
                                                                                {
                                                                                    for (int Índice = 0; Índice < ListView_Efectos.Items.Count; Índice++)
                                                                                    {
                                                                                        try
                                                                                        {
                                                                                            ListView_Efectos.Items[Índice].Checked = false;
                                                                                        }
                                                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                                    }
                                                                                }
                                                                                int Nivel_Bendiciones = 1;
                                                                                int Nivel_Maldiciones = 1;
                                                                                int Índice_Actual = -1;
                                                                                int Matrices_Abiertas_Efectos = 0; // Keep track of the openings and closings of JSON arrays.
                                                                                while (Lector_JSON.Read())
                                                                                {
                                                                                    try
                                                                                    {
                                                                                        if (Lector_JSON.TokenType == JsonToken.StartArray)
                                                                                        {
                                                                                            Matrices_Abiertas_Efectos++; // "[".
                                                                                        }
                                                                                        else if (Lector_JSON.TokenType == JsonToken.EndArray)
                                                                                        {
                                                                                            Matrices_Abiertas_Efectos--; // "]".
                                                                                            if (Matrices_Abiertas_Efectos <= 0) break;
                                                                                        }
                                                                                        else if (Lector_JSON.TokenType == JsonToken.StartObject)
                                                                                        {
                                                                                            Índice_Actual = -1; // "{".
                                                                                        }
                                                                                        else if (Lector_JSON.TokenType == JsonToken.EndObject)
                                                                                        {
                                                                                            Índice_Actual = -1; // "}".
                                                                                        }
                                                                                        else if (Lector_JSON.TokenType == JsonToken.PropertyName)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                try { Línea = Lector_JSON.Value.ToString(); }
                                                                                                catch { Línea = null; }
                                                                                                if (Cargar_Efectos &&
                                                                                                    !string.IsNullOrEmpty(Línea))
                                                                                                {
                                                                                                    if (string.Compare(Línea, "id", true) == 0)
                                                                                                    {
                                                                                                        try
                                                                                                        {
                                                                                                            if (Lector_JSON.Read())
                                                                                                            {
                                                                                                                int Índice = Efectos.Buscar_Índice(Lector_JSON.Value.ToString());
                                                                                                                if (Índice > -1)
                                                                                                                {
                                                                                                                    ListView_Efectos.Items[Índice].Checked = true;
                                                                                                                    Índice_Actual = Índice;
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                                    }
                                                                                                    else if (string.Compare(Línea, "level", true) == 0)
                                                                                                    {
                                                                                                        try
                                                                                                        {
                                                                                                            if (Lector_JSON.Read() &&
                                                                                                                Índice_Actual > -1)
                                                                                                            {
                                                                                                                if (Efectos.Matriz[Índice_Actual].Tipo == Efectos.Tipos.Blessing)
                                                                                                                {
                                                                                                                    int Nivel = int.Parse(Lector_JSON.Value.ToString());
                                                                                                                    if (Nivel > Nivel_Bendiciones) Nivel_Bendiciones = Nivel;
                                                                                                                }
                                                                                                                else if (Efectos.Matriz[Índice_Actual].Tipo == Efectos.Tipos.Minor_curse)
                                                                                                                {
                                                                                                                    int Nivel = int.Parse(Lector_JSON.Value.ToString());
                                                                                                                    if (Nivel > Nivel_Maldiciones) Nivel_Maldiciones = Nivel;
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                        }
                                                                                    }
                                                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                                }
                                                                                NumericUpDown_Nivel_Bendiciones.Value = Math.Max(Nivel_Bendiciones, 1);
                                                                                NumericUpDown_Nivel_Maldiciones.Value = Math.Max(Nivel_Maldiciones, 1);
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "modifiers", true) == 0) // Unknown. Always was empty so far.
                                                                        {
                                                                            try
                                                                            {
                                                                                // Ignored.
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "familiarXP", true) == 0) // The data of the current familiars.
                                                                        {
                                                                            try
                                                                            {
                                                                                List<int> Lista_Índices_Familiares = new List<int>();
                                                                                List<int> Lista_XP_Familiares = new List<int>();
                                                                                int Matrices_Abiertas_Familiares = 0; // Keep track of the openings and closings of JSON "arrays".
                                                                                while (Lector_JSON.Read())
                                                                                {
                                                                                    try
                                                                                    {
                                                                                        if (Lector_JSON.TokenType == JsonToken.StartArray)
                                                                                        {
                                                                                            Matrices_Abiertas_Familiares++; // "[".
                                                                                        }
                                                                                        else if (Lector_JSON.TokenType == JsonToken.EndArray)
                                                                                        {
                                                                                            Matrices_Abiertas_Familiares--; // "]".
                                                                                            if (Matrices_Abiertas_Familiares <= 0) break;
                                                                                        }
                                                                                        else if (Lector_JSON.TokenType == JsonToken.PropertyName)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                try { Línea = Lector_JSON.Value.ToString(); }
                                                                                                catch { Línea = null; }
                                                                                                if (!string.IsNullOrEmpty(Línea))
                                                                                                {
                                                                                                    if (string.Compare(Línea, "id", true) == 0)
                                                                                                    {
                                                                                                        try
                                                                                                        {
                                                                                                            if (Lector_JSON.Read())
                                                                                                            {
                                                                                                                Lista_Índices_Familiares.Add(Familiares.Buscar_Índice(Lector_JSON.Value.ToString()));
                                                                                                            }
                                                                                                        }
                                                                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                                    }
                                                                                                    else if (string.Compare(Línea, "xp", true) == 0)
                                                                                                    {
                                                                                                        try
                                                                                                        {
                                                                                                            if (Lector_JSON.Read())
                                                                                                            {
                                                                                                                Lista_XP_Familiares.Add(int.Parse(Lector_JSON.Value.ToString()));
                                                                                                            }
                                                                                                        }
                                                                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                        }
                                                                                    }
                                                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                                }
                                                                                while (Lista_Índices_Familiares.Count < 2) Lista_Índices_Familiares.Add(0);
                                                                                while (Lista_XP_Familiares.Count < 2) Lista_XP_Familiares.Add(0);
                                                                                ComboBox_Familiar_1_ID.SelectedIndex = Lista_Índices_Familiares[0];
                                                                                ComboBox_Familiar_2_ID.SelectedIndex = Lista_Índices_Familiares[1];
                                                                                NumericUpDown_Familiar_1_XP.Value = Lista_XP_Familiares[0];
                                                                                NumericUpDown_Familiar_2_XP.Value = Lista_XP_Familiares[1];
                                                                                Lista_Índices_Familiares = null;
                                                                                Lista_XP_Familiares = null;
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "dropHistory", true) == 0) // The currently dropped items.
                                                                        {
                                                                            try
                                                                            {
                                                                                // Ignored.
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "killHistory", true) == 0) // The currently killed enemies.
                                                                        {
                                                                            try
                                                                            {
                                                                                // Ignored.
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "pickupHistory", true) == 0) // The currently picked up items.
                                                                        {
                                                                            try
                                                                            {
                                                                                // Ignored.
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "prayHistory", true) == 0) // Unknown. Always was empty so far.
                                                                        {
                                                                            try
                                                                            {
                                                                                // Ignored.
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                    }
                                                }
                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                            }
                                            else if (string.Compare(Línea, "rogueSaveData", true) == 0) // Main header for Othermine save data.
                                            {
                                                try
                                                {
                                                    int Objetos_Abiertos = 0; // Keep track of the openings and closings of JSON objects.
                                                    while (Lector_JSON.Read())
                                                    {
                                                        try
                                                        {
                                                            if (Lector_JSON.TokenType == JsonToken.StartObject)
                                                            {
                                                                Objetos_Abiertos++; // "{".
                                                            }
                                                            else if (Lector_JSON.TokenType == JsonToken.EndObject)
                                                            {
                                                                Objetos_Abiertos--; // "}".
                                                                if (Objetos_Abiertos <= 0) break;
                                                            }
                                                            else if (Lector_JSON.TokenType == JsonToken.PropertyName)
                                                            {
                                                                if (Cargar_Othermine) // Othermine settings.
                                                                {
                                                                    try { Línea = Lector_JSON.Value.ToString(); }
                                                                    catch { Línea = null; }
                                                                    if (!string.IsNullOrEmpty(Línea))
                                                                    {
                                                                        if (string.Compare(Línea, "seed", true) == 0) // The random seed for the current level.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    NumericUpDown_Seed.Value = int.Parse(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "timer", true) == 0) // Current play time in seconds.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    NumericUpDown_Timer.Value = decimal.Parse(Lector_JSON.Value.ToString().Replace(Program.Caracter_Coma_Decimal_Invertido, Program.Caracter_Coma_Decimal));
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "zone", true) == 0) // The ID of the current level.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    ComboBox_Zone.SelectedIndex = Zonas.Buscar_Índice(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "hp", true) == 0) // The remaining health points.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    NumericUpDown_HealthPoints.Value = int.Parse(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "padding", true) == 0) // Unknown, but is 1 when the player is on the Othermine.
                                                                        {
                                                                            try
                                                                            {
                                                                                // Coming soon...
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "gold", true) == 0) // The current gold.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    NumericUpDown_Gold.Value = int.Parse(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "thorium", true) == 0) // The current thorium.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    NumericUpDown_Thorium.Value = int.Parse(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "nether", true) == 0) // The current nether.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    NumericUpDown_Nether.Value = int.Parse(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "bombs", true) == 0) // The current bombs.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    NumericUpDown_Bombs.Value = int.Parse(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "keys", true) == 0) // The current keys.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    NumericUpDown_Keys.Value = int.Parse(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "bomb", true) == 0) // The current unique bomb upgrade.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    ComboBox_Bomb.SelectedIndex = Mejoras_Bomba.Buscar_Índice(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "gloves", true) == 0) // The current unique gloves upgrade.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    ComboBox_Gloves.SelectedIndex = Mejoras_Guantes.Buscar_Índice(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "weapon", true) == 0) // The current unique weapon upgrade.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    ComboBox_Weapon.SelectedIndex = Mejoras_Arma.Buscar_Índice(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "hat", true) == 0) // The current unique hat upgrade.
                                                                        {
                                                                            try
                                                                            {
                                                                                if (Lector_JSON.Read())
                                                                                {
                                                                                    ComboBox_Hat.SelectedIndex = Mejoras_Sombrero.Buscar_Índice(Lector_JSON.Value.ToString());
                                                                                }
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "potions", true) == 0) // Unknown. Always was empty so far.
                                                                        {
                                                                            try
                                                                            {
                                                                                // Ignored.
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "potionDatas", true) == 0) // The data of the current potions.
                                                                        {
                                                                            try
                                                                            {
                                                                                List<int> Lista_Índices_Pociones = new List<int>();
                                                                                int Matrices_Abiertas_Pociones = 0; // Keep track of the openings and closings of JSON "arrays".
                                                                                while (Lector_JSON.Read())
                                                                                {
                                                                                    try
                                                                                    {
                                                                                        if (Lector_JSON.TokenType == JsonToken.StartArray)
                                                                                        {
                                                                                            Matrices_Abiertas_Pociones++; // "[".
                                                                                        }
                                                                                        else if (Lector_JSON.TokenType == JsonToken.EndArray)
                                                                                        {
                                                                                            Matrices_Abiertas_Pociones--; // "]".
                                                                                            if (Matrices_Abiertas_Pociones <= 0) break;
                                                                                        }
                                                                                        else if (Lector_JSON.TokenType == JsonToken.PropertyName)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                try { Línea = Lector_JSON.Value.ToString(); }
                                                                                                catch { Línea = null; }
                                                                                                if (!string.IsNullOrEmpty(Línea))
                                                                                                {
                                                                                                    if (string.Compare(Línea, "id", true) == 0) // The random seed for the current level.
                                                                                                    {
                                                                                                        try
                                                                                                        {
                                                                                                            if (Lector_JSON.Read())
                                                                                                            {
                                                                                                                Lista_Índices_Pociones.Add(Pociones.Buscar_Índice(Lector_JSON.Value.ToString()));
                                                                                                            }
                                                                                                        }
                                                                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                        }
                                                                                    }
                                                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                                }
                                                                                while (Lista_Índices_Pociones.Count < 4) Lista_Índices_Pociones.Add(0);
                                                                                ComboBox_Poción_1.SelectedIndex = Lista_Índices_Pociones[0];
                                                                                ComboBox_Poción_2.SelectedIndex = Lista_Índices_Pociones[1];
                                                                                ComboBox_Poción_3.SelectedIndex = Lista_Índices_Pociones[2];
                                                                                ComboBox_Poción_4.SelectedIndex = Lista_Índices_Pociones[3];
                                                                                Lista_Índices_Pociones = null;
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "statusEffects", true) == 0) // The data of the status effects like blessings, curses, relics, etc.
                                                                        {
                                                                            try
                                                                            {
                                                                                bool Cargar_Efectos = !CheckBox_Mantener_Efectos_Originales.Checked;
                                                                                if (Cargar_Efectos)
                                                                                {
                                                                                    for (int Índice = 0; Índice < ListView_Efectos.Items.Count; Índice++)
                                                                                    {
                                                                                        try
                                                                                        {
                                                                                            ListView_Efectos.Items[Índice].Checked = false;
                                                                                        }
                                                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                                    }
                                                                                }
                                                                                int Nivel_Bendiciones = 1;
                                                                                int Nivel_Maldiciones = 1;
                                                                                int Índice_Actual = -1;
                                                                                int Matrices_Abiertas_Efectos = 0; // Keep track of the openings and closings of JSON arrays.
                                                                                while (Lector_JSON.Read())
                                                                                {
                                                                                    try
                                                                                    {
                                                                                        if (Lector_JSON.TokenType == JsonToken.StartArray)
                                                                                        {
                                                                                            Matrices_Abiertas_Efectos++; // "[".
                                                                                        }
                                                                                        else if (Lector_JSON.TokenType == JsonToken.EndArray)
                                                                                        {
                                                                                            Matrices_Abiertas_Efectos--; // "]".
                                                                                            if (Matrices_Abiertas_Efectos <= 0) break;
                                                                                        }
                                                                                        else if (Lector_JSON.TokenType == JsonToken.StartObject)
                                                                                        {
                                                                                            Índice_Actual = -1; // "{".
                                                                                        }
                                                                                        else if (Lector_JSON.TokenType == JsonToken.EndObject)
                                                                                        {
                                                                                            Índice_Actual = -1; // "}".
                                                                                        }
                                                                                        else if (Lector_JSON.TokenType == JsonToken.PropertyName)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                try { Línea = Lector_JSON.Value.ToString(); }
                                                                                                catch { Línea = null; }
                                                                                                if (Cargar_Efectos &&
                                                                                                    !string.IsNullOrEmpty(Línea))
                                                                                                {
                                                                                                    if (string.Compare(Línea, "id", true) == 0)
                                                                                                    {
                                                                                                        try
                                                                                                        {
                                                                                                            if (Lector_JSON.Read())
                                                                                                            {
                                                                                                                int Índice = Efectos.Buscar_Índice(Lector_JSON.Value.ToString());
                                                                                                                if (Índice > -1)
                                                                                                                {
                                                                                                                    ListView_Efectos.Items[Índice].Checked = true;
                                                                                                                    Índice_Actual = Índice;
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                                    }
                                                                                                    else if (string.Compare(Línea, "level", true) == 0)
                                                                                                    {
                                                                                                        try
                                                                                                        {
                                                                                                            if (Lector_JSON.Read() &&
                                                                                                                Índice_Actual > -1)
                                                                                                            {
                                                                                                                if (Efectos.Matriz[Índice_Actual].Tipo == Efectos.Tipos.Blessing)
                                                                                                                {
                                                                                                                    int Nivel = int.Parse(Lector_JSON.Value.ToString());
                                                                                                                    if (Nivel > Nivel_Bendiciones) Nivel_Bendiciones = Nivel;
                                                                                                                }
                                                                                                                else if (Efectos.Matriz[Índice_Actual].Tipo == Efectos.Tipos.Minor_curse)
                                                                                                                {
                                                                                                                    int Nivel = int.Parse(Lector_JSON.Value.ToString());
                                                                                                                    if (Nivel > Nivel_Maldiciones) Nivel_Maldiciones = Nivel;
                                                                                                                }
                                                                                                            }
                                                                                                        }
                                                                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                        }
                                                                                    }
                                                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                                }
                                                                                NumericUpDown_Nivel_Bendiciones.Value = Math.Max(Nivel_Bendiciones, 1);
                                                                                NumericUpDown_Nivel_Maldiciones.Value = Math.Max(Nivel_Maldiciones, 1);
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "modifiers", true) == 0) // Unknown. Always was empty so far.
                                                                        {
                                                                            try
                                                                            {
                                                                                // Ignored.
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "familiarXP", true) == 0) // The data of the current familiars.
                                                                        {
                                                                            try
                                                                            {
                                                                                List<int> Lista_Índices_Familiares = new List<int>();
                                                                                List<int> Lista_XP_Familiares = new List<int>();
                                                                                int Matrices_Abiertas_Familiares = 0; // Keep track of the openings and closings of JSON "arrays".
                                                                                while (Lector_JSON.Read())
                                                                                {
                                                                                    try
                                                                                    {
                                                                                        if (Lector_JSON.TokenType == JsonToken.StartArray)
                                                                                        {
                                                                                            Matrices_Abiertas_Familiares++; // "[".
                                                                                        }
                                                                                        else if (Lector_JSON.TokenType == JsonToken.EndArray)
                                                                                        {
                                                                                            Matrices_Abiertas_Familiares--; // "]".
                                                                                            if (Matrices_Abiertas_Familiares <= 0) break;
                                                                                        }
                                                                                        else if (Lector_JSON.TokenType == JsonToken.PropertyName)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                try { Línea = Lector_JSON.Value.ToString(); }
                                                                                                catch { Línea = null; }
                                                                                                if (!string.IsNullOrEmpty(Línea))
                                                                                                {
                                                                                                    if (string.Compare(Línea, "id", true) == 0)
                                                                                                    {
                                                                                                        try
                                                                                                        {
                                                                                                            if (Lector_JSON.Read())
                                                                                                            {
                                                                                                                Lista_Índices_Familiares.Add(Familiares.Buscar_Índice(Lector_JSON.Value.ToString()));
                                                                                                            }
                                                                                                        }
                                                                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                                    }
                                                                                                    else if (string.Compare(Línea, "xp", true) == 0)
                                                                                                    {
                                                                                                        try
                                                                                                        {
                                                                                                            if (Lector_JSON.Read())
                                                                                                            {
                                                                                                                Lista_XP_Familiares.Add(int.Parse(Lector_JSON.Value.ToString()));
                                                                                                            }
                                                                                                        }
                                                                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                                    }
                                                                                                }
                                                                                            }
                                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                        }
                                                                                    }
                                                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                                }
                                                                                while (Lista_Índices_Familiares.Count < 2) Lista_Índices_Familiares.Add(0);
                                                                                while (Lista_XP_Familiares.Count < 2) Lista_XP_Familiares.Add(0);
                                                                                ComboBox_Familiar_1_ID.SelectedIndex = Lista_Índices_Familiares[0];
                                                                                ComboBox_Familiar_2_ID.SelectedIndex = Lista_Índices_Familiares[1];
                                                                                NumericUpDown_Familiar_1_XP.Value = Lista_XP_Familiares[0];
                                                                                NumericUpDown_Familiar_2_XP.Value = Lista_XP_Familiares[1];
                                                                                Lista_Índices_Familiares = null;
                                                                                Lista_XP_Familiares = null;
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "dropHistory", true) == 0) // The currently dropped items.
                                                                        {
                                                                            try
                                                                            {
                                                                                // Ignored.
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "killHistory", true) == 0) // The currently killed enemies.
                                                                        {
                                                                            try
                                                                            {
                                                                                // Ignored.
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "pickupHistory", true) == 0) // The currently picked up items.
                                                                        {
                                                                            try
                                                                            {
                                                                                // Ignored.
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                        else if (string.Compare(Línea, "prayHistory", true) == 0) // Unknown. Always was empty so far.
                                                                        {
                                                                            try
                                                                            {
                                                                                // Ignored.
                                                                            }
                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                    }
                                                }
                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                            }
                                        }
                                    }
                                }
                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                            }
                            Lector_JSON.Close();
                            Lector_JSON = null;
                            Lector_Texto.Close();
                            Lector_Texto.Dispose();
                            Lector_Texto = null;
                        }
                        Lector.Close();
                        Lector.Dispose();
                        Lector = null;
                    }
                    if (!string.IsNullOrEmpty(Texto_Portapapeles) && string.Compare(Environment.UserName, "Jupisoft", true) == 0)
                    {
                        SystemSounds.Asterisk.Play();
                        Clipboard.SetText(Texto_Portapapeles);
                        Texto_Portapapeles = null;
                    }
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        /// <summary>
        /// Saves a full UnderMine save file.
        /// </summary>
        /// <param name="Cargar_Othermine">False to save to the Undermine data. True to save to the Othermine data.</param>
        private void Guardar_Partida(bool Guardar_Othermine)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ComboBox_Guardado.Refresh();
                if (ComboBox_Guardado.SelectedIndex > -1)
                {
                    string Ruta = TextBox_Ruta.Text + "\\" + ComboBox_Guardado.Text + ".json";
                    if (!string.IsNullOrEmpty(Ruta) && File.Exists(Ruta))
                    {
                        Program.Crear_Carpetas(Ruta_Backups);
                        string Ruta_Backup = Ruta_Backups + "\\" + Program.Obtener_Nombre_Temporal() + " " + ComboBox_Guardado.Text + ".json";
                        // Make a backup of the original file and only continue if it was a success.
                        if (Program.Copiar_Archivo(Ruta, Ruta_Backup))
                        {
                            // Load the backup file as read-only to copy it's contents to the original file.
                            FileStream Lector_Entrada = new FileStream(Ruta_Backup, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                            Lector_Entrada.Seek(0L, SeekOrigin.Begin);
                            if (Lector_Entrada.Length > 0L)
                            {
                                StreamReader Lector_Texto_Entrada = new StreamReader(Lector_Entrada, Encoding.UTF8, true);
                                JsonTextReader Lector_JSON_Entrada = new JsonTextReader(Lector_Texto_Entrada);
                                FileStream Lector_Salida = new FileStream(Ruta, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                                //FileStream Lector_Salida = new FileStream(Path.GetDirectoryName(Ruta) + "\\" + Path.GetFileNameWithoutExtension(Ruta) + "_" + Path.GetExtension(Ruta), FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                                Lector_Salida.SetLength(0L);
                                Lector_Salida.Seek(0L, SeekOrigin.Begin);
                                StreamWriter Lector_Texto_Salida = new StreamWriter(Lector_Salida, Encoding.UTF8);
                                Lector_Texto_Salida.AutoFlush = true;
                                JsonTextWriter Lector_JSON_Salida = new JsonTextWriter(Lector_Texto_Salida);
                                // Write multiple lines with 4 white spaces at the beginning of each one.
                                Lector_JSON_Salida.Formatting = Formatting.Indented;
                                Lector_JSON_Salida.Indentation = 4;
                                // Tried this to only add "\n" as the end of line, but still added "\r\n" instead.
                                Lector_JSON_Salida.StringEscapeHandling = StringEscapeHandling.Default;
                                //Lector_JSON_Salida.StringEscapeHandling = StringEscapeHandling.Default;
                                while (Lector_JSON_Entrada.Read())
                                {
                                    try
                                    {
                                        string Línea = null;
                                        if (Lector_JSON_Entrada.TokenType == JsonToken.PropertyName)
                                        {
                                            Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                            try { Línea = Lector_JSON_Entrada.Value.ToString(); }
                                            catch { Línea = null; }
                                            if (!string.IsNullOrEmpty(Línea))
                                            {
                                                if (string.Compare(Línea, "version", true) == 0) // Version of the save data.
                                                {
                                                    try
                                                    {
                                                        if (Lector_JSON_Entrada.Read())
                                                        {
                                                            Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, (int)NumericUpDown_Version.Value);
                                                        }
                                                    }
                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                }
                                                else if (string.Compare(Línea, "guid", true) == 0) // Current random guid.
                                                {
                                                    try
                                                    {
                                                        if (Lector_JSON_Entrada.Read())
                                                        {
                                                            Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, TextBox_Guid.Text);
                                                        }
                                                    }
                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                }
                                                else if (string.Compare(Línea, "playTime", true) == 0) // Total play time in seconds.
                                                {
                                                    try
                                                    {
                                                        if (Lector_JSON_Entrada.Read())
                                                        {
                                                            Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, NumericUpDown_PlayTime.Value);
                                                        }
                                                    }
                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                }
                                                else if (string.Compare(Línea, "peonName", true) == 0) // Peasant name.
                                                {
                                                    try
                                                    {
                                                        if (Lector_JSON_Entrada.Read())
                                                        {
                                                            Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, TextBox_Nombre.Text);
                                                        }
                                                    }
                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                }
                                                else if (string.Compare(Línea, "peonColor", true) == 0) // Peasant clothes, eyes, hair and skin colors.
                                                {
                                                    try
                                                    {
                                                        if (Lector_JSON_Entrada.Read())
                                                        {
                                                            Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, (int)NumericUpDown_PeonColor.Value);
                                                        }
                                                    }
                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                }
                                                else if (string.Compare(Línea, "peonID", true) == 0) // Peasant sex.
                                                {
                                                    try
                                                    {
                                                        if (Lector_JSON_Entrada.Read())
                                                        {
                                                            Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Sexos.Matriz[ComboBox_Sexo.SelectedIndex > -1 ? ComboBox_Sexo.SelectedIndex : 0].Id);
                                                        }
                                                    }
                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                }
                                                else if (string.Compare(Línea, "familiar", true) == 0) // First familiar ID.
                                                {
                                                    try
                                                    {
                                                        if (Lector_JSON_Entrada.Read())
                                                        {
                                                            Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, ComboBox_Familiar_1_ID.SelectedIndex > 0 ? Familiares.Matriz[ComboBox_Familiar_1_ID.SelectedIndex - 1].Id : "");
                                                        }
                                                    }
                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                }
                                                else if (string.Compare(Línea, "altarItemID", true) == 0) // The ID of the item stored for later runs.
                                                {
                                                    try
                                                    {
                                                        if (Lector_JSON_Entrada.Read())
                                                        {
                                                            Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, ComboBox_Altar_Item.Text);
                                                        }
                                                    }
                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                }
                                                else if (string.Compare(Línea, "unlocked", true) == 0) // The IDs of the unlocked items.
                                                {
                                                    try
                                                    {
                                                        // Coming soon...
                                                    }
                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                }
                                                else if (string.Compare(Línea, "discovered", true) == 0) // The IDs of the discovered items.
                                                {
                                                    try
                                                    {
                                                        // Coming soon...
                                                    }
                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                }
                                                else if (string.Compare(Línea, "logEntries", true) == 0) // Unknown. Always was empty so far.
                                                {
                                                    try
                                                    {
                                                        // Ignored.
                                                    }
                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                }
                                                else if (string.Compare(Línea, "upgradeString", true) == 0) // Massive string with all the upgraded items, etc.
                                                {
                                                    try
                                                    {
                                                        if (Lector_JSON_Entrada.Read())
                                                        {
                                                            try { Línea = Lector_JSON_Entrada.Value.ToString(); }
                                                            catch { Línea = null; }
                                                            if (!string.IsNullOrEmpty(Línea))
                                                            {
                                                                /*foreach (char Caracter in Línea) // Test: OK. All is lower case so far.
                                                                {
                                                                    if (char.IsUpper(Caracter))
                                                                    {
                                                                        MessageBox.Show(Caracter.ToString(), ((int)Caracter).ToString());
                                                                    }
                                                                }*/
                                                                // First remove the last "," and add it again at the end.
                                                                // Then split the massive string by the "," separator.
                                                                string[] Matriz_Líneas = Línea.TrimEnd(",".ToCharArray()).Split(",".ToCharArray(), StringSplitOptions.None);
                                                                if (Matriz_Líneas != null && Matriz_Líneas.Length > 0)
                                                                {
                                                                    Línea = null; // Reset to rebuild the modified string later on.
                                                                    SortedDictionary<string, string> Diccionario_Mejoras = new SortedDictionary<string, string>();
                                                                    foreach (string Línea_Mejora in Matriz_Líneas)
                                                                    {
                                                                        try
                                                                        {
                                                                            if (!string.IsNullOrEmpty(Línea_Mejora) &&
                                                                                Línea_Mejora.Contains(':'))
                                                                            {
                                                                                // Now split each new string by the ":" separator.
                                                                                string[] Matriz_Mejora = Línea_Mejora.Split(":".ToCharArray(), StringSplitOptions.None);
                                                                                if (Matriz_Mejora != null &&
                                                                                    Matriz_Mejora.Length >= 2 &&
                                                                                    !string.IsNullOrEmpty(Matriz_Mejora[0]) &&
                                                                                    !Diccionario_Mejoras.ContainsKey(Matriz_Mejora[0]))
                                                                                {
                                                                                    Diccionario_Mejoras.Add(Matriz_Mejora[0], Matriz_Mejora[1]);
                                                                                }
                                                                                Matriz_Mejora = null;
                                                                            }
                                                                        }
                                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                    }
                                                                    // Now look at the upgrade strings we've found and add the missing ones if needed.
                                                                    if (!Diccionario_Mejoras.ContainsKey("summon_count"))
                                                                    {
                                                                        Diccionario_Mejoras.Add("summon_count", Math.Min((int)NumericUpDown_Summoning_Stone.Value, (int)NumericUpDown_Summoning_Stone_Máximo.Value).ToString());
                                                                    }
                                                                    else Diccionario_Mejoras["summon_count"] = Math.Min((int)NumericUpDown_Summoning_Stone.Value, (int)NumericUpDown_Summoning_Stone_Máximo.Value).ToString();

                                                                    if (!Diccionario_Mejoras.ContainsKey("max_summon_count"))
                                                                    {
                                                                        Diccionario_Mejoras.Add("max_summon_count", Math.Max((int)NumericUpDown_Summoning_Stone.Value, (int)NumericUpDown_Summoning_Stone_Máximo.Value).ToString());
                                                                    }
                                                                    else Diccionario_Mejoras["max_summon_count"] = Math.Max((int)NumericUpDown_Summoning_Stone.Value, (int)NumericUpDown_Summoning_Stone_Máximo.Value).ToString();

                                                                    if (ComboBox_Doors.SelectedIndex == 1) // Close all.
                                                                    {
                                                                        if (!Diccionario_Mejoras.ContainsKey("dungeon_opened"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("dungeon_opened", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["dungeon_opened"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("cavern_entered"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("cavern_entered", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["cavern_entered"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("core_opened"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("core_opened", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["core_opened"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("halls_opened"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("halls_opened", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["halls_opened"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("final_gate_opened"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("final_gate_opened", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["final_gate_opened"] = "0";
                                                                    }
                                                                    else if (ComboBox_Doors.SelectedIndex == 2) // Open all.
                                                                    {
                                                                        if (!Diccionario_Mejoras.ContainsKey("dungeon_opened"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("dungeon_opened", "1");
                                                                        }
                                                                        else Diccionario_Mejoras["dungeon_opened"] = "1";

                                                                        if (!Diccionario_Mejoras.ContainsKey("cavern_entered"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("cavern_entered", "1");
                                                                        }
                                                                        else Diccionario_Mejoras["cavern_entered"] = "1";

                                                                        if (!Diccionario_Mejoras.ContainsKey("core_opened"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("core_opened", "1");
                                                                        }
                                                                        else Diccionario_Mejoras["core_opened"] = "1";

                                                                        if (!Diccionario_Mejoras.ContainsKey("halls_opened"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("halls_opened", "1");
                                                                        }
                                                                        else Diccionario_Mejoras["halls_opened"] = "1";

                                                                        if (!Diccionario_Mejoras.ContainsKey("final_gate_opened"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("final_gate_opened", "1");
                                                                        }
                                                                        else Diccionario_Mejoras["final_gate_opened"] = "1";
                                                                    }

                                                                    if (ComboBox_Bosses.SelectedIndex == 1) // Revive all.
                                                                    {
                                                                        if (!Diccionario_Mejoras.ContainsKey("sandworm_revived"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("sandworm_revived", "1");
                                                                        }
                                                                        else Diccionario_Mejoras["sandworm_revived"] = "1";

                                                                        if (!Diccionario_Mejoras.ContainsKey("shadowlord_revived"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("shadowlord_revived", "1");
                                                                        }
                                                                        else Diccionario_Mejoras["shadowlord_revived"] = "1";

                                                                        if (!Diccionario_Mejoras.ContainsKey("stonelord_revived"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("stonelord_revived", "1");
                                                                        }
                                                                        else Diccionario_Mejoras["stonelord_revived"] = "1";

                                                                        if (!Diccionario_Mejoras.ContainsKey("crystallord_revived"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("crystallord_revived", "1");
                                                                        }
                                                                        else Diccionario_Mejoras["crystallord_revived"] = "1";

                                                                        if (!Diccionario_Mejoras.ContainsKey("firelord_revived"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("firelord_revived", "1");
                                                                        }
                                                                        else Diccionario_Mejoras["firelord_revived"] = "1";
                                                                    }
                                                                    else if (ComboBox_Bosses.SelectedIndex == 2) // Kill all.
                                                                    {
                                                                        if (!Diccionario_Mejoras.ContainsKey("sandworm_revived"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("sandworm_revived", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["sandworm_revived"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("shadowlord_revived"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("shadowlord_revived", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["shadowlord_revived"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("stonelord_revived"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("stonelord_revived", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["stonelord_revived"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("crystallord_revived"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("crystallord_revived", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["crystallord_revived"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("firelord_revived"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("firelord_revived", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["firelord_revived"] = "0";
                                                                    }

                                                                    if (ComboBox_Upgrades.SelectedIndex == 1) // Remove all.
                                                                    {
                                                                        if (!Diccionario_Mejoras.ContainsKey("pickaxe_upgrade"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("pickaxe_upgrade", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["pickaxe_upgrade"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("tunic_upgrade"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("tunic_upgrade", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["tunic_upgrade"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("glove_upgrade"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("glove_upgrade", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["glove_upgrade"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("counterweight_upgrade"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("counterweight_upgrade", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["counterweight_upgrade"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("bomb_upgrade"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("bomb_upgrade", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["bomb_upgrade"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("goldsack_upgrade"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("goldsack_upgrade", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["goldsack_upgrade"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("show_enemy_hp"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("show_enemy_hp", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["show_enemy_hp"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("geckos_foot"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("geckos_foot", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["geckos_foot"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("big_bomb_upgrade"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("big_bomb_upgrade", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["big_bomb_upgrade"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("speedboost_boots"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("speedboost_boots", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["speedboost_boots"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("potion_count"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("potion_count", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["potion_count"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("shaker"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("shaker", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["shaker"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("drink_duration"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("drink_duration", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["drink_duration"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("shop_transmute_machine"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("shop_transmute_machine", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["shop_transmute_machine"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("shop_potion_relic"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("shop_potion_relic", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["shop_potion_relic"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("shop_basic_item"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("shop_basic_item", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["shop_basic_item"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("shop_food"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("shop_food", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["shop_food"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("start_blessing"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("start_blessing", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["start_blessing"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("altar_blessing_count"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("altar_blessing_count", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["altar_blessing_count"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("dibble_discount"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("dibble_discount", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["dibble_discount"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("dibble_extra_item"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("dibble_extra_item", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["dibble_extra_item"] = "0";

                                                                        if (!Diccionario_Mejoras.ContainsKey("dibble_relic"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("dibble_relic", "0");
                                                                        }
                                                                        else Diccionario_Mejoras["dibble_relic"] = "0";
                                                                    }
                                                                    else if (ComboBox_Upgrades.SelectedIndex == 2) // Give all.
                                                                    {
                                                                        if (!Diccionario_Mejoras.ContainsKey("pickaxe_upgrade"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("pickaxe_upgrade", "15");
                                                                        }
                                                                        else Diccionario_Mejoras["pickaxe_upgrade"] = "15";

                                                                        if (!Diccionario_Mejoras.ContainsKey("tunic_upgrade"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("tunic_upgrade", "15");
                                                                        }
                                                                        else Diccionario_Mejoras["tunic_upgrade"] = "15";

                                                                        if (!Diccionario_Mejoras.ContainsKey("glove_upgrade"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("glove_upgrade", "15");
                                                                        }
                                                                        else Diccionario_Mejoras["glove_upgrade"] = "15";

                                                                        if (!Diccionario_Mejoras.ContainsKey("counterweight_upgrade"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("counterweight_upgrade", "4");
                                                                        }
                                                                        else Diccionario_Mejoras["counterweight_upgrade"] = "4";

                                                                        if (!Diccionario_Mejoras.ContainsKey("bomb_upgrade"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("bomb_upgrade", "10");
                                                                        }
                                                                        else Diccionario_Mejoras["bomb_upgrade"] = "10";

                                                                        if (!Diccionario_Mejoras.ContainsKey("goldsack_upgrade"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("goldsack_upgrade", "9");
                                                                        }
                                                                        else Diccionario_Mejoras["goldsack_upgrade"] = "9";

                                                                        if (!Diccionario_Mejoras.ContainsKey("show_enemy_hp"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("show_enemy_hp", "1");
                                                                        }
                                                                        else Diccionario_Mejoras["show_enemy_hp"] = "1";

                                                                        if (!Diccionario_Mejoras.ContainsKey("geckos_foot"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("geckos_foot", "2");
                                                                        }
                                                                        else Diccionario_Mejoras["geckos_foot"] = "2";

                                                                        if (!Diccionario_Mejoras.ContainsKey("big_bomb_upgrade"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("big_bomb_upgrade", "1");
                                                                        }
                                                                        else Diccionario_Mejoras["big_bomb_upgrade"] = "1";

                                                                        if (!Diccionario_Mejoras.ContainsKey("speedboost_boots"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("speedboost_boots", "1");
                                                                        }
                                                                        else Diccionario_Mejoras["speedboost_boots"] = "1";

                                                                        if (!Diccionario_Mejoras.ContainsKey("potion_count"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("potion_count", "4");
                                                                        }
                                                                        else Diccionario_Mejoras["potion_count"] = "4";

                                                                        if (!Diccionario_Mejoras.ContainsKey("shaker"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("shaker", "3");
                                                                        }
                                                                        else Diccionario_Mejoras["shaker"] = "3";

                                                                        if (!Diccionario_Mejoras.ContainsKey("drink_duration"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("drink_duration", "5");
                                                                        }
                                                                        else Diccionario_Mejoras["drink_duration"] = "5";

                                                                        if (!Diccionario_Mejoras.ContainsKey("shop_transmute_machine"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("shop_transmute_machine", "3");
                                                                        }
                                                                        else Diccionario_Mejoras["shop_transmute_machine"] = "3";

                                                                        if (!Diccionario_Mejoras.ContainsKey("shop_potion_relic"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("shop_potion_relic", "3");
                                                                        }
                                                                        else Diccionario_Mejoras["shop_potion_relic"] = "3";

                                                                        if (!Diccionario_Mejoras.ContainsKey("shop_basic_item"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("shop_basic_item", "2");
                                                                        }
                                                                        else Diccionario_Mejoras["shop_basic_item"] = "2";

                                                                        if (!Diccionario_Mejoras.ContainsKey("shop_food"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("shop_food", "3");
                                                                        }
                                                                        else Diccionario_Mejoras["shop_food"] = "3";

                                                                        if (!Diccionario_Mejoras.ContainsKey("start_blessing"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("start_blessing", "3");
                                                                        }
                                                                        else Diccionario_Mejoras["start_blessing"] = "3";

                                                                        if (!Diccionario_Mejoras.ContainsKey("altar_blessing_count"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("altar_blessing_count", "5");
                                                                        }
                                                                        else Diccionario_Mejoras["altar_blessing_count"] = "5";

                                                                        if (!Diccionario_Mejoras.ContainsKey("dibble_discount"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("dibble_discount", "2");
                                                                        }
                                                                        else Diccionario_Mejoras["dibble_discount"] = "2";

                                                                        if (!Diccionario_Mejoras.ContainsKey("dibble_extra_item"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("dibble_extra_item", "1");
                                                                        }
                                                                        else Diccionario_Mejoras["dibble_extra_item"] = "1";

                                                                        if (!Diccionario_Mejoras.ContainsKey("dibble_relic"))
                                                                        {
                                                                            Diccionario_Mejoras.Add("dibble_relic", "1");
                                                                        }
                                                                        else Diccionario_Mejoras["dibble_relic"] = "1";
                                                                    }

                                                                    /*if (!Diccionario_Mejoras.ContainsKey(""))
                                                                    {
                                                                        Diccionario_Mejoras.Add("", "");
                                                                    }
                                                                    else Diccionario_Mejoras[""] = "";*/

                                                                    // Finally rebuild the upgrade string with all the modified options.
                                                                    foreach (KeyValuePair<string, string> Entrada in Diccionario_Mejoras)
                                                                    {
                                                                        try
                                                                        {
                                                                            Línea += Entrada.Key + ":" + Entrada.Value + ",";
                                                                        }
                                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                    }
                                                                    Diccionario_Mejoras = null;
                                                                }
                                                                Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Línea);
                                                                Matriz_Líneas = null;
                                                            }
                                                            else Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                        }
                                                    }
                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                }
                                                else if (string.Compare(Línea, "foundCountString", true) == 0) // Massive string with a counter for each found item, etc.
                                                {
                                                    try
                                                    {
                                                        // Ignored.
                                                    }
                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                }
                                                else if (string.Compare(Línea, "killCountString", true) == 0) // Massive string with a counter for each killed enemy, etc.
                                                {
                                                    try
                                                    {
                                                        // Ignored.
                                                    }
                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                }
                                                else if (string.Compare(Línea, "autoSaveData", true) == 0) // Main header for UnderMine save data.
                                                {
                                                    try
                                                    {
                                                        int Objetos_Abiertos = 0; // Keep track of the openings and closings of JSON objects.
                                                        while (Lector_JSON_Entrada.Read())
                                                        {
                                                            try
                                                            {
                                                                if (Lector_JSON_Entrada.TokenType == JsonToken.StartObject)
                                                                {
                                                                    Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                    Objetos_Abiertos++; // "{".
                                                                }
                                                                else if (Lector_JSON_Entrada.TokenType == JsonToken.EndObject)
                                                                {
                                                                    Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                    Objetos_Abiertos--; // "}".
                                                                    if (Objetos_Abiertos <= 0) break;
                                                                }
                                                                else if (Lector_JSON_Entrada.TokenType == JsonToken.PropertyName)
                                                                {
                                                                    Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                    if (!Guardar_Othermine) // Undermine settings.
                                                                    {
                                                                        try { Línea = Lector_JSON_Entrada.Value.ToString(); }
                                                                        catch { Línea = null; }
                                                                        if (!string.IsNullOrEmpty(Línea))
                                                                        {
                                                                            if (string.Compare(Línea, "seed", true) == 0) // The random seed for the current level.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, (int)NumericUpDown_Seed.Value);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "timer", true) == 0) // Current play time in seconds.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, (int)NumericUpDown_Timer.Value);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "zone", true) == 0) // The ID of the current level.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, ComboBox_Zone.SelectedIndex > 0 ? Zonas.Matriz[ComboBox_Zone.SelectedIndex - 1].Id : Lector_JSON_Entrada.Value.ToString());
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "hp", true) == 0) // The remaining health points.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, (int)NumericUpDown_HealthPoints.Value);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "padding", true) == 0) // Unknown, but is 1 when the player is on the Othermine.
                                                                            {
                                                                                try
                                                                                {
                                                                                    // Coming soon...
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "gold", true) == 0) // The current gold.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, (int)NumericUpDown_Gold.Value);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "thorium", true) == 0) // The current thorium.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, (int)NumericUpDown_Thorium.Value);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "nether", true) == 0) // The current nether.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, (int)NumericUpDown_Nether.Value);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "bombs", true) == 0) // The current bombs.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, (int)NumericUpDown_Bombs.Value);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "keys", true) == 0) // The current keys.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, (int)NumericUpDown_Keys.Value);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "bomb", true) == 0) // The current unique bomb upgrade.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Mejoras_Bomba.Matriz[ComboBox_Bomb.SelectedIndex > -1 ? ComboBox_Bomb.SelectedIndex : 0].Id);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "gloves", true) == 0) // The current unique gloves upgrade.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Mejoras_Guantes.Matriz[ComboBox_Gloves.SelectedIndex > -1 ? ComboBox_Gloves.SelectedIndex : 0].Id);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "weapon", true) == 0) // The current unique weapon upgrade.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Mejoras_Arma.Matriz[ComboBox_Weapon.SelectedIndex > -1 ? ComboBox_Weapon.SelectedIndex : 0].Id);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "hat", true) == 0) // The current unique hat upgrade.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Mejoras_Sombrero.Matriz[ComboBox_Hat.SelectedIndex > -1 ? ComboBox_Hat.SelectedIndex : 0].Id);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "potions", true) == 0) // Unknown. Always was empty so far.
                                                                            {
                                                                                try
                                                                                {
                                                                                    // Ignored.
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "potionDatas", true) == 0) // The data of the current potions.
                                                                            {
                                                                                try
                                                                                {
                                                                                    int Matrices_Abiertas_Pociones = 0; // Keep track of the openings and closings of JSON "arrays".
                                                                                    while (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        try
                                                                                        {
                                                                                            if (Lector_JSON_Entrada.TokenType == JsonToken.StartArray)
                                                                                            {
                                                                                                Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                                                Matrices_Abiertas_Pociones++; // "[".
                                                                                            }
                                                                                            else if (Lector_JSON_Entrada.TokenType == JsonToken.EndArray)
                                                                                            {
                                                                                                Matrices_Abiertas_Pociones--; // "]".
                                                                                                if (Matrices_Abiertas_Pociones <= 0) break;
                                                                                            }
                                                                                        }
                                                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                    }
                                                                                    List<int> Lista_Índices_Pociones = new List<int>();
                                                                                    if (ComboBox_Poción_1.SelectedIndex > 0) Lista_Índices_Pociones.Add(ComboBox_Poción_1.SelectedIndex - 1);
                                                                                    if (ComboBox_Poción_2.SelectedIndex > 0) Lista_Índices_Pociones.Add(ComboBox_Poción_2.SelectedIndex - 1);
                                                                                    if (ComboBox_Poción_3.SelectedIndex > 0) Lista_Índices_Pociones.Add(ComboBox_Poción_3.SelectedIndex - 1);
                                                                                    if (ComboBox_Poción_4.SelectedIndex > 0) Lista_Índices_Pociones.Add(ComboBox_Poción_4.SelectedIndex - 1);
                                                                                    if (Lista_Índices_Pociones.Count > 0)
                                                                                    {
                                                                                        for (int Índice_Poción = 0; Índice_Poción < Lista_Índices_Pociones.Count; Índice_Poción++)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                Lector_JSON_Salida.WriteToken(JsonToken.StartObject, null);
                                                                                                Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "id");
                                                                                                Lector_JSON_Salida.WriteToken(JsonToken.String, Pociones.Matriz[Lista_Índices_Pociones[Índice_Poción]].Id);
                                                                                                Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "currentHP");
                                                                                                Lector_JSON_Salida.WriteToken(JsonToken.Integer, 5);
                                                                                                Lector_JSON_Salida.WriteToken(JsonToken.EndObject, null);
                                                                                            }
                                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                                        }
                                                                                    }
                                                                                    Lista_Índices_Pociones = null;
                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.EndArray, null);
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "statusEffects", true) == 0) // The data of the status effects like blessings, curses, relics, etc.
                                                                            {
                                                                                try
                                                                                {
                                                                                    int Matrices_Abiertas_Efectos = 0; // Keep track of the openings and closings of JSON arrays.
                                                                                    if (!CheckBox_Mantener_Efectos_Originales.Checked)
                                                                                    {
                                                                                        int Nivel_Bendiciones = (int)NumericUpDown_Nivel_Bendiciones.Value;
                                                                                        while (Lector_JSON_Entrada.Read())
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                if (Lector_JSON_Entrada.TokenType == JsonToken.StartArray)
                                                                                                {
                                                                                                    Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                                                    Matrices_Abiertas_Efectos++; // "[".
                                                                                                }
                                                                                                else if (Lector_JSON_Entrada.TokenType == JsonToken.EndArray)
                                                                                                {
                                                                                                    Matrices_Abiertas_Efectos--; // "]".
                                                                                                    if (Matrices_Abiertas_Efectos <= 0) break;
                                                                                                }
                                                                                            }
                                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                        }
                                                                                        for (int Índice_Efecto = 0; Índice_Efecto < ListView_Efectos.Items.Count; Índice_Efecto++)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                if (ListView_Efectos.Items[Índice_Efecto].Checked)
                                                                                                {
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.StartObject, null);
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "id");
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.String, Efectos.Matriz[Índice_Efecto].Id);
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "level");
                                                                                                    if (Efectos.Matriz[Índice_Efecto].Tipo != Efectos.Tipos.Blessing)
                                                                                                    {
                                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.Integer, 0);
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.Integer, Nivel_Bendiciones);
                                                                                                    }
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "duration");
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.Float, -1);
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "durationRatio");
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.Float, 0);
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "userData");
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.Integer, 0);
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "userString");
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.String, "");
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "sticky");
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.Boolean, false);
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.EndObject, null);
                                                                                                }
                                                                                            }
                                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                                        }
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.EndArray, null);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        while (Lector_JSON_Entrada.Read())
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                if (Lector_JSON_Entrada.TokenType == JsonToken.StartArray)
                                                                                                {
                                                                                                    Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                                                    Matrices_Abiertas_Efectos++; // "[".
                                                                                                }
                                                                                                else if (Lector_JSON_Entrada.TokenType == JsonToken.EndArray)
                                                                                                {
                                                                                                    Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                                                    Matrices_Abiertas_Efectos--; // "]".
                                                                                                    if (Matrices_Abiertas_Efectos <= 0) break;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                                                }
                                                                                            }
                                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                        }
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "modifiers", true) == 0) // Unknown. Always was empty so far.
                                                                            {
                                                                                try
                                                                                {
                                                                                    // Ignored.
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "familiarXP", true) == 0) // The data of the current familiars.
                                                                            {
                                                                                try
                                                                                {
                                                                                    int Matrices_Abiertas_Familiares = 0; // Keep track of the openings and closings of JSON "arrays".
                                                                                    while (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        try
                                                                                        {
                                                                                            if (Lector_JSON_Entrada.TokenType == JsonToken.StartArray)
                                                                                            {
                                                                                                Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                                                Matrices_Abiertas_Familiares++; // "[".
                                                                                            }
                                                                                            else if (Lector_JSON_Entrada.TokenType == JsonToken.EndArray)
                                                                                            {
                                                                                                Matrices_Abiertas_Familiares--; // "]".
                                                                                                if (Matrices_Abiertas_Familiares <= 0) break;
                                                                                            }
                                                                                        }
                                                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                                    }
                                                                                    if (ComboBox_Familiar_1_ID.SelectedIndex > 0)
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.StartObject, null);
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "id");
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.String, ComboBox_Familiar_1_ID.SelectedIndex > 0 ? Familiares.Matriz[ComboBox_Familiar_1_ID.SelectedIndex - 1].Id : null);
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "xp");
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.Integer, (int)NumericUpDown_Familiar_1_XP.Value);
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.EndObject, null);
                                                                                    }
                                                                                    if (ComboBox_Familiar_2_ID.SelectedIndex > 0)
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.StartObject, null);
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "id");
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.String, ComboBox_Familiar_2_ID.SelectedIndex > 0 ? Familiares.Matriz[ComboBox_Familiar_2_ID.SelectedIndex - 1].Id : null);
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "xp");
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.Integer, (int)NumericUpDown_Familiar_2_XP.Value);
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.EndObject, null);
                                                                                    }
                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.EndArray, null);
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "dropHistory", true) == 0) // The currently dropped items.
                                                                            {
                                                                                try
                                                                                {
                                                                                    // Ignored.
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "killHistory", true) == 0) // The currently killed enemies.
                                                                            {
                                                                                try
                                                                                {
                                                                                    // Ignored.
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "pickupHistory", true) == 0) // The currently picked up items.
                                                                            {
                                                                                try
                                                                                {
                                                                                    // Ignored.
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "prayHistory", true) == 0) // Unknown. Always was empty so far.
                                                                            {
                                                                                try
                                                                                {
                                                                                    // Ignored.
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                }
                                                            }
                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                        }
                                                    }
                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                }
                                                else if (string.Compare(Línea, "rogueSaveData", true) == 0) // Main header for Othermine save data.
                                                {
                                                    try
                                                    {
                                                        int Objetos_Abiertos = 0; // Keep track of the openings and closings of JSON objects.
                                                        while (Lector_JSON_Entrada.Read())
                                                        {
                                                            try
                                                            {
                                                                if (Lector_JSON_Entrada.TokenType == JsonToken.StartObject)
                                                                {
                                                                    Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                    Objetos_Abiertos++; // "{".
                                                                }
                                                                else if (Lector_JSON_Entrada.TokenType == JsonToken.EndObject)
                                                                {
                                                                    Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                    Objetos_Abiertos--; // "}".
                                                                    if (Objetos_Abiertos <= 0) break;
                                                                }
                                                                else if (Lector_JSON_Entrada.TokenType == JsonToken.PropertyName)
                                                                {
                                                                    Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                    if (Guardar_Othermine) // Othermine settings.
                                                                    {
                                                                        try { Línea = Lector_JSON_Entrada.Value.ToString(); }
                                                                        catch { Línea = null; }
                                                                        if (!string.IsNullOrEmpty(Línea))
                                                                        {
                                                                            if (string.Compare(Línea, "seed", true) == 0) // The random seed for the current level.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, (int)NumericUpDown_Seed.Value);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "timer", true) == 0) // Current play time in seconds.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, (int)NumericUpDown_Timer.Value);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "zone", true) == 0) // The ID of the current level.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, ComboBox_Zone.SelectedIndex > 0 ? Zonas.Matriz[ComboBox_Zone.SelectedIndex - 1].Id : Lector_JSON_Entrada.Value.ToString());
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "hp", true) == 0) // The remaining health points.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, (int)NumericUpDown_HealthPoints.Value);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "padding", true) == 0) // Unknown, but is 1 when the player is on the Othermine.
                                                                            {
                                                                                try
                                                                                {
                                                                                    // Coming soon...
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "gold", true) == 0) // The current gold.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, (int)NumericUpDown_Gold.Value);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "thorium", true) == 0) // The current thorium.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, (int)NumericUpDown_Thorium.Value);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "nether", true) == 0) // The current nether.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, (int)NumericUpDown_Nether.Value);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "bombs", true) == 0) // The current bombs.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, (int)NumericUpDown_Bombs.Value);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "keys", true) == 0) // The current keys.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, (int)NumericUpDown_Keys.Value);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "bomb", true) == 0) // The current unique bomb upgrade.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Mejoras_Bomba.Matriz[ComboBox_Bomb.SelectedIndex > -1 ? ComboBox_Bomb.SelectedIndex : 0].Id);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "gloves", true) == 0) // The current unique gloves upgrade.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Mejoras_Guantes.Matriz[ComboBox_Gloves.SelectedIndex > -1 ? ComboBox_Gloves.SelectedIndex : 0].Id);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "weapon", true) == 0) // The current unique weapon upgrade.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Mejoras_Arma.Matriz[ComboBox_Weapon.SelectedIndex > -1 ? ComboBox_Weapon.SelectedIndex : 0].Id);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "hat", true) == 0) // The current unique hat upgrade.
                                                                            {
                                                                                try
                                                                                {
                                                                                    if (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Mejoras_Sombrero.Matriz[ComboBox_Hat.SelectedIndex > -1 ? ComboBox_Hat.SelectedIndex : 0].Id);
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "potions", true) == 0) // Unknown. Always was empty so far.
                                                                            {
                                                                                try
                                                                                {
                                                                                    // Ignored.
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "potionDatas", true) == 0) // The data of the current potions.
                                                                            {
                                                                                try
                                                                                {
                                                                                    int Matrices_Abiertas_Pociones = 0; // Keep track of the openings and closings of JSON "arrays".
                                                                                    while (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        try
                                                                                        {
                                                                                            if (Lector_JSON_Entrada.TokenType == JsonToken.StartArray)
                                                                                            {
                                                                                                Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                                                Matrices_Abiertas_Pociones++; // "[".
                                                                                            }
                                                                                            else if (Lector_JSON_Entrada.TokenType == JsonToken.EndArray)
                                                                                            {
                                                                                                Matrices_Abiertas_Pociones--; // "]".
                                                                                                if (Matrices_Abiertas_Pociones <= 0) break;
                                                                                            }
                                                                                        }
                                                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                                    }
                                                                                    List<int> Lista_Índices_Pociones = new List<int>();
                                                                                    if (ComboBox_Poción_1.SelectedIndex > 0) Lista_Índices_Pociones.Add(ComboBox_Poción_1.SelectedIndex - 1);
                                                                                    if (ComboBox_Poción_2.SelectedIndex > 0) Lista_Índices_Pociones.Add(ComboBox_Poción_2.SelectedIndex - 1);
                                                                                    if (ComboBox_Poción_3.SelectedIndex > 0) Lista_Índices_Pociones.Add(ComboBox_Poción_3.SelectedIndex - 1);
                                                                                    if (ComboBox_Poción_4.SelectedIndex > 0) Lista_Índices_Pociones.Add(ComboBox_Poción_4.SelectedIndex - 1);
                                                                                    if (Lista_Índices_Pociones.Count > 0)
                                                                                    {
                                                                                        for (int Índice_Poción = 0; Índice_Poción < Lista_Índices_Pociones.Count; Índice_Poción++)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                Lector_JSON_Salida.WriteToken(JsonToken.StartObject, null);
                                                                                                Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "id");
                                                                                                Lector_JSON_Salida.WriteToken(JsonToken.String, Pociones.Matriz[Lista_Índices_Pociones[Índice_Poción]].Id);
                                                                                                Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "currentHP");
                                                                                                Lector_JSON_Salida.WriteToken(JsonToken.Integer, 5);
                                                                                                Lector_JSON_Salida.WriteToken(JsonToken.EndObject, null);
                                                                                            }
                                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                                        }
                                                                                    }
                                                                                    Lista_Índices_Pociones = null;
                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.EndArray, null);
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "statusEffects", true) == 0) // The data of the status effects like blessings, curses, relics, etc.
                                                                            {
                                                                                try
                                                                                {
                                                                                    int Matrices_Abiertas_Efectos = 0; // Keep track of the openings and closings of JSON arrays.
                                                                                    if (!CheckBox_Mantener_Efectos_Originales.Checked)
                                                                                    {
                                                                                        int Nivel_Bendiciones = (int)NumericUpDown_Nivel_Bendiciones.Value;
                                                                                        int Nivel_Maldiciones = (int)NumericUpDown_Nivel_Maldiciones.Value;
                                                                                        while (Lector_JSON_Entrada.Read())
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                if (Lector_JSON_Entrada.TokenType == JsonToken.StartArray)
                                                                                                {
                                                                                                    Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                                                    Matrices_Abiertas_Efectos++; // "[".
                                                                                                }
                                                                                                else if (Lector_JSON_Entrada.TokenType == JsonToken.EndArray)
                                                                                                {
                                                                                                    Matrices_Abiertas_Efectos--; // "]".
                                                                                                    if (Matrices_Abiertas_Efectos <= 0) break;
                                                                                                }
                                                                                            }
                                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                                        }
                                                                                        for (int Índice_Efecto = 0; Índice_Efecto < ListView_Efectos.Items.Count; Índice_Efecto++)
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                if (ListView_Efectos.Items[Índice_Efecto].Checked)
                                                                                                {
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.StartObject, null);
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "id");
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.String, Efectos.Matriz[Índice_Efecto].Id);
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "level");
                                                                                                    if (Efectos.Matriz[Índice_Efecto].Tipo == Efectos.Tipos.Blessing)
                                                                                                    {
                                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.Integer, Nivel_Bendiciones);
                                                                                                    }
                                                                                                    else if (Efectos.Matriz[Índice_Efecto].Tipo == Efectos.Tipos.Minor_curse)
                                                                                                    {
                                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.Integer, Nivel_Maldiciones);
                                                                                                    }
                                                                                                    else
                                                                                                    {
                                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.Integer, 0);
                                                                                                    }
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "duration");
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.Float, -1);
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "durationRatio");
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.Float, 0);
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "userData");
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.Integer, 0);
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "userString");
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.String, "");
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "sticky");
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.Boolean, false);
                                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.EndObject, null);
                                                                                                }
                                                                                            }
                                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                                        }
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.EndArray, null);
                                                                                    }
                                                                                    else
                                                                                    {
                                                                                        while (Lector_JSON_Entrada.Read())
                                                                                        {
                                                                                            try
                                                                                            {
                                                                                                if (Lector_JSON_Entrada.TokenType == JsonToken.StartArray)
                                                                                                {
                                                                                                    Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                                                    Matrices_Abiertas_Efectos++; // "[".
                                                                                                }
                                                                                                else if (Lector_JSON_Entrada.TokenType == JsonToken.EndArray)
                                                                                                {
                                                                                                    Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                                                    Matrices_Abiertas_Efectos--; // "]".
                                                                                                    if (Matrices_Abiertas_Efectos <= 0) break;
                                                                                                }
                                                                                                else
                                                                                                {
                                                                                                    Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                                                }
                                                                                            }
                                                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                                        }
                                                                                    }
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "modifiers", true) == 0) // Unknown. Always was empty so far.
                                                                            {
                                                                                try
                                                                                {
                                                                                    // Ignored.
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "familiarXP", true) == 0) // The data of the current familiars.
                                                                            {
                                                                                try
                                                                                {
                                                                                    int Matrices_Abiertas_Familiares = 0; // Keep track of the openings and closings of JSON "arrays".
                                                                                    while (Lector_JSON_Entrada.Read())
                                                                                    {
                                                                                        try
                                                                                        {
                                                                                            if (Lector_JSON_Entrada.TokenType == JsonToken.StartArray)
                                                                                            {
                                                                                                Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                                                Matrices_Abiertas_Familiares++; // "[".
                                                                                            }
                                                                                            else if (Lector_JSON_Entrada.TokenType == JsonToken.EndArray)
                                                                                            {
                                                                                                Matrices_Abiertas_Familiares--; // "]".
                                                                                                if (Matrices_Abiertas_Familiares <= 0) break;
                                                                                            }
                                                                                        }
                                                                                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                                                    }
                                                                                    if (ComboBox_Familiar_1_ID.SelectedIndex > 0)
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.StartObject, null);
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "id");
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.String, ComboBox_Familiar_1_ID.SelectedIndex > 0 ? Familiares.Matriz[ComboBox_Familiar_1_ID.SelectedIndex - 1].Id : null);
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "xp");
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.Integer, (int)NumericUpDown_Familiar_1_XP.Value);
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.EndObject, null);
                                                                                    }
                                                                                    if (ComboBox_Familiar_2_ID.SelectedIndex > 0)
                                                                                    {
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.StartObject, null);
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "id");
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.String, ComboBox_Familiar_2_ID.SelectedIndex > 0 ? Familiares.Matriz[ComboBox_Familiar_2_ID.SelectedIndex - 1].Id : null);
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.PropertyName, "xp");
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.Integer, (int)NumericUpDown_Familiar_2_XP.Value);
                                                                                        Lector_JSON_Salida.WriteToken(JsonToken.EndObject, null);
                                                                                    }
                                                                                    Lector_JSON_Salida.WriteToken(JsonToken.EndArray, null);
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "dropHistory", true) == 0) // The currently dropped items.
                                                                            {
                                                                                try
                                                                                {
                                                                                    // Ignored.
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "killHistory", true) == 0) // The currently killed enemies.
                                                                            {
                                                                                try
                                                                                {
                                                                                    // Ignored.
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "pickupHistory", true) == 0) // The currently picked up items.
                                                                            {
                                                                                try
                                                                                {
                                                                                    // Ignored.
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                            else if (string.Compare(Línea, "prayHistory", true) == 0) // Unknown. Always was empty so far.
                                                                            {
                                                                                try
                                                                                {
                                                                                    // Ignored.
                                                                                }
                                                                                catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                                                }
                                                            }
                                                            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                                        }
                                                    }
                                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Lector_JSON_Salida.WriteToken(Lector_JSON_Entrada.TokenType, Lector_JSON_Entrada.Value);
                                        }
                                    }
                                    catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; continue; }
                                }
                                Lector_JSON_Salida.Close();
                                Lector_JSON_Salida = null;
                                Lector_Texto_Salida.Close();
                                Lector_Texto_Salida.Dispose();
                                Lector_Texto_Salida = null;
                                Lector_Salida.Close();
                                Lector_Salida.Dispose();
                                Lector_Salida = null;
                                Lector_Texto_Entrada.Close();
                                Lector_Texto_Entrada.Dispose();
                                Lector_Texto_Entrada = null;
                                SystemSounds.Asterisk.Play();
                            }
                            Lector_Entrada.Close();
                            Lector_Entrada.Dispose();
                            Lector_Entrada = null;
                        }
                    }
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void CheckBox_Mantener_Efectos_Originales_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //ListView_Efectos.Enabled = !CheckBox_Mantener_Efectos_Originales.Checked;
                //ListView_Efectos.BackColor = !CheckBox_Mantener_Efectos_Originales.Checked ? Color.White : Color.Gray;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_UnderMine_Wiki_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://undermine.gamepedia.com/UnderMine_Wiki", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_UnderMine_Wiki_Lista_IDs_Click(object sender, EventArgs e)
        {
            try
            {
                Program.Ejecutar_Ruta("https://undermine.gamepedia.com/Template:GetID", ProcessWindowStyle.Normal);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Convertir_Imágenes_UnderMine_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                string Ruta_Entrada = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\UnderMine Wiki";
                if (MessageBox.Show(this, "Do you want to cut, center and convert to 18 x 18 all the images located on \"" + Ruta_Entrada + "\" and it's subfolders? This can't be undone later...", Program.Texto_Título_Versión, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    Recortar_Bordes_Imágenes(Ruta_Entrada, true);
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        private void Button_Efectos_Ninguno_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListView_Efectos.Items != null && ListView_Efectos.Items.Count > 0)
                {
                    for (int Índice_Efecto = 0; Índice_Efecto < ListView_Efectos.Items.Count; Índice_Efecto++)
                    {
                        try
                        {
                            ListView_Efectos.Items[Índice_Efecto].Checked = false;
                        }
                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Efectos_Todos_Click(object sender, EventArgs e)
        {
            try
            {
                if (ListView_Efectos.Items != null && ListView_Efectos.Items.Count > 0)
                {
                    for (int Índice_Efecto = 0; Índice_Efecto < ListView_Efectos.Items.Count; Índice_Efecto++)
                    {
                        try
                        {
                            if (Efectos.Matriz[Índice_Efecto].Tipo != Efectos.Tipos.Familiar &&
                                Efectos.Matriz[Índice_Efecto].Tipo != Efectos.Tipos.UnderMod_relic)
                            {
                                ListView_Efectos.Items[Índice_Efecto].Checked = true;
                            }
                        }
                        catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); continue; }
                    }
                }
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void ComboBox_Zone_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PictureBox_Zone.Image = ComboBox_Zone.SelectedIndex > 0 ? Zonas.Matriz[ComboBox_Zone.SelectedIndex - 1].Imagen : Resources.Status_Effect_Blank_Map;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_PeonColor_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                // Update: possible bug found when the "peonColor" is 0,
                // it will be converted in game to 458.761... Why?

                // "peonColor" full analysis, it's a 32 bits value.
                // 11111111 00000000 00000000 00000000 = Unused?
                // 00000000 11111111 00000000 00000000 = Eyes.
                // 00000000 00000000 11111110 00000000 = Skin.
                // 00000000 00000000 00000001 00000000 = Hair.
                // 00000000 00000000 00000000 11111111 = Clothes.
                //int Valor = NULL | (Eyes << 16) | (Skin << 9) | (Hair << 8) | Clothes;
                NumericUpDown_PeonColor.Refresh();
                uint Valor = (uint)NumericUpDown_PeonColor.Value;
                uint Clothes = ((Valor << 24) >> 24);
                uint Eyes = ((Valor << 8) >> 24);
                uint Hair = ((Valor << 23) >> 31);
                uint Skin = ((Valor << 16) >> 25);
                if (NumericUpDown_Clothes.Value != Clothes) NumericUpDown_Clothes.Value = Clothes;
                if (NumericUpDown_Eyes.Value != Eyes) NumericUpDown_Eyes.Value = Eyes;
                if (NumericUpDown_Hair.Value != Hair) NumericUpDown_Hair.Value = Hair;
                if (NumericUpDown_Skin.Value != Skin) NumericUpDown_Skin.Value = Skin;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void CheckBox_Peasant_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox_Peasant.BackColor = !CheckBox_Peasant.Checked ? Color.FromArgb(30, 19, 13) : Color.FromArgb(157, 125, 79);
                //PictureBox_Peasant.BackColor = Color.Fuchsia; //!CheckBox_Peasant.Checked ? Color.FromArgb(30, 19, 13) : Color.FromArgb(157, 125, 79);
                PictureBox_Peasant.Image = Crear_Imagen_Peasant(true);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Cargar_Undermine_Click(object sender, EventArgs e)
        {
            try
            {
                Othermine_Cargado = false;
                Cargar_Partida(Othermine_Cargado);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Button_Cargar_Othermine_Click(object sender, EventArgs e)
        {
            try
            {
                Othermine_Cargado = true;
                Cargar_Partida(Othermine_Cargado);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void Menú_Contextual_Ejecutar_Ruta_Guardado_Predeterminada_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                Program.Crear_Carpetas(Ruta_Saves);
                Program.Ejecutar_Ruta(Ruta_Saves, ProcessWindowStyle.Maximized);
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            finally { this.Cursor = Cursors.Default; }
        }

        internal Bitmap Obtener_Imagen_XP_Familiar(int Valor, int Máximo)
        {
            try
            {
                Bitmap Imagen = new Bitmap(18, 18, PixelFormat.Format32bppArgb);
                Graphics Pintar = Graphics.FromImage(Imagen);
                Pintar.CompositingMode = CompositingMode.SourceCopy;
                Pintar.CompositingQuality = CompositingQuality.HighQuality;
                Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Pintar.SmoothingMode = SmoothingMode.None;
                Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                double Ángulo = ((double)Math.Min(Math.Max(Valor, 0), Máximo) * 360d) / (double)Máximo;
                TextureBrush Pincel = new TextureBrush(Resources.Familiar_XP, WrapMode.Tile);
                Pintar.FillPie(Pincel, -18f, -18f, 54f, 54f, -90f, (float)Ángulo);
                Pincel.Dispose();
                Pincel = null;
                Pintar.Dispose();
                Pintar = null;
                return Imagen;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
            return new Bitmap(18, 18, PixelFormat.Format32bppArgb);
        }

        private void NumericUpDown_Familiar_1_XP_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int Índice_Familiar = ComboBox_Familiar_1_ID.SelectedIndex - 1;
                if (Índice_Familiar > -1)
                {
                    Bitmap Imagen = Resources.Familiar_XP_Frame;
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceOver;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.None;
                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                    Pintar.DrawImage(Obtener_Imagen_XP_Familiar((int)NumericUpDown_Familiar_1_XP.Value, Familiares.Matriz[Índice_Familiar].Xp), new Rectangle(0, 0, 18, 18), new Rectangle(0, 0, 18, 18), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;
                    PictureBox_Familiar_1_XP.Image = Imagen;
                }
                else PictureBox_Familiar_1_XP.Image = Resources.Familiar_XP_Frame;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }

        private void NumericUpDown_Familiar_2_XP_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                int Índice_Familiar = ComboBox_Familiar_2_ID.SelectedIndex - 1;
                if (Índice_Familiar > -1)
                {
                    Bitmap Imagen = Resources.Familiar_XP_Frame;
                    Graphics Pintar = Graphics.FromImage(Imagen);
                    Pintar.CompositingMode = CompositingMode.SourceOver;
                    Pintar.CompositingQuality = CompositingQuality.HighQuality;
                    Pintar.InterpolationMode = InterpolationMode.NearestNeighbor;
                    Pintar.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Pintar.SmoothingMode = SmoothingMode.None;
                    Pintar.TextRenderingHint = TextRenderingHint.AntiAlias;
                    Pintar.DrawImage(Obtener_Imagen_XP_Familiar((int)NumericUpDown_Familiar_2_XP.Value, Familiares.Matriz[Índice_Familiar].Xp), new Rectangle(0, 0, 18, 18), new Rectangle(0, 0, 18, 18), GraphicsUnit.Pixel);
                    Pintar.Dispose();
                    Pintar = null;
                    PictureBox_Familiar_2_XP.Image = Imagen;
                }
                else PictureBox_Familiar_2_XP.Image = Resources.Familiar_XP_Frame;
            }
            catch (Exception Excepción) { Debugger.Escribir_Excepción(Excepción != null ? Excepción.ToString() : null); Variable_Excepción_Total++; Variable_Excepción = true; }
        }
    }
}
