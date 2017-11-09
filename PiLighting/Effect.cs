using System;
using System.Threading;

namespace PiLighting
{
    class Effect
    {
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
            Lights.Off();

            foreach (var effect in Config.effectConfigList)
            {
                Console.WriteLine("********** Begin {0} **********", effect.Name);
                foreach (var step in effect.Steps)
                {
                    foreach (var light in step.Lights)
                    {
                        var whiteSearch = Lights.WhiteLights.Find(x => x.Name.ToLower() == light.ToLower());
                        var blackSearch = Lights.BlackLights.Find(x => x.Name.ToLower() == light.ToLower());
                        if (whiteSearch != null)
                        {
                            whiteSearch.isOn = step.On;
                        }
                        if (blackSearch != null)
                        {
                            blackSearch.isOn = step.On;
                        }
                    }
                    Thread.Sleep(step.Delay);
                }
                Console.WriteLine("********** End {0} **********", effect.Name);
            }
        }
    }
}