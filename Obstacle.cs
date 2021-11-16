﻿using Microsoft.Xna.Framework;
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

        public Rectangle Bounds
        {
            get { return _rectangle; }
            set
            {
                _rectangle = value;
                _location = new Vector2(value.X, value.Y);
            }
        }

        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(this.Texture, this.Bounds, Color.White);
        }

        public void Move()
        {

            _location.X += _speed.X;
            _rectangle.Location = new Point(_location.ToPoint().X, _location.ToPoint().Y);

        }

    }
}
