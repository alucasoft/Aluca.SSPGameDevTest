/*====================================================================================================================*/
/*

        Solution:       Aluca.SSPGameDevTest
        ===================================
        
        Project:        Aluca.SSPGameServer
        File:           SSPGameSimulationController.cs
        Version:        1.0.0
        Responsible:    Wolfgang Jurczik
        Created:        20.02.2022 
        Modified:       20.02.2022 
        
        copyright 2022 aluca Software
        all rights reserved
        
*/
/*====================================================================================================================*/

using Aluca.SSPGameEngine;
using Aluca.SSPGameServer.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Aluca.SSPGameServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SSPGameSimulationController : ControllerBase
    {
        private ISSPGameViewModel _gameViewModel = null;

        public SSPGameSimulationController(ISSPGameViewModel vm)
        {
            _gameViewModel = vm;
        }

        /// <summary>
        /// Executes the Simulation of Game.
        /// </summary>
        /// <returns>List ( List of SSPGameTurnData ) of all Turns of the game.</returns>
        /// 
        [HttpGet]
        public Task<IList<SSPGameTurnData>> Get()
        {
            _gameViewModel.ProcessSimulation();
            var gameProtocol = _gameViewModel.GetProtocolObject();
            if (gameProtocol != null)
                return gameProtocol.GetGameProtocolDataAsync();
            else
                return null;
        }
    }
}
