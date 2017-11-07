using System;
using System.IO;
using System.Threading;

namespace PiLighting
{
	class PinControl
	{
		string PINGPIOPATH = "";
		static readonly string SYSGPIOPATH = Config.SysGpioPath;
		public int Number {get; private set;}
		public bool isOutput {get; private set;}
		public string Name { get; private set; }

		void PrintMessage(string message)
		{
			Console.WriteLine ("{0}({1:d2}): {2}", Name, Number, message);
		}

		public PinControl(int num, bool isOut, string name)
		{
			Number = num;
			isOutput = isOut;
			Name = name;
			PINGPIOPATH = SYSGPIOPATH + "gpio" + Number + "/";
			PrintMessage ("New Pin");
		}

		public void Initialize()
		{
			try
			{
				File.WriteAllText (SYSGPIOPATH + "export", Number.ToString ());
				PrintMessage("activated");
				Thread.Sleep (100); //Pi Zero needs at least 60 - 80 to activate GPIO pins
			}
			catch
			{
				PrintMessage ("error activating (May already be active)");
			}

			try
			{
				File.WriteAllText (PINGPIOPATH + "direction", isOutput ? "out" : "in");
				PrintMessage(string.Format("set as {0} pin", isOutput ? "output" : "input"));
			} 
			catch
			{
				PrintMessage ("error setting direction (GPIO may not be ready)");
			}
		}

		public void Dispose()
		{
			try
			{
				File.WriteAllText (SYSGPIOPATH + "unexport", Number.ToString());
				PrintMessage("deactivated");
			} 
			catch
			{
				PrintMessage("error disposing");
			}
		}

		public bool isOn 
		{
			get 
			{
				try
				{
					string currentValue = File.ReadAllText (PINGPIOPATH + "value").Trim();
					return currentValue == "1" ? true : false;
				} 
				catch
				{
					PrintMessage ("error reading value");
					return false;
				}
			}
			set 
			{
				try
				{
					File.WriteAllText (PINGPIOPATH + "value", value ? "1" : "0");
					PrintMessage(string.Format("is now {0}", value ? "on" : "off"));
				} 
				catch
				{
					PrintMessage ("error writing value");
				}
			}
		}
	}
}