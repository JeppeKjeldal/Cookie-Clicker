using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pastry_Presser
{
    internal class Factory : GameObject
    {
        private Vector2 factoryMax;
        public Factory(Texture2D texture, Vector2 position, SpriteFont font)
        {
            this.texture = texture;
            this.position = position;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.font = font;
        }

        public override void LoadContent(ContentManager content)
        {
            positions = new List<Vector2>();
            first = new Vector2(650, 600);
            scale = 0.5f;
            fontPos = new Vector2(1750, 640);
            sprite = content.Load<Texture2D>("oga-blueroofinn");
            factoryMax = new Vector2(-50, 0);
            cost = 50;
        }

        public override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            MouseState mouseState = Mouse.GetState();
            Rectangle cursor = new Rectangle(mouseState.Position.X, mouseState.Position.Y, 1, 1);

            if (cursor.Intersects(rectangle))
            {
                colorCode = Color.LightGray;
                if (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released && Cookie.cookieCounter >= cost)
                {
                    AddFactory();
                }
            }
            else
            {
                colorCode = Color.White;
            }

            timeElapsed += deltaTime;

            if (timeElapsed > 2f && amount > 0)
            {
                ProduceLoadOfCookies();
            }

            lastMouseState = mouseState;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, colorCode, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, "Add Factory", fontPos, Color.Black);
            spriteBatch.DrawString(font, "Factories: " + amount, position + new Vector2(0, 100), Color.White);


            if (draw == true)
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    spriteBatch.Draw(sprite, positions[i], null, colorCode, 0, origin, scale, SpriteEffects.None, 0f);
                }
            }
        }

        public void AddFactory()
        {
            Cookie.cookieCounter -= cost;

            cookiesProducedPerSecond += 100;

            currentPos += new Vector2(120, 0);

            if (first.X + currentPos.X >= maxPos.X + factoryMax.X)
            {
                first.X = 700;
                first.Y += 20;
                currentPos.X = 60;
            }

            draw = true;
            amount++;
            timeElapsed = 0;
            positions.Add(first + currentPos);
        }

        public void ProduceLoadOfCookies()
        {
            if (timeElapsed > 2f)
            {
                Cookie.cookieCounter += amount * 100;
                timeElapsed = 0;
            }
        }
    }
}
