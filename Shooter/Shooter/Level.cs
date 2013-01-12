﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Input;
using System.Windows.Forms;
using System.Drawing;
using Tao.OpenGl;

namespace Shooter
{
    class Level
    {
        Input _input;
        PersistentGameData _gameData;
        PlayerCharacter _playerCharacter;
        TextureManager _textureManager;
        ScrollingBackground _background;
        ScrollingBackground _backgroundLayer;
        List<Enemy> _enemyList = new List<Enemy>();
        BulletManager _bulletManager = new BulletManager(new RectangleF(-1300 / 2, -750 / 2, 1300, 750));

        public Level(Input input, TextureManager textureManager, PersistentGameData gameData)
        {
            _input = input;
            _gameData = gameData;
            _textureManager = textureManager;
            _playerCharacter = new PlayerCharacter(_textureManager, _bulletManager);
            _enemyList.Add(new Enemy(_textureManager));

            _background = new ScrollingBackground(textureManager.Get("background"));
            _background.SetScale(2, 2);
            _background.Speed = 0.15f;

            _backgroundLayer = new ScrollingBackground(textureManager.Get("background_layer_1"));
            _backgroundLayer.Speed = 0.1f;
            _backgroundLayer.SetScale(2.0, 2.0);
        }

        public bool HasPlayerDied()
        {
            return _playerCharacter.IsDead;
        }

        private void UpdateCollisions()
        {
            foreach (Enemy enemy in _enemyList)
            {
                if (enemy.GetBoundingBox().IntersectsWith(_playerCharacter.GetBoundingBox()))
                {
                    enemy.OnCollision(_playerCharacter);
                    _playerCharacter.OnCollision(enemy);
                }
            }
        }
        public void Update(double elapsedTime)
        {
            _playerCharacter.Update(elapsedTime);

            _background.Update((float)elapsedTime);
            _backgroundLayer.Update((float)elapsedTime);

            UpdateCollisions();
            _enemyList.ForEach(x => x.Update(elapsedTime));
            _bulletManager.Update(elapsedTime);

            UpdateInput(elapsedTime);
        }

        private void UpdateInput(double elapsedTime) {

            if(_input.Keyboard.IsKeyPressed(Keys.Space) || _input.Controller.ButtonA.Pressed) {
                _playerCharacter.Fire();
            }            
            
            // Get controls and apply to player character
            double _x = _input.Controller.LeftControlStick.X;
            double _y = _input.Controller.LeftControlStick.Y * -1;
            Vector controlInput = new Vector(_x, _y, 0);

            if (Math.Abs(controlInput.Length()) < 0.0001)
            {
                // If the input is very small, then the player may not be using
                // a controller; he might be using the keyboard.
                if (_input.Keyboard.IsKeyHeld(Keys.Left))
                {
                    controlInput.X = -1;
                }

                if (_input.Keyboard.IsKeyHeld(Keys.Right))
                {
                    controlInput.X = 1;
                }

                if (_input.Keyboard.IsKeyHeld(Keys.Up))
                {
                    controlInput.Y = 1;
                }

                if (_input.Keyboard.IsKeyHeld(Keys.Down))
                {
                    controlInput.Y = -1;
                }

                _playerCharacter.Move(controlInput * elapsedTime);
            }
        }

        public void Render(Renderer renderer)
        {
            _background.Render(renderer);
            _backgroundLayer.Render(renderer);

            _enemyList.ForEach(x => x.Render(renderer));
            _playerCharacter.Render(renderer);
            _bulletManager.Render(renderer);
        }
    }
}