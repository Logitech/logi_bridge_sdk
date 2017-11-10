## MS Visual Studio console sample app

This sample project  loads the developer DLL and performs on-demand example API calls. We also provide a C# wrapper made for Unity (Unity project and package file). 

> This client application needs to connect to the Bridge software. Therefore you must have Bridge running prior to executing the samples

Our developer DLL is called “BridgeOverlay_SDK.dll” and has the following dependencies:
* LIBEAY32.dll
* SSLEAY32.dll
* libuv.dll
* uWS.dll
* zlib1.dll


**IMPORTANT:** <br/>
You will get an error if you try to use the developer DLL without calling the Init function first, and call the Shutdown function as well when your application exits. Both code samples illustrate automated init and shutdown.

The developer DLL performs (local) I/O operations. It is therefore recommended that you call the DLL’s functions in a separate thread.

C++
The BridgeSDK folder at the project’s root contains the .dll and .lib file that you need as well as the necessary header files.

After you build the project and run the application, you will be able to hit the ‘H’ key to display a help message.





## Notes

* Warning: At this stage of development, in order for an applications to use the developer DLL, you must make sure that:

* to compile in 'Release' mode. be aware that 'Debug' mode has still some shortcomings.
* you deactivated the compiler’s SDL checks: <br/> To deactivate the SDL checks in Visual Studio, open the project’s ‘Configuration Properties’ and under ‘C/C++’ >> ‘General’, set the ‘SDL checks’ to ‘No’. Alternatively, you can manually add the compiler’s flag “/sdl-”.

* Please contact supportsdk@logitech.com or reach us via GitHub if you have any question.
