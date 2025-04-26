using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Match3Game.Utils;
using System;

namespace Match3Game.Screens
{
    public class GameScreen : IScreen
    {
        private Texture2D cellTexture;
        private int cellSize = 64;
        private int gridSize = 8;

        private int[,] grid;
        private Random random;

        public GameScreen()
        {
            grid = new int[8, 8];
            random = new Random();
        }

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            cellTexture = new Texture2D(graphicsDevice, 1, 1);
            cellTexture.SetData(new[] { Color.White });

            GenerateRandomGrid();
        }

        private void GenerateRandomGrid()
        {
            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    grid[x, y] = random.Next(0, 5);
                }
            }
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            for (int y = 0; y < gridSize; y++)
            {
                for (int x = 0; x < gridSize; x++)
                {
                    Vector2 position = new Vector2(x * cellSize + 100, y * cellSize + 50);
                    Rectangle cellRect = new Rectangle((int)position.X, (int)position.Y, cellSize, cellSize);

                    Color color = GetColorByType(grid[x, y]);

                    spriteBatch.Draw(cellTexture, cellRect, color);
                }
            }

            spriteBatch.End();
        }

        private Color GetColorByType(int type)
        {
            switch (type)
            {
                case 0: return Color.Red;
                case 1: return Color.Blue;
                case 2: return Color.Green;
                case 3: return Color.Yellow;
                case 4: return Color.Purple;
                default: return Color.Gray;
            }
        }
    }
}
