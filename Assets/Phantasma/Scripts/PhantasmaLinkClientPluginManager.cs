using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class PhantasmaLinkClientPluginManager : MonoBehaviour
{
    public static PhantasmaLinkClientPluginManager Instance { get; private set; }
    [SerializeField] private const string PluginName = "com.phantasma.phantasmalinkclient.PhantasmaLinkClientClass";
    
    private AndroidJavaClass UnityClass;
    private AndroidJavaObject UnityActivity;
    private AndroidJavaObject _PluginInstance;
    [SerializeField] private TMP_Text DebugOutput;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
#if UNITY_ANDROID || UNITY_EDITOR
        InitializePlugin(PluginName);
#endif
    }

    private void InitializePlugin(string pluginName)
    {
#if UNITY_ANDROID
        UnityClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        UnityActivity = UnityClass.GetStatic<AndroidJavaObject>("currentActivity");
        _PluginInstance = new AndroidJavaObject(pluginName);
        if (_PluginInstance == null)
        {
            Debug.LogError("Error Loading Plugin..");
        }
        
        _PluginInstance.CallStatic("ReceiveActivity", UnityActivity);
        PhantasmaLinkClient.Instance.Enable();
#endif
    }

    public void OnDoSomething()
    {
#if UNITY_ANDROID
        var result = _PluginInstance.Call<string>("DoSomething");
        if (DebugOutput != null )
            DebugOutput.text = $"Something: {result}";
#endif
    }

    public void OpenWallet()
    {
        #if UNITY_ANDROID
        _PluginInstance.Call("OpenWallet");
        #endif
    } 


    public async Task SendTransaction(string tx)
    {
        #if UNITY_ANDROID
        var result = _PluginInstance.Call<string>("SendMyCommand", tx);
        await Task.Delay(0);
        #endif
    }

    public void Example()
    {
        #if UNITY_ANDROID
        PhantasmaLinkClient.Instance.Login();
        #endif
    }

    public void HandleResult()
    {
        
    }
}
