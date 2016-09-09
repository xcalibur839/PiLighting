using System;
using System.Threading;

namespace PiLighting
{
	partial class Program
	{
		static PinControl Button = new PinControl (2, false, "Button");
		static LightControl Lights = new LightControl ();

		static bool ExitOrOption()
		{
			switch (Console.ReadKey ().Key)
			{
			case ConsoleKey.Q:
				Console.WriteLine ();
				return false;
			case ConsoleKey.T:
				Console.WriteLine ();
				Test ();
				return true;
			case ConsoleKey.W:
				Console.WriteLine ();
				Lights.WhiteOn ();
				return true;
			case ConsoleKey.B:
				Console.WriteLine ();
				Lights.BlackOn ();
				return true;
			case ConsoleKey.O:
				Console.WriteLine ();
				Lights.Off ();
				return true;
			default:
				Console.WriteLine ();
				return true;
			}
		}

		public static void Main()
		{
			bool buttonHandled = false;

			Button.Initialize ();
			Lights.Initialize ();

			for (int i = 0; !Console.KeyAvailable || ExitOrOption(); i++)
			{
				if (Button.isOn && !buttonHandled) 
				{
					buttonHandled = true;

					switch (Lights.LightState)
					{
					case LightStates.Off:
						Lights.WhiteOn();
						break;
					case LightStates.WhiteOn:
						Lights.BlackOn();
						break;
					case LightStates.BlackOn:
						Lights.Off();
						break;
					}
				}

				if (!Button.isOn && buttonHandled) 
				{
					Thread.Sleep(30);
					buttonHandled = false;
				}
			}

			Button.Dispose ();
			Lights.Dispose ();
		}
	}
}