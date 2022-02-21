using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aluca.SSPGameEngine
{

    #region enums
    public enum SSPGameGesture
    {
        Scissor = 1,
        Stone   = 2,
        Paper   = 3
    }


    public enum SSPGameTurnResult
    {
        PlayerAWon  = 1,
        PlayerBWon  = 2,
        Tie         = 3
    }

    public enum SSPGameError
    {
        UserAInvalid,
        UserBInvalid,
        NumberOfGamesInvalid,
        NoError
    };

    #endregion enums





    interface ISSPGame
    {
        string SetPlayers(string user1Name, string user2Name);

        SSPGameError Play(int gameTurns = 100, bool writeProtokol = true);

        string GetFinalGameResult();

        SSPGameProtocol GetProtocolObject();

        SSPGameError Init(int gameTurns, bool useProtocol);

        void PlayTurn(int turnCounter);

        SSPGameTurnResult CreateGameTurnResult(int gameTurnCounter, SSPGameGesture playerAGesture,
                                               SSPGameGesture playerBGesture);

        SSPGameGesture GetRandomGameGesture();

        void Finish();
    }
}
