using System.Collections;
using UnityEngine;

// Bridge client code sample for Unity
// Copyright Logitech - 2017

/** \brief This code sample shows a set of example calls to the Bridge runtime.

Note that the Bridge software has to run prior to running
this code sample.
*/

public class BridgeSampleScript : MonoBehaviour {

    int errorCode;

    // Listen to keys events
    void Update () {
        if (Input.anyKeyDown) {
            switch (Input.inputString)
            {
                // ---------------------------------------------------------------------
                // Keyboard functions examples
                // ---------------------------------------------------------------------

                case "a":
                    // Set keyboard to visible
                    errorCode = (int)BridgeSDK.Instance.bridgeSDK.SetKeyboardVisibility(true);
                    Debug.Log(errorCode == 0 ? "Keyboard is now visible" : "Error with code: " + errorCode);
                    break;

                case "s":
                    // Set keyboard to hidden
                    errorCode = (int)BridgeSDK.Instance.bridgeSDK.SetKeyboardVisibility(false);
                    Debug.Log(errorCode == 0 ? "Keyboard is now hidden" : "Error with code: " + errorCode);
                    break;

                case "d":
                    // Get keyboard status
                    KeyboardStatus ks = new KeyboardStatus();
                    errorCode = (int)BridgeSDK.Instance.bridgeSDK.GetKeyboardStatus(ref ks);
                    Debug.Log(errorCode == 0 ? ("Keyboard visible: " + ks.isVisible + ", id: '" + ks.pairedTrackerID + "'") : ("Error with code: " + errorCode));
                    break;

                case "f":
                    // Get supported keyboards and their respective skins
                    SupportedKeyboards sk = new SupportedKeyboards();
                    errorCode = (int)BridgeSDK.Instance.bridgeSDK.GetSupportedKeyboards(ref sk);
                    Debug.Log(errorCode == 0 ? (sk.Print()) : "Error with code: " + errorCode);
                    break;

                case "g":
                    // Set the "CrashTest" skin
                    errorCode = (int)BridgeSDK.Instance.bridgeSDK.SetSkin("CrashTest"); ;
                    Debug.Log(errorCode == 0 ? "Skin CrashTest applied" : "Error with code: " + errorCode);
                    break;

                case "t":
                    // Launch a kit 2000-like demo effect using the VR LEDs
                    StartCoroutine(Kit2000());
                    break;

                // ---------------------------------------------------------------------
                // Hands functions examples
                // ---------------------------------------------------------------------

                case "z":
                    // Set hands to visible
                    errorCode = (int)BridgeSDK.Instance.bridgeSDK.SetHandsVisibility(true);
                    Debug.Log(errorCode == 0 ? "Hands are now visible" : "Error with code: " + errorCode);
                    break;

                case "x":
                    // Set hands to hidden
                    errorCode = (int)BridgeSDK.Instance.bridgeSDK.SetHandsVisibility(false);
                    Debug.Log(errorCode == 0 ? "Hands are now hidden" : "Error with code: " + errorCode);
                    break;

                case "c":
                    // Get hands status
                    HandsFeedbackStatus hs = new HandsFeedbackStatus();
                    errorCode = (int)BridgeSDK.Instance.bridgeSDK.GetHandsStatus(ref hs);
                    Debug.Log(errorCode == 0 ? ("Hands feedback status: " + hs.isVisible + ", " + hs.eHandsRepresentationMode + ", (" + hs.color[0] + ", " + hs.color[1] + ", " + hs.color[2] + "), " + hs.opacityLevel + "," + hs.segmentationThreshold) : "Error with code: " + errorCode);
                    break;

                case "v":
                    // Set hands representation to "Hands Segmentation"
                    errorCode = (int)BridgeSDK.Instance.bridgeSDK.SetHandsRepresentationMode(BridgeEnums.EHandsRepresentationMode.HANDS_SEGMENTATION);
                    Debug.Log(errorCode == 0 ? "Hands representation set to 'Hands Segmentation'" : "Error with code: " + errorCode);
                    break;

                case "b":
                    // Set hands representation to "Seethrough"
                    errorCode = (int)BridgeSDK.Instance.bridgeSDK.SetHandsRepresentationMode(BridgeEnums.EHandsRepresentationMode.SEETHRU);
                    Debug.Log(errorCode == 0 ? "Hands representation set to 'Seethrough'" : "Error with code: " + errorCode);
                    break;

                case "n":
                    // Set random hand color in 'Hands segmentation' mode
                    errorCode = (int)BridgeSDK.Instance.bridgeSDK.SetHandsColor(BridgeEnums.EHandsRepresentationMode.HANDS_SEGMENTATION, Random.Range(0, 255), Random.Range(0, 255), Random.Range(0, 255));
                    Debug.Log(errorCode == 0 ? "Hands set to random color in 'Hands Segmentation' mode" : "Error with code: " + errorCode);
                    break;

                case "m":
                    // Set Hands Segmentation threshold to 10%
                    errorCode = (int)BridgeSDK.Instance.bridgeSDK.SetHandsSegmentationThreshold(BridgeEnums.EHandsRepresentationMode.HANDS_SEGMENTATION, 0.1f);
                    Debug.Log(errorCode == 0 ? "Hands Segmentation threshold set to 10%" : "Error with code: " + errorCode);
                    break;

                case "j":
                    // Set Hands Segmentation threshold to 30%
                    errorCode = (int)BridgeSDK.Instance.bridgeSDK.SetHandsSegmentationThreshold(BridgeEnums.EHandsRepresentationMode.HANDS_SEGMENTATION, 0.1f);
                    Debug.Log(errorCode == 0 ? "Hands Segmentation threshold set to 30%" : "Error with code: " + errorCode);
                    break;

                case "k":
                    // Set Hands opacity to 70%
                    errorCode = (int)BridgeSDK.Instance.bridgeSDK.SetHandsOpacity(BridgeEnums.EHandsRepresentationMode.HANDS_SEGMENTATION, 1.0f);
                    Debug.Log(errorCode == 0 ? "Hands Segmentation opacity set to 100%" : "Error with code: " + errorCode);
                    break;

                case "l":
                    // Set Hands opacity to 70%
                    errorCode = (int)BridgeSDK.Instance.bridgeSDK.SetHandsOpacity(BridgeEnums.EHandsRepresentationMode.HANDS_SEGMENTATION, 0.7f);
                    Debug.Log(errorCode == 0 ? "Hands Segmentation opacity set to 70%" : "Error with code: " + errorCode);
                    break;

                case "p":
                    // Auto align for 5s
                    errorCode = (int)BridgeSDK.Instance.bridgeSDK.AutoAlignFor(5000);
                    Debug.Log(errorCode == 0 ? "Auto align started for 5 seconds." : "Error with code: " + errorCode);
                    break;
            }
        }
    }

