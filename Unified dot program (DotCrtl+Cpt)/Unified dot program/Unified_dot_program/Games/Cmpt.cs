using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using RawInput_dll;
using System.Windows.Forms;

namespace Unified_dot_program.Games
{
    public class Cmpt
    {
        string text;
        Boolean displayText;
        public int session;
        Boolean ready;
        Boolean started;
        public Boolean taskEnded; 
        Boolean playStarted;
        Boolean playEnded;
        public Boolean left;
        public Boolean right;
        public Boolean APic;
        public Boolean LPic;
        Boolean setTimer;
        int keyboardHandleA;
        int keyboardHandleL;


        int timer;
        int playTimer;
        int stage1Time;
        int stage2Time;
        int stage3Time;
        int stage4Time;
        public int RT;

        public int ballSpeed;
        public int stage;
        public int trialID;
        public int trialType;
        int[] trialArray;

        string response;
        Boolean responseDone;
        Boolean response_ADone;
        Boolean response_LDone;

        public Logger logger;
        public Entities.Ball ball;
        Entities.Box box;
        Entities.Effects effects;
        Entities.Images images;

        string tt;
        SpriteFont font;
        private RawInput rawInput;
       

        public Cmpt()
        {
            ball = new Entities.Ball();
            box = new Entities.Box();
            effects = new Entities.Effects();
            images = new Entities.Images();
            trialArray = new int[80];
            
        }

        public void Initialize()
        {
            text = "";
            response = "";

            tt = "keyboard id";
            stage = 1;
            trialArray = this.GenerateTrialArray();
            stage1Time = Convert.ToInt32(Statics.XML.Root.Element("cmpt").Element("stage1Time").Value);
            stage2Time = Convert.ToInt32(Statics.XML.Root.Element("cmpt").Element("stage2Time").Value);
            stage3Time = Convert.ToInt32(Statics.XML.Root.Element("cmpt").Element("stage3Time").Value);
            stage4Time = Convert.ToInt32(Statics.XML.Root.Element("cmpt").Element("stage4Time").Value);
            ballSpeed = Convert.ToInt32(Statics.XML.Root.Element("cmpt").Element("ballSpeed").Value);
            keyboardHandleA = Convert.ToInt32(Statics.XML.Root.Element("cmpt").Element("keyboardHandleA").Value);
            keyboardHandleL = Convert.ToInt32(Statics.XML.Root.Element("cmpt").Element("keyboardHandleL").Value);

        }

        public void LoadContent()
        {
            ball.LoadContent();
            effects.LoadContent();
            images.LoadContent();
            font = Statics.GAME1.Content.Load<SpriteFont>("SpriteFont1");
        }

        public void Update()
        {

            GetStarted();

            if (started)
            {
                trialType = trialArray[trialID] / 10;

                if (stage == 1)
                {
                    this.Stage1();
                } // inter-trial interval

                else if (stage == 2)
                {
                    this.Stage2();
                } // crosshair

                else if (stage == 3)
                {
                    this.Stage3();
                } // blank interval 

                else if (stage == 4)
                {
                    this.Stage4();
                } // response

                else if (stage == 5)
                {
                    this.ResetTrial();
                } // reset for next trial

            }


        }

        public void Draw()
        {

            if (playStarted && !playEnded)
            {
                if (trialType == 5 || trialType == 6)
                {
                    effects.Clear_lightBlue();
                }
                else if (trialType == 7 || trialType == 8)
                {
                    effects.Clear_lightRed();
                }
            }

            if (Statics.INPUT.CurrentState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.U)) // testing the keyboard handle
                Statics.GAME1.spriteBatch.DrawString(font, tt, new Vector2(Statics.DIMENSION.X / 2, Statics.DIMENSION.Y / 2 + 200), Color.Black);


            if (displayText)
            {
                Vector2 stringSize = font.MeasureString(text);
                Statics.GAME1.spriteBatch.DrawString(font, text, new Vector2(Statics.DIMENSION.X / 2 - (stringSize.X / 2), Statics.DIMENSION.Y / 2), Color.Black);
            }

            if (stage == 2)
                images.DrawCrosshair();

            

            if (stage == 4 && playStarted && !playEnded)
            {
                if (trialType == 1 || trialType == 2 || trialType == 3 || trialType == 4)
                    DrawDirection();

                if (APic)
                    images.DrawLetterA();

                if (LPic)
                    images.DrawLetterL();

                
            }

