using System;
using System.Collections;
using System.Collections.Generic;
using Phantasma.Business.VM.Utils;
using Phantasma.Core.Cryptography;
using Phantasma.Core.Numerics;
using Phantasma.SDK;
using UnityEngine;

public class WalletInteractions : MonoBehaviour
{
    #region Events
    public event Action<string, bool> OnLoginEvent;
    public event Action<string, bool> OnGetBalancesEvent;
    public event Action<string, bool> OnSendRawTransactionEvent;
    public event Action<string, bool> OnGetTransactionEvent;
    public event Action<string, bool> OnGetNFTEvent;
    public event Action<string, bool> OnGetNFTsEvent;
    public event Action<string, bool> OnInvokeRawScriptEvent;
    public event Action<string, bool> OnMintNFTEvent;
    public event Action<string, bool> OnBurnNFTEvent;
    public event Action<string, bool> OnSendNFTEvent;
    public event Action<string, bool> OnInfuseTokenEvent;
    public event Action<string, bool> OnMintTokensEvent;
    public event Action<string, bool> OnBurnTokensEvent;
    public event Action<string, bool> OnTransferTokensEvent;
    public event Action<string, bool> OnTransferBalanceEvent;
    public event Action<string, bool> OnSignDataEvent;
    public event Action<string, bool> OnMultiSigEvent;
    #endregion

    /// <summary>
    /// Get the balances of the logged account.
    /// </summary>
    public void GetBalances()
    {
        if (!PhantasmaLinkClient.Instance.IsLogged) return;
        
        PhantasmaAPI api = new PhantasmaAPI("https://testnet.phantasma.io/rpc");
        StartCoroutine(api.GetAccount(PhantasmaLinkClient.Instance.Address, account =>
        {
            Debug.Log(account.balances.Length);
        }));
    }

    /// <summary>
    /// Send a Raw Transaction to the blockchain - this method is use to send a transaction to the blockchain.
    /// </summary>
    public void SendRawTransaction()
    {
        if (!PhantasmaLinkClient.Instance.IsLogged) return;

        ScriptBuilder sb = new ScriptBuilder();
        var userAddress = Address.FromText(PhantasmaLinkClient.Instance.Address);
        var toAddress = Address.FromText("P2KKEjZK7AbcKZjuZMsWKKgEjNzeGtr2zBiV7qYJHxNXvUa");
        var symbol = "SOUL";
        var amount = UnitConversion.ToBigInteger(1, 8);
        var payload = Base16.Decode("OurDappExample");
        var script = sb.AllowGas(userAddress, Address.Null, PhantasmaLinkClient.Instance.GasPrice, PhantasmaLinkClient.Instance.GasLimit ).
            CallInterop("Runtime.TransferTokens", userAddress, toAddress, symbol, amount).
            SpendGas(userAddress).
            EndScript();
        
        PhantasmaLinkClient.Instance.SendTransaction("main", script, payload, (hash, s) =>
        {
            if ( hash.IsNull )
            {
                Debug.Log("Transaction failed: " + s);
                return;
            }
            
            Debug.Log("Transaction sent: " + hash);
        });
    }

    /// <summary>
    /// Get the transaction by hash. 
    /// </summary>
    /// <param name="hash"></param>
    /// <param name="callback"></param>
    public void GetTransaction(string hash, Action<Transaction> callback)
    {
        if (!PhantasmaLinkClient.Instance.IsLogged) return;
        
        PhantasmaAPI api = new PhantasmaAPI("https://testnet.phantasma.io/rpc");
        StartCoroutine(api.GetTransaction(hash, callback));
    }

    /// <summary>
    /// Get NFT
    /// </summary>
    public void GetNFT()
    {
        if (!PhantasmaLinkClient.Instance.IsLogged) return;
        PhantasmaAPI api = new PhantasmaAPI("https://testnet.phantasma.io/rpc");
        var symbol = "CROWN";
        var ID = "";
        StartCoroutine(api.GetNFT(symbol, ID, (nft) =>
        {
            Debug.Log(nft);
        }));
    }

