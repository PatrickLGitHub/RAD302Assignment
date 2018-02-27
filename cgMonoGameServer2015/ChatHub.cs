using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace cgMonoGameServer2015
{
    public class ChatHub : Hub
    {
        public void SendMess(string name, string message)
        {
            Clients.All.heyThere(name, message);
        }
    }
}