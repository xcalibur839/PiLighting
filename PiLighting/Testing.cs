using System;
using System.Collections.Generic;
using System.Threading;

namespace PiLighting
{
    enum MenuItems
    {
        BlinkWhite = 1,
        BlinkBlack,
        FlashWhite,
        FlashBlack,
        BlinkAll,
        FlashAll
    }

    partial class Program
    {
        static void Test()
        {
            int selection = 0;

            Console.WriteLine("What do you want to test");
            Console.WriteLine("[1] Blink White Light(s)");
            Console.WriteLine("[2] Blink Black Light(s)");
            Console.WriteLine("[3] Flash White Light(s)");
            Console.WriteLine("[4] Flash Black Light(s)");
            Console.WriteLine("[5] Blink All Lights");
            Console.WriteLine("[6] Flash All Lights");
            Console.WriteLine("[Other] Exit");
            int.TryParse(Console.ReadKey().KeyChar.ToString(), out selection);
            Console.WriteLine();

            switch (selection)
            {
                case (int)MenuItems.BlinkWhite:
                    BlinkTest(false, Lights.WhiteLights);
                    break;
                case (int)MenuItems.BlinkBlack:
                    BlinkTest(false, Lights.BlackLights);
                    break;
                case (int)MenuItems.FlashWhite:
                    FlashTest(false, Lights.WhiteLights);
                    break;
                case (int)MenuItems.FlashBlack:
                    FlashTest(false, Lights.BlackLights);
                    break;
                case (int)MenuItems.BlinkAll:
                    BlinkTest(true, Lights.WhiteLights);
                    break;
                case (int)MenuItems.FlashAll:
                    FlashTest(true, Lights.WhiteLights);
                    break;
            }

            Console.WriteLine("********** End Test **********");
        }

        static void FlashTest(bool all, List<PinControl> lightController)
        {
            int delay = 500;
            int testSubject = 0;
            int loops = 0;

            if (all)
            {
                Console.WriteLine("How many loops?");
                int.TryParse(Console.ReadLine(), out loops);

                Console.WriteLine("How much delay (ms)?");
                int.TryParse(Console.ReadLine(), out delay);

                for (int i = 0; i < loops * 2; i++)
                {
                    if ((i + 1) % 2 == 0)
                    {
                        Console.WriteLine("Loop {0} out of {1}", (i + 1) / 2, loops);
                    }

                    foreach (var light in Lights.WhiteLights)
                    {
                        light.isOn = !light.isOn;
                    }
                    foreach (var light in Lights.BlackLights)
                    {
                        light.isOn = !light.isOn;
                    }
                    Thread.Sleep(delay);
                }
            }
            else
            {
                do
                {
                    Console.WriteLine("Which Light?");
                    Console.WriteLine("Enter [0] for All");

                    for (int i = 0; i < lightController.Count; i++)
                    {
                        Console.WriteLine("Enter [{0}] for Pin{1:d2}: {2}", i + 1,
                                           lightController[i].Number,
                                           lightController[i].Name);
                    }
                    int.TryParse(Console.ReadKey().KeyChar.ToString(), out testSubject);
                } while (testSubject > lightController.Count);

                Console.WriteLine();
                Console.WriteLine("How many loops?");
                int.TryParse(Console.ReadLine(), out loops);

                Console.WriteLine("How much delay (ms)?");
                int.TryParse(Console.ReadLine(), out delay);

                if (testSubject == 0)
                {
                    for (int i = 0; i < loops * 2; i++)
                    {
                        if ((i + 1) % 2 == 0)
                        {
                            Console.WriteLine("Loop {0} out of {1}", (i + 1) / 2, loops);
                        }

                        foreach (var light in lightController)
                        {
                            light.isOn = !light.isOn;
                        }
                        Thread.Sleep(delay);
                    }
                }
                else
                {
                    for (int i = 0; i < loops * 2; i++)
                    {
                        if ((i + 1) % 2 == 0)
                        {
                            Console.WriteLine("Loop {0} out of {1}", (i + 1) / 2, loops);
                        }

                        lightController[testSubject - 1].isOn = !lightController[testSubject - 1].isOn;
                        Thread.Sleep(delay);
                    }
                }
            }
        }

        static void BlinkTest(bool all, List<PinControl> lightController)
        {
            int delay = 500;
            int testSubject = 0;
            int loops = 0;

            if (all)
            {
                Console.WriteLine("How many loops?");
                int.TryParse(Console.ReadLine(), out loops);

                Console.WriteLine("How much delay (ms)?");
                int.TryParse(Console.ReadLine(), out delay);

                for (int i = 0; i < loops; i++)
                {
                    Console.WriteLine("Loop {0} out of {1}", i + 1, loops);

                    foreach (var light in Lights.WhiteLights)
                    {
                        light.isOn = !light.isOn;
                        Thread.Sleep(delay);
                        light.isOn = !light.isOn;
                    }

                    foreach (var light in Lights.BlackLights)
                    {
                        light.isOn = !light.isOn;
                        Thread.Sleep(delay);
                        light.isOn = !light.isOn;
                    }
                }
            }
            else
            {
                do
                {
                    Console.WriteLine("Which Light?");
                    Console.WriteLine("Enter [0] for All");

                    for (int i = 0; i < lightController.Count; i++)
                    {
                        Console.WriteLine("Enter [{0}] for Pin{1:d2}: {2}", i + 1,
                                           lightController[i].Number,
                                           lightController[i].Name);
                    }
                    int.TryParse(Console.ReadKey().KeyChar.ToString(), out testSubject);
                } while (testSubject > lightController.Count);

                Console.WriteLine();
                Console.WriteLine("How many loops?");
                int.TryParse(Console.ReadLine(), out loops);

                Console.WriteLine("How much delay (ms)?");
                int.TryParse(Console.ReadLine(), out delay);

                if (testSubject == 0)
                {
                    for (int i = 0; i < loops; i++)
                    {
                        Console.WriteLine("Loop {0} out of {1}", i + 1, loops);

                        foreach (var light in lightController)
                        {
                            light.isOn = !light.isOn;
                            Thread.Sleep(delay);
                            light.isOn = !light.isOn;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < loops * 2; i++)
                    {
                        if ((i + 1) % 2 == 0)
                        {
                            Console.WriteLine("Loop {0} out of {1}", (i + 1) / 2, loops);
                        }

                        lightController[testSubject - 1].isOn = !lightController[testSubject - 1].isOn;
                        Thread.Sleep(delay);
                    }
                }
            }
        }
    }
}