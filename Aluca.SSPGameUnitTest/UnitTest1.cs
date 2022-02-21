using Aluca.SSPGameEngine;
using System;
using Xunit;

namespace Aluca.SSPGameUnitTest
{
    public class UnitTest1
    {
        [Fact]
        public void TestInvalidUser()
        {
            // User-Names cannot be null or empty 
            SSPGame game = new();
            game.SetPlayers("", null);   
            SSPGameError ret = game.Play();
            Assert.NotEqual(SSPGameError.NoError, ret);
        }

        [Fact]
        public void TestMaxTurns()
        {
            // number of Turns per Game has to be in a range of 50 and 10 000
            SSPGame game = new();
            var ret = game.Play(10001);
            Assert.Equal(SSPGameError.NumberOfGamesInvalid,ret);
        }

        [Fact]
        public void TestMinTurns()
        {
            // number of Turns per Game has to be in a range of 50 and 10 000
            SSPGame game = new();
            var ret = game.Play(49);
            Assert.Equal(SSPGameError.NumberOfGamesInvalid, ret);
        }

        [Fact]
        public void TestProtocolOnListDataCount()
        {
            // if the user dont set the number of turns, then
            // the default value is 100 ==> 101 , because of finalGameResult will always be added at the end of the list
            // if the user dont set the useProtocol-flag to false
            // the default value is true ==> collect data in ProtocolObject
            SSPGame game = new();
            game.Play();
            var protocolObj = game.GetProtocolObject();
            Assert.Equal(101,protocolObj.GetListCount());
        }

        [Fact]
        public void TestHighProtocolListDataCount()
        {
            // turn off the protocol list
            // but we still have to expect one item in this list, the final GameResult-item
            SSPGame game = new();
            game.Play(9500,true);   // 9500 turns and Protocol is on
            var protocolObj = game.GetProtocolObject();
            Assert.Equal(9501, protocolObj.GetListCount());  
        }
    }
}
