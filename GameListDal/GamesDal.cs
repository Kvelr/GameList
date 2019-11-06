using GameListCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameListDal
{
    public class GamesDal
    {
        public GamesDal(GameListDBContext ctx)
        {       
                ctx.Games.Add(new Game
                {
                    AwayTeamName = "me",
                    ContestName = "context",
                    HomeTeamName = "home",
                    LeagueName = "league",
                    SportType = SportTypes.soccer,
                    StartDate = DateTime.Now
                });         
        }
    }
}
