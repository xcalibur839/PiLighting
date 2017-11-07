using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Xml;

namespace PiLighting
{
    enum lightType
    {
        black,
        white
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

    static class Config
    {
        public static string SysGpioPath = "/sys/class/gpio/";
        public static IEnumerable<lightConfig> lightConfigList { get; private set; }
		public static IEnumerable<buttonConfig> buttonConfigList { get; private set; }
        public static bool ConfigLoaded = false;

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
                            Type = elem.Attribute("Type").Value == "black" ? lightType.black : lightType.white
                        };
					buttonConfigList = 
						from elem in configFile.Descendants("Buttons").Descendants("Button")
						orderby elem.Attribute("Name").Value
						select new buttonConfig
					{
						Name = elem.Attribute("Name").Value,
						Pin = int.Parse(elem.Attribute("Pin").Value)
					};
                    ConfigLoaded = true;
                }
                catch
                {
                    Console.WriteLine("Error loading XML data. Please try loading a different config file.");
                }
            }
            else
            {
                Console.WriteLine("Could not find config file: {0}\n", file);
            }
        }
    }
}