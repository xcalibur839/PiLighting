using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;

namespace PiLighting
{
    enum lightType
    {
        black,
        white,
        blue,
        red
    }

    struct lightConfig
    {
        public string Name;
        public int Pin;
        public lightType Type;
    }
    struct buttonConfig
    {
        public string Name;
        public int Pin;
    }
    class effectConfig
    {
        public string Name;
        public IEnumerable<effectStep> Steps;
    }
    struct effectStep
    {
        public int Number;
        public string[] Lights;
        public bool On;
        public int Delay;
    }

    static class Config
    {
        public static string SysGpioPath = "/sys/class/gpio/";
        public static IEnumerable<lightConfig> lightConfigList { get; private set; }
        public static IEnumerable<buttonConfig> buttonConfigList { get; private set; }
        public static bool ConfigLoaded = false;
        public static List<effectConfig> effectConfigList { get; private set; }

        public static void Load(string file = "Config.xml")
        {
            if (File.Exists(file))
            {
                try
                {
                    XElement configFile = XElement.Load(file);
                    SysGpioPath = configFile.Element("Program").Attribute("SysGpioPath").Value;
                    lightConfigList =
                        from elem in configFile.Descendants("Lights").Descendants("Light")
                        orderby elem.Attribute("Name").Value
                        select new lightConfig
                        {
                            Name = elem.Attribute("Name").Value,
                            Pin = int.Parse(elem.Attribute("Pin").Value),
                            Type = (lightType) Enum.Parse(typeof(lightType), elem.Attribute("Type").Value)
                        };
                    buttonConfigList =
                        from elem in configFile.Descendants("Buttons").Descendants("Button")
                        orderby elem.Attribute("Name").Value
                        select new buttonConfig
                        {
                            Name = elem.Attribute("Name").Value,
                            Pin = int.Parse(elem.Attribute("Pin").Value)
                        };
                    effectConfigList = new List<effectConfig>();
                    foreach(var effect in configFile.Element("Effects").Descendants("Effect"))
                    {
                        effectConfig fxConfig = new effectConfig();
                        fxConfig.Name = effect.Attribute("Name").Value;
                        fxConfig.Steps =
                            from steps in effect.Descendants("Steps").Descendants("Step")
                            orderby int.Parse(steps.Attribute("Number").Value)
                            select new effectStep
                            {
                                Number = int.Parse(steps.Attribute("Number").Value),
                                Lights = steps.Attribute("Lights").Value.Split(','),
                                On = steps.Attribute("Value").Value.ToLower() == "on" ? true : false,
                                Delay = int.Parse(steps.Attribute("Delay").Value)
                            };
                        effectConfigList.Add(fxConfig);
                    }
                    ConfigLoaded = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error loading XML data. Please try loading a different config file.");
                    Console.WriteLine(ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Could not find config file: {0}\n", file);
            }
        }
    }
}