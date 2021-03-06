﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace AlmostFlappyBird
{
    public class Background
    {
        public Vector2 Location;
        public Vector2 Velocity;
        private Texture2D image;
        public Rectangle Bounds;

        public Background(Vector2 location, Texture2D image)
        {
            this.Location = location;
            this.image = image;

            this.Bounds = new Rectangle(0, 0, 1920, 1080);
        }
        public void Update(float elapsed)
        {
            this.Location += this.Velocity * elapsed;
            this.Bounds.X = (int)Location.X;
            this.Bounds.Y = (int)Location.Y;

            if (this.Bounds.Right < 0)
            {
                this.Location.X = 1900;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(this.image, this.Location); // opet isto location umisto this.location
        }
    }
}
