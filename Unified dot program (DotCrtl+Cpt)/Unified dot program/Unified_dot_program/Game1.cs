using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;


using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Unified_dot_program
{
   


    public class Game1 : Microsoft.Xna.Framework.Game
    {

        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Managers.GameManager gameManager;
        


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            
            this.graphics.PreferredBackBufferWidth = (int)Statics.DIMENSION.X;
            this.graphics.PreferredBackBufferHeight = (int)Statics.DIMENSION.Y;
            this.graphics.IsFullScreen = Statics.ISFULLSCREEN;
            this.IsMouseVisible = false;
            this.Window.Title = Statics.GAME_TITLE;
            this.graphics.ApplyChanges();
            Statics.GAME_WINDOW_HANDLE = this.Window.Handle;

            gameManager = new Managers.GameManager();
            Managers.InputManager input = new Managers.InputManager();

            

        }
     
        protected override void Initialize()
        {
            
            gameManager.Initialize();
            base.Initialize();
        }
       
        protected override void LoadContent()
        {

            spriteBatch = new SpriteBatch(GraphicsDevice);
            Statics.GAME1 = this;
            Statics.PIXEL = Content.Load<Texture2D>("BlackPixel");
            
            gameManager.LoadContent();

        }

       
        protected override void UnloadContent()
        {
           

        }

       
        protected override void Update(GameTime gameTime)
        {
            Statics.GAMETIME = gameTime;
            Statics.INPUT.Update();
            if (Statics.INPUT.CurrentState().IsKeyDown(Keys.Escape))
            {
                this.Exit();
            }

            gameManager.Update();
            base.Update(gameTime);
        }

       
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Gray);
            gameManager.Draw();    
            base.Draw(gameTime);
        }

    }
}
