using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;



namespace Unified_dot_program.Entities
{
    public class Effects
    {
        public SoundEffect AToneEngine;
        public SoundEffect LToneEngine;

        public SoundEffectInstance ATone;
        public SoundEffectInstance LTone;


        public static Color LIGHTRED = new Color(255, 128, 128);
        public static Color LIGHTBLUE = new Color(128, 128, 255);
        public static Color LIGHTPINK = new Color(255, 170, 255);



        public Effects()
        {
         

        }

        public void LoadContent()
        {
            AToneEngine = Statics.GAME1.Content.Load<SoundEffect>("ATone");
            LToneEngine = Statics.GAME1.Content.Load<SoundEffect>("LTone");

            ATone = AToneEngine.CreateInstance();
            LTone = LToneEngine.CreateInstance();


        }

        public void Clear_lightRed()
        {
            Statics.GAME1.GraphicsDevice.Clear(LIGHTRED);
        }

        public void Clear_lightBlue()
        {
            Statics.GAME1.GraphicsDevice.Clear(LIGHTBLUE);
        }

        public void Clear_lightPink()
        {
            Statics.GAME1.GraphicsDevice.Clear(LIGHTPINK);
        }








    }
}
