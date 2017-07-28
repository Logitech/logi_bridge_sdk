using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BridgeSDKPlugin {

    [DllImport("SeraphOverlay_Dev_DLL_Unity")]
    unsafe static extern void UInit(byte* returnBuffer, int returnBufferSize);

    [DllImport("SeraphOverlay_Dev_DLL_Unity")]
    unsafe static extern void UShutdown(byte* returnBuffer, int returnBufferSize);

    [DllImport("SeraphOverlay_Dev_DLL_Unity")]
    unsafe static extern void USetKeyboardVisibility(byte* returnBuffer, int returnBufferSize, bool visible);

    [DllImport("SeraphOverlay_Dev_DLL_Unity")]
    unsafe static extern void USetHandsVisibility(byte* returnBuffer, int returnBufferSize, bool visible);

    [DllImport("SeraphOverlay_Dev_DLL_Unity")]
    unsafe static extern void USetHandsRepresentationMode(byte* returnBuffer, int returnBufferSize, int handsRepresentationMode);

    [DllImport("SeraphOverlay_Dev_DLL_Unity")]
    unsafe static extern void USetHandsColor(byte* returnBuffer, int returnBufferSize, int handsRepresentationMode, int colorR, int colorG, int colorB);

    [DllImport("SeraphOverlay_Dev_DLL_Unity")]
    unsafe static extern void UGetKeyboardStatus(byte* returnBuffer, int returnBufferSize);

    [DllImport("SeraphOverlay_Dev_DLL_Unity")]
    unsafe static extern void UGetHandsStatus(byte* returnBuffer, int returnBufferSize);




    int responseBufferSize = 2048;
    byte[] responseBuffer;
    string responseAsString;
    BaseServerJSONResponse baseServerResponse;
    KeyboardStatusServerJSONResponse keyboardStatusServerResponse;
    HandsStatusServerJSONResponse handsStatusServerResponse;


    public BridgeSDKPlugin()
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
        }

        return (BridgeEnums.EGetHandsStatusErrorCode)handsStatusServerResponse.error_code;

    }
}