    IEnumerator Kit2000()
    {
        BridgeSDK.Instance.bridgeSDK.SetAllKeysLEDColor(50, 50, 50, 100);
        int[] keyCodesArray = { 65, 83, 68, 70, 71, 72, 74, 75, 76 };

        for (int i = 0; i < keyCodesArray.Length; i++)
        {
            BridgeSDK.Instance.bridgeSDK.SetKeyLEDColor(keyCodesArray[i], 255, 0, 0, 100);
            yield return new WaitForSeconds(.05f);
            BridgeSDK.Instance.bridgeSDK.SetKeyLEDColor(keyCodesArray[i], 50, 50, 50, 100);
        }

        yield return new WaitForSeconds(.05f);

        for (int j = keyCodesArray.Length - 1; j >= 0 ; j--)
        {
            BridgeSDK.Instance.bridgeSDK.SetKeyLEDColor(keyCodesArray[j], 255, 0, 0, 100);
            yield return new WaitForSeconds(.05f);
            BridgeSDK.Instance.bridgeSDK.SetKeyLEDColor(keyCodesArray[j], 50, 50, 50, 100);
        }

        // What really matters here is the 0 alpha, meaning that we resume the
        // skin-defined color for each key print
        BridgeSDK.Instance.bridgeSDK.SetAllKeysLEDColor(255, 255, 255, 0);

        yield return null;
    }
}
