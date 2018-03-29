using UnityEngine;

// Bridge client code sample for Unity
// Copyright Logitech - 2017

/** \brief MonoBehaviour script giving access to the Bridge API
 
 This component allows Unity to communicate with the Bridge
 runtime.

 Note that the Bridge software has to run prior to running
 this code sample.
*/
public class BridgeSDK : MonoBehaviour
{
    private static BridgeSDK _instance;
    /// Singleton for ease of access
    public static BridgeSDK Instance { get { return _instance; } }
    
    /// Gives access to the set of API calls.
    public BridgeSDKUnityPlugin bridgeSDK;
    bool initCalled;

    /// Allow changing the name from the editor
    [Tooltip("Change this to your application name")]
    public string appName;
    
    /** \brief Create the singleton and make it visible to other classes
     */
    private void Awake()
    {
        // If a different instance exists, destroy the object
        if (_instance != null && _instance != this)
        {
            if (initCalled && bridgeSDK != null)
            {
                bridgeSDK.Shutdown();
            }

            Debug.LogWarning("You seem to have more than one BridgeSDK instance in your scene.");
            Destroy(this.gameObject);
        }
        // If no instance exists, attempt to connect to the Bridge runtime
        else
        {
            bridgeSDK = new BridgeSDKUnityPlugin();
            initCalled = false;

            unsafe
            {
                BridgeEnums.EInitErrorCode errorCode = bridgeSDK.Init(appName);

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
    
    /** \brief Make sure we release the connection to the Bridge runtime upon exiting
     */
    void OnApplicationQuit()
    {
        if (bridgeSDK != null && initCalled)
        {
            bridgeSDK.Shutdown();
        }
    }

    /** \brief Example of wrapping the hand visibility function
    
        This is used by the buttons of the UI in the example scene.
    */
    public void SetKeyboardVisibility(bool visible)
    {
        bridgeSDK.SetKeyboardVisibility(visible);
    }

    /** \brief Example of wrapping the keyboard visibility function
    
        This is used by the buttons of the UI in the example scene.
    */
    public void SetHandsVisiblity(bool visible)
    {
        bridgeSDK.SetHandsVisibility(visible);
    }

    /** \brief Example of wrapping the keyboard skin function
    
        This is used by the buttons of the UI in the example scene.
    */
    public void SetKeyboardSkin(string name)
    {
        bridgeSDK.SetSkin(name);
    }
}
