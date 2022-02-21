/*====================================================================================================================*/
/*

        Solution:       Aluca.SSPGameDevTest
        ===================================
        
        Project:        Aluca.SSPGameEngine
        File:           SSPGame.cs
        Version:        1.0.0
        Responsible:    Wolfgang Jurczik
        Created:        20.02.2022 
        Modified:       20.02.2022 
        
        copyright 2022 aluca Software
        all rights reserved
        
*/
/*====================================================================================================================*/

using System;

namespace Aluca.SSPGameEngine
{
    public class SSPGame //: ISSPGame
    {
        #region fields

        private const int       MAX_GAMETURNS = 10000;
        private const int       MIN_GAMETURNS = 50;
        private const int       MAX_PLAYERNAME_LENGTH = 25;
        private SSPGamePlayer   _playerA = new() { Id = 1, Name = "Player-A" };
        private SSPGamePlayer   _playerB = new() { Id = 2, Name = "Player-B" };
        private Random          _random = new Random();
        private SSPGameProtocol _gameProtocol = new ();
        private int             _gameTurns = 100;
        #endregion fields

        #region constructor

        public SSPGame()
        {
        }

        #endregion



        #region public methods


        /// <summary>
        /// Sets the two Player-names in the game. Player-names cannot be empty and should not exceed MAX_PLAYERNAME_LENGTH characters
        /// </summary>
        /// <param name="userAName">the name of the first player.</param>
        /// <param name="userBName">the name of the second player.</param>
        /// <returns>Returns an GameError if the Game does NOT have valid player names.</returns>
        public SSPGameError SetPlayers(string userAName, string userBName)
        {
            if (String.IsNullOrEmpty(userAName) || userAName.Length > MAX_PLAYERNAME_LENGTH)
            {
                _playerA.Name = "";
                return SSPGameError.UserAInvalid;
            }
            if (String.IsNullOrEmpty(userBName) || userBName.Length > MAX_PLAYERNAME_LENGTH)
            {
                _playerB.Name = "";
                return SSPGameError.UserBInvalid;
            }

            _playerA = new SSPGamePlayer { Id = 100, Name = userAName };
            _playerA.StaticGameGesture = SSPGameGesture.Stone;

            _playerB = new SSPGamePlayer { Id = 200, Name = userBName };

            return SSPGameError.NoError;
        }

        /// <summary>
        /// Is initializing (reseting) and actually PLAYING the game, respectively the defined number of gameTurns
        /// </summary>
        /// <param name="gameTurns">the number if Turns.</param>
        /// <param name="useProtocol">if useProtocol is true, writes/saves data for each turn of the game.</param>
        /// <returns>Returns an GameError if the Game dont have valid data.</returns>
        public SSPGameError Play(int gameTurns = 100, bool writeProtokol = true)
        {
            SSPGameError retval = SSPGameError.NoError;

            if ( (retval = Init(gameTurns, writeProtokol)) == SSPGameError.NoError)
            {
                for (int gameTurnCounter = 1; gameTurnCounter <= gameTurns; ++gameTurnCounter)
                {
                    PlayTurn(gameTurnCounter);
                }
                Finish();
            }
            return retval;
        }

        /// <summary>
        /// Creates the final Textinformation about the winner of the played Game
        /// </summary>
        /// <returns>Returns useful Textinformation about the played game.</returns>
        public string GetFinalGameResult()
        {
            string gameInfo1 = "";

            if (_playerA.HasWon == _playerB.HasWon)
                gameInfo1 = $"NO WINNER, we finaly have a TIE ! Both, {_playerA.Name} and {_playerB.Name} have won {_playerA.HasWon} times";
            else if (_playerA.HasWon > _playerB.HasWon)
                gameInfo1 = $"{_playerA.Name}  has won  {_playerA.HasWon}/{_gameTurns} times      ==> {_playerA.Name}  is the WINNER !!!!!";
            else
                gameInfo1 = $"{_playerB.Name}  has won  {_playerB.HasWon}/{_gameTurns} times      ==> {_playerB.Name}  is the WINNER !!!!!";

            return gameInfo1;
        }

        /// <summary>
        /// Gets the linked Protocol Object which is responsible for creating/saving information for each single gameTurn.
        /// </summary>
        /// <returns>Returns the Protocol Object.</returns>
        public SSPGameProtocol GetProtocolObject()
        {
            return _gameProtocol;
        }

