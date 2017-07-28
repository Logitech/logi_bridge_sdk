#pragma once
#include "BridgeEnums.h"

struct HandsStatus
{
	bool isVisible = false;
	EHandsRepresentationMode handsMode = EHandsRepresentationMode::GHOST;
	int colorR = 0; int colorG = 0; int colorB = 0;
};

