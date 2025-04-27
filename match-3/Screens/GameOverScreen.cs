using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Match3Game.Utils;
using match_3;
using System;

namespace Match3Game.Screens
{
    public class GameOverScreen : IScreen
    {
        private SpriteFont font;
        private Texture2D buttonTexture;
        private Game1 game;
        private Button okButton;

        public GameOverScreen(Game1 game)
        {
            this.game = game;
        }

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/DefaultFont");
            buttonTexture = content.Load<Texture2D>("sprites/button");

            okButton = new Button(game, buttonTexture, new Point(300, 300), new Point(buttonTexture.Width, buttonTexture.Height))
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
            ScreenManager.ChangeScreen(new MainMenuScreen(game));
        }

        public void Update(GameTime gameTime)
        {
            okButton.HandleInput();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Game Over", new Vector2(300, 150), Color.Black, 0, Vector2.Zero, 2f, SpriteEffects.None, 0);

            okButton.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
