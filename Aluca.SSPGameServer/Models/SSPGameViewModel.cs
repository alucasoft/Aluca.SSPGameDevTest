/*====================================================================================================================*/
/*

        Solution:       Aluca.SSPGameDevTest
        ===================================
        
        Project:        Aluca.SSPGameServer
        File:           SSPGameViewModel.cs
        Version:        1.0.0
        Responsible:    Wolfgang Jurczik
        Created:        20.02.2022 
        Modified:       20.02.2022 
        
        copyright 2022 aluca Software
        all rights reserved
        
*/
/*====================================================================================================================*/

using Aluca.SSPGameEngine;
using System.Collections.Generic;

namespace Aluca.SSPGameServer.Models
{

    public interface ISSPGameViewModel
    {
        List<SSPGameTurnData> ProcessSimulation();
        SSPGameProtocol GetProtocolObject();
    }

    public class SSPGameViewModel : ISSPGameViewModel
    {
        private List<SSPGameTurnData> _gameProtocolList = new();
        private SSPGame               _game = new();

        /// <summary>
        /// Creates the game and initializes the game with default players. 
        /// </summary>
        public SSPGameViewModel()
        {
            // default ==>  100 turns, create protocol => true);
            _game.SetPlayers("Player-A", "Player-B");
        }

        /// <summary>
        /// Creates the game and is processing the game simulation
        /// </summary>
        /// <returns>Returns the List of gameTurns.Each game is a sequence of n gameTurns</returns>
        public List<SSPGameTurnData>ProcessSimulation()
        {
            List<SSPGameTurnData> gameProtocolList = new();
            _game.Play();  
            var gameProtocol = _game.GetProtocolObject();
            if (gameProtocol != null)
            {
                var listData = gameProtocol.GetGameProtocolDataAsync();
                foreach (var item in listData.Result)
                {
                    gameProtocolList.Add(item);
                }
            }
            return gameProtocolList;
        }

        public SSPGameProtocol GetProtocolObject()
        {
            return _game.GetProtocolObject();
        }
    }
}
