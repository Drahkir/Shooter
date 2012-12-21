using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Engine;
using Engine.Input;

namespace Shooter
{
    class StartMenuState : IGameObject
    {
        Renderer _renderer = new Renderer();
        Text _title;
        Engine.Font _generalFont;
        Input _input;
        VerticalMenu _menu;
        StateSystem _system;

        public StartMenuState(Engine.Font titleFont, Engine.Font generalFont, Input input, StateSystem system)
        {
            _input = input;
            _system = system;
            _generalFont = generalFont;
            InitializeMenu();
            _title = new Text("Another Castle", titleFont);
            _title.SetColor(new Color(0, 0, 0, 1));

            //Center on the x and place somewhere near the top
            _title.SetPosition(-_title.Width / 2, 300);
        }

        private void InitializeMenu()
        {
            _menu = new VerticalMenu(0, 150, _input);
            Button startGame = new Button(delegate(object o, EventArgs e) { _system.ChangeState("inner_game"); }, new Text("Start", _generalFont));
            Button gameSettings = new Button(delegate(object o, EventArgs e) { /*Go to settings */ }, new Text("Settings", _generalFont));
            Button exitGame = new Button(delegate(object o, EventArgs e) { /*Quit*/ System.Windows.Forms.Application.Exit(); }, new Text("Exit", _generalFont));

            _menu.AddButton(startGame);
            _menu.AddButton(gameSettings);
            _menu.AddButton(exitGame);
        }

        public void Update(double elapsedTime) {
            _menu.HandleInput();
        }

        public void Render()
        {
            Gl.glClearColor(1, 1, 1, 0);

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            _renderer.DrawText(_title);
            _menu.Render(_renderer);
            _renderer.Render();
        }
    }
}