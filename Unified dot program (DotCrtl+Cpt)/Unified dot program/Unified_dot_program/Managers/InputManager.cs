using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;



namespace Unified_dot_program.Managers
{
    
    public class InputManager
    {
        private KeyboardState _oldKS;
        private KeyboardState _KS;
        

        public InputManager()
        {
            Statics.INPUT = this;
        }

        public void Update()
        {

            _oldKS = _KS;
            _KS = Keyboard.GetState();
        }

        public KeyboardState CurrentState()
        {
            return this._KS;
        }


        public bool StateChanged()
        {
            return (_oldKS != _KS);
        }

        public bool IsKeyPressed(Keys k)
        {
            return (_oldKS.IsKeyUp(k) && _KS.IsKeyDown(k));
        }

        public bool IsKeyReleased(Keys k)
        {
            return (_oldKS.IsKeyDown(k) && _KS.IsKeyUp(k));
        }



        public string ListToKeys()
        {
            string s = "";
            Keys[] list = this._KS.GetPressedKeys();
            foreach (Keys k in list)
            {
                s += k.ToString();
            }
            return s;
        }

        
    }
}
