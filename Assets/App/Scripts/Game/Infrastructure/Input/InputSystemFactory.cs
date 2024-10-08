﻿using UnityEngine;

namespace App.Scripts.Game.Infrastructure.Input {
    public class InputSystemFactory : IInputSystemFactory {
        private IInputSystem _inputSystem;

        public IInputSystem GetInput()
        {
            if (_inputSystem == null)
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                    {
                        _inputSystem = new TouchInputSystem();
                        break;
                    }
                    default:
                    {
                        _inputSystem = new MouseInputSystem();
                        break;
                    }
                }
            }
            
            return _inputSystem;
        }
    }
}