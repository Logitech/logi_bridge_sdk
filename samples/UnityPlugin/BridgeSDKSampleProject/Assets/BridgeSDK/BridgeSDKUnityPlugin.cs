using System.Runtime.InteropServices;
using UnityEngine;

/** \brief Wraps the API functions of the DLL application.

These functions are the one used to interact with Bridge.
*/
public class BridgeSDKUnityPlugin {
#if !DOXYGEN_SKIP
    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void UInit(byte* returnBuffer, int returnBufferSize, byte* clientName, int nameSize);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void UAutoAlignFor(byte* returnBuffer, int returnBufferSize, ushort milliseconds);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void UShutdown(byte* returnBuffer, int returnBufferSize);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void USetKeyboardVisibility(byte* returnBuffer, int returnBufferSize, bool visible);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void USetHandsVisibility(byte* returnBuffer, int returnBufferSize, bool visible);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void USetHandsRepresentationMode(byte* returnBuffer, int returnBufferSize, int handsRepresentationMode);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void USetHandsColor(byte* returnBuffer, int returnBufferSize, int handsRepresentationMode, int colorR, int colorG, int colorB);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void USetHandsSegmentationThreshold(byte* returnBuffer, int returnBufferSize, int handsRepresentationMode, float segmentationThreshold);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void USetHandsOpacity(byte* returnBuffer, int returnBufferSize, int handsRepresentationMode, float opacityLevel);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void USetAlternativeHandTintOffset(byte* returnBuffer, int returnBufferSize, int handTintOffset);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void UGetKeyboardStatus(byte* returnBuffer, int returnBufferSize);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void UGetSupportedKeyboards(byte* returnBuffer, int returnBufferSize);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void UGetHandsStatus(byte* returnBuffer, int returnBufferSize);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void USetSkin(byte* returnBuffer, int returnBufferSize, byte* skinName, int nameLength);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void USetAllKeysLEDColor(byte* returnBuffer, int returnBufferSize, int r, int g, int b, int a);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void USetKeyLEDColor(byte* returnBuffer, int returnBufferSize, int keyCode, int r, int g, int b, int a);

#endif // DOXYGEN_SKIP

    int responseBufferSize = 2048;
    byte[] responseBuffer;
    string responseAsString;
    BaseServerJSONResponse baseServerResponse;
    KeyboardStatusServerJSONResponse keyboardStatusServerResponse;
    HandsStatusServerJSONResponse handsStatusServerResponse;
    SupportedKeyboardsServerJSONResponse supportedKeyboardsServerResponse;
    
    public BridgeSDKUnityPlugin()
    {
        responseBuffer = new byte[responseBufferSize];
    }

