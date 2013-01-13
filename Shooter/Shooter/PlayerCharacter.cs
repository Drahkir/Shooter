using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace Shooter
{
    public class PlayerCharacter : Entity
    {
        BulletManager _bulletManager;
        Texture _bulletTexture;
        Vector _gunOffset = new Vector(55, 0, 0);
        static readonly double FireRecovery = 0.25;
        double _fireRecoveryTime = FireRecovery;
        double _speed = 512; // pixels per second
        double _scale = 1;
        bool _dead = false;

        public PlayerCharacter(TextureManager textureManager, BulletManager bulletManager)
        {
            _bulletManager = bulletManager;
            _bulletTexture = textureManager.Get("bullet");
            _sprite.Texture = textureManager.Get("player_spaceship");
            _sprite.SetScale(_scale, _scale);
            //_sprite.SetRotation(Math.PI / 2);
        }

        public void Render(Renderer renderer) {
            renderer.DrawSprite(_sprite);
            //Render_Debug();
        }

        public void Fire()
        {
            if (_fireRecoveryTime > 0)
            {
                return;
            }
            else
            {
                _fireRecoveryTime = FireRecovery;
            }

            
            Bullet bullet = new Bullet(_bulletTexture);
            bullet.SetColor(new Color(0, 1, 0, 1));
            bullet.SetPosition(_sprite.GetPosition() + _gunOffset);
            _bulletManager.Shoot(bullet);
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

        internal void OnCollision(Bullet bullet)
        {
            _dead = true;
        }

        public void Move(Vector amount)
        {
            amount *= _speed;
            _sprite.SetPosition(_sprite.GetPosition() + amount);
        }

        public void Update(double elapsedTime)
        {
            _fireRecoveryTime = Math.Max(0, (_fireRecoveryTime - elapsedTime));
        }
    }
}
