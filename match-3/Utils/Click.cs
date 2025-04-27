using match_3;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Match3Game
{
    public class Clickable : DrawableGameComponent
    {
        protected Rectangle Rectangle { get; set; }

        public Point RectanglePosition
        {
            get { return new Point(Rectangle.X, Rectangle.Y); }
            set { Rectangle = new Rectangle(value, Rectangle.Size); }
        }

        protected bool IsHighlighted { get; private set; }
        public bool IsClicked { get; private set; }

        private ButtonState oldClickState = Mouse.GetState().LeftButton;

        protected new Game1 Game => (Game1)base.Game;

        protected Clickable(Game game, Rectangle targetRectangle) : base(game)
        {
            Rectangle = targetRectangle;
        }

        protected Clickable(Game game) : base(game)
        {
        }

        public virtual void HandleInput()
        {
            var mouseState = Mouse.GetState();
            Point mousePosition = new Point(mouseState.X, mouseState.Y);

            IsHighlighted = Rectangle.Contains(mousePosition);

            IsClicked = IsHighlighted && mouseState.LeftButton == ButtonState.Pressed && oldClickState == ButtonState.Released;

            oldClickState = mouseState.LeftButton;
        }
    }
}
