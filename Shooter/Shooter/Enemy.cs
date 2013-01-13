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
        public int Health { get; set; }
        double _scale = 0.3;
        static readonly double HitFlashTime = 0.25;
        double _hitFlashCountDown = 0;
        EffectsManager _effectsManager;
        public Path Path { get; set; }
        public double MaxTimeToShoot { get; set; }
        public double MinTimeToShoot { get; set; }
        Random _random = new Random();
        double _shootCountDown;
        BulletManager _bulletManager;
        Texture _bulletTexture;

        public void RestartShootCountDown()
        {
            _shootCountDown = MinTimeToShoot + (_random.NextDouble() * MaxTimeToShoot);
        }

        public Enemy(TextureManager textureManager, EffectsManager effectsManager, BulletManager bulletManager)
        {
            Health = 50;
            _sprite.Texture = textureManager.Get("enemy_ship");
            _sprite.SetScale(_scale, _scale);
            _sprite.SetRotation(Math.PI);
            _sprite.SetPosition(200, 0);
            _effectsManager = effectsManager;
            _bulletManager = bulletManager;
            _bulletTexture = textureManager.Get("bullet");
            MaxTimeToShoot = 12;
            MinTimeToShoot = 1;
            RestartShootCountDown();
        }

        internal void OnCollision(PlayerCharacter playerCharacter)
        {
            // Handle Collision with player.
        }

        internal void OnCollision(Bullet bullet)
        {
            if (Health == 0)
            {
                return;
            }

            Health = Math.Max(0, Health - 25);
            _hitFlashCountDown = HitFlashTime;
            _sprite.SetColor(new Engine.Color(1, 1, 0, 1));

            if(Health == 0) {
                OnDestroyed();
            }
        }

        private void OnDestroyed() {
            _effectsManager.AddExplosion(_sprite.GetPosition());
        }

        public bool IsDead
        {
            get { return Health == 0; }
        }

        internal void SetPosition(Vector position)
        {
            _sprite.SetPosition(position);
        }

        public void Update(double elapsedTime) {
            _shootCountDown = _shootCountDown - elapsedTime;

            if (_shootCountDown <= 0)
            {
                Bullet bullet = new Bullet(_bulletTexture);
                bullet.Speed = 350;
                bullet.Direction = new Vector(-1, 0, 0);
                bullet.SetPosition(_sprite.GetPosition());
                bullet.SetColor(new Engine.Color(1, 0, 0, 1));
                _bulletManager.EnemyShoot(bullet);
                RestartShootCountDown();
            }

            if (Path != null)
            {
                Path.UpdatePosition(elapsedTime, this);
            }
            if(_hitFlashCountDown != 0) {
                _hitFlashCountDown = Math.Max(0, _hitFlashCountDown - elapsedTime);
                double scaledTime = 1 - (_hitFlashCountDown / HitFlashTime);
                _sprite.SetColor(new Engine.Color(1, 1, (float)scaledTime, 1));
            }
        }

        public void Render(Renderer renderer)
        {
            renderer.DrawSprite(_sprite);
            //Render_Debug();
        }
    }
}