    /** \brief Initializes the API with the backend runtime.

	Uses a blocking call for websocket initialization; call this outside the main thread unless you
	are ok with it being frozen for a while (especially, if Bridge is not launched this will timeout
	after several seconds).
	*/
    public BridgeEnums.EInitErrorCode Init(string clientName)
    {
        byte[] clientNameBytes = System.Text.Encoding.ASCII.GetBytes(clientName);
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                fixed (byte* clientNameBytesPtr = &clientNameBytes[0])
                {
                    UInit(responseBufferPtr, responseBufferSize, clientNameBytesPtr, clientNameBytes.Length);
                }
            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length);
        }

        // Parse json message we received:
        baseServerResponse = JsonUtility.FromJson<BaseServerJSONResponse>(responseAsString);
        return (BridgeEnums.EInitErrorCode)baseServerResponse.error_code;
    }

    /**	\brief Stops the DLL.
	
	\ref Init should be called again afterwards if one wants to use the API again.
	*/
    public BridgeEnums.EShutdownErrorCode Shutdown()
    {
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                UShutdown(responseBufferPtr, responseBufferSize);
            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length); 
        }

        // Parse json message we received:
        baseServerResponse = JsonUtility.FromJson<BaseServerJSONResponse>(responseAsString);
        return (BridgeEnums.EShutdownErrorCode)baseServerResponse.error_code;

    }

    /**	\brief Sets whether the keyboard should be visible or not.
	*/
    public BridgeEnums.ESetKeyboardVisibilityErrorCode SetKeyboardVisibility(bool visible)
    {
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                USetKeyboardVisibility(responseBufferPtr, responseBufferSize, visible);
            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length); 
        }

        // Parse json message we received:
        baseServerResponse = JsonUtility.FromJson<BaseServerJSONResponse>(responseAsString);
        return (BridgeEnums.ESetKeyboardVisibilityErrorCode)baseServerResponse.error_code;
    }

    /**	\brief Gets a list of the available \ref SupportedKeyboard and their respective \ref SupportedSkin list.

	The result is stored in the supplied memory space given as argument.
	*/
    public BridgeEnums.EGetSupportedKeyboardsErrorCode GetSupportedKeyboards(ref SupportedKeyboards sk)
    {
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                UGetSupportedKeyboards(responseBufferPtr, responseBufferSize);
            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length);
        }

        // Parse json message we received:
        supportedKeyboardsServerResponse = JsonUtility.FromJson<SupportedKeyboardsServerJSONResponse>(responseAsString);

        if ((BridgeEnums.EGetSupportedKeyboardsErrorCode)supportedKeyboardsServerResponse.error_code == BridgeEnums.EGetSupportedKeyboardsErrorCode.SUCCESS)
        {
            sk.LoadFromJSON(supportedKeyboardsServerResponse);
        }

        return (BridgeEnums.EGetSupportedKeyboardsErrorCode)supportedKeyboardsServerResponse.error_code;
    }

    /**	\brief Sets a skin for the current keyboard.

	\param skinName The name of the skin to be set

	\returns SUCESS if the skin could be set\n
	INVALID_INPUT if the provided skin name does not exist\n
	or other members of \ref BridgeEnums.ESetSkinErrorCode depending on the encountered
	error.
	*/
    public BridgeEnums.ESetSkinErrorCode SetSkin(string skinName)
    {
        byte[] skinNameBytes = System.Text.Encoding.ASCII.GetBytes(skinName);
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                fixed (byte* skinNameBytesPtr = &skinNameBytes[0])
                {
                    USetSkin(responseBufferPtr, responseBufferSize, skinNameBytesPtr, skinNameBytes.Length);
                }
            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length);
        }

        // Parse json message we received:
        baseServerResponse = JsonUtility.FromJson<BaseServerJSONResponse>(responseAsString);
        return (BridgeEnums.ESetSkinErrorCode)baseServerResponse.error_code;

    }

    /**	\brief Sets whether the hands should be visible on top of the keyboard or not.

	If the keyboard is hidden, whether it is on or off has no impact on performances.
	*/
    public BridgeEnums.ESetHandsVisibilityErrorCode SetHandsVisibility(bool visible)
    {
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                USetHandsVisibility(responseBufferPtr, responseBufferSize, visible);
            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length); 
        }

        // Parse json message we received:
        baseServerResponse = JsonUtility.FromJson<BaseServerJSONResponse>(responseAsString);
        return (BridgeEnums.ESetHandsVisibilityErrorCode)baseServerResponse.error_code;

    }

    /**	\brief Changes the displayed \ref BridgeEnums.EHandsRepresentationMode.
	*/
    public BridgeEnums.ESetHandsRepresentationModeErrorCode SetHandsRepresentationMode(BridgeEnums.EHandsRepresentationMode eHandsRepresentationMode)
    {
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                USetHandsRepresentationMode(responseBufferPtr, responseBufferSize, (int)eHandsRepresentationMode);
            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length); 
        }

        // Parse json message we received:
        baseServerResponse = JsonUtility.FromJson<BaseServerJSONResponse>(responseAsString);
        return (BridgeEnums.ESetHandsRepresentationModeErrorCode)baseServerResponse.error_code;

    }

    /** \brief Sets the color for a given \ref BridgeEnums.EHandsRepresentationMode.

	R, G and B should be integers between 0 and 255.
	*/
    public BridgeEnums.ESetHandsColorErrorCode SetHandsColor(BridgeEnums.EHandsRepresentationMode eHandsRepresentationMode, int colorR, int colorG, int colorB)
    {
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                USetHandsColor(responseBufferPtr, responseBufferSize, (int)eHandsRepresentationMode, colorR, colorG, colorB);
            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length); 
        }

        // Parse json message we received:
        baseServerResponse = JsonUtility.FromJson<BaseServerJSONResponse>(responseAsString);
        return (BridgeEnums.ESetHandsColorErrorCode)baseServerResponse.error_code;

    }

    /**	\brief Sets the hand opacity for a given \ref BridgeEnums.EHandsRepresentationMode mode.

	An opacity level of 0 corresponds to 100% transparency.
	*/
    public BridgeEnums.ESetHandsOpacityErrorCode SetHandsOpacity(BridgeEnums.EHandsRepresentationMode eHandsRepresentationMode, float opacityLevel)
    {
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                USetHandsOpacity(responseBufferPtr, responseBufferSize, (int)eHandsRepresentationMode, opacityLevel);


            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length);
        }

        // Parse json message we received:
        baseServerResponse = JsonUtility.FromJson<BaseServerJSONResponse>(responseAsString);
        return (BridgeEnums.ESetHandsOpacityErrorCode)baseServerResponse.error_code;
    }

#if !DOXYGEN_SKIP // Can be used, but is not exposed in the doc
    /**	\brief Sets hand tint for the alternative segmentation mode.
	*/
    public BridgeEnums.ESetAlternativeHandTintOffsetErrorCode SetAlternativeHandTintOffset(int handTintOffset)
    {
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                USetAlternativeHandTintOffset(responseBufferPtr, responseBufferSize, handTintOffset);
            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length);
        }

        // Parse json message we received:
        baseServerResponse = JsonUtility.FromJson<BaseServerJSONResponse>(responseAsString);
        return (BridgeEnums.ESetAlternativeHandTintOffsetErrorCode)baseServerResponse.error_code;
    }
