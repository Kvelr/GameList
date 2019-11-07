using GameListCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameListDal
{
    public class GamesDal
    {
        private GameListDBContext _ctx;

        public GamesDal(GameListDBContext ctx)
        {
            _ctx = ctx;
        }

        public void AddGame(Game gameToAdd)
        {
            _ctx.Games.Add(gameToAdd);
        }
    }
}
