using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Unified_dot_program.Entities
{
    public class Box
    {
        public int height;
        public int width;
        public int edgeThickness;
        public Rectangle leftEdge;
        public Rectangle rightEdge;
        public Rectangle topEdge;
        public Rectangle bottomEdge;
        

        public Box ()
        {
            height = Convert.ToInt32(Statics.XML.Root.Element("all").Element("boxHeight").Value);
            width = Convert.ToInt32(Statics.XML.Root.Element("all").Element("boxWidth").Value);
            edgeThickness = Convert.ToInt32(Statics.XML.Root.Element("all").Element("edgeThickness").Value);

            leftEdge = new Rectangle((int)Statics.CENTER.X - width / 2, (int)Statics.CENTER.Y - height / 2, edgeThickness, height);
            rightEdge = new Rectangle((int)Statics.CENTER.X + width / 2 - edgeThickness, (int)Statics.CENTER.Y - height / 2, edgeThickness, height);
            topEdge = new Rectangle((int)Statics.CENTER.X - width / 2, (int)Statics.CENTER.Y - height / 2, width, edgeThickness);
            bottomEdge = new Rectangle((int)Statics.CENTER.X - width / 2, (int)Statics.CENTER.Y + height / 2 - edgeThickness, width, edgeThickness);

        }

        public void Update()
        {

        }

        public void Draw()
        {
            Statics.GAME1.spriteBatch.Draw(Statics.PIXEL, leftEdge, Color.Black);
            Statics.GAME1.spriteBatch.Draw(Statics.PIXEL, rightEdge, Color.Black);
            Statics.GAME1.spriteBatch.Draw(Statics.PIXEL, topEdge, Color.Black);
            Statics.GAME1.spriteBatch.Draw(Statics.PIXEL, bottomEdge, Color.Black);
        }



        

    }
}
