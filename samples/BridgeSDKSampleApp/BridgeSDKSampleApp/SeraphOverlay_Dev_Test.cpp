#include "stdafx.h"
#include <thread>
#include <conio.h>
#include <iostream>
#include "..\SeraphOverlay_Dev_DLL\SeraphOverlay_Dev_DLL.h"
//#pragma comment(lib, "SeraphOverlay_Dev_DLL.lib")
#include "..\SeraphOverlay_Dev_DLL\BridgeEnums.h"
#include "..\SeraphOverlay_Dev_DLL\KeyboardStatus.h"
#include "..\SeraphOverlay_Dev_DLL\HandsStatus.h"

using namespace std;


void displayHelpMessage() {
	cout << 
		"Available shortcuts:\n" << 
		"'.' : Stop the application.\n" << 
		"'h' : Display a help message.\n" <<
		"'z' : Init the developer DLL.\n" <<
		"'x' : Shutdown (i.e. disconnect) the developer DLL.\n" <<
		"'q' : Display the keyboard.\n" <<
		"'w' : Hide the keyboard.\n" <<
		"'e' : Set the hands representation mode to 'GHOST'.\n" <<
		"'r' : Set the hands representation mode to 'SEETHRU'.\n" <<
		"'t' : Set the hands color in 'GHOST' mode to RGB values (0, 140, 245).\n" <<
		"'y' : Set the hands color in 'GHOST' mode to RGB values (127, 255, 127).\n" <<
		"'a' : Update the KeyboardStatus instance you passed as a parameter.\n" <<
		"'s' : Update the HandsStatus instance you passed as a parameter.\n"
		<< endl;
}

int main()
{
	bool quitApp = false;

	cout << "This is a sample project which loads the Bridge Developer DLL and shows how to use it." << endl;
	cout << "Press 'h' to display a help message with the different shortcuts. Press '.' to stop the application." << endl;

	do {
		if (kbhit()) {
			char c = getch();
			switch (c) {
			case '.':
				quitApp = true;
				break;

			case 'h':
				displayHelpMessage();
				break;
			case 'z':
			{
				EInitErrorCode initServerResponse = Init();
				cout << "Server response: " << (int)initServerResponse << endl;
				break;
			}
			case 'x':
			{
				EShutdownErrorCode shutdownServerResponse = Shutdown();
				cout << "Server response: " << (int)shutdownServerResponse << endl;
				break;
			}

			case 'q':
			{
				ESetKeyboardVisibilityErrorCode kbVisiServerResponse = SetKeyboardVisibility(true);
				cout << "Server response: " << (int)kbVisiServerResponse << endl;
				break;
			}

			case 'w':
			{
				ESetKeyboardVisibilityErrorCode kbVisiServerResponse = SetKeyboardVisibility(false);
				cout << "Server response: " << (int)kbVisiServerResponse << endl;
				break;
			}

			case 'e':
			{
				ESetHandsRepresentationModeErrorCode handModeServerResponse = SetHandsRepresentationMode(EHandsRepresentationMode::GHOST);
				cout << "Server response: " << (int)handModeServerResponse << endl;
				break;
			}
			case 'r':
			{
				ESetHandsRepresentationModeErrorCode handModeServerResponse = SetHandsRepresentationMode(EHandsRepresentationMode::SEETHRU);
				cout << "Server response: " << (int)handModeServerResponse << endl;
				break;
			}

			case 't':
			{
				ESetHandsColorErrorCode handColorServerResponse = SetHandsColor(EHandsRepresentationMode::GHOST, 0, 140, 245);
				cout << "Server response: " << (int)handColorServerResponse << endl;
				break;
			}
			case 'y':
			{
				ESetHandsColorErrorCode handColorServerResponse = SetHandsColor(EHandsRepresentationMode::GHOST, 127, 255, 127);
				cout << "Server response: " << (int)handColorServerResponse << endl;
				break;
			}
			case 'a':
			{
				KeyboardStatus ks;
				EGetKeyboardStatusErrorCode keyboardStatusServerResponse = GetKeyboardStatus(&ks);
				cout << "Server response: " << (int)keyboardStatusServerResponse << endl;
				cout << "    visible: " << ks.isVisible << ", tracker S/N: " << ks.pairedTrackerID << endl;
				break;
			}
			case 's':
			{
				HandsStatus hs;
				EGetHandsStatusErrorCode handsStatusServerResponse = GetHandsStatus(&hs);
				cout << "Server response: " << (int)handsStatusServerResponse << endl;
				printf("    visible: %d, mode: %d, color: (%d,%d,%d)\n", hs.isVisible, (int)hs.handsMode, hs.colorR, hs.colorG, hs.colorB);
				break;
			}


			}
		}
		std::this_thread::sleep_for(std::chrono::milliseconds(100));
	} while (!quitApp);

    return 0;
}

