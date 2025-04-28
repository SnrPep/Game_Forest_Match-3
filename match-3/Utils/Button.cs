using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Match3Game
{
    public class Button : Clickable
    {
        private readonly Texture2D texture;
        public string Text { get; set; }
        public SpriteFont Font { get; set; }
        public Color TextColor { get; set; } = Color.Black;
        public Color NormalColor { get; set; } = Color.White;
        public Color HighlightedColor { get; set; } = Color.Wheat;
        public Color ClickedColor { get; set; } = Color.Orange;

        public event EventHandler Clicked;

        public Button(Texture2D texture, Point position, Point size)
            : base(new Rectangle(position, size))
        {
            this.texture = texture;
        }

        public override void HandleInput()
        {
            base.HandleInput();
            if (IsClicked)
            {
                Clicked?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            var color = NormalColor;

            if (IsHighlighted)
                color = HighlightedColor;
            if (IsClicked)
                color = ClickedColor;

            spriteBatch.Draw(texture, Rectangle, color);

            if (!string.IsNullOrEmpty(Text) && Font != null)
            {
                Vector2 textSize = Font.MeasureString(Text);
                Vector2 textPosition = new Vector2(
                    Rectangle.X + (Rectangle.Width - textSize.X) / 2,
                    Rectangle.Y + (Rectangle.Height - textSize.Y) / 2
                );

                spriteBatch.DrawString(Font, Text, textPosition, TextColor);
            }
        }
    }
}
