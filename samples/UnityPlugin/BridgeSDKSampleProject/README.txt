This is a sample Unity project which integrates the Bridge developer DLL using C# scripts.

The project was created using Unity 2017.1.f1, it is however compatible with version 5.5.3

Important notice: Make sure that you tell Unity to compile C# scripts in unsafe mode by having the mcs.rsp (or smcs.rsp) file in the Assets folder.

The Logitech Bridge libraries and their dependencies must be part of the project, so make sure that you copied all the DLLs which are located in the "Assets\Plugins\x64" folder.

All the scripts in "Assets\BridgeSDK" are necessary. "BridgeSampleScript.cs" (located in "Assets\Scripts") is just an example of how to use the plugin.

Information about performances: Since the Bridge developer DLL and the Unity plugin are performing I/O operations, it is therefore recommended that you call the API functions within a separate thread, else it might impact your game's performances. At this stage of development, multithreading and concurrent access have not been tested yet.

Please contact supportsdk@logitech.com or reach us via GitHub if you have any question.