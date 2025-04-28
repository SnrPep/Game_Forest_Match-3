using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Match3Game.Utils;
using Match3Game.Grid;
using System;
using System.Collections.Generic;

namespace Match3Game.Screens
{
    public class GameScreen : IScreen
    {
        private double timeLeft = 10.0;
        private SpriteFont font;
        private GridManager gridManager;
        private List<Texture2D> elementTextures;

        private int[,] grid;
        private Random random;

        public GameScreen()
        {
            grid = new int[8, 8];
            random = new Random();
        }

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/DefaultFont");
            Element.InitializePixel(graphicsDevice);

            elementTextures = new List<Texture2D>
            {
                content.Load<Texture2D>("sprites/rhombus"),
                content.Load<Texture2D>("sprites/round"),
                content.Load<Texture2D>("sprites/square"),
                content.Load<Texture2D>("sprites/triangle"),
                content.Load<Texture2D>("sprites/star")
            };

            gridManager = new GridManager(elementTextures);
        }

        public void Update(GameTime gameTime)
        {
            double elapsedSeconds = gameTime.ElapsedGameTime.TotalSeconds;
            timeLeft -= elapsedSeconds;

            gridManager.Update(gameTime);

            if (timeLeft <= 0)
            {
                timeLeft = 0;
                ScreenManager.ChangeScreen(new GameOverScreen(gridManager.Score));
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            gridManager.Draw(spriteBatch);

            spriteBatch.DrawString(font, $"Time Left: {Math.Ceiling(timeLeft)}", new Vector2(100, 10), Color.Black);
            spriteBatch.DrawString(font, $"Score: {gridManager.Score}", new Vector2(400, 10), Color.Black);

            spriteBatch.End();
        }
    }
}
