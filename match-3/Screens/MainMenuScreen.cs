using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Match3Game.Utils;

namespace Match3Game.Screens
{
    public class MainMenuScreen : IScreen
    {
        private SpriteFont font;
        private Texture2D buttonTexture;
        private Rectangle playButtonRect;
        private MouseState oldMouseState;

        public MainMenuScreen()
        {
        }

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            buttonTexture = new Texture2D(graphicsDevice, 1, 1);
            buttonTexture.SetData(new[] { Color.White });

            playButtonRect = new Rectangle(300, 300, 200, 60);
        }

        public void Update(GameTime gameTime)
        {
            MouseState mouseState = Mouse.GetState();

            if (mouseState.LeftButton == ButtonState.Pressed &&
                oldMouseState.LeftButton == ButtonState.Released)
            {
                Point mousePosition = mouseState.Position;
                if (playButtonRect.Contains(mousePosition))
                {
                    ScreenManager.ChangeScreen(new GameScreen());
                }
            }

            oldMouseState = mouseState;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(buttonTexture, playButtonRect, Color.LightBlue);

            spriteBatch.End();
        }
    }
}
