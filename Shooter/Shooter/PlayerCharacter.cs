using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Shooter
{
    public class PlayerCharacter : Entity
    {
        double _speed = 512; // pixels per second
        double _scale = 0.5;
        bool _dead = false;

        public PlayerCharacter(TextureManager textureManager)
        {
            _sprite.Texture = textureManager.Get("player_spaceship");
            _sprite.SetScale(_scale, _scale); 
        }

        public void Render(Renderer renderer) {
            renderer.DrawSprite(_sprite);
            Render_Debug();
        }

        public bool IsDead
        {
            get
            {
                return _dead;
            }
        }

        internal void OnCollision(Enemy enemy)
        {
            _dead = true;
        }

        public void Move(Vector amount)
        {
            amount *= _speed;
            _sprite.SetPosition(_sprite.GetPosition() + amount);
        }
    }
}
