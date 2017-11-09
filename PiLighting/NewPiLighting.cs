using System;
using System.Threading;

namespace PiLighting
{
    partial class Program
    {
        static ButtonControl Buttons;
        static LightControl Lights;
        static Effect Effects;

        static bool OptionOrExit() //Return true to continue execution
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.Q:
                    Console.WriteLine();
                    return false;
                case ConsoleKey.T:
                    Console.WriteLine();
                    Test();
                    return true;
                case ConsoleKey.W:
                    Console.WriteLine();
                    Lights.BlackOff();
                    Lights.WhiteOn();
                    return true;
                case ConsoleKey.B:
                    Console.WriteLine();
                    Lights.WhiteOff();
                    Lights.BlackOn();
                    return true;
                case ConsoleKey.O:
                    Console.WriteLine();
                    Lights.Off();
                    return true;
                case ConsoleKey.A:
                    Console.WriteLine();
                    Lights.AllOn();
                    return true;
                case ConsoleKey.L:
                    Effects.Lightning();
                    return true;
                case ConsoleKey.M:
                    Effects.Marquee();
                    return true;
                default:
                    Console.WriteLine();
                    return true;
            }
        }

        public static void Main(string[] args)
        {
            bool buttonHandled = false;

            //Locate and load the configuation file.
            //Default is Config.xml but also supports custom config XML file with command line parameter
            string file = "Config.xml";
            foreach (string arg in args)
            {
                if (arg.ToLower().EndsWith(".xml"))
                {
                    file = arg;
                }
            }

            //Attempt to load the config file
            Config.Load(file);
            if (!Config.ConfigLoaded)
            {
                Console.WriteLine("Can't start without Config file. Press enter to exit...");
                Console.ReadLine();
                return;
            }

            Buttons = new ButtonControl();
            Lights = new LightControl();
            Effects = new Effect(ref Lights);

            Buttons.Initialize();
            Lights.Initialize();

            while (!Console.KeyAvailable || OptionOrExit())
            {
                if (Buttons.MainButtons.Count > 0)
                {
                    if (Buttons.MainButtons[0].isOn && !buttonHandled)
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
                                Lights.Off();
                                break;
                        }
                    }

                    if (!Buttons.MainButtons[0].isOn && buttonHandled)
                    {
                        Thread.Sleep(30);
                        buttonHandled = false;
                    }

                    if (!Buttons.MainButtons[0].isOn && !buttonHandled)
                    { //This should never occur under normal circumstances
                        Console.WriteLine("*****************************************************************");
                        Console.WriteLine("A fatal error occurred when attempting to read the button state");
                        Console.WriteLine("This program will now close");
                        Console.WriteLine("*****************************************************************");
                        break;
                    }
                }
            }

            Buttons.Dispose();
            Lights.Dispose();
            Console.ReadLine();
        }
    }
}