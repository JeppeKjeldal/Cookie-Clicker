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
    internal class Grandma : GameObject
    {
        private Vector2 grannyScale;
        private Texture2D sprite;

        public Grandma(Texture2D texture, Vector2 position, SpriteFont font)
        {
            this.texture = texture;
            this.position = position;
            this.font = font;
            grannyScale = new Vector2(0.025f, 0.02f);
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public override void LoadContent(ContentManager content)
        {
            positions = new List<Vector2>();
            first = new Vector2(700, 20);
            sprite = content.Load<Texture2D>("granny");
            scale = 0.5f;
            fontPos = new Vector2(1750, 75);
            cost = 10;
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
                    AddGrandma();
                }
            }
            else
            {
                colorCode = Color.White;
            }

            timeElapsed += deltaTime;

            if (timeElapsed > 2f && amount > 0)
            {
                BakeCookie();
            }

            lastMouseState = mouseState;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, colorCode, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, "Add Grandma", fontPos, Color.Black);
            spriteBatch.DrawString(font, "Grandmas: " + amount, position + new Vector2(0, 100), Color.White);

            if (draw == true)
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    spriteBatch.Draw(sprite, positions[i], null, colorCode, 0, origin, grannyScale, SpriteEffects.None, 0f);
                }
            }
        }

        public void AddGrandma()
        {
            Cookie.cookieCounter -= cost;

            cookiesProducedPerSecond++;

            currentPos += new Vector2(60, 0);

            if (first.X + currentPos.X >= maxPos.X)
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

        public void BakeCookie()
        {
            if (timeElapsed > 2f)
            {
                Cookie.cookieCounter += amount;
                timeElapsed = 0;
            }
        }
    }
}
