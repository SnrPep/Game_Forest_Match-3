using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Match3Game.Utils;
using System;

namespace Match3Game.Screens
{
    public class MainMenuScreen : IScreen
    {
        private SpriteFont font;
        private Button playButton;
        private Texture2D playButtonTexture;

        public MainMenuScreen()
        {
        }

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            font = content.Load<SpriteFont>("Fonts/DefaultFont");
            playButtonTexture = content.Load<Texture2D>("sprites/button");

            playButton = new Button(playButtonTexture, new Point(300, 300), new Point(playButtonTexture.Width, playButtonTexture.Height))
            {
                Font = font,
                Text = "Play",
                NormalColor = Color.White,
                HighlightedColor = Color.LightGray,
                ClickedColor = Color.Gray,
                TextColor = Color.Black
            };

            playButton.Clicked += PlayButton_Clicked;
        }


        private void PlayButton_Clicked(object sender, EventArgs e)
        {
            ScreenManager.ChangeScreen(new GameScreen());
        }


        public void Update(GameTime gameTime)
        {
            playButton.HandleInput();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.DrawString(font, "Match-3", new Vector2(300, 150), Color.Black, 0, Vector2.Zero, 2f, SpriteEffects.None, 0);

            playButton.Draw(spriteBatch);

            spriteBatch.End();
        }
    }
}
