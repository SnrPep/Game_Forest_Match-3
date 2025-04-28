using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Match3Game.Utils;
using System;
using Match3Game.Grid;

namespace Match3Game.Screens
{
    public class GameOverScreen : IScreen
    {
        private SpriteFont font;
        private Texture2D buttonTexture;
        private Button okButton;
        private int score;

        public GameOverScreen(int score)
        {
            this.score = score;
        }

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/DefaultFont");
            buttonTexture = content.Load<Texture2D>("sprites/button");

            okButton = new Button(buttonTexture, new Point(300, 300), new Point(buttonTexture.Width, buttonTexture.Height))
            {
                Font = font,
                Text = "Ok",
                NormalColor = Color.White,
                HighlightedColor = Color.LightGray,
                ClickedColor = Color.Gray,
                TextColor = Color.Black
            };

            okButton.Clicked += OkButton_Clicked;
        }

        private void OkButton_Clicked(object sender, EventArgs e)
        {
            ScreenManager.ChangeScreen(new MainMenuScreen());
        }

        public void Update(GameTime gameTime)
        {
            okButton.HandleInput();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Game Over", new Vector2(300, 100), Color.Black, 0, Vector2.Zero, 2f, SpriteEffects.None, 0);
            spriteBatch.DrawString(font, $"Final score: {score}", new Vector2(300, 200), Color.Black, 0, Vector2.Zero, 2f, SpriteEffects.None, 0);

            okButton.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
