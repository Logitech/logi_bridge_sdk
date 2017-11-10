using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// -------------------------------------------------------------------------
// Bridge client code sample for Unity
// Copyright Logitech - 2017
// 
// This code sample allows Unity to communicate with the Bridge
// runtime.
//
// Note that the Bridge software has to run prior to running
// this code sample.
// -------------------------------------------------------------------------

public class BridgeSDK : MonoBehaviour
{
    // Singleton for ease of access
    private static BridgeSDK _instance;
    public static BridgeSDK Instance { get { return _instance; } }

    // State
    public BridgeSDKUnityPlugin bridgeSDK;
    bool initCalled;

    // --------------------------------------------------------------------
    // Create the singleton and make it visible to other classes
    // --------------------------------------------------------------------

    private void Awake()
    {
        // If a different instance exists, destroy the object
        if (_instance != null && _instance != this)
        {
            if (initCalled && bridgeSDK != null)
            {
                bridgeSDK.Shutdown();
            }

            Destroy(this.gameObject);
        }
        // If no instance exists, attempt to connect to the Bridge runtime
        else
        {
            bridgeSDK = new BridgeSDKUnityPlugin();
            initCalled = false;

            unsafe
            {
                BridgeEnums.EInitErrorCode errorCode = bridgeSDK.Init();

                if (errorCode == BridgeEnums.EInitErrorCode.SUCCESS)
                {
                    initCalled = true;
                    Debug.Log("Connected to Bridge runtime");
                }
                else
                {
                    Debug.Log("Failed to connect to Bridge runtime. Make sure"
                        + " the Bridge software is running prior to launching"
                        + " this code sample.");
                }
            }

            _instance = this;
        }
    }

    // Make sure we release the connection to the Bridge runtime upon exiting
    void OnApplicationQuit()
    {
        if (bridgeSDK != null && initCalled)
        {
            bridgeSDK.Shutdown();
        }
    }

    // --------------------------------------------------------------------
    // Example of wrapping functions to make them accessible in the Unity
    // UI system
    // --------------------------------------------------------------------

    public void SetKeyboardVisibility(bool visible) {
        bridgeSDK.SetKeyboardVisibility(visible);
    }

    public void SetHandsVisiblity(bool visible) {
        bridgeSDK.SetHandsVisibility(visible);
    }

    public void SetKeyboardSkin(string name) {
        bridgeSDK.SetSkin(name);
    }
}
