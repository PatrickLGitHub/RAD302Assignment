using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.Xna.Framework;
using System.Timers;

namespace cgMonoGameServer2015
{
    public class MoveCharacterHub : Hub
    {
        //private Vector2 v1;
        private Point point;
        static Timer t;
        public MoveCharacterHub():base()
        {
            t = new Timer(500);
            t.Elapsed += T_Elapsed;
            t.Start();
        }

        private void T_Elapsed(object sender, ElapsedEventArgs e)
        {
            Clients.All.setPosition(Point);
        }

        public void GetPoint()
        {
            Clients.All.setPosition(Point);
        }

        public Point Point
        {
            get
            {
                Random r = new Random();
                return new Point(r.Next(400), r.Next(400));
            }

            set
            {
                point = value;
            }
        }
    }
}