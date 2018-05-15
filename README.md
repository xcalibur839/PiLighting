# PiLighting
Code used for controlling the Raspberry Pi GPIO as a light controller

When this program runs, it reads its configuration from Config.xml in the same directory as the program. 
You can specify a custom config file with a command line parameter. 
An example Config.xml can be found in the PiLighting/PiLighting/bin/Debug/ directory. 

The program will attempt to initialize any lights and buttons specified in the Config.xml file. 
Once initialized, you can control the lights with the following keyboard keys:

- Q: Quit the program
- T: Enter the Testing mode
- W: Turn on all white lights
- B: Turn on all black lights
- O: Turn off all lights
- A: Turn on all lights
- L: Flash lights like Lightning
- M: Play the Marquee effect(s) from Config.xml
