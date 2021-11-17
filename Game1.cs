using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kick__Push
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        //Game screens
        enum Screen
        {
            Title,
            MainGame
        }

        //misc
        Screen currentScreen;
        KeyboardState keyboardState;
        Random genertaor;

        //title screen
        SpriteFont title;
        Vector2 titleSize;
        Color titleColor;
        Texture2D playButton;


        //Skater
        int frame;
        bool ollieState;
        float animationStartTime, ollieHeight, ollieStartTime;
        List<Texture2D> skaterAnimationList = new List<Texture2D>();
        Texture2D skaterTexture, up, down;
        Rectangle skaterBounds;

        //background objects
        Texture2D bush1, tree1;
        List<BackgroundObject> backgroundObjects = new List<BackgroundObject>();
        Vector2 speedLevel1, speedLevel2, speedLevel3;

        //obstacles
        Texture2D obstacleKicker;
        List<Obstacle> obstacles = new List<Obstacle>();


        //Textures
        Texture2D street;

       

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 1200;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            //initial screen
            currentScreen = Screen.Title;

            //Initialize main animation
            frame = 0;
            animationStartTime = 0;
            ollieHeight = 0;
            ollieStartTime = 0;

            //initialize random generator
            genertaor = new Random();

            //initialize skater rectangle
            skaterBounds = new Rectangle(_graphics.PreferredBackBufferWidth / 3, 280, 141, 180);

            base.Initialize();

            //title text size
            titleSize = title.MeasureString("Kick, Push");
            titleColor = new Color(242, 225, 65);

            //set initial frame of skater
            skaterTexture = skaterAnimationList[frame];

            //initialize background objects
            speedLevel1 = new Vector2(-2f, 0);
            speedLevel2 = new Vector2(-1, 0);
            speedLevel3 = new Vector2(-0.5f, 0);

            backgroundObjects.Add(new BackgroundObject(bush1, new Rectangle(600, 230, 60, 60), speedLevel1));
            backgroundObjects.Add(new BackgroundObject(bush1, new Rectangle(800, 200, 40, 40), speedLevel2));
            backgroundObjects.Add(new BackgroundObject(bush1, new Rectangle(900, 180, 30, 30), speedLevel3));
            backgroundObjects.Add(new BackgroundObject(bush1, new Rectangle(200, 180, 30, 30), speedLevel3));
            backgroundObjects.Add(new BackgroundObject(tree1, new Rectangle(440, 100, 120, 160), speedLevel2));
            backgroundObjects.Add(new BackgroundObject(tree1, new Rectangle(740, 100, 90, 120), speedLevel3));

            backgroundObjects = backgroundObjects.OrderByDescending(o => o.Speed.X).ToList();

            obstacles.Add(new Obstacle(obstacleKicker, new Rectangle(800, 400, 180, 100), speedLevel1));
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            //title textures
            title = Content.Load<SpriteFont>("TitleFont");
            playButton = Content.Load<Texture2D>("play button");

            //Skater Animation Textures
            skaterAnimationList.Add(Content.Load<Texture2D>("skaterTexture1"));
            skaterAnimationList.Add(Content.Load<Texture2D>("skaterTexture2"));
            up = Content.Load<Texture2D>("Up");
            down = Content.Load<Texture2D>("Down");


            //Background Object Textures
            street = Content.Load<Texture2D>("ROAD 2 (bigger)");
            bush1 = Content.Load<Texture2D>("bush1");
            tree1 = Content.Load<Texture2D>("tree");

            //obstacle Textures
            obstacleKicker = Content.Load<Texture2D>("Kicker");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                currentScreen = Screen.MainGame;

            // Determine current screen and appply appropriate update logic
            if (currentScreen == Screen.Title)
                Title();

            else if (currentScreen == Screen.MainGame)
                MainGame(gameTime);

            base.Update(gameTime);
        }

        protected void Title()
        {
            // update loop for title screen
            
            
        }

        protected void MainGame(GameTime gameTime)
        {
            // update loop for main game screen

            keyboardState = Keyboard.GetState();

            //controls
            if (keyboardState.IsKeyDown(Keys.Down))
            {
                skaterBounds.Y += 1;
                skaterTexture = down;
            }

            if (keyboardState.IsKeyDown(Keys.Up))
            {
                skaterBounds.Y -= 1;
                skaterTexture = up;
            }

            if (keyboardState.IsKeyDown(Keys.S))
			{
              
			}

            if (keyboardState.IsKeyUp(Keys.S))
			{
                
			}




            //animate skater
            float elapsedAnimationTime = (float)gameTime.TotalGameTime.TotalMilliseconds - animationStartTime;

            if (elapsedAnimationTime > 450)
            {
                if (frame == 1)
                    frame = 0;
                else
                    frame++;

                if (keyboardState.IsKeyUp(Keys.Up) && keyboardState.IsKeyUp(Keys.Down))
                {
                    skaterTexture = skaterAnimationList[frame];
                    animationStartTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
                }
            }

            //move background
            
            for (int i = 0; i < backgroundObjects.Count; i++)
            {

                backgroundObjects[i].Move();

                if (backgroundObjects[i].Bounds.X <= (0 - backgroundObjects[i].Bounds.Width))
                {
                    //backgroundObjects[i].Reset(_graphics);
                    backgroundObjects[i].Bounds = new Rectangle(genertaor.Next(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferWidth + 100), backgroundObjects[i].Bounds.Y, backgroundObjects[i].Bounds.Width, backgroundObjects[i].Bounds.Height);
                    //backgroundObjects[i].Update(genertaor.Next(1100, 1200));
                    //backgroundObject.Location = new Point(genertaor.Next(1200, 1300), 200);
                }
                
            }

            //move obstacles

            for (int i = 0; i < obstacles.Count; i++)
            {

                obstacles[i].Move();

                if (obstacles[i].Bounds.X <= (0 - obstacles[i].Bounds.Width))
                {
                    obstacles[i].Bounds = new Rectangle(genertaor.Next(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferWidth + 100), genertaor.Next(300, 400), obstacles[i].Bounds.Width, obstacles[i].Bounds.Height);
                }

                else if (obstacles[i].Bounds.Left <= skaterBounds.Right && obstacles[i].Bounds.Left >= skaterBounds.Right - 110 && skaterBounds.Bottom <= obstacles[i].Bounds.Bottom && skaterBounds.Bottom >= obstacles[i].Bounds.Top)
                {
                    skaterBounds.Y -= 1;
                }

                else if (obstacles[i].Bounds.Right <= skaterBounds.Left  + 40 && obstacles[i].Bounds.Right >= skaterBounds.Left - 100 && skaterBounds.Bottom <= obstacles[i].Bounds.Bottom && skaterBounds.Bottom >= obstacles[i].Bounds.Top)
                {
                    skaterBounds.Y += 2;
                }
            }

            //check collisions



            //foreach (BackgroundObject backgroundObject in backgroundObjects)
            //{
            //    backgroundObject.Move();

            //    if (backgroundObject.Bounds.X <= (0 - backgroundObject.Bounds.Width/2))
            //    {
            //        backgroundObject.Bounds = new Rectangle(genertaor.Next(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferWidth + 100), backgroundObject.Bounds.Y, backgroundObject.Bounds.Width, backgroundObject.Bounds.Height);
            //        //backgroundObject.Location = new Point(genertaor.Next(1200, 1300), 200);
            //    }
            //}

        }



        protected override void Draw(GameTime gameTime)
        {

            // TODO: Add your drawing code here

            // determine current screen and draw apropriate textures
            if (currentScreen == Screen.Title)
            {
                GraphicsDevice.Clear(titleColor);
                DrawTitle();
            }

            else if (currentScreen == Screen.MainGame)
            {
                GraphicsDevice.Clear(Color.CornflowerBlue);
                DrawMainGame();
            }

            base.Draw(gameTime);
        }

        protected void DrawTitle()
        {
            // draw loop for title screen

            _spriteBatch.Begin();
            _spriteBatch.DrawString(title, "Kick, Push", new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 5), Color.Black, 0f, new Vector2(titleSize.X / 2, titleSize.Y / 2), 1f, SpriteEffects.None, 0f);
            _spriteBatch.Draw(playButton, new Rectangle(_graphics.PreferredBackBufferWidth / 2 - 256, _graphics.PreferredBackBufferHeight / 2 - 180, 512, 512), Color.White);
            _spriteBatch.End();
        }

        protected void DrawMainGame()
        {
            // draw loop for main game screen

            _spriteBatch.Begin();

            //street
            _spriteBatch.Draw(street, new Rectangle(0, 200, 1200, 400), Color.White);

            //background 
            foreach (BackgroundObject backgroundObject in backgroundObjects)
            {
                backgroundObject.Draw(_spriteBatch);
            }

            //obstacles
            foreach (Obstacle obstacle in obstacles)
            {
                obstacle.Draw(_spriteBatch);
            }

            //skater
            _spriteBatch.Draw(skaterTexture, skaterBounds, Color.White);

            _spriteBatch.End();


        }
    }
}
