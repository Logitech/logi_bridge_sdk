#pragma once
#include "BridgeEnums.h"

/**
	Used to represent the state of hand representation.
*/
struct HandsStatus
{
	bool isVisible = false;
	EHandsRepresentationMode handsMode = EHandsRepresentationMode::HANDS_SEGMENTATION;
	int colorR = 0; int colorG = 0; int colorB = 0;
	float opacityLevel = 0.0;
	float segmentationThreshold = 0.0;
	int handTintOffset = 0; ///< Only used in alternative segmentation hand mode.
};

