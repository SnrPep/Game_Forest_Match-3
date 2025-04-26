using Match3Game.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Match3Game.Utils
{
    public static class ScreenManager
    {
        private static IScreen currentScreen;
        private static GraphicsDevice graphicsDevice;
        private static ContentManager contentManager;

        public static void Init(GraphicsDevice gd, ContentManager content)
        {
            graphicsDevice = gd;
            contentManager = content;
        }

        public static void ChangeScreen(IScreen newScreen)
        {
            currentScreen = newScreen;
            currentScreen.LoadContent(graphicsDevice, contentManager);
        }

        public static void Update(GameTime gameTime)
        {
            currentScreen?.Update(gameTime);
        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            currentScreen?.Draw(spriteBatch);
        }
    }
}
