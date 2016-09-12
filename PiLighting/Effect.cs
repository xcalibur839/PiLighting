using System;
using System.Threading;

namespace PiLighting
{
	class Effect
	{
		static Random random = new Random ();
		LightControl Lights;

		public Effect (ref LightControl MainLights)
		{
			Lights = MainLights;
		}

		public void Lightning()
		{
			Lights.Off ();
			Console.WriteLine ("********** Begin Lighting Effect **********");

			int flashCount = random.Next (2, 7) * 2;
			for (int i = 0; i < flashCount; i++)
			{
				foreach (var light in Lights.BlackLights)
				{
					light.isOn = !light.isOn;
				}
				Thread.Sleep (random.Next(5, 25));
				foreach (var light in Lights.WhiteLights)
				{
					light.isOn = !light.isOn;
					Thread.Sleep (random.Next(5, 30)); 
				}
				Thread.Sleep (random.Next(5, 20));
			}
			Console.WriteLine ("********** End Lighting Effect **********");
		}

		public void Marquee()
		{
			int delay = 200;
			Lights.Off ();

			for (int i = 0; i < 4; i++)
			{
				Lights.WhiteLights [0].isOn = true;
				Lights.WhiteLights [2].isOn = true;
				Thread.Sleep (delay);
				Lights.BlackLights [2].isOn = true;
				Lights.BlackLights [6].isOn = true;
				Thread.Sleep (delay);
				Lights.BlackLights [4].isOn = true;
				//Lights.BlackLights [7].isOn = true;
				Thread.Sleep (delay);
				Lights.BlackLights [1].isOn = true;
				//Lights.BlackLights [7].isOn = true;
				Thread.Sleep (delay);
				Lights.BlackLights [0].isOn = true;
				Lights.BlackLights [7].isOn = true;
				Thread.Sleep (delay);
				Lights.WhiteLights [1].isOn = true;
				Lights.WhiteLights [3].isOn = true;

				Thread.Sleep (delay * 2);

				Lights.WhiteLights [1].isOn = false;
				Lights.WhiteLights [3].isOn = false;
				Thread.Sleep (delay);
				Lights.BlackLights [0].isOn = false;
				Lights.BlackLights [7].isOn = false;
				Thread.Sleep (delay);
				Lights.BlackLights [1].isOn = false;
				//Lights.BlackLights [7].isOn = true;
				Thread.Sleep (delay);
				Lights.BlackLights [4].isOn = false;
				//Lights.BlackLights [7].isOn = true;
				Thread.Sleep (delay);
				Lights.BlackLights [2].isOn = false;
				Lights.BlackLights [6].isOn = false;
				Thread.Sleep (delay);
				Lights.WhiteLights [0].isOn = false;
				Lights.WhiteLights [2].isOn = false;

				Thread.Sleep (delay * 2);
			}
		}
	}
}

