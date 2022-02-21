/*====================================================================================================================*/
/*

        Solution:       Aluca.SSPGameDevTest
        ===================================
        
        Project:        Aluca.SSPGameEngine
        File:           SSPGameProtocol.cs
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
using System.Threading.Tasks;

namespace Aluca.SSPGameEngine
{
    public class SSPGameProtocol
    {
        private List<SSPGameTurnData> _protokolList = new();

        public SSPGameProtocol()
        {
        }

        /// <summary>
        /// Resets/Clears the Protocol-Data-List, to get prepared for a new or another Game-Simulation. 
        /// </summary>
        public void Reset()
        {
            if (_protokolList.Count > 0)
                _protokolList.Clear();

        }

        /// <summary>
        /// Creates and sets the data for an GameTurnData-Item, once a single gameTurn/Step has been played.
        /// </summary>
        /// <param name="gameTurnCounter">The number of the played GameTurn ( Step).</param> 
        /// <param name="playerAName">The name of the first player.</param>
        /// <param name="playerBName">The name of the second player.</param>
        /// <param name="playerAGesture">The played ( in the current Turn ) GameGesture by the first Player.</param>
        /// <param name="playerBGesture">The played ( in the current Turn ) GameGesture by the second Player.</param>
        /// <param name="gameTurnResult">By the Game-Logic created information, who is the winner of this current GameTurn.</param>
        public void AddProtokolItem(int gameTurnCounter, string playerAName,string playerBName,SSPGameGesture playerAGesture, 
                                    SSPGameGesture playerBGesture, SSPGameTurnResult gameTurnResult)
        {
            string playerAGestureName = Enum.GetName(typeof(SSPGameGesture), playerAGesture);
            string playerBGestureName = Enum.GetName(typeof(SSPGameGesture), playerBGesture);
            string winnerName    = (gameTurnResult == SSPGameTurnResult.PlayerAWon) ? $"{playerAName}" : $"{playerBName}";
            string winnerGesture = (gameTurnResult == SSPGameTurnResult.PlayerAWon) ? $"{playerAGestureName}" : $"{playerBGestureName}";

            SSPGameGesture winGesture = (gameTurnResult == SSPGameTurnResult.PlayerAWon) ? playerAGesture : playerBGesture;
            var gestureVerb = GetGestureVerb(winGesture);  // each Gesture has a curtain verb ==> a scissor CUTS a paper ... ect

            string turnInfo = (gameTurnResult == SSPGameTurnResult.PlayerAWon) ?
                              $"{playerAGestureName}  {gestureVerb}   {playerBGestureName}" :
                              $"{playerBGestureName}  {gestureVerb}   {playerAGestureName}";

            SSPGameTurnData newItem = new();
            newItem.GameTurnId    = gameTurnCounter;
            newItem.WinnerName    = winnerName;
            newItem.WinnerGesture = winnerGesture;
            newItem.TurnInfo      = turnInfo;

            _protokolList.Add(newItem);
        }


        /// <summary>
        /// Finds and sets a fitting Verb for a certain GameGesture, e.g. scissor CUTS paper while Paper WRAPS a Stone.
        /// </summary>
        /// <param name="winGesture">The Id of the last winning GameGesture.</param> 
        /// <returns>Returns the fitting Verb.</returns>
        private string GetGestureVerb(SSPGameGesture winGesture)
        {
            string gestureVerb = "grinds";
            if (winGesture == SSPGameGesture.Paper)
                gestureVerb = "wraps";
            if (winGesture == SSPGameGesture.Scissor)
                gestureVerb = "cuts";
            return gestureVerb;
        }

        /// <summary>
        /// Appends a final GameResult-TextInformation to the GameProtocol-List />
        /// </summary>
        /// <param name="finalGameResult">The final Text-Information of the finished ( finalized ) played Game.</param> 
        public void AddFinalGameResultItem(string finalGameResult)
        {
            SSPGameTurnData newItem = new() { TurnInfo = finalGameResult };
            _protokolList.Add(newItem);
        }

        /// <summary>
        /// Gets all the collected GameTurn-data-Items asynchronized.
        /// </summary>
        /// <returns>IList Object of all collected GameTurn-data-Items.</returns>
        public async Task<IList<SSPGameTurnData>> GetGameProtocolDataAsync()
        {
            IList<SSPGameTurnData> list = await Task.Run(() => _protokolList);
            return list;
        }

        public int GetListCount()
        {
            return _protokolList.Count;
        }
    }
}
