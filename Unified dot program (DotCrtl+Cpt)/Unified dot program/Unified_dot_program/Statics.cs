using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Unified_dot_program
{
    public class Statics
    {
        public static string GAME_TITLE = "Dot";
   
        public static XDocument XML = XDocument.Load(Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments) + "\\Settings.xml");
        public static Vector2 DIMENSION = new Vector2(Convert.ToInt32(Statics.XML.Root.Element("all").Element("XDimension").Value), Convert.ToInt32(Statics.XML.Root.Element("all").Element("YDimension").Value));
        public static Vector2 CENTER = DIMENSION / 2;
        public static Boolean ISFULLSCREEN = Convert.ToBoolean(Statics.XML.Root.Element("all").Element("fullScreen").Value);
        public static IntPtr GAME_WINDOW_HANDLE;
        public static Game1 GAME1;
        public static GameTime GAMETIME;

        public static Texture2D PIXEL;

              
        
        public static Managers.InputManager INPUT;

        


    }
}
