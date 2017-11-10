## Unity Plugin sample

We provide a sample Unity project with some scripts which are required to integrate the DLL (they are located in  “Assets/BridgeSDK”). The libraries in the “Plugins” directory are also required. There is a sample script (in the Scripts directory) which will show you how to use the wrapper.

You can load and run the scene we provided to see a help message displayed in front of the main camera:


Screen in the sample Unity scene

Note:  the shortcuts will output their respective results in the console.

The scene runs the Bridge SDK in a singleton object that handles connecting to and disconnecting from the Bridge runtime during Unity’s Awake(), respectively OnApplicationQuit() functions. Make sure the Bridge runtime is running prior to launching the Unity scene. If the connection fails, a message will appear in the console.

The example uses the “BridgeManager” object to attach the BridgeSDK to. From there, any game object with a script component can access functions, as shown in the script attached to the “SampleController” object.


Sample scene hierarchy


## Notes
Warning: At this stage of development, the C# to C++ wrapper requires the C# compiler to run in ‘unsafe’ mode.

NB: The sample project does not include any third party VR plugin or software (SteamVR not included).
