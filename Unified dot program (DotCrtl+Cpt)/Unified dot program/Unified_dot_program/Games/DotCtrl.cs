using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Unified_dot_program.Games
{
    public class DotCtrl
    {
        public int block;
        public int[] speedList;
        public int[] timerList;
        public double[] inBoxTime;
        public int inBoxTimeTemp;
        public Logger logger_E;
        public Logger logger_T;


        public Entities.Ball ball;
        public Entities.Box box;
        public Entities.Effects effects;
        public int timer;
        public int elapsedTime;
        Boolean started;
        Boolean ready;
        public Boolean taskEnded;
        string text;
        Boolean displayText;

        Boolean ATonePlayed;
        Boolean LTonePlayed;
        SpriteFont font;
        


        public DotCtrl()
        {            
            ball = new Entities.Ball();
            box = new Entities.Box();
            effects = new Entities.Effects();
            
        }

        public void Initialize()
        {
            speedList = new int[12];
            timerList = new int[12];
            inBoxTime = new double[12];
            text = "";
            for (int i = 0; i < 12; i++)
            {
                string element = "block" + i;
                speedList[i] = Convert.ToInt32(Statics.XML.Root.Element(element).Element("ballSpeed").Value);
                timerList[i] = Convert.ToInt32(Statics.XML.Root.Element(element).Element("roundTimer").Value);
            }
            

        }

        public void LoadContent()
        {
            ball.LoadContent();
            effects.LoadContent();
            font = Statics.GAME1.Content.Load<SpriteFont>("SpriteFont1");
        }


        public void Update()
        {
                     
            this.GetStarted();

            if (started)
            {
                elapsedTime += (int)Statics.GAMETIME.ElapsedGameTime.TotalMilliseconds;
                timer -= (int)Statics.GAMETIME.ElapsedGameTime.TotalMilliseconds;

                if (timer > 0)
                {                   
                    Log_E();
                    Log_T();
                    AddInBoxTime();
                    ball.Update();
                }

                else if (timer <= 0)
                {                    
                    this.ResetBlock();
                }
            }

        }


        public void Draw()
        {
            if (started)
            {
                if (Logger.effect == "C")
                    this.ChangeBackgroundColor();
                if (Logger.effect == "T")
                    this.MakeTone();
            }
            
            box.Draw();
            ball.Draw();
            

            
            if (displayText)
            {
                Vector2 stringSize = font.MeasureString(text);
                Statics.GAME1.spriteBatch.DrawString(font, text, new Vector2(Statics.CENTER.X - (stringSize.X / 2), Statics.CENTER.Y - 100), Color.Black);
            }

        }



        public void ResetBlock()
        {
            if (block < 11)
            {
                inBoxTime[block] = (double)inBoxTimeTemp / timerList[block];
                inBoxTimeTemp = 0;
            }
            
            else if (block == 11)
            {
                EndLog(logger_E);
                EndLog(logger_T);
            }
          
            ready = false;
            started = false;
            ATonePlayed = false;
            LTonePlayed = false;
            effects.ATone.Stop();
            effects.LTone.Stop();
            ball.position = Statics.CENTER;
            ball.currentSpeedVector = new Vector2(0, 0);
            taskEnded = true;

        }


        public void MakeTone()
        {
            if (Statics.INPUT.CurrentState().IsKeyDown(Keys.A) && !ATonePlayed)
            {
                effects.ATone.Play();
                ATonePlayed = true;
            }

            if (!Statics.INPUT.CurrentState().IsKeyDown(Keys.A))
            {
                effects.ATone.Stop();
                ATonePlayed = false;
            }

            if (Statics.INPUT.CurrentState().IsKeyDown(Keys.L) && !LTonePlayed)
            {
                effects.LTone.Play();
                LTonePlayed = true;
            }

            if (!Statics.INPUT.CurrentState().IsKeyDown(Keys.L))
            {
                effects.LTone.Stop();
                LTonePlayed = false; 
            }

           
        }

        public void ChangeBackgroundColor()
        {

            if (Statics.INPUT.CurrentState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A) && Statics.INPUT.CurrentState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.L))
            {
                effects.Clear_lightPink();
            }

            else if (Statics.INPUT.CurrentState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
            {
                effects.Clear_lightRed();
            }

            else if (Statics.INPUT.CurrentState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.L))
            {
                effects.Clear_lightBlue();
            }

        }


        public void GetStarted()
        {
            
            if (!ready)
            {
                taskEnded = false;
                text = "Press Enter to Begin";
                displayText = true;
                if (Statics.INPUT.CurrentState().IsKeyDown(Keys.Enter))
                {
                    timer = 3000;
                    ready = true;
                }
            }

            else if (!started)
            {
                timer -= (int)Statics.GAMETIME.ElapsedGameTime.TotalMilliseconds;
                if (timer > 0)
                {
                    text = "Begins in " + ((timer / 1000).ToString("0")) + " ";
                }
                else
                {
                    started = true;
                    displayText = false;
                    timer = timerList[block];
                    elapsedTime = 0;
                    ball.definedSpeedVector = new Vector2(speedList[block], speedList[block]);
                    
                }
            }
        }


        public void Log_E()
        {
            if (Statics.INPUT.StateChanged())
            {
                string logString = "," + Logger.parNum;
                logString += "," + Logger.mode;
                logString += "," + Logger.effect;
                logString += "," + elapsedTime;
                logString += "," + block;
                logString += "," + Statics.INPUT.ListToKeys();
                logString += "," + ((int)ball.position.X - (int)Statics.CENTER.X);
                logString += "," + ((int)ball.position.Y - (int)Statics.CENTER.Y);
                logString += "," + ball.currentSpeedVector.X;
                logString += "," + ball.currentSpeedVector.Y;
                if (ball.position.X > Statics.CENTER.X - box.width / 2 && ball.position.X < Statics.CENTER.X + box.width / 2 && ball.position.Y > Statics.CENTER.Y - box.height / 2 && ball.position.Y < Statics.CENTER.Y + box.height / 2)
                    logString += ",1";
                else
                    logString += ",0";
                logString += "," + inBoxTimeTemp;
                logger_E.Log(logString);
            }
          
        }

        public void Log_T()
        {
            if (elapsedTime / logger_T.samplingInterval != logger_T.r)
            {
                logger_T.r = elapsedTime / logger_T.samplingInterval;
                string logString = "," + Logger.parNum;
                logString += "," + Logger.mode;
                logString += "," + Logger.effect;
                logString += "," + elapsedTime;
                logString += "," + block;
                logString += "," + Statics.INPUT.ListToKeys();
                logString += "," + ((int)ball.position.X - (int)Statics.CENTER.X);
                logString += "," + ((int)ball.position.Y - (int)Statics.CENTER.Y);
                logString += "," + ball.currentSpeedVector.X;
                logString += "," + ball.currentSpeedVector.Y;
                if (ball.position.X > Statics.CENTER.X - box.width / 2 && ball.position.X < Statics.CENTER.X + box.width / 2 && ball.position.Y > Statics.CENTER.Y - box.height / 2 && ball.position.Y < Statics.CENTER.Y + box.height / 2)                 
                    logString += ",1";          
                else
                    logString += ",0";
                logString += "," + inBoxTimeTemp;
                logger_T.Log(logString);
            }
        }

        public void AddInBoxTime()
        {
            if (ball.position.X > Statics.CENTER.X - box.width / 2 && ball.position.X < Statics.CENTER.X + box.width / 2 && ball.position.Y > Statics.CENTER.Y - box.height / 2 && ball.position.Y < Statics.CENTER.Y + box.height / 2)
                inBoxTimeTemp = (int)(inBoxTimeTemp + Statics.GAMETIME.ElapsedGameTime.TotalMilliseconds);
        }


        public void CreateLogger()
        {
            string filePath_E = Convert.ToString(Statics.XML.Root.Element("all").Element("logPath_E").Value);
            string filePath_T = Convert.ToString(Statics.XML.Root.Element("all").Element("logPath_T").Value);
            logger_E = new Logger(filePath_E, "DotCtrl_E");
            logger_T = new Logger(filePath_T, "DotCtrl_T");
            
        }

        public void StartLog(Logger logger)
        {
            
            logger.Begin();
            string logString = "";
            logString += ",lateralspeed: " + speedList[block];
            logString += ",verticalspeed: " + speedList[block];
            logString += ",XDimension: " + Statics.DIMENSION.X;
            logString += ",YDimension: " + Statics.DIMENSION.Y;
            logString += ",boxHeight: " + box.height;
            logString += ",boxWidth: " + box.width;
            logString += ",edgeThickness: " + box.edgeThickness;
            logString += ",roundTimer: " + timerList[block];
            logString += ",Box range x: " + (-box.width / 2) + " to " + (box.width / 2);
            logString += ",Box range y: " + (-box.height / 2) + " to " + (box.height / 2);
            logString += "\n,ParNum,Mode,Effect,TimeElapsed,Block,Key,X,Y,XSpeed,YSpeed,InBox,InBoxTime";
            logger.Log(logString);


        }
        public void EndLog(Logger logger)
        {
            inBoxTime[block] = (double)inBoxTimeTemp / timerList[block];
            string logString = "";
            for (int i = 0; i < inBoxTime.Length; i++)
            {
                logString += "," + i;
            }

            logString += "\nInBoxTime";

            for (int i = 0; i < inBoxTime.Length; i++)
            {
                logString += "," + inBoxTime[i].ToString("0.000");

            }
            logger.Log(logString);
            logger.Close();
           
        }


        
    }
}
