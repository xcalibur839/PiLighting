using System;
using System.Threading;

namespace PiLighting
{
    class Effect
    {
        const int MARQUEE_COUNT = 2;
        static Random random = new Random();
        LightControl Lights;

        public Effect(ref LightControl MainLights)
        {
            Lights = MainLights;
        }

        public void Lightning()
        {
            int flashCount = random.Next(2, 7) * 2;
            Lights.Off();
            Console.WriteLine("********** Begin Lightning Effect **********");
            for (int i = 0; i < flashCount; i++)
            {
                foreach (var light in Lights.BlackLights)
                {
                    light.isOn = !light.isOn;
                }
                Thread.Sleep(random.Next(5, 25));
                foreach (var light in Lights.WhiteLights)
                {
                    light.isOn = !light.isOn;
                    Thread.Sleep(random.Next(5, 30));
                }
                Thread.Sleep(random.Next(5, 20));
            }
            Console.WriteLine("********** End Lightning Effect **********");
        }

        public void Marquee()
        {
            int delay = 200;
            int index = 0;
            int[] bottomUpLightOrder = {
                0, 2,
                2, 6,
                4, 5, //5 is a placeholder for now
				1, 5,
                0, 7,
                1, 3
            };

            Lights.Off();
            Console.WriteLine("********** Begin Marquee Effect **********");

            for (int i = 0; i < MARQUEE_COUNT; i++)
            {
                Lights.WhiteLights[bottomUpLightOrder[index]].isOn = !Lights.WhiteLights[bottomUpLightOrder[index++]].isOn;
                Lights.WhiteLights[bottomUpLightOrder[index]].isOn = !Lights.WhiteLights[bottomUpLightOrder[index++]].isOn;
                Thread.Sleep(delay);

                for (int j = 0; j < 4; j++)
                {
                    Lights.BlackLights[bottomUpLightOrder[index]].isOn = !Lights.BlackLights[bottomUpLightOrder[index++]].isOn;
                    Lights.BlackLights[bottomUpLightOrder[index]].isOn = !Lights.BlackLights[bottomUpLightOrder[index++]].isOn;
                    Thread.Sleep(delay);
                }

                Lights.WhiteLights[bottomUpLightOrder[index]].isOn = !Lights.WhiteLights[bottomUpLightOrder[index++]].isOn;
                Lights.WhiteLights[bottomUpLightOrder[index]].isOn = !Lights.WhiteLights[bottomUpLightOrder[index]].isOn;

                Thread.Sleep(delay * 3);

                Lights.WhiteLights[bottomUpLightOrder[index]].isOn = !Lights.WhiteLights[bottomUpLightOrder[index--]].isOn;
                Lights.WhiteLights[bottomUpLightOrder[index]].isOn = !Lights.WhiteLights[bottomUpLightOrder[index--]].isOn;
                Thread.Sleep(delay);

                for (int j = 0; j < 4; j++)
                {
                    Lights.BlackLights[bottomUpLightOrder[index]].isOn = !Lights.BlackLights[bottomUpLightOrder[index--]].isOn;
                    Lights.BlackLights[bottomUpLightOrder[index]].isOn = !Lights.BlackLights[bottomUpLightOrder[index--]].isOn;
                    Thread.Sleep(delay);
                }

                Lights.WhiteLights[bottomUpLightOrder[index]].isOn = !Lights.WhiteLights[bottomUpLightOrder[index--]].isOn;
                Lights.WhiteLights[bottomUpLightOrder[index]].isOn = !Lights.WhiteLights[bottomUpLightOrder[index]].isOn;

                Thread.Sleep(delay * 3);
            }

            Console.WriteLine("********** End Marquee Effect **********");
        }
    }
}