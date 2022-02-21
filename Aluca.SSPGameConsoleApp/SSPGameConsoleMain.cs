/*====================================================================================================================*/
/*

        Solution:       Aluca.SSPGameDevTest
        ===================================
        
        Project:        SSPGameConsoleApp
        File:           SSPGameConsoleMain.cs
        Version:        1.0.0
        Responsible:    Wolfgang Jurczik
        Created:        20.02.2022 
        Modified:       20.02.2022 
        
        copyright 2022 aluca Software
        all rights reserved
        
*/
/*====================================================================================================================*/

using Aluca.SSPGameEngine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Aluca.SSPGameConsoleApp
{
    public abstract class ApiHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static void InitializeClient()
        {
            string baseUrl = $"https://localhost:44358/";

            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            ApiClient.BaseAddress = new Uri(baseUrl);
        }
    }

    class SSPGameConsoleMain
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Aluca.SSPGameConsole App has been started");
            Console.WriteLine("\n-------------------------------------------------------\n");

            ApiHelper.InitializeClient();

            await DoSSPGameSimulation();

            Console.WriteLine("\n---------------------------------------------------------\n");
            Console.WriteLine("Aluca.SSPGameConsole has been finished");
            Console.ReadLine();
        }
        
        public static async Task DoSSPGameSimulation()
        {
            string url = $"api/SSPGameSimulation"; 

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();

                    //Console.WriteLine("result: " + result);
                    Console.WriteLine(    "\n-------------------------------------------------------------\n");

                    List<SSPGameTurnData> gameProtocolData = JsonConvert.DeserializeObject<List<SSPGameTurnData>>(result);

                    if (gameProtocolData != null)
                    {
                        foreach (SSPGameTurnData currTurn in gameProtocolData)
                        {
                            if (currTurn.GameTurnId == 0)  // the final GameResult Row
                            {
                                Console.WriteLine("   " + currTurn.TurnInfo);
                                Console.WriteLine("\n---------------------------------------------------------\n");
                            }
                            else
                            {
                                Console.WriteLine("   Turn:    " + currTurn.GameTurnId);
                                Console.WriteLine("   Winner:  " + currTurn.WinnerName);
                                Console.WriteLine("   Gesture: " + currTurn.WinnerGesture);
                                Console.WriteLine("   Info:    " + currTurn.TurnInfo);
                                Console.WriteLine("\n---------------------------------------------------------\n");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Protocol data did not get created !");
                    }
                }
                else
                {
                    Console.WriteLine("failed: " + response);
                }
            }
        }
    }
}
