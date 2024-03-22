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
    internal class FallingCookie : GameObject
    {
        private Random rnd;
        public FallingCookie(Texture2D texture)
        {
            this.texture = texture;
        }

        public override void LoadContent(ContentManager content)
        {
            rnd = new Random();
            base.LoadContent(content);
            position = new Vector2(rnd.Next(0, 700), rnd.Next(0,GameWorld.Height));
        }

        public override void Update(GameTime gameTime)
        {
            position.Y++;
            if (position.Y > GameWorld.ScreenSize.Y)
            {
                position.Y = 0;
                position.X = rnd.Next(1, 700);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < 10; i++)
            {
                spriteBatch.Draw(texture, position, null, Color.White * 0.1f, 0, origin, 0.1f, SpriteEffects.None, 0.1f);
            }
        }
    }
}