    /// <summary>
    /// Get Multiple NFTs in one go.
    /// </summary>
    public void GetNFTs()
    {
        if (!PhantasmaLinkClient.Instance.IsLogged) return;
        PhantasmaAPI api = new PhantasmaAPI("https://testnet.phantasma.io/rpc");
        var symbol = "CROWN";
        var IDs = new String[] { "" };
        StartCoroutine(api.GetNFTs(symbol, IDs, (nfts) =>
        {
            Debug.Log(nfts.Length);
        }));
    }


    /// <summary>
    /// Invoke Raw Script, this method is to call the blockchain directly to get information.
    /// </summary>
    public void InvokeRawScript()
    {
        PhantasmaAPI api = new PhantasmaAPI("https://testnet.phantasma.io/rpc");
        var toAddress = Address.FromText("P2KKEjZK7AbcKZjuZMsWKKgEjNzeGtr2zBiV7qYJHxNXvUa");
        ScriptBuilder sb = new ScriptBuilder();
        var script = sb.
            CallContract("stake", "getStake", toAddress).
            EndScript();
        var scriptEncoded = Base16.Encode(script);
        StartCoroutine(api.InvokeRawScript("main", scriptEncoded, scriptResult =>
        {
            Debug.Log(scriptResult.results.Length);
        }));
    }

    public void MintNFT()
    {
        if (!PhantasmaLinkClient.Instance.IsLogged) return;

        ScriptBuilder sb = new ScriptBuilder();
        var userAddress = Address.FromText(PhantasmaLinkClient.Instance.Address);
        var toAddress = Address.FromText("P2KKEjZK7AbcKZjuZMsWKKgEjNzeGtr2zBiV7qYJHxNXvUa");
        var symbol = "SOUL";
        var amount = UnitConversion.ToBigInteger(1, 8);
        var payload = Base16.Decode("OurDappExample");
        var script = sb.AllowGas(userAddress, Address.Null, PhantasmaLinkClient.Instance.GasPrice, PhantasmaLinkClient.Instance.GasLimit ).
            CallInterop("Runtime.TransferTokens", userAddress, toAddress, symbol, amount).
            SpendGas(userAddress).
            EndScript();
        
        PhantasmaLinkClient.Instance.SendTransaction("main", script, payload, (hash, s) =>
        {
            if ( hash.IsNull )
            {
                Debug.Log("Transaction failed: " + s);
                return;
            }
            
            Debug.Log("Transaction sent: " + hash);
        });
    }

    public void BurnNFT()
    {

    }

    public void SendNFT()
    {
        if (!PhantasmaLinkClient.Instance.IsLogged) return;

        ScriptBuilder sb = new ScriptBuilder();
        var userAddress = Address.FromText(PhantasmaLinkClient.Instance.Address);
        var toAddress = Address.FromText("P2KKEjZK7AbcKZjuZMsWKKgEjNzeGtr2zBiV7qYJHxNXvUa");
        var symbol = "SOUL";
        var tokenID = "";
        var payload = Base16.Decode("OurDappExample");
        var script = sb.AllowGas(userAddress, Address.Null, PhantasmaLinkClient.Instance.GasPrice, PhantasmaLinkClient.Instance.GasLimit ).
            CallInterop("Runtime.TransferToken", userAddress, toAddress, symbol, tokenID).
            SpendGas(userAddress).
            EndScript();
        
        PhantasmaLinkClient.Instance.SendTransaction("main", script, payload, (hash, s) =>
        {
            if ( hash.IsNull )
            {
                Debug.Log("Transaction failed: " + s);
                return;
            }
            
            Debug.Log("Transaction sent: " + hash);
        });
    }

    public void InfuseToken()
    {

    }

    public void MintTokens()
    {

    }

    public void BurnTokens()
    {

    }

