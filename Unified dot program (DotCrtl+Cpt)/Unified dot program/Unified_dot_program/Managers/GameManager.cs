using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Unified_dot_program.Managers
{
    public class GameManager
    {
              
        Boolean condition;
        Boolean parNumDone; 
        Boolean displayText;
        Boolean pause;
        string text;

        Games.DotCtrl dotCtrl;
        Games.Cmpt cmpt;     
        SpriteFont font;



        public GameManager()
        {
            
            dotCtrl = new Games.DotCtrl();
            cmpt = new Games.Cmpt();
           
        }

        public void Initialize()
        {
            dotCtrl.Initialize();
            cmpt.Initialize();
            Logger.Initialize();
                       
        }


        public void LoadContent()
        {
            dotCtrl.LoadContent();
            cmpt.LoadContent();
            font = Statics.GAME1.Content.Load<SpriteFont>("SpriteFont1");
        }



        public void Update()
        {
            


            if (!condition)
                this.SetCondition();

            else if (condition)
            {
                if (pause)
                {                 
                    text = "Section finished! \nPlease call experimenter for further instruction";
                    displayText = true;
                    if (Statics.INPUT.CurrentState().IsKeyDown(Keys.C))
                    {
                        pause = false;
                        displayText = false;
                    }
                }
                
                else if (dotCtrl.block == 0 && cmpt.session == 0)
                {
                    dotCtrl.Update();
                    if (dotCtrl.taskEnded)
                    {
                        dotCtrl.block++;
                        pause = true;
                    }
                        
                }

                else if (dotCtrl.block == 1 && cmpt.session == 0)
                {
                    cmpt.Update();
                    if (cmpt.taskEnded)
                    {
                        cmpt.session++;
                        pause = true;
                    }
                        
                }
                else if (dotCtrl.block == 1 && cmpt.session == 1)
                {
                    dotCtrl.Update();
                    if (dotCtrl.taskEnded)
                        dotCtrl.block++;
                }
                else if (dotCtrl.block == 2 && cmpt.session == 1)
                {
                    dotCtrl.Update();
                    if (dotCtrl.taskEnded)
                        dotCtrl.block++;
                }
                else if (dotCtrl.block == 3 && cmpt.session == 1)
                {
                    dotCtrl.Update();
                    if (dotCtrl.taskEnded)
                        dotCtrl.block++;
                }
                else if (dotCtrl.block == 4 && cmpt.session == 1)
                {
                    dotCtrl.Update();
                    if (dotCtrl.taskEnded)
                        dotCtrl.block++;
                }
                else if (dotCtrl.block == 5 && cmpt.session == 1)
                {
                    dotCtrl.Update();
                    if (dotCtrl.taskEnded)
                        dotCtrl.block++;
                }
                else if (dotCtrl.block == 6 && cmpt.session == 1)
                {
                    dotCtrl.Update();
                    if (dotCtrl.taskEnded)
                        dotCtrl.block++;
                }
                else if (dotCtrl.block == 7 && cmpt.session == 1)
                {
                    dotCtrl.Update();
                    if (dotCtrl.taskEnded)
                        dotCtrl.block++;
                }
                else if (dotCtrl.block == 8 && cmpt.session == 1)
                {
                    dotCtrl.Update();
                    if (dotCtrl.taskEnded)
                        dotCtrl.block++;
                }
                else if (dotCtrl.block == 9 && cmpt.session == 1)
                {
                    dotCtrl.Update();
                    if (dotCtrl.taskEnded)
                        dotCtrl.block++;
                }
                else if (dotCtrl.block == 10 && cmpt.session == 1)
                {
                    dotCtrl.Update();
                    if (dotCtrl.taskEnded)
                        dotCtrl.block++;
                }
                else if (dotCtrl.block == 11 && cmpt.session == 1)
                {
                    dotCtrl.Update();
                    if (dotCtrl.taskEnded)
                    {
                        dotCtrl.block++;
                        pause = true;
                    }
                        
                }
                else if (dotCtrl.block == 12 && cmpt.session == 1)
                {
                    cmpt.Update();
                    if (cmpt.taskEnded)
                        cmpt.session++;
                }

                else
                {
                    text = "All sections finished! Thank you!";
                    displayText = true;
                }

                    
            } // end of if (condition)


        } // end of Update()
        

        public void Draw()
        {
            Statics.GAME1.spriteBatch.Begin();

            if (!pause && condition)
            {
                if (dotCtrl.block == 0 && cmpt.session == 0)
                {
                    dotCtrl.Draw();
                }

                else if (dotCtrl.block == 1 && cmpt.session == 0)
                {
                    cmpt.Draw();
                }
                else if (dotCtrl.block == 1 && cmpt.session == 1)
                {
                    dotCtrl.Draw();
                }
                else if (dotCtrl.block == 2 && cmpt.session == 1)
                {
                    dotCtrl.Draw();
                }
                else if (dotCtrl.block == 3 && cmpt.session == 1)
                {
                    dotCtrl.Draw();
                }
                else if (dotCtrl.block == 4 && cmpt.session == 1)
                {
                    dotCtrl.Draw();
                }
                else if (dotCtrl.block == 5 && cmpt.session == 1)
                {
                    dotCtrl.Draw();
                }
                else if (dotCtrl.block == 6 && cmpt.session == 1)
                {
                    dotCtrl.Draw();
                }
                else if (dotCtrl.block == 7 && cmpt.session == 1)
                {
                    dotCtrl.Draw();
                }
                else if (dotCtrl.block == 8 && cmpt.session == 1)
                {
                    dotCtrl.Draw();
                }
                else if (dotCtrl.block == 9 && cmpt.session == 1)
                {
                    dotCtrl.Draw();
                }
                else if (dotCtrl.block == 10 && cmpt.session == 1)
                {
                    dotCtrl.Draw();
                }
                else if (dotCtrl.block == 11 && cmpt.session == 1)
                {
                    dotCtrl.Draw();
                }
                else if (dotCtrl.block == 12 && cmpt.session == 1)
                {
                    cmpt.Draw();
                }

                
            }

      

            if (displayText)
            {
                Vector2 stringSize = font.MeasureString(text);
                Statics.GAME1.spriteBatch.DrawString(font, text, new Vector2(Statics.CENTER.X - (stringSize.X / 2), Statics.CENTER.Y - 100), Color.Black);
                if (!parNumDone)
                {
                    Vector2 stringSize2 = font.MeasureString(Logger.parNum);
                    Statics.GAME1.spriteBatch.DrawString(font, Logger.parNum, new Vector2(Statics.DIMENSION.X / 2 - (stringSize2.X / 2), Statics.DIMENSION.Y / 2 + 50), Color.Black);
                }
            }

            Statics.GAME1.spriteBatch.End();

        }




        public void SetCondition()
        {
            displayText = true;
            if (!parNumDone)
            {
                text = "Please input the participant number: ";
                if (Statics.INPUT.CurrentState().IsKeyDown(Keys.Enter))
                    parNumDone = true;
                else if (Statics.INPUT.StateChanged())
                {
                    if (Statics.INPUT.CurrentState().GetPressedKeys().Length > 0)
                        Logger.parNum += Statics.INPUT.ListToKeys().Substring(1);
                    //previous = state;
                }
            }

            else if (Logger.mode == "")
            {
                text = "Please choose task mode (I/D)";
                if (Statics.INPUT.CurrentState().IsKeyDown(Keys.I))
                    Logger.mode = "I";
                if (Statics.INPUT.CurrentState().IsKeyDown(Keys.D))
                    Logger.mode = "D";
            }

            else if (Logger.effect == "")
            {
                text = "Please choose effect (C/N)";
                if (Statics.INPUT.CurrentState().IsKeyDown(Keys.T))
                {
                    Logger.effect = "T";

                }


                if (Statics.INPUT.CurrentState().IsKeyDown(Keys.C))
                {
                    Logger.effect = "C";

                }
                    
                if (Statics.INPUT.CurrentState().IsKeyDown(Keys.N))
                    Logger.effect = "N";
            }

            else
            {
                condition = true;
                dotCtrl.CreateLogger();
                dotCtrl.StartLog(dotCtrl.logger_E);
                dotCtrl.StartLog(dotCtrl.logger_T);             
                cmpt.CreateLogger();
                cmpt.StartLog();
                if (Logger.mode == "D")
                    cmpt.UseRawInput();
     

                displayText = false;
            }
        }









    }
}
