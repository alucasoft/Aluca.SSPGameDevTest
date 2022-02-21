/*====================================================================================================================*/
/*

        Solution:       Aluca.SSPGameDevTest
        ===================================
        
        Project:        Aluca.SSPGameEngine
        File:           SSPGameTurnData.cs
        Version:        1.0.0
        Responsible:    Wolfgang Jurczik
        Created:        20.02.2022 
        Modified:       20.02.2022 
        
        copyright 2022 aluca Software
        all rights reserved
        
*/
/*====================================================================================================================*/

namespace Aluca.SSPGameEngine
{
    public class SSPGameTurnData
    {
        public int    GameTurnId { get; set; }
        public string WinnerName { get; set; }
        public string WinnerGesture { get; set; }
        public string TurnInfo { get; set; }
    }
}
