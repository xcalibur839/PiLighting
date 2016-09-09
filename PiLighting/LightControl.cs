using System;
using System.Collections.Generic;

namespace PiLighting
{
	enum LightStates
	{
		Off,
		WhiteOn,
		BlackOn
	}

	class LightControl
	{
		public List<PinControl> WhiteLights = new List<PinControl> ();
		public List<PinControl> BlackLights = new List<PinControl> ();
		public LightStates LightState = LightStates.Off;

		public LightControl()
		{
			WhiteLights.Add (new PinControl (4, true, "White1"));
			WhiteLights.Add (new PinControl (14, true, "White2"));
			WhiteLights.Add (new PinControl (15, true, "White3"));
			WhiteLights.Add (new PinControl (18, true, "White4"));

			BlackLights.Add (new PinControl (5, true, "Black1"));
			BlackLights.Add (new PinControl (6, true, "Black2"));
			BlackLights.Add (new PinControl (13, true, "Black3"));
			BlackLights.Add (new PinControl (16, true, "Black4"));
			BlackLights.Add (new PinControl (19, true, "Black5"));
			BlackLights.Add (new PinControl (20, true, "Black6"));
			BlackLights.Add (new PinControl (21, true, "Black7"));
			BlackLights.Add (new PinControl (26, true, "Black8"));
		}

		public void Initialize()
		{
			foreach (var light in WhiteLights)
			{
				light.Initialize ();
			}

			foreach (var light in BlackLights)
			{
				light.Initialize ();
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
			foreach (var light in BlackLights) 
			{
				light.isOn = false;
			}
		}

		public void BlackOn()
		{
			Console.WriteLine("Turning on Black Lights");
			LightState = LightStates.BlackOn;
			foreach (var light in WhiteLights) 
			{
				light.isOn = false;
			}
			foreach (var light in BlackLights) 
			{
				light.isOn = true;
			}
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
				light.Dispose ();
			}

			foreach (var light in BlackLights)
			{
				light.Dispose ();
			}
		}
	}
}