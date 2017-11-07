using System;
using System.Collections.Generic;

namespace PiLighting
{
    enum LightStates
    {
        Off,
        WhiteOn,
        BlackOn,
        AllOn,
        Undefined
    }

    class LightControl
    {
        public List<PinControl> WhiteLights = new List<PinControl>();
        public List<PinControl> BlackLights = new List<PinControl>();
        public LightStates LightState = LightStates.Off;

        public LightControl()
        {
            if (Config.lightConfigList != null)
            {
                foreach (var light in Config.lightConfigList)
                {
                    Console.WriteLine("Found a {0} light: {1}({2})", light.Type.ToString(), light.Name, light.Pin);
                    if (light.Type == lightType.black)
                    {
                        BlackLights.Add(new PinControl(light.Pin, true, light.Name));
                    }
                    else if (light.Type == lightType.white)
                    {
                        WhiteLights.Add(new PinControl(light.Pin, true, light.Name));
                    }
                }
            }

            /*
            if (WhiteLights.Count == 0)
            {
                Console.WriteLine("No white lights found. Adding hard-coded defaults");
                WhiteLights.Add(new PinControl(4, true, "White1"));
                WhiteLights.Add(new PinControl(14, true, "White2"));
                WhiteLights.Add(new PinControl(15, true, "White3"));
                WhiteLights.Add(new PinControl(18, true, "White4"));
            }

            if (BlackLights.Count == 0)
            {
                Console.WriteLine("No black lights found. Adding hard-coded defaults");
                BlackLights.Add(new PinControl(5, true, "Black1"));
                BlackLights.Add(new PinControl(6, true, "Black2"));
                BlackLights.Add(new PinControl(13, true, "Black3"));
                BlackLights.Add(new PinControl(16, true, "Black4"));
                BlackLights.Add(new PinControl(19, true, "Black5"));
                BlackLights.Add(new PinControl(20, true, "Black6"));
                BlackLights.Add(new PinControl(21, true, "Black7"));
                BlackLights.Add(new PinControl(26, true, "Black8"));
            }
            */
        }

        public void Initialize()
        {
            foreach (var light in WhiteLights)
            {
                light.Initialize();
            }

            foreach (var light in BlackLights)
            {
                light.Initialize();
            }
        }

        public void WhiteOn()
        {
            Console.WriteLine("Turning on White Lights");
            LightState = LightStates.WhiteOn;
            foreach (var light in WhiteLights)
            {
                light.isOn = true;
            }
        }

        public void WhiteOff()
        {
            Console.WriteLine("Turning off White Lights");
            LightState = LightStates.Undefined;
            foreach (var light in WhiteLights)
            {
                light.isOn = false;
            }
        }

        public void BlackOn()
        {
            Console.WriteLine("Turning on Black Lights");
            LightState = LightStates.BlackOn;

            foreach (var light in BlackLights)
            {
                light.isOn = true;
            }
        }

        public void BlackOff()
        {
            Console.WriteLine("Turning off Black Lights");
            LightState = LightStates.Undefined;

            foreach (var light in BlackLights)
            {
                light.isOn = false;
            }
        }

        public void AllOn()
        {
            LightState = LightStates.AllOn;

            WhiteOn();
            BlackOn();
        }

        public void Off()
        {
            Console.WriteLine("Turning off All Lights");
            LightState = LightStates.Off;
            foreach (var light in BlackLights)
            {
                light.isOn = false;
            }
            foreach (var light in WhiteLights)
            {
                light.isOn = false;
            }
        }

        public void Dispose()
        {
            foreach (var light in WhiteLights)
            {
                light.Dispose();
            }

            foreach (var light in BlackLights)
            {
                light.Dispose();
            }
        }
    }
}