using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Unified_dot_program.Entities
{
    public class Ball
    {
        public Texture2D texture;
        public Vector2 position;
        public Vector2 definedSpeedVector;
        public Vector2 currentSpeedVector;



        


        public Ball ()
        {
            
            position = Statics.CENTER;
            currentSpeedVector = new Vector2(0, 0);
            
        }



        public void LoadContent()
        {
            texture = Statics.GAME1.Content.Load<Texture2D>("ball");
        }


        public void Update()
        {
            this.SetSpeed();
            this.Move();
            
        }

        public void Draw()
        {
            Statics.GAME1.spriteBatch.Draw(this.texture, this.position - new Vector2(texture.Width, texture.Height)/2, Color.White);
        }



        public void SetSpeed()
        {
            if (Statics.INPUT.CurrentState().IsKeyDown(Keys.A) && Statics.INPUT.CurrentState().IsKeyDown(Keys.L))
            {
                currentSpeedVector.Y = -definedSpeedVector.Y;
            }

            else if (Statics.INPUT.CurrentState().IsKeyDown(Keys.A))
            {
                currentSpeedVector.X = definedSpeedVector.X;
                currentSpeedVector.Y = 0;
            }

            else if (Statics.INPUT.CurrentState().IsKeyDown(Keys.L))
            {
                currentSpeedVector.X = -definedSpeedVector.X;
                currentSpeedVector.Y = 0;
            }

            else
            {
                currentSpeedVector.Y = definedSpeedVector.Y;
            }
        }

        public void Move()
        {

            Vector2 temp = position + (currentSpeedVector * (float)Statics.GAMETIME.ElapsedGameTime.TotalSeconds);

            if (temp.X >= 0 && temp.X <= Statics.DIMENSION.X)
            {
                position.X = temp.X;
            }
            else if (temp.X > Statics.DIMENSION.X)
            {
                position.X = Statics.DIMENSION.X;
            }
            else if (temp.X < 0)
            {
                position.X = 0;
            }

            if (temp.Y >= 0 && temp.Y <= Statics.DIMENSION.Y)
            {
                position.Y = temp.Y;
            }
            else if (temp.Y > Statics.DIMENSION.Y)
            {
                position.Y = Statics.DIMENSION.Y;
            }
            else if (temp.Y < 0)
            {
                position.Y = 0;
            }
        }

        



        

    }
}
