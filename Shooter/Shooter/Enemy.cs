using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Tao.OpenGl;
using System.Drawing;

namespace Shooter
{
    public class Enemy : Entity
    {
        double _scale = 0.3;

        public Enemy(TextureManager textureManager)
        {
            _sprite.Texture = textureManager.Get("enemy_ship");
            _sprite.SetScale(_scale, _scale);
            _sprite.SetRotation(Math.PI);
            _sprite.SetPosition(200, 0);
        }

        public void Update(double elapsedTime) { }

        public void Render(Renderer renderer)
        {
            renderer.DrawSprite(_sprite);
            //Render_Debug();
        }

        internal void OnCollision(Entity entity)
        {
            // Handle Collision with player.
        }
    }
}
