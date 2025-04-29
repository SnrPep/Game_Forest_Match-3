using Match3Game.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Match3Game.Utils
{
    public static class ScreenManager
    {
        private static IScreen currentScreen;
        private static GraphicsDevice graphicsDevice;
        private static ContentManager contentManager;

        private static Dictionary<string, IScreen> screens = new Dictionary<string, IScreen>();

        public static void Init(GraphicsDevice gd, ContentManager content)
        {
            graphicsDevice = gd;
            contentManager = content;

            Register("MainMenu", new MainMenuScreen());
            Register("Game", new GameScreen());
            Register("GameOver", new GameOverScreen());

            Show("MainMenu");
        }

        private static void Register(string name, IScreen screen)
        {
            screens[name] = screen;
            screen.LoadContent(graphicsDevice, contentManager);
        }

        public static void Show(string name)
        {
            if (screens.ContainsKey(name))
            {
                currentScreen = screens[name];
            }
        }

        public static void Reset(string name, IScreen newInstance)
        {
            if (screens.ContainsKey(name))
                screens.Remove(name);

            Register(name, newInstance);
            Show(name);
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
