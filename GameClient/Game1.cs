using DataClasses;
using Engine.Engines;
using GameComponentNS;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Rad302CameraClass;
using Sprites;
using System.Collections.Generic;

namespace GameClient
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        HubConnection serverConnection;
        string connectionMessage = string.Empty;
        IHubProxy proxy;
        Camera cam;
        SpriteFont font;
        Viewport v;
        PlayerDataObject PlayerInfo;
        Texture2D Playerimage;
        SimplePlayerSprite player;
        Texture2D Background;
        Rectangle mainFrame;
        public static List<SimplePlayerSprite> totalPlayers = new List<SimplePlayerSprite>();

        public string ID { get; private set; }

        public bool Connected { get; private set; }
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            new InputEngine(this);

            //new Vector2(tileMap.GetLength(1) * tileWidth, tileMap.GetLength(0) * tileHeight));
            serverConnection = new HubConnection("https://rad302gameass.azurewebsites.net");
            //serverConnection = new HubConnection("http://localhost:55712/");
            serverConnection.StateChanged += ServerConnection_StateChanged;
            proxy = serverConnection.CreateHubProxy("GameHub");
            serverConnection.Start();
            cam = new Camera(Vector2.Zero, new Vector2(v.Width, v.Height));
            Playerimage = Content.Load<Texture2D>("Player 1");
            player = new SimplePlayerSprite(this,  PlayerInfo, Playerimage,
                                    new Point(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2));
            totalPlayers.Add(player);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService<SpriteBatch>(spriteBatch);
            Background = Content.Load<Texture2D>("Background");
            mainFrame = new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height);
            

            font = Content.Load<SpriteFont>("Message");
            Services.AddService<SpriteFont>(font);
            // TODO: use this.Content to load your game content here
        }

        private void ServerConnection_StateChanged(StateChange State)
        {
            switch (State.NewState)
            {
                case ConnectionState.Connected:
                    connectionMessage = "Connected......";
                    Connected = true;
                    //startGame();
                    break;
                case ConnectionState.Disconnected:
                    connectionMessage = "Disconnected.....";
                    if (State.OldState == ConnectionState.Connected)
                        connectionMessage = "Lost Connection....";
                    Connected = false;
                    break;
                case ConnectionState.Connecting:
                    connectionMessage = "Connecting.....";
                    Connected = false;
                    break;
            }
        }

        private void startGame()
        {
            // Continue on and subscribe to the incoming messages joined, currentPlayers, otherMove messages

            // Immediate Pattern
            proxy.Invoke<PlayerDataObject>("Join")
                .ContinueWith( // This is an inline delegate pattern that processes the message 
                               // returned from the async Invoke Call
                        (r) =>
                        { // Wtih p do 
                            if (r.Result == null)
                                connectionMessage = "No player Data returned";
                            else
                            {
                                CreatePlayer(r.Result);
                                // Here we'll want to create our game player using the image name in the PlayerData 
                                // Player Data packet to choose the image for the player
                                // We'll use a simple sprite player for the purposes of demonstration 
                            }

                        });

        }

        private void CreatePlayer(PlayerDataObject player)
        {
            //ID = player.GamerTag;
            PlayerInfo = player;
            //new SimplePlayerSprite(this, player, Playerimage,
            //                        new Point(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2));

            //Playerimage = Content.Load<Texture2D>(player.textureName);
            //new FadeText(this, Vector2.Zero, " Welcome " + player.GamerTag + " you are playing as " + player.textureName);
            //totalPlayers.Add(player);
            //cam.follow(new Vector2((int)player.playerPosition.X, (int)player.playerPosition.Y), GraphicsDevice.Viewport);
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();

            //spriteBatch.Draw(Playerimage, new Rectangle(Player.position.X,
            //              Player.position.Y,
            //              Playerimage.Width,
            //              Playerimage.Height),
            //                Color.White);
            spriteBatch.Draw(Background, mainFrame, Color.White);
            //spriteBatch.Draw(Playerimage, new Vector2(450, 240), Color.White);
            spriteBatch.DrawString(font, connectionMessage, new Vector2(10, 10), Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            // TODO: Add your drawing code here
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
