#pragma once
#include "BridgeEnums.h"
#include "KeyboardStatus.h"
#include "HandsStatus.h"


#define SERAPH_OVERLAY_DEV_DLL_API __declspec(dllexport)

using namespace std;


extern "C" {

	// API functions exposed for the developers should be declared here:

	//Template
	/*
	Doc goes here.
	*/
	//SERAPH_OVERLAY_DLL_API void MyFun();
	
	SERAPH_OVERLAY_DEV_DLL_API EInitErrorCode Init();
	SERAPH_OVERLAY_DEV_DLL_API EShutdownErrorCode Shutdown();

	SERAPH_OVERLAY_DEV_DLL_API ESetKeyboardVisibilityErrorCode SetKeyboardVisibility(bool visible);

	SERAPH_OVERLAY_DEV_DLL_API ESetHandsVisibilityErrorCode SetHandsVisibility(bool visible);

	SERAPH_OVERLAY_DEV_DLL_API ESetHandsRepresentationModeErrorCode SetHandsRepresentationMode(EHandsRepresentationMode eHandsRepresentationMode);

	SERAPH_OVERLAY_DEV_DLL_API ESetHandsColorErrorCode SetHandsColor(EHandsRepresentationMode eHandsRepresentationMode, int R, int G, int B);

	SERAPH_OVERLAY_DEV_DLL_API EGetKeyboardStatusErrorCode GetKeyboardStatus(KeyboardStatus* keyboardStatus);

	SERAPH_OVERLAY_DEV_DLL_API EGetHandsStatusErrorCode GetHandsStatus(HandsStatus* handsStatus);
}