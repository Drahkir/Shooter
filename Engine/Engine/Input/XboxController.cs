using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.Sdl;

namespace Engine.Input
{
    public class XboxController : IDisposable
    {
        // Analog Sticks
        public ControlStick LeftControlStick { get; private set; }
        public ControlStick RightControlStick { get; private set; }

        // Regular Buttons
        public ControllerButton ButtonA { get; private set; }
        public ControllerButton ButtonB { get; private set; }
        public ControllerButton ButtonX { get; private set; }
        public ControllerButton ButtonY { get; private set; }

        // Front Shoulder Buttons
        public ControllerButton ButtonLB { get; private set; }
        public ControllerButton ButtonRB { get; private set; }

        // Analog Stick Buttons
        public ControllerButton ButtonL3 { get; private set; }
        public ControllerButton ButtonR3 { get; private set; }

        // Option Buttons
        public ControllerButton ButtonBack { get; private set; }
        public ControllerButton ButtonStart { get; private set; }

        // Shoulder Triggers
        public ControlTrigger RightTrigger { get; private set; }
        public ControlTrigger LeftTrigger { get; private set; }

        // Digital Pad
        public DPad Dpad { get; private set; }


        IntPtr _joystick;

        public XboxController(int player)
        {
            _joystick = Sdl.SDL_JoystickOpen(player);

            LeftControlStick = new ControlStick(_joystick, 0, 1);
            RightControlStick = new ControlStick(_joystick, 4, 3);

            ButtonA = new ControllerButton(_joystick, 0);
            ButtonB = new ControllerButton(_joystick, 1);
            ButtonX = new ControllerButton(_joystick, 2);
            ButtonY = new ControllerButton(_joystick, 3);
            ButtonLB = new ControllerButton(_joystick, 4);
            ButtonRB = new ControllerButton(_joystick, 5);
            ButtonBack = new ControllerButton(_joystick, 6);
            ButtonStart = new ControllerButton(_joystick, 7);
            ButtonL3 = new ControllerButton(_joystick, 8);
            ButtonR3 = new ControllerButton(_joystick, 9);
            RightTrigger = new ControlTrigger(_joystick, 2, false);
            LeftTrigger = new ControlTrigger(_joystick, 2, true);
            Dpad = new DPad(_joystick, 0);
        }

        public void Update()
        {
            LeftControlStick.Update();
            RightControlStick.Update();

            ButtonA.Update();
            ButtonB.Update();
            ButtonX.Update();
            ButtonY.Update();
            ButtonLB.Update();
            ButtonRB.Update();
            ButtonL3.Update();
            ButtonR3.Update();
            ButtonBack.Update();
            ButtonStart.Update();
            RightTrigger.Update();
            LeftTrigger.Update();
            Dpad.Update();
        }

        #region IDisposable Members

        public void Dispose()
        {
            Sdl.SDL_JoystickClose(_joystick);
        }

        #endregion IDisposable Members
    }
}