    /// <summary>
    /// Transfer Tokens to another address.
    /// </summary>
    public void TransferTokens()
    {
        if (!PhantasmaLinkClient.Instance.IsLogged) return;

        ScriptBuilder sb = new ScriptBuilder();
        var userAddress = Address.FromText(PhantasmaLinkClient.Instance.Address);
        var toAddress = Address.FromText("P2KKEjZK7AbcKZjuZMsWKKgEjNzeGtr2zBiV7qYJHxNXvUa");
        var symbol = "SOUL";
        var amount = UnitConversion.ToBigInteger(1, 8);
        var payload = Base16.Decode("OurDappExample");
        var script = sb.AllowGas(userAddress, Address.Null, PhantasmaLinkClient.Instance.GasPrice, PhantasmaLinkClient.Instance.GasLimit ).
            CallInterop("Runtime.TransferTokens", userAddress, toAddress, symbol, amount).
            SpendGas(userAddress).
            EndScript();
        
        PhantasmaLinkClient.Instance.SendTransaction("main", script, payload, (hash, s) =>
        {
            if ( hash.IsNull )
            {
                Debug.Log("Transaction failed: " + s);
                return;
            }
            
            Debug.Log("Transaction sent: " + hash);
        });
    }

    /// <summary>
    /// Transfer the whole balance of a token to another address.
    /// </summary>
    public void TransferBalance()
    {
        if (!PhantasmaLinkClient.Instance.IsLogged) return;

        ScriptBuilder sb = new ScriptBuilder();
        var userAddress = Address.FromText(PhantasmaLinkClient.Instance.Address);
        var toAddress = Address.FromText("P2KKEjZK7AbcKZjuZMsWKKgEjNzeGtr2zBiV7qYJHxNXvUa");
        var symbol = "SOUL";
        var payload = Base16.Decode("OurDappExample");
        var script = sb.AllowGas(userAddress, Address.Null, PhantasmaLinkClient.Instance.GasPrice, PhantasmaLinkClient.Instance.GasLimit ).
            CallInterop("Runtime.TransferBalance", userAddress, toAddress, symbol).
            SpendGas(userAddress).
            EndScript();
        
        PhantasmaLinkClient.Instance.SendTransaction("main", script, payload, (hash, s) =>
        {
            if ( hash.IsNull )
            {
                Debug.Log("Transaction failed: " + s);
                return;
            }
            
            Debug.Log("Transaction sent: " + hash);
        });
    }

    /// <summary>
    /// Sign Data with the user private key.
    /// </summary>
    public void SignData()
    {
        string dataToSign = "This is a test";
        PhantasmaLinkClient.Instance.SignData(dataToSign, (success, random, signed, data) =>
        {
            
        } );
    }

    /// <summary>
    /// MultiSig transaction.
    /// </summary>
    public void MultiSig()
    {
        if (!PhantasmaLinkClient.Instance.IsLogged) return;

        ScriptBuilder sb = new ScriptBuilder();
        var userAddress = Address.FromText(PhantasmaLinkClient.Instance.Address);
        var toAddress = Address.FromText("P2KKEjZK7AbcKZjuZMsWKKgEjNzeGtr2zBiV7qYJHxNXvUa");
        var symbol = "SOUL";
        var amount = UnitConversion.ToBigInteger(1, 8);
        var payload = Base16.Decode("OurDappExample");
        var script = sb.AllowGas(userAddress, Address.Null, PhantasmaLinkClient.Instance.GasPrice, PhantasmaLinkClient.Instance.GasLimit ).
            CallInterop("Runtime.TransferTokens", userAddress, toAddress, symbol, amount).
            SpendGas(userAddress).
            EndScript();
        
        PhantasmaLinkClient.Instance.SendTransaction("main", script, payload, (hash, s) =>
        {
            if ( hash.IsNull )
            {
                Debug.Log("Transaction failed: " + s);
                return;
            }
            
            Debug.Log("Transaction sent: " + hash);
        });
    }


    /// <summary>
    /// Method used to connect to the wallet.
    /// </summary>
    public void OnLogin()
    {
        if (PhantasmaLinkClient.Instance.Ready)
        {
            if (!PhantasmaLinkClient.Instance.IsLogged)
                PhantasmaLinkClient.Instance.Login((result, msg) =>
                {
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
            else
                OnLoginEvent?.Invoke("Logged In.", false);
        }
        else
        {
            Debug.LogWarning("Phantasma Link connection is not ready.");
            OnLoginEvent?.Invoke("Phantasma Link connection is not ready.", true);
            PhantasmaLinkClient.Instance.Enable();
        }
    }

}
