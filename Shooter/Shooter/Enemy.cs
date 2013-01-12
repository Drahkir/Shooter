using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Tao.OpenGl;
using System.Drawing;

namespace Shooter
{
    class Enemy
    {
        Sprite _spaceship = new Sprite();
        double _scale = 0.3;

        public Enemy(TextureManager textureManager)
        {
            _spaceship.Texture = textureManager.Get("enemy_ship");
            _spaceship.SetScale(_scale, _scale);
            _spaceship.SetRotation(Math.PI);
            _spaceship.SetPosition(200, 0);
        }

        public void Update(double elapsedTime) { }

        public void Render(Renderer renderer)
        {
            renderer.DrawSprite(_spaceship);
        }

        public RectangleF GetBoundingBox()
        {
            float width = (float)(_spaceship.Texture.Width * _scale);
            float height = (float)(_spaceship.Texture.Height * _scale);
            return new RectangleF((float)_spaceship.GetPosition().X - width / 2, (float)_spaceship.GetPosition().Y - height / 2, width, height);
        }
    }
}
