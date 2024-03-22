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
    public abstract class GameObject
    {
        protected Texture2D sprite;
        protected Texture2D texture;
        protected Vector2 position;
        protected Vector2 first;
        protected Vector2 origin;
        protected Vector2 currentPos = Vector2.Zero;
        protected Vector2 maxPos = new Vector2(1650, 20);
        protected int counter;
        protected int amount;
        protected int cost;
        protected static int cookiesProducedPerSecond;
        protected static float deltaTime;
        protected float timeElapsed;
        protected bool draw;
        protected Rectangle rectangle;
        protected Color colorCode = Color.White;
        protected SpriteFont font;
        protected List<Vector2> positions;
        protected MouseState lastMouseState;
        protected float scale;
        protected Vector2 fontPos;

        public virtual void LoadContent(ContentManager content)
        {
            origin = new Vector2(texture.Width / 2, texture.Height / 2);
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
