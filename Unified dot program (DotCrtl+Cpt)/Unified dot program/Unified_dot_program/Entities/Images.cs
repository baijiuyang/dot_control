using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Unified_dot_program.Entities
{
    public class Images
    {
        Texture2D letterA;
        Texture2D letterL;
        Texture2D crosshair;
        Vector2 position;

        public Images()
        {
            position = new Vector2(Statics.CENTER.X, Statics.CENTER.Y);

        }


        public void LoadContent()
        {
            letterA = Statics.GAME1.Content.Load<Texture2D>("A");
            letterL = Statics.GAME1.Content.Load<Texture2D>("L");
            crosshair = Statics.GAME1.Content.Load<Texture2D>("cross");
        }



        public void DrawLetterA()
        {
            Statics.GAME1.spriteBatch.Draw(letterA, position - new Vector2(letterA.Width, letterA.Height) / 2, Color.White);                       
        }

        public void DrawLetterL()
        {
            Statics.GAME1.spriteBatch.Draw(letterL, position - new Vector2(letterL.Width, letterL.Height) / 2, Color.White);
        }

        public void DrawCrosshair()
        {
            Statics.GAME1.spriteBatch.Draw(crosshair, position - new Vector2(crosshair.Width, crosshair.Height) / 2, Color.White);
        }

    }
}
