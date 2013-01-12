using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Shooter
{
    public class PlayerCharacter
    {
        Sprite _spaceship = new Sprite();
        double _speed = 512; // pixels per second
        double _scale = 0.5;

        public PlayerCharacter(TextureManager textureManager)
        {
            _spaceship.Texture = textureManager.Get("player_spaceship");
            _spaceship.SetScale(_scale, _scale); 
        }

        public void Render(Renderer renderer) {
            renderer.DrawSprite(_spaceship);
        }

        public void Move(Vector amount)
        {
            amount *= _speed;
            _spaceship.SetPosition(_spaceship.GetPosition() + amount);
        }
    }
}
