/*====================================================================================================================*/
/*

        Solution:       Aluca.SSPGameDevTest
        ===================================
        
        Project:        Aluca.SSPGameEngine
        File:           SSPGamePlayer.cs
        Version:        1.0.0
        Responsible:    Wolfgang Jurczik
        Created:        20.02.2022 
        Modified:       20.02.2022 
        
        copyright 2022 aluca Software
        all rights reserved
        
*/
/*====================================================================================================================*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aluca.SSPGameEngine
{
    public class SSPGamePlayer
    {
        private SSPGameGesture _staticGesture = SSPGameGesture.Stone;

        public int Id { get; set; }

        public string Name { get; set; }

        public int HasWon { get; private set; }

        public bool UseStaticGameGesture { get; private set; } = false;

        public SSPGameGesture StaticGameGesture
        {
            get { return _staticGesture; }
            set
            {
                _staticGesture       = value;
                UseStaticGameGesture = true;
            }
        }

        public void AccumulateWin()
        {
            HasWon = HasWon + 1;
        }

        public void Reset()
        {
            HasWon = 0;
        }
    }
}



