using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pastry_Presser
{
    internal class Oven : GameObject
    {
        public Oven(Texture2D texture, Vector2 position, SpriteFont font)
        {
            this.texture = texture;
            this.position = position;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.font = font;
        }

        public override void LoadContent(ContentManager content)
        {
            positions = new List<Vector2>();
            first = new Vector2(700, 330);
            sprite = content.Load<Texture2D>("Horno");
            scale = 0.5f;
            fontPos = new Vector2(1750, 345);
            cost = 25;
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
                    AddOven();
                }
            }
            else
            {
                colorCode = Color.White;
            }

            timeElapsed += deltaTime;

            if (timeElapsed > 2f && amount > 0)
            {
                BakeBatchOfCookies();
            }

            lastMouseState = mouseState;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, colorCode, 0, new Vector2(0, 0), scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, "Add Oven", fontPos, Color.Black);
            spriteBatch.DrawString(font, "Ovens: " + amount, position + new Vector2(0, 100), Color.White);

            if (draw == true)
            {
                for (int i = 0; i < positions.Count; i++)
                {
                    spriteBatch.Draw(sprite, positions[i], Color.White);
                }
            }
        }

        public void AddOven()
        {
            Cookie.cookieCounter -= cost;

            currentPos += new Vector2(60, 0);

            cookiesProducedPerSecond += 10;

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

        public void BakeBatchOfCookies()
        {
            if (timeElapsed > 2f)
            {
                Cookie.cookieCounter += amount * 10;
                timeElapsed = 0;
            }
        }
    }
}
