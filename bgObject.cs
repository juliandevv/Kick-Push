using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SKATE_GAME_tests3___Background
{
    class bgObject
    {
        private Texture2D _texture;
        private Rectangle _rectangle;
        private Vector2 _speed;
        private Rectangle _source;

        public bgObject(Texture2D texture, Rectangle rect, Rectangle sourceRect, Vector2 speed)
        {
            _texture = texture;
            _rectangle = rect;
            _speed = speed;
            _source = sourceRect;
        }

        public Texture2D Texture
        {
            get { return _texture; }
        }

        public Rectangle Bounds
        {
            get { return _rectangle; }
            set { _rectangle = value; }
        }

        public Rectangle Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public Vector2 Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }


        public void move()
        {
            _rectangle.Offset(_speed);
        }        
        public void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(this.Texture, this.Bounds, Color.White);
        }
    }
}
