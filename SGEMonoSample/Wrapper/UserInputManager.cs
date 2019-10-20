using System;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace ScapeInternal
{
    static class InputManager
    {
        [MethodImplAttribute(MethodImplOptions.InternalCall)]
        extern public static bool isPressed(int key);
    }
}

namespace Scape
{
    class UserInputMapping
    {
        public UserInputMapping(Dictionary<string, float> axes, Dictionary<Keys, Dictionary<string, float>> weights)
        {
            AxesCurrentValues = AxesDefault = axes;
            KeysPerAxisValues = weights;
        }

        public void Update()
        {
            //Update the axes' values by summing up all currently pressed keys'
            //weights and applying them to AxesCurrentValues
            AxesCurrentValues = AxesDefault;

            foreach (KeyValuePair<Keys, Dictionary<string, float>> keyWeights in KeysPerAxisValues)
            {
                if (ScapeInternal.InputManager.isPressed((int)keyWeights.Key))
                {
                    foreach (KeyValuePair<string, float> weight in keyWeights.Value)
                    {
                        //Apply the weights here
                        AxesCurrentValues[weight.Key] += weight.Value;
                    }
                }
            }
        }

        private Dictionary<string, float> AxesDefault;
        public Dictionary<string, float> AxesCurrentValues { get; private set; }
        private Dictionary<Keys, Dictionary<string, float>> KeysPerAxisValues;
    }

    static class UserInputManager
    {
        static UserInputManager()
        {
            //Setup basic WASD controls
            var initialAxes = new Dictionary<string, float> {
                { "X", 0 },
                { "Z", 0 }
            };

            var weights = new Dictionary<Keys, Dictionary<string, float>>()
            {
                { Keys.KEY_D, new Dictionary<string, float> { { "X", -1f } } },
                { Keys.KEY_A, new Dictionary<string, float> { { "X", 1f } } },
                { Keys.KEY_W, new Dictionary<string, float> { { "Z", -1f } } },
                { Keys.KEY_S, new Dictionary<string, float> { { "Z", 1f } } },
            };

            mapping = new UserInputMapping(initialAxes, weights);
        }

        public static void SetMapping(UserInputMapping mp)
        {
            mapping = mp;
        }

        public static void UpdateInputs()
        {
            mapping.Update();
        }

        public static float GetAxisValue(string axis)
        {
            return mapping.AxesCurrentValues[axis];
        }

        public static bool IsPressed(Scape.Keys key)
        {
            return ScapeInternal.InputManager.isPressed((int)key);
        }

        private static UserInputMapping mapping;
    }
}