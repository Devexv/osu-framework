// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using System;
using System.Linq;
using osu.Framework.Input;
using SDL2;

namespace osu.Framework.Platform.Sdl
{
    public class Sdl2ControllerState
    {
        public readonly int InstanceID;
        public readonly IntPtr JoystickHandle;
        public readonly IntPtr ControllerHandle;

        /// <summary>
        /// Bindings returned from <see cref="SDL.SDL_GameControllerGetBindForButton"/>, indexed by <see cref="SDL.SDL_GameControllerButton"/>.
        /// Empty if the joystick does not have a corresponding ControllerHandle.
        /// </summary>
        public SDL.SDL_GameControllerButtonBind[] ButtonBindings;

        /// <summary>
        /// Bindings returned from <see cref="SDL.SDL_GameControllerGetBindForAxis"/>, indexed by <see cref="SDL.SDL_GameControllerAxis"/>.
        /// Empty if the joystick does not have a corresponding ControllerHandle.
        /// </summary>
        public SDL.SDL_GameControllerButtonBind[] AxisBindings;

        public JoystickButton[] AxisDirectionButtons;

        public Sdl2ControllerState(int instanceID, IntPtr joystickHandle, IntPtr controllerHandle)
        {
            InstanceID = instanceID;
            JoystickHandle = joystickHandle;
            ControllerHandle = controllerHandle;
            AxisDirectionButtons = new JoystickButton[(int)JoystickAxisSource.AxisCount];

            PopulateBindings();
        }

        public void PopulateBindings()
        {
            if (ControllerHandle == IntPtr.Zero)
                return;

            ButtonBindings = Enumerable.Range(0, (int)SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_MAX)
                                       .Select(i => SDL.SDL_GameControllerGetBindForButton(ControllerHandle, (SDL.SDL_GameControllerButton)i)).ToArray();

            AxisBindings = Enumerable.Range(0, (int)SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_MAX)
                                     .Select(i => SDL.SDL_GameControllerGetBindForAxis(ControllerHandle, (SDL.SDL_GameControllerAxis)i)).ToArray();
        }

        public SDL.SDL_GameControllerButton GetButtonForIndex(byte index)
        {
            for (var i = 0; i < ButtonBindings.Length; i++)
            {
                if (ButtonBindings[i].bindType != SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE && ButtonBindings[i].value.button == index)
                    return (SDL.SDL_GameControllerButton)i;
            }

            return SDL.SDL_GameControllerButton.SDL_CONTROLLER_BUTTON_INVALID;
        }

        public SDL.SDL_GameControllerAxis GetAxisForIndex(byte index)
        {
            for (var i = 0; i < AxisBindings.Length; i++)
            {
                if (AxisBindings[i].bindType != SDL.SDL_GameControllerBindType.SDL_CONTROLLER_BINDTYPE_NONE && AxisBindings[i].value.button == index)
                    return (SDL.SDL_GameControllerAxis)i;
            }

            return SDL.SDL_GameControllerAxis.SDL_CONTROLLER_AXIS_INVALID;
        }
    }
}
