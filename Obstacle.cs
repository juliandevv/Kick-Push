using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kick__Push
{
    class Obstacle
    {
        private Texture2D _texture;
        private Rectangle _rectangle;
        private Vector2 _speed;
        private Vector2 _location;

        public Obstacle(Texture2D texture, Rectangle rect, Vector2 speed)
		{
            _texture = texture;
            _rectangle = rect;
            _speed = speed;
            _location = new Vector2(rect.X, rect.Y);
        }

        public Texture2D Texture
		{
			get { return _texture; }
		}

    }
}