#endif

    /**	\brief Sets the hand segmentation threshold for a given \ref BridgeEnums.EHandsRepresentationMode mode.

	For the default HANDS_SEGMENTATION mode, this represents a lightness threshold above which pixels are displayed.
	The value is discarded for SEETHRU mode, and represents a hue width value for ALTERNATIVE_SEGMENTATION.
	*/
    public BridgeEnums.ESetHandsSegmentationThresholdErrorCode SetHandsSegmentationThreshold(BridgeEnums.EHandsRepresentationMode eHandsRepresentationMode, float segmentationThreshold)
    {
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                USetHandsSegmentationThreshold(responseBufferPtr, responseBufferSize, (int)eHandsRepresentationMode, segmentationThreshold);


            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length);
        }

        // Parse json message we received:
        baseServerResponse = JsonUtility.FromJson<BaseServerJSONResponse>(responseAsString);
        return (BridgeEnums.ESetHandsSegmentationThresholdErrorCode)baseServerResponse.error_code;
    }

    /**	\brief Gets information about the keyboard visualization.

	The result is stored in the supplied memory space given as argument.
	*/
    public BridgeEnums.EGetKeyboardStatusErrorCode GetKeyboardStatus(ref KeyboardStatus ks)
    {
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                UGetKeyboardStatus(responseBufferPtr, responseBufferSize);
            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length); 
        }

        // Parse json message we received:
        keyboardStatusServerResponse = JsonUtility.FromJson<KeyboardStatusServerJSONResponse>(responseAsString);

        if ((BridgeEnums.EGetKeyboardStatusErrorCode)keyboardStatusServerResponse.error_code == BridgeEnums.EGetKeyboardStatusErrorCode.SUCCESS)
        {
            ks.isVisible = keyboardStatusServerResponse.status.visible;
            ks.pairedTrackerID = keyboardStatusServerResponse.status.paired_tracker_id;
        }

        return (BridgeEnums.EGetKeyboardStatusErrorCode)keyboardStatusServerResponse.error_code;

    }

    /**	\brief Gets information about the hand visualization.

	The result is stored in the supplied memory space given as argument.
	*/
    public BridgeEnums.EGetHandsStatusErrorCode GetHandsStatus(ref HandsFeedbackStatus hs)
    {
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                UGetHandsStatus(responseBufferPtr, responseBufferSize);
            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length); 
        }

        // Parse json message we received:
        handsStatusServerResponse = JsonUtility.FromJson<HandsStatusServerJSONResponse>(responseAsString);

        if ((BridgeEnums.EGetHandsStatusErrorCode)handsStatusServerResponse.error_code == BridgeEnums.EGetHandsStatusErrorCode.SUCCESS)
        {
            hs.isVisible = handsStatusServerResponse.status.visible;
            hs.eHandsRepresentationMode = (BridgeEnums.EHandsRepresentationMode)handsStatusServerResponse.status.hands_representation;
            hs.color = handsStatusServerResponse.status.color;
            hs.opacityLevel = handsStatusServerResponse.status.opacity_level;
            hs.segmentationThreshold = handsStatusServerResponse.status.segmentation_threshold;
        }

        return (BridgeEnums.EGetHandsStatusErrorCode)handsStatusServerResponse.error_code;

    }

    /** \brief Sets the LED color for all keys.

    R, G and B should be integers between 0 and 255.
    */
    public BridgeEnums.ESetAllKeysLEDErrorCode SetAllKeysLEDColor(int colorR, int colorG, int colorB, int colorA)
    {
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                USetAllKeysLEDColor(responseBufferPtr, responseBufferSize, colorR, colorG, colorB, colorA);
            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length);
        }

        // Parse json message we received:
        baseServerResponse = JsonUtility.FromJson<BaseServerJSONResponse>(responseAsString);
        return (BridgeEnums.ESetAllKeysLEDErrorCode)baseServerResponse.error_code;
    }

    /** \brief Sets the LED color for a specific key using keyCode (scan code).

    R, G and B should be integers between 0 and 255.
    */
    public BridgeEnums.ESetKeyLEDErrorCode SetKeyLEDColor(int keyCode, int colorR, int colorG, int colorB, int colorA)
    {
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                USetKeyLEDColor(responseBufferPtr, responseBufferSize, keyCode, colorR, colorG, colorB, colorA);
            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length);
        }

        // Parse json message we received:
        baseServerResponse = JsonUtility.FromJson<BaseServerJSONResponse>(responseAsString);
        return (BridgeEnums.ESetKeyLEDErrorCode)baseServerResponse.error_code;
    }

    /**	\brief Starts automatic alignment for a given amount of time.
	*/
    public BridgeEnums.EAutoAlignForErrorCode AutoAlignFor(ushort milliseconds)
    {
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                UAutoAlignFor(responseBufferPtr, responseBufferSize, milliseconds);
            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length);
        }

        // Parse json message we received:
        baseServerResponse = JsonUtility.FromJson<BaseServerJSONResponse>(responseAsString);
        return (BridgeEnums.EAutoAlignForErrorCode)baseServerResponse.error_code;
    }
}
