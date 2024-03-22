using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pastry_Presser
{
    internal class Achievement : UI
    {
        private SpriteFont achievementFont;
        private Texture2D star;
        private float deltaTime;
        private float timeElapsed;
        private bool draw;

        public Achievement(Texture2D texture, Vector2 position)
        {
            this.texture = texture;
            this.position = position;
        }

        public override void LoadContent(ContentManager content)
        {
            achievementFont = content.Load<SpriteFont>("File");
        }

        public override void Update(GameTime gameTime)
        {
            deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            timeElapsed += deltaTime;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            if (Cookie.timesClicked == 1)
            {
                spriteBatch.Draw(texture, new Vector2(150, 800), Color.White);
                spriteBatch.DrawString(achievementFont, "Click the cookie for the first time", new Vector2(200, 850), Color.White);
            }

            if (Cookie.timesClicked == 100)
            {
                spriteBatch.Draw(texture, new Vector2(150, 800), Color.White);
                spriteBatch.DrawString(achievementFont, "Click the cookie 100 times", new Vector2(200, 850), Color.White);
            }
        }
    }
}
