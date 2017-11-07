using System;
using System.Collections.Generic;

namespace PiLighting
{
	class ButtonControl
	{
		public List<PinControl> MainButtons = new List<PinControl>();

		public ButtonControl ()
		{
			if (Config.buttonConfigList != null) 
			{
				foreach (var button in Config.buttonConfigList) 
				{
					Console.WriteLine ("Found a button: {0}({1})", button.Name, button.Pin);
					MainButtons.Add (new PinControl (button.Pin, false, button.Name));
				}
			}
		}

		public void Initialize()
		{
			foreach (var button in MainButtons) 
			{
				button.Initialize ();
			}
		}

		public void Dispose()
		{
			foreach (var button in MainButtons) 
			{
				button.Dispose ();
			}
		}
	}
}