            //spriteBatch.DrawString(spriteFont, "trial ID = " + trialID.ToString(), new Vector2(700, 500), Color.Black);
            //spriteBatch.DrawString(spriteFont, "random = " + trialArray[trialID].ToString(), new Vector2(700, 600), Color.Black);
            //spriteBatch.DrawString(spriteFont, "trial type = " + trialType.ToString(), new Vector2(700, 700), Color.Black);
            // spriteBatch.DrawString(spriteFont, test, new Vector2(800, 700), Color.Black);



        } // End of Draw()





        public void UpdateDirection()
        {
            
            ball.Move();
         
        }

        public void DrawDirection()
        {
            box.Draw();
            ball.Draw();
        }


        public void GetStarted()
        {
            
            if (!ready)
            {
                taskEnded = false;
                text = "Press Enter to Begin";
                displayText = true;
                
                if (Statics.INPUT.CurrentState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter))
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
                }
            }
        }



        public int[] GenerateTrialArray()
        {
            int[] array = new int[80];
            for (int i = 0; i < 80; i++)
            {
                array[i] = i + 10;
            } // generate an array of numbers from 10 to 89.

            Random random = new Random();
            int k, r;
            for (int i = 0; i < array.Length; i++)
            {
                r = random.Next(0, 80);
                k = array[i];
                array[i] = array[r];
                array[r] = k;
            }
            return array;
        }  // generate a shuffled array based on BasicArray()


        public void Stage1()
        {
            if (!setTimer)
            {
                timer = stage1Time;
                setTimer = true;
            }
            else
            {
                timer -= (int)Statics.GAMETIME.ElapsedGameTime.TotalMilliseconds;
                if (timer <= 0)
                {
                    stage = 2;
                    setTimer = false;
                }

            }

        }

        public void Stage2()
        {
            if (!setTimer)
            {
                timer = stage2Time;
                setTimer = true;
            }
            else
            {
                timer -= (int)Statics.GAMETIME.ElapsedGameTime.TotalMilliseconds;
                if (timer <= 0)
                {
                    stage = 3;
                    setTimer = false;
                }
            }
        }

        public void Stage3()
        {
            if (!setTimer)
            {
                timer = stage3Time;
                setTimer = true;
            }
            else
            {
                timer -= (int)Statics.GAMETIME.ElapsedGameTime.TotalMilliseconds;
                if (timer <= 0)
                {
                    stage = 4;
                    setTimer = false;
                }
            }
        }

        public void Stage4()
        {
            if (!setTimer)
            {
                timer = stage4Time;
                setTimer = true;
            }
            else
            {
                timer -= (int)Statics.GAMETIME.ElapsedGameTime.TotalMilliseconds;
                if (timer <= 0)
                {
                    stage = 5;
                    setTimer = false;
                }
            }

            if (!playEnded)
            {
                if (trialType == 1 || trialType == 2)
                {
                    left = true;
                    if (!playStarted)
                    {
                        ball.position = new Vector2(Statics.CENTER.X + 150, Statics.CENTER.Y);
                        playStarted = true;
                    }
                    else if (playStarted)
                    {
                        ball.currentSpeedVector = new Vector2(-ballSpeed, 0);
                        ball.Move();
                        if (ball.position.X < Statics.CENTER.X - 150)
                            playEnded = true;
                    }

                }
                else if (trialType == 3 || trialType == 4)
                {
                    right = true;
                    if (!playStarted)
                    {
                        ball.position = new Vector2(Statics.CENTER.X - 150, Statics.CENTER.Y);
                        playStarted = true;
                    }
                    else
                    {
                        ball.currentSpeedVector = new Vector2(ballSpeed, 0);
                        ball.Move();
                        if (ball.position.X > Statics.CENTER.X + 150)
                            playEnded = true;
                    }
                }

                else if (trialType == 5 || trialType == 6)
                {
                    left = true;
                    if (!playStarted)
                    {
                        playTimer = 800;
                        playStarted = true;
                        if (Logger.effect == "T")
                            effects.LTone.Play();
                    }
                    else
                    {
                        playTimer -= (int)Statics.GAMETIME.ElapsedGameTime.TotalMilliseconds;
                        if (playTimer <= 0)
                        {
                            if (Logger.effect == "T")
                                effects.LTone.Stop();
                            playEnded = true;
                        }

                    }
                }
                else if (trialType == 7 || trialType == 8)
                {
                    right = true;
                    if (!playStarted)
                    {
                        playTimer = 800;
                        playStarted = true;
                        if (Logger.effect == "T")
                            effects.ATone.Play();
                    }
                    else
                    {
                        playTimer -= (int)Statics.GAMETIME.ElapsedGameTime.TotalMilliseconds;
                        if (playTimer <= 0)
                        {
                            if (Logger.effect == "T")
                                effects.ATone.Stop();
                            playEnded = true;
                        }

                    }
                }
            }

            RT += (int)Statics.GAMETIME.ElapsedGameTime.TotalMilliseconds;

            if (Logger.mode == "I")
            {
                if (!responseDone)
                {
                    
                    if (Statics.INPUT.CurrentState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.A))
                    {
                        response = "A";
                        responseDone = true;

                    }
                    else if (Statics.INPUT.CurrentState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.L))
                    {
                        response = "L";
                        responseDone = true;
                    }

                    if (responseDone)
                    {
                        String logString = "," + Logger.parNum;
                        logString += "," + (session + 1);
                        logString += "," + Logger.mode;
                        logString += "," + Logger.effect;
                        logString += "," + (trialID + 1).ToString();
                        logString += ",";
                        if (trialType == 1 || trialType == 2 || trialType == 3 || trialType == 4) logString += "D";
                        else if (Logger.effect == "C") logString += "C";
                        else if (Logger.effect == "T") logString += "T";
                        logString += ",";
                        if (left) logString += "L";
                        else if (right) logString += "A";
                        logString += ",";
                        if (APic) logString += "A";
                        else if (LPic) logString += "L";
                        logString += ",";
                        if (left && LPic || right && APic) logString += 1;
                        else logString += 0;
                        logString += ",";
                        logString += response;
                        logString += "," + (int)RT;
                        logString += ",";
                        if (APic && response == "A" || LPic && response == "L") logString += 1;
                        else logString += 0;
                        logger.Log(logString);
                    }

                }
                    
                
            }

            

            if (trialType == 1 || trialType == 3 || trialType == 5 || trialType == 7)
                APic = true;
            else if (trialType == 2 || trialType == 4 || trialType == 6 || trialType == 8)
                LPic = true;

         

           
        } // response


        public void ResetTrial()
        {
            RT = 0;
            left = false;
            right = false;
            APic = false;
            LPic = false;
            playStarted = false;
            playEnded = false;
            stage = 1;
            responseDone = false;
            response_ADone = false;
            response_LDone = false;
            trialArray = this.GenerateTrialArray();
            if (trialID < 79)
                trialID++;
            else if (trialID == 79)
                this.ResetSession();
        }
        

        public void ResetSession()
        {
            ready = false;
            started = false;
            trialID = 0;
            taskEnded = true;
        }

        public void CreateLogger()
        {
            string filePath_cmpt = Convert.ToString(Statics.XML.Root.Element("all").Element("logPath_cmpt").Value);
            logger = new Logger(filePath_cmpt, "CmptTest");


        }

        public void UseRawInput()
        {
            rawInput = new RawInput(Statics.GAME_WINDOW_HANDLE, true);
            rawInput.KeyPressed += OnKeyPressed;
        }

        private void OnKeyPressed(object sender, RawInputEventArg e)
        {
            
            if (stage == 4 && Logger.mode == "D")
            {
                tt = e.KeyPressEvent.DeviceHandle.ToString();
                if (!response_ADone)
                {

                    if (e.KeyPressEvent.VKeyName == "A" && (int)e.KeyPressEvent.DeviceHandle == keyboardHandleA)
                    {
                        response = "A";
                        response_ADone = true;
                      
                    }

                    else if (e.KeyPressEvent.VKeyName == "L" && (int)e.KeyPressEvent.DeviceHandle == keyboardHandleA)
                    {
                        response = "L";
                        response_ADone = true;
                        
                    }

                    if (response_ADone)
                    {
                        String logString = "," + Logger.parNum;
                        logString += ",A";
                        logString += "," + Logger.parNum + "A";
                        logString += "," + (session + 1);
                        logString += "," + Logger.mode;
                        logString += "," + Logger.effect;
                        logString += "," + (trialID + 1).ToString();
                        logString += ",";
                        if (trialType == 1 || trialType == 2 || trialType == 3 || trialType == 4) logString += "D";
                        else if (Logger.effect == "C") logString += "C";
                        else if (Logger.effect == "T") logString += "T";
                        logString += ",";
                        if (left) logString += "L";
                        else if (right) logString += "A";
                        logString += ",";
                        if (APic) logString += "A";
                        else if (LPic) logString += "L";
                        logString += ",";
                        if (left && LPic || right && APic) logString += 1;
                        else logString += 0;
                        logString += ",";
                        logString += response;
                        logString += "," + (int)RT;
                        logString += ",";
                        if (APic && response == "A" || LPic && response == "L") logString += 1;
                        else logString += 0;
                        logger.Log(logString);
                    }
                }

                if (!response_LDone)
                {
                    if (e.KeyPressEvent.VKeyName == "A" && (int)e.KeyPressEvent.DeviceHandle == keyboardHandleL)
                    {
                        response = "A";
                        response_LDone = true;
                        
                    }

                    else if (e.KeyPressEvent.VKeyName == "L" && (int)e.KeyPressEvent.DeviceHandle == keyboardHandleL)
                    {
                        response = "L";
                        response_LDone = true; 
                    }

                    if (response_LDone)
                    {
                        String logString = "," + Logger.parNum;
                        logString += ",L";
                        logString += "," + Logger.parNum + "L";
                        logString += "," + (session + 1);
                        logString += "," + Logger.mode;
                        logString += "," + Logger.effect;
                        logString += "," + (trialID + 1).ToString();
                        logString += ",";
                        if (trialType == 1 || trialType == 2 || trialType == 3 || trialType == 4) logString += "D";
                        else if (Logger.effect == "C") logString += "C";
                        else if (Logger.effect == "T") logString += "T";
                        logString += ",";
                        if (left) logString += "L";
                        else if (right) logString += "A";
                        logString += ",";
                        if (APic) logString += "A";
                        else if (LPic) logString += "L";
                        logString += ",";
                        if (left && LPic || right && APic) logString += 1;
                        else logString += 0;
                        logString += ",";
                        logString += response;
                        logString += "," + (int)RT;
                        logString += ",";
                        if (APic && response == "A" || LPic && response == "L") logString += 1;
                        else logString += 0;
                        logger.Log(logString);
                    }

                }


            }
            
        } // raw input


        public void StartLog()
        {
            logger.Begin();
            string logString = "";
            if (Logger.mode == "I")
            {
                logString += ",lateralspeed: " + ballSpeed;
                logString += ",crossTime: " + stage1Time;
                logString += ",primingTime: 800ms";
                logString += ",movementDistance: from -150 to 150";
                logString += ",XDimension: " + Statics.DIMENSION.X;
                logString += ",YDimension: " + Statics.DIMENSION.Y;
                logString += ",boxHeight: " + box.height;
                logString += ",boxWidth: " + box.width;
                logString += ",edgeThickness: " + box.edgeThickness;
                logString += ",Box range x: " + (-box.width / 2) + " to " + (box.width / 2);
                logString += ",Box range y: " + (-box.height / 2) + " to " + (box.height / 2);
                logString += "\n,ParNum,Session,Mode,Effect,TrialID,PrimingType,PrimedKey,TargetKey,Comp(1)/Incomp(0),Response,RT,Correctness(1_correct)";
            }
            else if (Logger.mode == "D")
            {
                logString += ",lateralspeed: " + ballSpeed;
                logString += ",crossTime: " + stage1Time;
                logString += ",primingTime: 800ms";
                logString += ",movementDistance: from -150 to 150";
                logString += ",XDimension: " + Statics.DIMENSION.X;
                logString += ",YDimension: " + Statics.DIMENSION.Y;
                logString += ",boxHeight: " + box.height;
                logString += ",boxWidth: " + box.width;
                logString += ",edgeThickness: " + box.edgeThickness;
                logString += ",Box range x: " + (-box.width / 2) + " to " + (box.width / 2);
                logString += ",Box range y: " + (-box.height / 2) + " to " + (box.height / 2);
                logString += "\n,PairNum,KeyRole,ID,Session,Mode,Effect,TrialID,PrimingType,PrimedKey,TargetKey,Comp(1)/Incomp(0),Response,RT,Correctness(1_correct)";
            }
            

            logger.Log(logString);
        }






    }
}
