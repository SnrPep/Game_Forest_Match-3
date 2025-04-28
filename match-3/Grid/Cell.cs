using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Match3Game.Grid
{
    public class Cell
    {
        public Point GridPosition { get; private set; }
        public Vector2 WorldPosition { get; private set; }
        public Element Element { get; set; }

        public event EventHandler<Cell> Clicked;

        private Rectangle rect;
        private ButtonState oldClickState = Mouse.GetState().LeftButton;
        public static int cellSize { get; } = 64;

        public Cell(int gridX, int gridY, Vector2 worldPos)
        {
            GridPosition = new Point(gridX, gridY);
            WorldPosition = worldPos;
            rect = new Rectangle((int)worldPos.X, (int)worldPos.Y, cellSize, cellSize);
        }

        public void Update(GameTime gameTime)
        {
            HandleInput();
        }

        private void HandleInput()
        {
            MouseState mouseState = Mouse.GetState();
            Point mousePos = new Point(mouseState.X, mouseState.Y);

            if (rect.Contains(mousePos))
            {
                if (mouseState.LeftButton == ButtonState.Pressed && oldClickState == ButtonState.Released)
                {
                    Clicked?.Invoke(this, this);
                }
            }

            oldClickState = mouseState.LeftButton;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Element?.Draw(spriteBatch, WorldPosition, cellSize);
        }
    }
}
