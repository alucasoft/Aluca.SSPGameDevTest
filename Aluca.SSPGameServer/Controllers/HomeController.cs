/*====================================================================================================================*/
/*

        Solution:       Aluca.SSPGameDevTest
        ===================================
        
        Project:        Aluca.SSPGameServer
        File:           HomeController.cs
        Version:        1.0.0
        Responsible:    Wolfgang Jurczik
        Created:        20.02.2022 
        Modified:       20.02.2022 
        
        copyright 2022 aluca Software
        all rights reserved
        
*/
/*====================================================================================================================*/

using Aluca.SSPGameServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;


namespace Aluca.SSPGameServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
