using System;
using System.Collections.Generic;
using System.Timers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace AlmostFlappyBird
{
    public class AlmostFlappy : Game
    {
        // 1280 × 720
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        const int GAME_HEIGHT = 720;
        const int GAME_WIDTH = 1280;
        Player player;
        Background background1;
        Background background2;
        Background gameover;

        List<List<Wall>> walls;
        Timer gameTimer;
        int holeLength;
        int wallVelocity;
        Random rng;

        SpriteFont basicFont;
        int score;

        bool isGameRunning = true;


        public AlmostFlappy()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = GAME_HEIGHT;
            graphics.PreferredBackBufferWidth = GAME_WIDTH;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            score++;
            walls = new List<List<Wall>>();
            rng = new Random();
            gameTimer = new Timer(1000);
            gameTimer.Elapsed += delegate { SpawnWall(); };
            gameTimer.Enabled = true;
            wallVelocity = 25;
            holeLength = 3;
            score = 0;

            base.Initialize();
        }
        private void SpawnWall()
        {
            List<Wall> currentwall = new List<Wall>();
            int holePosition = rng.Next(0, 10 - holeLength);
            for (int i =0; i < 20; i++)
            {
                if( (i<=holePosition ) || (i >= (holePosition + holeLength)))
                {
                    currentwall.Add(new Wall(new Vector2(GAME_WIDTH, i*50+10), Content.Load<Texture2D>("rock"), wallVelocity));
                }
                walls.Add(currentwall);
            }
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            player = new Player(new Vector2(20, GAME_HEIGHT/2), Content.Load<Texture2D>("bird")); // stavi ime slike
            background1 = new Background(new Vector2(0, -360), Content.Load<Texture2D>("sky"));
            background2 = new Background(new Vector2(1920, -360), Content.Load<Texture2D>("sky_reverse"));
            gameover = new Background(new Vector2(0, 0), Content.Load<Texture2D>("gameover"));
            basicFont = Content.Load<SpriteFont>("Font");



        }
        protected override void Update(GameTime gameTime)
        {
            if (isGameRunning)
            {
                ++score;
            }
            if(score >= 200 && score <= 400)
            {
                wallVelocity = 25;
            }
            if(score > 400 && score <= 600)
            {
                wallVelocity = 30;
            }
            if(score > 600 && score <= 800)
            {
                wallVelocity = 35;
            }
            if(score > 800 && score <= 1000)
            {
                wallVelocity = 40;
            }
            if(score > 1000)
            {
                wallVelocity = 50;
            }
            float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
            player.Velocity = new Vector2(0, 0);
            background1.Velocity = new Vector2(-100, 0);
            background2.Velocity = new Vector2(-100, 0);

            GameOver();
            KeyHandler();
            UpdateEntities(elapsed);
            base.Update(gameTime);
        }

        private void GameOver()
        {
            foreach (var rock in walls)
            {
                foreach (var wall in rock)
                {
                    if (wall.Bounds.Intersects(player.Bounds))
                    {
                        isGameRunning = false;
                    }
                }
            }
        }

        private void UpdateEntities(float elapsed)
        {
            player.Update(elapsed);
            background1.Update(elapsed);
            background2.Update(elapsed);
            foreach(var rock in walls)
            {
                foreach(var wall in rock)
                {
                    wall.Update(elapsed);
                }
            }
        }

        private void KeyHandler()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                player.Velocity.Y = 600;
                background1.Velocity.Y = -50;
                background2.Velocity.Y = -50;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                player.Velocity.Y = -600;
                background1.Velocity.Y = 50;
                background2.Velocity.Y = 50;
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            background1.Draw(spriteBatch);
            background2.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.DrawString(basicFont, "Score: " + score, new Vector2(0, 0), Color.Red);
            foreach(var rock in walls)
            {
                foreach(var wall in rock)
                {
                    wall.Draw(spriteBatch);
                }
            }
            if (!isGameRunning)
            {
                gameover.Draw(spriteBatch);
                spriteBatch.DrawString(basicFont, "Score: " + score, new Vector2(600, 50), Color.Green);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
