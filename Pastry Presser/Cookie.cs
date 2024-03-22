using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pastry_Presser
{
    internal class Cookie : GameObject
    {
        public static int cookieCounter; //COOKIESPRODUCED / 10 HVER TIENDEDEL AF ET SEKUND
        public static int timesClicked;
        private float rotationSpeed;
        private float cookieEffect;
        private bool drawEffect;
        private Vector2 effectPos;
        private float opacity;

        public Cookie(Texture2D texture, Vector2 position, SpriteFont font)
        {
            this.texture = texture;
            this.position = position;
            rectangle = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
            this.font = font;
        }

        public override void LoadContent(ContentManager content)
        {
            positions = new List<Vector2>();
            scale = 1f;
            cookieEffect = 0.1f;
            effectPos = new Vector2(440, 200);
            drawEffect = false;
            opacity = 1f;
        }

        public override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            MouseState mouseState = Mouse.GetState();
            Rectangle cursor = new Rectangle(mouseState.Position.X, mouseState.Position.Y, 1, 1);

            scale = 1f;
            effectPos.Y--;

            if (cursor.Intersects(rectangle))
            {
                colorCode = Color.LightGray;

                if (mouseState.LeftButton == ButtonState.Pressed && lastMouseState.LeftButton == ButtonState.Released)
                {
                    ClickCookie();
                }
            }
            else
            {
                colorCode = Color.White;
            }

            timeElapsed += deltaTime;


            lastMouseState = mouseState;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, null, Color.White, 0, origin, scale, SpriteEffects.None, 0f);
            spriteBatch.DrawString(font, "Cookies: " + cookieCounter, new Vector2(10, 10), Color.White);
            spriteBatch.DrawString(font, "Cookies produced per second: " + cookiesProducedPerSecond, new Vector2(10, 30), Color.White);

            if (drawEffect == true)
            {
                spriteBatch.Draw(texture, effectPos, null, Color.White, 0, origin, cookieEffect, SpriteEffects.None, 1f);
                spriteBatch.DrawString(font, "+1", effectPos + new Vector2(50, 0), Color.White);
                if (timeElapsed > 1f)
                {
                    drawEffect = false;
                    effectPos.Y = 200;
                    timeElapsed = 0;
                }
            }
        }

        public void ClickCookie() 
        {
            cookieCounter++;
            timesClicked++;
            scale = 0.97f;
            drawEffect = true;
        }
    }
}
