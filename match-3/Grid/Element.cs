using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Match3Game.Grid
{
    public class Element
    {
        public int Type { get; private set; }
        public Texture2D Texture { get; private set; }
        public bool IsSelected { get; set; } = false;
        public Vector2 Offset { get; set; } = Vector2.Zero;

        private static Texture2D pixel;

        public Element(int type, Texture2D texture)
        {
            Type = type;
            Texture = texture;
        }

        public static void InitializePixel(GraphicsDevice graphicsDevice)
        {
            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, int size)
        {
            Rectangle rect = new Rectangle(
                (int)(position.X + Offset.X),
                (int)(position.Y + Offset.Y),
                size,
                size
            );

            spriteBatch.Draw(Texture, rect, Color.White);

            if (IsSelected)
            {
                int thickness = 4;
                Color borderColor = Color.Aqua;

                spriteBatch.Draw(pixel, new Rectangle(rect.X, rect.Y, rect.Width, thickness), borderColor);
                spriteBatch.Draw(pixel, new Rectangle(rect.X, rect.Bottom - thickness, rect.Width, thickness), borderColor);
                spriteBatch.Draw(pixel, new Rectangle(rect.X, rect.Y, thickness, rect.Height), borderColor);
                spriteBatch.Draw(pixel, new Rectangle(rect.Right - thickness, rect.Y, thickness, rect.Height), borderColor);
            }
        }
    }
}
