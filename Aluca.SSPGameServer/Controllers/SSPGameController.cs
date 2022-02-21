/*====================================================================================================================*/
/*

        Solution:       Aluca.SSPGameDevTest
        ===================================
        
        Project:        Aluca.SSPGameServer
        File:           SSPGameController.cs
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

namespace Aluca.SSPGameServer.Controllers
{
    [Route("[controller]/[action]")]
    public class SSPGameController : Controller
    {
        private ISSPGameViewModel _gameViewModel = null;

        public SSPGameController(ISSPGameViewModel vm)
        {
            _gameViewModel = vm;
        }

        public IActionResult GameSimulation()
        {
            List<SSPGameTurnData> gameProtocolList = _gameViewModel.ProcessSimulation();
            if (gameProtocolList != null && gameProtocolList.Count > 0)
                this.ViewBag.GameFinalResult = gameProtocolList[gameProtocolList.Count-1].TurnInfo;
            return View(gameProtocolList);
        }
    }
}
