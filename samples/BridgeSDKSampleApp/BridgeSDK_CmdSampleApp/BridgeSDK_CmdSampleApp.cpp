#include "stdafx.h"

#include <thread>
#include <conio.h>
#include <iostream>
#include <sstream>

// Make sure the path to the Bridge SDK library is set in the project's properties
#include "BridgeOverlay_SDK.h" 

using namespace std;

void displayHelpMessage();

int main()
{
	cout << "// ----------------------------------------------------------------------------------------" << endl;
	cout << "// Logitech Bridge SDK: Command line sample application" << endl;
	cout << "// Copyrights Logitech - 2017\n"<< endl;

	cout << "// This is a sample project which loads the Bridge Developer DLL" << endl;
	cout << "// Press 'h' to display the shortcuts list" << endl;
	cout << "// Press 'x' to stop the application" << endl;
	cout << "// ----------------------------------------------------------------------------------------\n" << endl;

	bool quitApp = false;
	EInitErrorCode initServerResponse = Init();

	// Attempt to make the connection to the Bridge runtime
	if ((int)initServerResponse == 0) {

		// Connection established, allow performing commands
		cout << ">> Connection to Bridge Runtime worked, type in a command:\n" << endl;

		do {
			if (_kbhit()) {
				char c = _getch();
				switch (c) {

					// Exiting the client application
					case 'x': {
						quitApp = true;
						EShutdownErrorCode shutdownServerResponse = Shutdown();
						break;
					}

					// Show help
					case 'h': {
						displayHelpMessage();
						break;
					}

					// Display the keyboard
					case 'q':
					{
						ESetKeyboardVisibilityErrorCode kbVisiServerResponse = SetKeyboardVisibility(true);

						if ((int) kbVisiServerResponse == 0) {
							cout << ">> Server -> Keyboard visible" << endl;
						}
						else {
							cout << ">> Server -> Keyboard visible returned an error: " << (int) kbVisiServerResponse << endl;
						}

						break;
					}

					// Hide the keyboard
					case 'w':
					{
						ESetKeyboardVisibilityErrorCode kbVisiServerResponse = SetKeyboardVisibility(false);

						if ((int) kbVisiServerResponse == 0) {
							cout << ">> Server -> Keyboard hidden" << endl;
						}
						else {
							cout << ">> Server -> Keyboard hidden returned an error: " << (int) kbVisiServerResponse << endl;
						}

						break;
					}

					// Set the hands representation mode to 'HANDS_SEGMENTATION'
					case 'e':
					{
						ESetHandsRepresentationModeErrorCode handModeServerResponse = SetHandsRepresentationMode(EHandsRepresentationMode::HANDS_SEGMENTATION);
					
						if ((int) handModeServerResponse == 0) {
							cout << ">> Server -> Render mode set to 'Hands segmentation'" << endl;
						}
						else {
							cout << ">> Server -> Set render mode to 'Hands segmentation' returned an error: " << (int) handModeServerResponse << endl;
						}
					
						break;
					}

					// Set the hands representation mode to 'SEETHRU'
					case 'r':
					{
						ESetHandsRepresentationModeErrorCode handModeServerResponse = SetHandsRepresentationMode(EHandsRepresentationMode::SEETHRU);
					
						if ((int) handModeServerResponse == 0) {
							cout << ">> Server -> Render mode set to 'Seethrough'" << endl;
						}
						else {
							cout << ">> Server -> Set render mode to 'Seethrough' returned an error: " << (int) handModeServerResponse << endl;
						}

						break;
					}

					// Set the hands color in 'HANDS_SEGMENTATION'
					case 't':
					{
						ESetHandsColorErrorCode handColorServerResponse = SetHandsColor(EHandsRepresentationMode::HANDS_SEGMENTATION, 0, 140, 245);
					
						if ((int) handColorServerResponse == 0) {
							cout << ">> Server -> Hands colors changed, in 'Hands segmentation' mode" << endl;
						}
						else {
							cout << ">> Server -> Changing hands colors in 'Hands segmentation' mode returned an error: " << (int) handColorServerResponse << endl;
						}
					
						break;
					}

					// Set the hands segmentation threshold to 30%
					case 'u':
					{
						ESetHandsSegmentationThresholdErrorCode handSegmentationThresholdServerResponse = SetHandsSegmentationThreshold(EHandsRepresentationMode::HANDS_SEGMENTATION, 0.3);
						
						if ((int) handSegmentationThresholdServerResponse == 0) {
							cout << ">> Server -> Segementation threshold changed to 30% in 'Hands segmentation' mode" << endl;
						}
						else {
							cout << ">> Server -> Changing segmentation threshold in 'Hands segmentation' mode returned an error: " << (int) handSegmentationThresholdServerResponse << endl;
						}
						
						break;
					}

					// Set the hands segmentation threshold to 10%
					case 'i':
					{
						ESetHandsSegmentationThresholdErrorCode handSegmentationThresholdServerResponse = SetHandsSegmentationThreshold(EHandsRepresentationMode::HANDS_SEGMENTATION, 0.1);
						
						if ((int) handSegmentationThresholdServerResponse == 0) {
							cout << ">> Server -> Segementation threshold changed to 10% in 'Hands segmentation' mode" << endl;
						}
						else {
							cout << ">> Server -> Changing segmentation threshold in 'Hands segmentation' mode returned an error: " << (int) handSegmentationThresholdServerResponse << endl;
						}

						break;
					}


					// Get the keyboard status (more fields available in the documentation)
					case 'a':
					{
						KeyboardStatus ks;
						EGetKeyboardStatusErrorCode keyboardStatusServerResponse = GetKeyboardStatus(&ks);

						if ((int)keyboardStatusServerResponse == 0) {
							cout << ">> Server -> Keyboard status:" << endl;
							cout << "\tvisible: " << ks.isVisible << ", tracker S/N: " << ks.pairedTrackerID << endl;
						}
						else {
							cout << ">> Server -> Getting the keyboard status returned an error: " << (int) keyboardStatusServerResponse << endl;
						}

						break;
					}

					// Get the hands status (more fields available in the documentation)
					case 's':
					{
						HandsStatus hs;
						EGetHandsStatusErrorCode handsStatusServerResponse = GetHandsStatus(&hs);

						if ((int)handsStatusServerResponse == 0) {
							cout << ">> Server -> Hands status:" << endl;
							printf("\tvisible: %d, mode: %d, color: (%d,%d,%d), opacity: %f, threshold: %f, hand tint: %d\n", hs.isVisible, (int)hs.handsMode, hs.colorR, hs.colorG, hs.colorB, hs.opacityLevel, hs.segmentationThreshold, hs.handTintOffset);
						}
						else {
							cout << ">> Server -> Getting the hands status returned an error: " << (int) handsStatusServerResponse << endl;
						}
						
						break;
					}

					// Set opacity level to 70% ('HANDS_SEGMENTATION' mode)
					case 'f':
					{
						ESetHandsOpacityErrorCode handOpacityServerResponse = SetHandsOpacity(EHandsRepresentationMode::HANDS_SEGMENTATION, 0.7);
						
						if ((int) handOpacityServerResponse == 0) {
							cout << ">> Server -> Opacity level set to 70% in 'Hands Segmentation' mode" << endl;
						}
						else {
							cout << ">> Server -> Setting opacity level to 70% in 'Hands Segmentation' mode returned an error: " << (int) handOpacityServerResponse << endl;
						}
						
						break;
					}


					// Change skin
					case 'c':
					{
						string input = "";
						cout << "Please enter a valid skin name: (Logitech, CrashTest, Battered, etc.)" << endl;
						getline(cin, input);

						ESetSkinErrorCode skinServerResponse = SetSkin(input);

						if ((int)skinServerResponse == 0) {
							cout << ">> Server -> Skin changed" << endl;
						}
						else {
							cout << ">> Server -> Skin change returned an error: " << (int)skinServerResponse << endl;
						}

						break;
					}

					// List supported keyboards with their respective skins
					case 'g':
					{
						SupportedKeyboards kbs;
						EGetSupportedKeyboardsErrorCode skinServerResponse = GetSupportedKeyboards(&kbs);

						if ((int)skinServerResponse == 0) {
							for (int i = 0; i < kbs.keyboards.size(); i++) {
								cout << kbs.keyboards[i].name << "\n" << endl;

								for (int j = 0; j < kbs.keyboards[i].skins.size(); j++) {
									cout << kbs.keyboards[i].skins[j].name << endl;
								}

								cout << "\n" << endl;
							}
						}
						else {
							cout << ">> Listing of supported keyboards returned an error: " << (int)skinServerResponse << endl;
						}

						break;
					}
				}
			}
			std::this_thread::sleep_for(std::chrono::milliseconds(100));
		} while (!quitApp);
	}
	else {
		// Something went wrong with the connection to the runtime
		cout << ">> Connection failed. Ensure the Logitech Bridge application is running before launching the sample app" << endl;
		cout << ">> Type any key to exit..." << endl;
		
		_getch();
		return 1;
	}

	return 0;
}

void displayHelpMessage() {
	cout <<
		">> Available shortcuts:\n\n" <<
		"\t'x' : Stop the application.\n" <<
		"\t'q' : Display the keyboard.\n" <<
		"\t'w' : Hide the keyboard.\n" <<
		"\t'e' : Set the hands representation mode to 'HANDS_SEGMENTATION'.\n" <<
		"\t'r' : Set the hands representation mode to 'SEETHRU'.\n" <<
		"\t't' : Set the hands color in 'HANDS_SEGMENTATION' mode to RGB values (0, 140, 245).\n" <<
		"\t'u' : Set the hands segmentation threshold to 30%.\n" <<
		"\t'i' : Set the hands segmentation threshold to 10%.\n" <<
#ifndef _DEBUG
		"\t'a' : Update the KeyboardStatus instance you passed as a parameter.\n" <<
		"\t's' : Update the HandsStatus instance you passed as a parameter.\n" <<
#endif
		"\t'f' : Set opacity level to 70% ('HANDS_SEGMENTATION' mode).\n" <<
#ifndef _DEBUG
		"\t'g' : Get supported keyboards.\n" <<
#endif
		"\t'c' : Set skin.\n"
		<< endl;
}

