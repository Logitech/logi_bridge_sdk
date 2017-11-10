using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BridgeSDKUnityPlugin {

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void UInit(byte* returnBuffer, int returnBufferSize);

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
    unsafe static extern void USendIPMessage(byte* returnBuffer, int returnBufferSize, int IPMessage);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void UGetKeyboardStatus(byte* returnBuffer, int returnBufferSize);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void UGetSupportedKeyboards(byte* returnBuffer, int returnBufferSize);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void UGetHandsStatus(byte* returnBuffer, int returnBufferSize);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void USetSkin(byte* returnBuffer, int returnBufferSize, byte* skinName, int nameLength);

    [DllImport("BridgeOverlay_SDK_Unity")]
    unsafe static extern void UNotifyRuntime(byte* returnBuffer, int returnBufferSize, int notification);

    [DllImport("BridgeOverlay_SDK_Unity")]
    static extern int UPopNotification();




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
	

    public BridgeEnums.EInitErrorCode Init()
    {
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                UInit(responseBufferPtr, responseBufferSize);
            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length); 
        }

        // Parse json message we received:
        baseServerResponse = JsonUtility.FromJson<BaseServerJSONResponse>(responseAsString);
        return (BridgeEnums.EInitErrorCode)baseServerResponse.error_code;

    }

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

    /// <summary>
    /// Function to set the keyboard to visible / hidden 
    /// </summary>
    /// <param name="visible">True to set to visible, false to set to hidden</param>
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

    public BridgeEnums.ESendIPMessageErrorCode SendIPMessage(BridgeEnums.EIPMessages IPMessage)
    {
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                USendIPMessage(responseBufferPtr, responseBufferSize, (int)IPMessage);
            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length);
        }

        // Parse json message we received:
        baseServerResponse = JsonUtility.FromJson<BaseServerJSONResponse>(responseAsString);
        return (BridgeEnums.ESendIPMessageErrorCode)baseServerResponse.error_code;
    }

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

    public BridgeEnums.ENotifyRuntimeErrorCode NotifyRuntime(BridgeEnums.ENotification message)
    {
        unsafe
        {
            fixed (byte* responseBufferPtr = &responseBuffer[0])
            {
                UNotifyRuntime(responseBufferPtr, responseBufferSize, (int)message);
            }
            responseAsString = System.Text.Encoding.ASCII.GetString(responseBuffer, 0, responseBuffer.Length);
        }

        // Parse json message we received:
        baseServerResponse = JsonUtility.FromJson<BaseServerJSONResponse>(responseAsString);
        return (BridgeEnums.ENotifyRuntimeErrorCode)baseServerResponse.error_code;
    }
    
    public BridgeEnums.ENotification PopNotification()
    {
        return (BridgeEnums.ENotification)UPopNotification();
    }
}
