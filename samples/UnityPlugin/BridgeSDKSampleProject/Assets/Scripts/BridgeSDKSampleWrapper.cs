using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSDKSampleWrapper : MonoBehaviour {

    BridgeSDKPlugin bridgeSDK;
    bool initCalled;

    void Start()
    {
        bridgeSDK = new BridgeSDKPlugin();
        initCalled = false;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            BridgeEnums.EInitErrorCode errorCode = bridgeSDK.Init();
            if(errorCode == BridgeEnums.EInitErrorCode.SUCCESS)
            {
                initCalled = true;
            }
            Debug.Log("Error Code: " + errorCode);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            BridgeEnums.EShutdownErrorCode errorCode = bridgeSDK.Shutdown();
            if (errorCode == BridgeEnums.EShutdownErrorCode.SUCCESS)
            {
                initCalled = false;
            }
            Debug.Log("Error Code: " + errorCode);
        }

        // ===== Keyboard
        else if (Input.GetKeyDown(KeyCode.A))
        {
            BridgeEnums.ESetKeyboardVisibilityErrorCode errorCode = bridgeSDK.SetKeyboardVisibility(true);
            Debug.Log("Error Code: " + errorCode);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            BridgeEnums.ESetKeyboardVisibilityErrorCode errorCode = bridgeSDK.SetKeyboardVisibility(false);
            Debug.Log("Error Code: " + errorCode);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            KeyboardStatus ks = new KeyboardStatus();
            BridgeEnums.EGetKeyboardStatusErrorCode errorCode = bridgeSDK.GetKeyboardStatus(ref ks);
            Debug.Log("Error Code: " + errorCode);
            if(errorCode == BridgeEnums.EGetKeyboardStatusErrorCode.SUCCESS)
            {
                Debug.Log("Keyboard status: " + ks.isVisible + ", " + ks.pairedTrackerID);
            }
        }
        
        // ===== Hands
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            BridgeEnums.ESetHandsVisibilityErrorCode errorCode = bridgeSDK.SetHandsVisibility(true);
            Debug.Log("Error Code: " + errorCode);
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            BridgeEnums.ESetHandsVisibilityErrorCode errorCode = bridgeSDK.SetHandsVisibility(false);
            Debug.Log("Error Code: " + errorCode);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            BridgeEnums.ESetHandsRepresentationModeErrorCode errorCode = bridgeSDK.SetHandsRepresentationMode(BridgeEnums.EHandsRepresentationMode.GHOST);
            Debug.Log("Error Code: " + errorCode);
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            BridgeEnums.ESetHandsRepresentationModeErrorCode errorCode = bridgeSDK.SetHandsRepresentationMode(BridgeEnums.EHandsRepresentationMode.SEETHRU);
            Debug.Log("Error Code: " + errorCode);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            BridgeEnums.ESetHandsColorErrorCode errorCode = bridgeSDK.SetHandsColor(BridgeEnums.EHandsRepresentationMode.GHOST, 0, 140, 245);
            Debug.Log("Error Code: " + errorCode);
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            BridgeEnums.ESetHandsColorErrorCode errorCode = bridgeSDK.SetHandsColor(BridgeEnums.EHandsRepresentationMode.GHOST, 127, 255, 127);
            Debug.Log("Error Code: " + errorCode);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            HandsFeedbackStatus hs = new HandsFeedbackStatus();
            BridgeEnums.EGetHandsStatusErrorCode errorCode = bridgeSDK.GetHandsStatus(ref hs);
            Debug.Log("Error Code: " + errorCode);
            if (errorCode == BridgeEnums.EGetHandsStatusErrorCode.SUCCESS)
            {
                Debug.Log("Hands feedback status: " + hs.isVisible + ", " + hs.eHandsRepresentationMode + ", (" + hs.color[0] + ", " + hs.color[1] + ", " + hs.color[2] + ")");
            }
        }

    }

    void OnApplicationQuit()
    {
        if (initCalled)
        {
            bridgeSDK.Shutdown();
        }
    }

}
