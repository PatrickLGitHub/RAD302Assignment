using cgMonoGameServer2015.Models;
using DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace cgMonoGameServer2015.Controllers
{
    [RoutePrefix("api/GameScores")]
    public class GameScoresController : ApiController
    {
        [Route("getTops/Count/{Count:int}/Game/{gameName}")]
        public dynamic getTop(int Count, string gameName )
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Game g = db.Games.FirstOrDefault(game => game.GameName == gameName);
                if (g == null)
                    return BadRequest("Game does not exist");

                return 
                    (from scores in db.GameScores
                     join players in db.Users
                     on scores.PlayerID equals players.Id
                     where scores.GameID == g.GameID
                     orderby scores.score descending
                     select new { g.GameID, g.GameName, players.GamerTag, scores.score })
                     .Take(Count).ToList();
            }

        }
        [Authorize]
        [Route("playerInfo")]
        [HttpGet]
        public PlayerProfile playerInfo()
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
               var info = db.Users.Where(u => u.UserName == User.Identity.Name)
                    .Select(p => new PlayerProfile { id= p.Id,
                        GamerTag = p.GamerTag,email = p.Email,userName = p.UserName, XP= p.XP })
                    .FirstOrDefault();
                return info;
            }
        }

        [HttpPost]
        [Route("postScore")]
        public IHttpActionResult postScore(PlayerScoreObject gs)
        {
            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                db.GameScores.Add(new GameScore { GameID = gs.GameId, PlayerID=gs.PlayerId, score = gs.score });
                db.SaveChanges();
                return Content(HttpStatusCode.OK, gs);
            }

        }
    }
}
