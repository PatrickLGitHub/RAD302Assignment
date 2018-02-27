namespace cgMonoGameServer2015.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Models;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;

    internal sealed class Configuration : DbMigrationsConfiguration<cgMonoGameServer2015.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "cgMonoGameServer2015.Models.ApplicationDbContext";
        }
        int _usercounter = 0;
        public int Counter { get { return ++_usercounter; } }

        int _emailCounter = 0;
        public int EmailCounter { get { return ++_emailCounter; } }

        int _passCounter = 0;
        public int PassCounter { get { return ++_passCounter; } }


        protected override void Seed(cgMonoGameServer2015.Models.ApplicationDbContext context)
        {

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Games.AddOrUpdate(g => g.GameName,
                new Game { GameName = "Battle Call" },
                new Game { GameName = "Pong" });

            context.SaveChanges();

            Random r = new Random();
            PasswordHasher hasher = new PasswordHasher();
            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    XP = r.Next(400),
                    UserName = "powell.paul@itsligo.ie" ,
                    Email = "powell.paul@itsligo.ie",
                    EmailConfirmed = true,
                    GamerTag = "ppowell",
                    PasswordHash = hasher.HashPassword("itsPaul$1" ),
                    SecurityStamp = Guid.NewGuid().ToString()
                },

                new ApplicationUser
                {
                    XP = r.Next(400),
                    UserName = "User" + Counter.ToString() + "@itsligo.ie",
                    Email = "User" + EmailCounter.ToString() + "@itsligo.ie",
                    EmailConfirmed = true,
                    GamerTag = "GamerTag" + _usercounter.ToString(), 
                    PasswordHash = hasher.HashPassword("Password$" + _usercounter.ToString()),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    XP = r.Next(400),
                    UserName = "User" + Counter.ToString() + "@itsligo.ie",
                    Email = "User" + EmailCounter.ToString() + "@itsligo.ie",
                    EmailConfirmed = true,
                    GamerTag = "GamerTag" + _usercounter.ToString(),
                    PasswordHash = hasher.HashPassword("Password$" + _usercounter.ToString()),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    XP = r.Next(400),
                    UserName = "User" + Counter.ToString() + "@itsligo.ie",
                    Email = "User" + EmailCounter.ToString() + "@itsligo.ie",
                    EmailConfirmed = true,
                    GamerTag = "GamerTag" + _usercounter.ToString(),
                    PasswordHash = hasher.HashPassword("Password$" + _usercounter.ToString()),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    XP = r.Next(400),
                    UserName = "User" + Counter.ToString() + "@itsligo.ie",
                    Email = "User" + EmailCounter.ToString() + "@itsligo.ie",
                    EmailConfirmed = true,
                    GamerTag = "GamerTag" + _usercounter.ToString(),
                    PasswordHash = hasher.HashPassword("Password$" + _usercounter.ToString()),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser {
                XP = r.Next(400),
                    UserName = "User" + Counter.ToString() + "@itsligo.ie",
                    Email = "User" + EmailCounter.ToString() + "@itsligo.ie",
                    EmailConfirmed = true,
                    GamerTag = "GamerTag" + _usercounter.ToString(),
                    PasswordHash = hasher.HashPassword("Password$" + _usercounter.ToString()),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    XP = r.Next(400),
                    UserName = "User" + Counter.ToString() + "@itsligo.ie",
                    Email = "User" + EmailCounter.ToString() + "@itsligo.ie",
                    EmailConfirmed = true,
                    GamerTag = "GamerTag" + _usercounter.ToString(),
                    PasswordHash = hasher.HashPassword("Password$" + _usercounter.ToString()),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    XP = r.Next(400),
                    UserName = "User" + Counter.ToString() + "@itsligo.ie",
                    Email = "User" + EmailCounter.ToString() + "@itsligo.ie",
                    EmailConfirmed = true,
                    GamerTag = "GamerTag" + _usercounter.ToString(),
                    PasswordHash = hasher.HashPassword("Password$" + _usercounter.ToString()),
                    SecurityStamp = Guid.NewGuid().ToString()
                },
                new ApplicationUser
                {
                    XP = r.Next(400),
                    UserName = "User" + Counter.ToString() + "@itsligo.ie",
                    Email = "User" + EmailCounter.ToString() + "@itsligo.ie",
                    EmailConfirmed = true,
                    GamerTag = "GamerTag" + _usercounter.ToString(),
                    PasswordHash = hasher.HashPassword("Password$" + _usercounter.ToString()),
                    SecurityStamp = Guid.NewGuid().ToString()
                }
                );
            context.SaveChanges();
            List<GameScore> scores = new List<GameScore>();
            Game bg = context.Games.FirstOrDefault(battle => battle.GameName == "Battle Call");
            if (bg != null)
            {
                foreach(ApplicationUser player in context.Users)
                {
                    //context.GameScores.AddOrUpdate(score => score.PlayerID,
                       scores.Add(new GameScore
                       { PlayerID = player.Id,
                           score = r.Next(1200),
                           GameID = bg.GameID }
                       );
                }
                context.GameScores.AddOrUpdate(score => score.PlayerID,
                    scores.ToArray());

                context.SaveChanges();
            }

        }
    }
}
