using System;
using System.Threading;

namespace PiLighting
{
	partial class Program
	{
		static PinControl Button = new PinControl (2, false, "Button");
		static LightControl Lights = new LightControl ();
		static Effect Effects = new Effect(ref Lights);

		static bool OptionOrExit() //Return true to continue execution
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
				Lights.BlackOff ();
				Lights.WhiteOn ();
				return true;
			case ConsoleKey.B:
				Console.WriteLine ();
				Lights.WhiteOff ();
				Lights.BlackOn ();
				return true;
			case ConsoleKey.O:
				Console.WriteLine ();
				Lights.Off ();
				return true;
			case ConsoleKey.A:
				Console.WriteLine ();
				Lights.AllOn ();
				return true;
			case ConsoleKey.L:
				Effects.Lightning ();
				return true;
			case ConsoleKey.M:
				Effects.Marquee ();
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

			for (int i = 0; !Console.KeyAvailable || OptionOrExit(); i++)
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
					default:
						Lights.Off ();
						break;
					}
				}

				if (!Button.isOn && buttonHandled)
				{
					Thread.Sleep (30);
					buttonHandled = false;
				}

				if (!Button.isOn && !buttonHandled) //This should never occur under normal circumstances
				{
					Console.WriteLine ("*****************************************************************");
					Console.WriteLine ("A fatal error occurred when attempting to read the button state");
					Console.WriteLine ("This program will now close");
					Console.WriteLine ("*****************************************************************");
					return;
				}
			}

			Button.Dispose ();
			Lights.Dispose ();
		}
	}
}