        #endregion public methods



        #region private methods


        /// <summary>
        /// Initializes ( resets ) all Game-Fields and checks if the Game has valid number of gameTurns/> class.
        /// </summary>
        /// <param name="gameTurns">The number of Turns ( playing steps ) of the Game.</param>
        /// <param name="useProtocol">If true, the Protocol-Object will be created and collect information for each turn (step) of the game.</param>
        /// <returns>Returns an GameError if the Game dont have valid data.</returns>
        private SSPGameError Init(int gameTurns, bool useProtocol)
        {
            if (gameTurns < MIN_GAMETURNS || gameTurns > MAX_GAMETURNS)
            {
                return SSPGameError.NumberOfGamesInvalid;
            }
            if (String.IsNullOrEmpty(_playerA.Name) || _playerA.Name.Length > MAX_PLAYERNAME_LENGTH)
            {
                return SSPGameError.UserAInvalid;
            }
            if (String.IsNullOrEmpty(_playerB.Name) || _playerB.Name.Length > MAX_PLAYERNAME_LENGTH)
            {
                return SSPGameError.UserBInvalid;
            }

            _gameTurns = gameTurns;
     
            _gameProtocol.Reset();
   
            _playerA.Reset();
            _playerB.Reset();

            _random = new Random();

            return SSPGameError.NoError;
        }

        /// <summary>
        /// Plays a single turn ( Step) of the Game/> class.
        /// </summary>
        /// <param name="turnCounter">the current turn/step of the Game.</param>
        private void PlayTurn(int turnCounter)
        {
            SSPGameTurnResult gameTurnResult = SSPGameTurnResult.Tie;

            do
            {
                SSPGameGesture playerAGesture = (_playerA.UseStaticGameGesture) ? _playerA.StaticGameGesture : GetRandomGameGesture();
                SSPGameGesture playerBGesture = (_playerB.UseStaticGameGesture) ? _playerB.StaticGameGesture : GetRandomGameGesture();

                gameTurnResult = CreateGameTurnResult(turnCounter, playerAGesture, playerBGesture);

            } while (gameTurnResult == SSPGameTurnResult.Tie);
        }

        private SSPGameTurnResult CreateGameTurnResult(int gameTurnCounter,SSPGameGesture playerAGesture, SSPGameGesture playerBGesture)
        {
            if (playerAGesture == playerBGesture)
            {
                return SSPGameTurnResult.Tie;
            }
            else
            {
                SSPGameTurnResult result = SSPGameTurnResult.Tie;

                if ((playerAGesture == SSPGameGesture.Paper   && playerBGesture == SSPGameGesture.Stone) ||   // paper wraps stone
                    (playerAGesture == SSPGameGesture.Scissor && playerBGesture == SSPGameGesture.Paper) ||   // scossors cuts paper
                    (playerAGesture == SSPGameGesture.Stone   && playerBGesture == SSPGameGesture.Scissor))   // stone grinds scissor
                {
                    _playerA.AccumulateWin();
                    result = SSPGameTurnResult.PlayerAWon;
                }
                else
                {
                    _playerB.AccumulateWin();
                    result = SSPGameTurnResult.PlayerBWon;
                }

                if (_gameProtocol != null)
                {
                    _gameProtocol.AddProtokolItem(gameTurnCounter, _playerA.Name, _playerB.Name, playerAGesture, playerBGesture, result);
                }
                return result;
            }
        }

        /// <summary>
        /// Creates a random number ( GameGesture ) of all available GameGestures/>
        /// </summary>
        /// <returns>Returns the create random number (GameGesture ).</returns>
        private SSPGameGesture GetRandomGameGesture()
        {
            var v = Enum.GetValues(typeof(SSPGameGesture));
            return (SSPGameGesture)v.GetValue(_random.Next(v.Length));
        }

        /// <summary>
        /// Will be invoked at the very end of a played Game. Appends final GameResult-TextInformation to the GameProtocol-List />
        /// </summary>
        private void Finish()
        {
        
            _gameProtocol.AddFinalGameResultItem(GetFinalGameResult());
        }

        #endregion private methods
    }
}
