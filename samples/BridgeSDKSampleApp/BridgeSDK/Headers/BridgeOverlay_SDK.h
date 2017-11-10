#pragma once
#include "BridgeEnums.h"
#include "KeyboardStatus.h"
#include "HandsStatus.h"
#include "SupportedKeyboards.h"
#include <queue>

#define BRIDGE_OVERLAY_SDK_API __declspec(dllexport)

using namespace std;


extern "C" {
	
	BRIDGE_OVERLAY_SDK_API EInitErrorCode Init();
	BRIDGE_OVERLAY_SDK_API EShutdownErrorCode Shutdown();

	
	BRIDGE_OVERLAY_SDK_API EGetKeyboardStatusErrorCode GetKeyboardStatus(KeyboardStatus* keyboardStatus);
	BRIDGE_OVERLAY_SDK_API ESetKeyboardVisibilityErrorCode SetKeyboardVisibility(bool visible);
	BRIDGE_OVERLAY_SDK_API EGetSupportedKeyboardsErrorCode GetSupportedKeyboards(SupportedKeyboards* supportedKeyboards);
	BRIDGE_OVERLAY_SDK_API ESetSkinErrorCode SetSkin(string skinName);

	BRIDGE_OVERLAY_SDK_API EGetHandsStatusErrorCode GetHandsStatus(HandsStatus* handsStatus);	
	BRIDGE_OVERLAY_SDK_API ESetHandsVisibilityErrorCode SetHandsVisibility(bool visible);
	BRIDGE_OVERLAY_SDK_API ESetHandsRepresentationModeErrorCode SetHandsRepresentationMode(EHandsRepresentationMode eHandsRepresentationMode);	
	BRIDGE_OVERLAY_SDK_API ESetHandsSegmentationThresholdErrorCode SetHandsSegmentationThreshold(EHandsRepresentationMode eHandsRepresentationMode, float segmentationThreshold);
	BRIDGE_OVERLAY_SDK_API ESetHandsOpacityErrorCode SetHandsOpacity(EHandsRepresentationMode eHandsRepresentationMode, float opacity_level);
	BRIDGE_OVERLAY_SDK_API ESetHandsColorErrorCode SetHandsColor(EHandsRepresentationMode eHandsRepresentationMode, int R, int G, int B);
}
