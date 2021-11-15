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

        //Animation variables
        int frame;
        float animationStartTime;
        List<Texture2D> skaterAnimationList = new List<Texture2D>();
        Texture2D skaterTexture;

        //background objects
        Texture2D bush1, tree1;
        List<BackgroundObject> backgroundObjects = new List<BackgroundObject>();
        Vector2 speedLevel1, speedLevel2, speedLevel3;

        //Textures
        Texture2D street;

        //Lists

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
            currentScreen = Screen.MainGame;

            //Initialize main animation
            frame = 0;
            animationStartTime = 0;

            //initialize random generator
            genertaor = new Random();


            base.Initialize();
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
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            
            //Skater Animation Textures
            skaterAnimationList.Add(Content.Load<Texture2D>("skaterTexture1"));
            skaterAnimationList.Add(Content.Load<Texture2D>("skaterTexture2"));

            //Background Object Textures
            street = Content.Load<Texture2D>("ROAD 2 (bigger)");
            bush1 = Content.Load<Texture2D>("bush1");
            tree1 = Content.Load<Texture2D>("tree");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (keyboardState.IsKeyDown(Keys.Space))
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
            
            //animate skater
            float elapsedAnimationTime = (float)gameTime.TotalGameTime.TotalMilliseconds - animationStartTime;

            if (elapsedAnimationTime > 450)
            {
                if (frame == 1)
                    frame = 0;
                else
                    frame++;


                skaterTexture = skaterAnimationList[frame];
                animationStartTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
                
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
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            // determine current screen and draw apropriate textures
            if (currentScreen == Screen.Title)
            {
                DrawTitle();
            }

            else if (currentScreen == Screen.MainGame)
            {
                DrawMainGame();
            }

            base.Draw(gameTime);
        }

        protected void DrawTitle()
        {
            // draw loop for title screen
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

            //skater
            _spriteBatch.Draw(skaterTexture, new Rectangle(_graphics.PreferredBackBufferWidth / 3, 280, 141, 180), Color.White);

            _spriteBatch.End();


        }
    }
}
