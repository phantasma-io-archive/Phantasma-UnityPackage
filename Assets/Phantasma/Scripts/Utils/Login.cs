using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login : MonoBehaviour
{
    #region Events
    public event Action<string,bool> OnLoginEvent;
    #endregion

    /// <summary>
    /// Method used to connect to the wallet.
    /// </summary>
    public void OnLogin()
    {
        if (PhantasmaLinkClient.Instance.Ready)
        {
            if (!PhantasmaLinkClient.Instance.IsLogged)
            {
                Debug.Log("Phantasma Link authorization logged.");
                PhantasmaLinkClient.Instance.Login((result, msg) =>
                {
                    Debug.Log(msg);
                    if (result)
                    {
                        // Call event to Handle Login
                        OnLoginEvent?.Invoke("Logged In.", false);
                        Debug.LogWarning("Phantasma Link authorization logged.");
                    }
                    else
                    {
                        OnLoginEvent?.Invoke("Phantasma Link authorization failed.", true);
                        Debug.LogWarning("Phantasma Link authorization failed.");
                    }
                });
            }
            else
            {
                OnLoginEvent?.Invoke("Logged In.", false);
                Debug.LogWarning("Phantasma Link authorization logged in.");
            }
        }
        else
        {
            Debug.LogWarning("Phantasma Link connection is not ready.");
            OnLoginEvent?.Invoke("Phantasma Link connection is not ready.", true);
            PhantasmaLinkClient.Instance.Enable();
        }
    }

}
