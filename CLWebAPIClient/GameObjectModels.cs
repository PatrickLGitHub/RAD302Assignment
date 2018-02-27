﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebAPIAuthenticationClient
{
    public class PlayerProfile
        {
        public string id;
        public string GamerTag;
        public string email;
        public string userName;
        public int XP;
        }

    public class GameObject
    {

        public int GameID { get; set; }

        public string GameName { get; set; }


    }
    public class GameScoreObject
    {
        public int GameId { get; set; }

        public string GameName { get; set; }

        public string GamerTag { get; set; }

        public int score { get; set; }

    }

    [Serializable]
    public class PlayerScoreObject
    {
        public int GameId { get; set; }

        public string PlayerId { get; set; }

        public int score { get; set; }

        public override string ToString()
        {
            return GameId.ToString() + " " + PlayerId + " " + score.ToString();
        }
    }

}
