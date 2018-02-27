using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using DataClasses;
using System.Threading.Tasks;

namespace cgMonoGameServer2015
{

    public static class Player
    {
        public static string PlayerID;
    }

    public static class HubState
    {
        public static HashSet<string> playerConnections = new HashSet<string>();
        public static string[] BarrierNames = new string[] {"","" };
        public static Dictionary<string, string> playerBArriers = new Dictionary<string, string>();
    }

    public class MultiHubV2 : Hub
    {
        // When a player joins if there are any other players then inform them of a new opponent
        public void join(string Joined, string CharacterName, int x, int y)
        {
            HubState.playerConnections.Add(Context.ConnectionId);
            Clients.All.opponentJoined(Joined, CharacterName,x,y);
            int playerCount = HubState.playerConnections.ToList()
                            .FindIndex(p => p.Equals(Context.ConnectionId));
            Clients.Caller.playerNumber(playerCount);
            Clients.Others.opponentNumber(playerCount);
        }
        
        // The original player that started needs to be added as an opponent as when instantiated
        // There was no other clients to be added to so we catch up and start the game
        public void addMe(string playerID, string CharacterName, int x, int y)
        {
            int playerCount = HubState.playerConnections.ToList().FindIndex(p => p.Equals(playerID));
            Clients.Others.opponentJoined(playerID, CharacterName, x, y);
            Clients.Others.opponentNumber(playerCount);
            Clients.All.setup();
        }
        // update other clients with barrier hits
        public void barrierHit(int x, int y)
        {
            Clients.Others.opponentBarrierHit(x, y, Context.ConnectionId);
        }
        // update other clients with items collected
        public void collected(int x, int y)
        {
            Clients.Others.opponentCollected(x, y, Context.ConnectionId);
        }

        public void winner()
        {
            Clients.Others.loosers(Context.ConnectionId);
        }

        //
        public void changeConnection(string OldConnection)
        {
            HubState.playerConnections.Remove(OldConnection);
            HubState.playerConnections.Add(Context.ConnectionId);
        }

        // When a player leaves the game let the other clients know
        public void leave()
        {
            Clients.All.opponentLeft(Context.ConnectionId);
            Clients.Others.Remove(Context.ConnectionId);
            HubState.playerConnections.Remove(Context.ConnectionId);
            
        }

        // When a player moves let the other clients know
        public void playerMoved(int x, int y)
        {
            Clients.Others.opponentMoved( x,y );
        }

        public void setUpOpponentBarrier(int x, int y)
        {
            Clients.Others.createOpponentBarrier(x, y);
        }

        public void setUpOpponentCollectable(int x, int y)
        {
            Clients.Others.createOpponentCollectable(x, y);
        }

        public int getPlayerCount()
        {
            return HubState.playerConnections.Count();
       }

        public int getPlayerNumber(string ConnID)
        {
            return HubState.playerConnections.ToList().FindIndex(p => p.Equals(ConnID));
        }


        


    }
}