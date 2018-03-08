using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using DataClasses;
using Microsoft.Xna.Framework;

namespace gameServer
{
    public class GameHub : Hub
    {
        #region Game hub variables

       // Use static to protect Data across dofferent hub invocations
        public static Queue<PlayerDataObject> RegisteredPlayers = new Queue<PlayerDataObject>(new PlayerDataObject[]
        {
            new PlayerDataObject { GamerTag = "M4sterchi3f", textureName = "", id = Guid.NewGuid().ToString(), XP = 200 },
            new PlayerDataObject { GamerTag = "N0SC0PE", textureName = "", id = Guid.NewGuid().ToString(), XP = 2000 },
            new PlayerDataObject { GamerTag = "NoobSlayer", textureName = "", id = Guid.NewGuid().ToString(), XP = 1200 },
            new PlayerDataObject { GamerTag = "Player5572", textureName = "", id = Guid.NewGuid().ToString(), XP = 3200 },
        });

        public static List<PlayerDataObject> Players = new List<PlayerDataObject>();

        public static Stack<string> characters = new Stack<string>(
                    new string[] { "Player 4", "Player 3", "Player 2", "Player 1" });

        #endregion

        public void Hello()
        {
            Clients.All.hello();
        }


        public PlayerDataObject Join()
        {
            // Check and if the charcters
            if (characters.Count > 0)
            {
                // pop name
                string character = characters.Pop();
                // if there is a registered player
                if (RegisteredPlayers.Count > 0)
                {
                    PlayerDataObject newPlayer = RegisteredPlayers.Dequeue();
                    newPlayer.textureName = character;
                    newPlayer.position = new Point
                    {
                        X = new Random().Next(700),
                        Y = new Random().Next(500)
                    };
                    // Tell all the other clients that this player has Joined
                    Clients.Others.Joined(newPlayer);
                    // Tell this client about all the other current 
                    Clients.Caller.CurrentPlayers(Players);
                    // Finaly add the new player on teh server
                    Players.Add(newPlayer);


                    return newPlayer;
                }


            }
            return null;
        }


        //public void Moved(string playerID, Position newPosition)
        //{
        //    // Update the collection with the new player position is the player exists
        //    PlayerData found = Players.FirstOrDefault(p => p.playerID == playerID);

        //    if (found != null)
        //    {
        //        // Update the server player position
        //        found.playerPosition = newPosition;
        //        // Tell all the other clients this player has moved
        //        Clients.Others.OtherMove(playerID, newPosition);

        //    }
        //}

        public string Chat(string message)
        {
   
            if (message != null)
            {
                Clients.Others.chat(message);
                return message;
            }

            return null;
            // Clients.All(new ChatText(this, Vector2.Zero, message););

        }
        public string LeaderBoardINvoke(string message)
        {

            if (message != null)
            {
                Clients.Others.leader(message);
                return message;
            }

            return null;
            // Clients.All(new ChatText(this, Vector2.Zero, message););

        }

        //public void RemovePlayer(string playerID)
        //{
        //    // Update the collection with the new player position is the player exists
        //    PlayerData found = Players.FirstOrDefault(p => p.playerID == playerID);

        //    if (found != null)
        //    {
                
        //        // Tell this client about all the other current 
        //        Clients.Caller.CurrentPlayers(Players);
        //        // Finaly add the new player on teh server
        //        Players.Remove(found);
        //        characters.Push(found.imageName);
        //        Clients.Others.Left(found);
        //    }

        //}
    }
}