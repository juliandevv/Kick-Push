using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

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

        Screen currentScreen;
        KeyboardState keyboardState;

        //Animation variables
        int frame;
        float animationStartTime;
        List<Texture2D> skaterAnimationList = new List<Texture2D>();
        Texture2D skaterTexture;

        //background objects
        Texture2D bush1;
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



            base.Initialize();
            //set initial frame of skater
            skaterTexture = skaterAnimationList[frame];

            //initialize background objects
            speedLevel1 = new Vector2(0, 2);
            speedLevel1 = new Vector2(0, 1);
            speedLevel1 = new Vector2(0, (float)0.5);

            backgroundObjects.Add(new BackgroundObject(bush1, new Rectangle(200, 600, 41, 41), speedLevel1));
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

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

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

            if (keyboardState.IsKeyDown(Keys.Enter))
                currentScreen = Screen.MainGame;
        }

        protected void MainGame(GameTime gameTime)
        {
            // update loop for main game screen
            
            //animate skater
            float elapsedAnimationTime = (float)gameTime.TotalGameTime.TotalMilliseconds - animationStartTime;

            if (elapsedAnimationTime > 550)
            {
                if (frame == 1)
                    frame = 0;
                else
                    frame++;


                skaterTexture = skaterAnimationList[frame];
                animationStartTime = (float)gameTime.TotalGameTime.TotalMilliseconds;
                
            }

            //move background
            foreach (BackgroundObject backgroundObject in backgroundObjects)
            {
                //backgroundObject.Move();
            }

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
            //skater
            _spriteBatch.Draw(skaterTexture, new Rectangle(_graphics.PreferredBackBufferWidth / 3, 300, 141, 180), Color.White);
            //background 
            backgroundObjects[0].Draw(_spriteBatch);
            //foreach (BackgroundObject backgroundObject in backgroundObjects)
            //{
            //    backgroundObject.Draw(_spriteBatch);
            //}
            _spriteBatch.End();


        }
    }
}
