using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace Pastry_Presser
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private static List<GameObject> gameObjects;
        private static List<UI> uiObjects;

        public static int Height { get; private set; }
        public static int Width { get; private set; }

        private static Vector2 screenSize;

        public static Vector2 ScreenSize { get => screenSize; }

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics.PreferredBackBufferWidth = 1900;
            _graphics.PreferredBackBufferHeight = 900;

            screenSize = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
        }

        protected override void Initialize()
        {
            GameWorld.Height = _graphics.PreferredBackBufferHeight;
            GameWorld.Width = _graphics.PreferredBackBufferWidth;
            gameObjects = new List<GameObject>();
            uiObjects = new List<UI>();

            gameObjects.Add(new Grandma(Content.Load<Texture2D>("button"), new Vector2(1720, 50), Content.Load<SpriteFont>("File")));
            gameObjects.Add(new Oven(Content.Load<Texture2D>("button"), new Vector2(1720, 320), Content.Load<SpriteFont>("File")));
            gameObjects.Add(new Factory(Content.Load<Texture2D>("button"), new Vector2(1720, 620), Content.Load<SpriteFont>("File")));
            for (int i = 0; i < 150; i++)
            {
                gameObjects.Add(new FallingCookie(Content.Load<Texture2D>("Cookie")));
            }
            gameObjects.Add(new Cookie(Content.Load<Texture2D>("Cookie"), new Vector2(-50, 150), Content.Load<SpriteFont>("File")));
            uiObjects.Add(new Vertical(Content.Load<Texture2D>("wood"), new Vector2(1220, 300)));
            uiObjects.Add(new Vertical(Content.Load<Texture2D>("wood"), new Vector2(1220, 600)));
            uiObjects.Add(new Horizontal(Content.Load<Texture2D>("wood"), new Vector2(1700, 450)));
            uiObjects.Add(new Horizontal(Content.Load<Texture2D>("wood"), new Vector2(740, 450)));
            uiObjects.Add(new Achievement(Content.Load<Texture2D>("Dialog Box"), new Vector2(GameWorld.Width / 2, 800)));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(Content);
            }

            foreach (UI ui in uiObjects)
            {
                ui.LoadContent(Content);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
            }

            foreach (UI achievement in uiObjects)
            {
                achievement.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SaddleBrown);

            _spriteBatch.Begin();

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(_spriteBatch);
            }

            foreach (UI ui in uiObjects)
            {
                ui.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
