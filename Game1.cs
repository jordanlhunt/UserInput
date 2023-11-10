using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Keys = Microsoft.Xna.Framework.Input.Keys;
namespace UserInput
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        const int WINDOW_WIDTH = 800;
        const int WINDOW_HEIGHT = 600;
        Texture2D currentCharacter;
        Texture2D character0;
        Texture2D character1;
        Texture2D character2;
        Texture2D character3;
        Texture2D character4;
        Rectangle drawRectangle;
        Random randomNumberGenerator = new Random(Guid.NewGuid().GetHashCode());
        // Click Support
        ButtonState previousMouseButtonState = ButtonState.Released;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            _graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
            IsMouseVisible = true;
        }
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            base.Initialize();
        }
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            character0 = Content.Load<Texture2D>(@"Graphics\sarabot");
            character1 = Content.Load<Texture2D>(@"Graphics\sarabotBlue");
            character2 = Content.Load<Texture2D>(@"Graphics\sarabotGreen");
            character3 = Content.Load<Texture2D>(@"Graphics\sarabotRed");
            character4 = Content.Load<Texture2D>(@"Graphics\sarabotNegative");
            // Start currentCharacter at the center of the screen
            currentCharacter = character0;
            drawRectangle = new Rectangle(WINDOW_WIDTH / 2 - currentCharacter.Width / 2, WINDOW_HEIGHT / 2 - currentCharacter.Height / 2, currentCharacter.Width, currentCharacter.Height);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // Left Thumbstick
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            MouseState mouseState = Mouse.GetState();
            drawRectangle.X = mouseState.X - currentCharacter.Width / 2;
            drawRectangle.Y = mouseState.Y - currentCharacter.Height / 2;
            if (drawRectangle.Left < 0)
            {
                drawRectangle.X = 0;
            }
            if (drawRectangle.Right > WINDOW_WIDTH)
            {
                drawRectangle.X = WINDOW_WIDTH - drawRectangle.Width;
            }
            if (drawRectangle.Top < 0)
            {
                drawRectangle.Y = 0;
            }
            if (drawRectangle.Bottom > WINDOW_HEIGHT)
            {
                drawRectangle.Y = WINDOW_HEIGHT - drawRectangle.Height;
            }
            HandleMouseInput(mouseState);
            previousMouseButtonState = mouseState.LeftButton;
            base.Update(gameTime);
        }
        private void HandleMouseInput(MouseState mouseState)
        {
            int newCharacter = randomNumberGenerator.Next(4) + 1;
            if (mouseState.LeftButton == ButtonState.Released && previousMouseButtonState == ButtonState.Pressed)
            {
                if (newCharacter == 0)
                {
                    currentCharacter = character0;
                }
                else if (newCharacter == 1)
                {
                    currentCharacter = character1;
                }
                else if (newCharacter == 2)
                {
                    currentCharacter = character2;
                }
                else if (newCharacter == 3)
                {
                    currentCharacter = character3;
                }
                else if (newCharacter == 4)
                {
                    currentCharacter = character4;
                }
            }
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();
            _spriteBatch.Draw(currentCharacter, drawRectangle, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}