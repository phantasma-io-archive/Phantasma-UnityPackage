﻿using System;
using System.Collections;

using UnityEngine;
using UnityEngine.Networking;

using LunarLabs.Parser;
using LunarLabs.Parser.JSON;
using System.Text;

namespace Phantasma.SDK
{
    public static class WebClient
    {
        public static int NoTimeout = 0;
        public static int DefaultTimeout = 30;
        private static long requestNumber = 0;
        private static object requestNumberLock = new object();
        private static long GetNextRequestNumber()
        {
            lock (requestNumberLock)
            {
                if (requestNumber == Int64.MaxValue)
                    requestNumber = 0;
                else
                    requestNumber++;
            }

            return requestNumber;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="timeout"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <param name="callback"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IEnumerator RPCRequest(string url, string method, int timeout, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback,
                                            Action<DataNode> callback, params object[] parameters)
        {
            var paramData = DataNode.CreateArray("params");

            if (parameters != null && parameters.Length > 0)
            {
                foreach (var obj in parameters)
                {
                    paramData.AddField(null, obj);
                }
            }

            var jsonRpcData = DataNode.CreateObject(null);
            jsonRpcData.AddField("jsonrpc", "2.0");
            jsonRpcData.AddField("method", method);
            jsonRpcData.AddField("id", "1");
            jsonRpcData.AddNode(paramData);

            UnityWebRequest request;
            string json;

            try
            {
                json = JSONWriter.WriteToString(jsonRpcData);
            }
            catch (Exception e)
            {
                throw e;
            }

            var requestNumber = GetNextRequestNumber();
            Log.Write($"RPC request [{requestNumber}]\nurl: {url}\njson: {json}", Log.Level.Networking);

            request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            DateTime startTime = DateTime.Now;

            if(timeout > 0)
                request.timeout = timeout;
            
            yield return request.SendWebRequest();
            
            TimeSpan responseTime = DateTime.Now - startTime;

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Log.Write($"RPC error [{requestNumber}]\nurl: {url}\nResponse time: {responseTime.Seconds}.{responseTime.Milliseconds} sec\n{request.error}\nisNetworkError: {request.result == UnityWebRequest.Result.ConnectionError}\nisHttpError: {request.result == UnityWebRequest.Result.ProtocolError}\nresponseCode: {request.responseCode}", Log.Level.Networking);
                if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.WEB_REQUEST_ERROR, request.error + $"\nURL: {url}\nIs network error: {request.result == UnityWebRequest.Result.ConnectionError}\nIs HTTP error: {request.result == UnityWebRequest.Result.ProtocolError}\nResponse code: {request.responseCode}");
            }
            else
            {
                Log.Write($"RPC response [{requestNumber}]\nurl: {url}\nResponse time: {responseTime.Seconds}.{responseTime.Milliseconds} sec\n{request.downloadHandler.text}", Log.Level.Networking);
                DataNode root = null;

                try
                {
                    root = JSONReader.ReadFromString(request.downloadHandler.text);
                }
                catch (Exception e)
                {
                    Log.Write($"RPC response [{requestNumber}]\nurl: {url}\nFailed to parse JSON: " + e.Message, Log.Level.Networking);
                    if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.FAILED_PARSING_JSON, "Failed to parse JSON: " + e.Message);
                    yield break;
                }

                if (root == null)
                {
                    Log.Write($"RPC response [{requestNumber}]\nurl: {url}\nFailed to parse JSON", Log.Level.Networking);
                    if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.FAILED_PARSING_JSON, "failed to parse JSON");
                }
                else
                if (root.HasNode("error"))
                {
                    var errorDesc = root["error"].GetString("message");
                    Log.Write($"RPC response [{requestNumber}]\nurl: {url}\nError node found: {errorDesc}", Log.Level.Networking);
                    if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.API_ERROR, errorDesc);
                }
                else
                if (root.HasNode("result"))
                {
                    var result = root["result"];

                    if (result.HasNode("error"))
                    {
                        // This is incorrect way of RPC error reporting,
                        // but it happens sometimes and should be handeled at least for now.
                        var errorDesc = result.GetString("error");
                        Log.Write($"RPC response [{requestNumber}]\nurl: {url}\nError node found (2): {errorDesc}", Log.Level.Networking);
                        if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.API_ERROR, errorDesc);
                    }
                    else
                    {
                        callback(result);
                    }
                }
                else
                {
                    if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.MALFORMED_RESPONSE, "malformed response");
                }
            }

            yield break;
        }
        
        
        public static IEnumerator RPCRequestJSON(string url, string method, int timeout, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback,
                                            Action<string> callback, params object[] parameters)
        {
            var paramData = DataNode.CreateArray("params");

            if (parameters != null && parameters.Length > 0)
            {
                foreach (var obj in parameters)
                {
                    paramData.AddField(null, obj);
                }
            }

            var jsonRpcData = DataNode.CreateObject(null);
            jsonRpcData.AddField("jsonrpc", "2.0");
            jsonRpcData.AddField("method", method);
            jsonRpcData.AddField("id", "1");
            jsonRpcData.AddNode(paramData);

            UnityWebRequest request;
            string json;

            try
            {
                json = JSONWriter.WriteToString(jsonRpcData);
            }
            catch (Exception e)
            {
                throw e;
            }

            var requestNumber = GetNextRequestNumber();
            Log.Write($"RPC request [{requestNumber}]\nurl: {url}\njson: {json}", Log.Level.Networking);

            request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            DateTime startTime = DateTime.Now;

            if(timeout > 0)
                request.timeout = timeout;
            
            yield return request.SendWebRequest();
            
            TimeSpan responseTime = DateTime.Now - startTime;

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Log.Write($"RPC error [{requestNumber}]\nurl: {url}\nResponse time: {responseTime.Seconds}.{responseTime.Milliseconds} sec\n{request.error}\nisNetworkError: {request.result == UnityWebRequest.Result.ConnectionError}\nisHttpError: {request.result == UnityWebRequest.Result.ProtocolError}\nresponseCode: {request.responseCode}", Log.Level.Networking);
                if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.WEB_REQUEST_ERROR, request.error + $"\nURL: {url}\nIs network error: {request.result == UnityWebRequest.Result.ConnectionError}\nIs HTTP error: {request.result == UnityWebRequest.Result.ProtocolError}\nResponse code: {request.responseCode}");
            }
            else
            {
                Log.Write($"RPC response [{requestNumber}]\nurl: {url}\nResponse time: {responseTime.Seconds}.{responseTime.Milliseconds} sec\n{request.downloadHandler.text}", Log.Level.Networking);
                DataNode root = null;

                try
                {
                    
                    root = JSONReader.ReadFromString(request.downloadHandler.text);
                }
                catch (Exception e)
                {
                    Log.Write($"RPC response [{requestNumber}]\nurl: {url}\nFailed to parse JSON: " + e.Message, Log.Level.Networking);
                    if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.FAILED_PARSING_JSON, "Failed to parse JSON: " + e.Message);
                    yield break;
                }

                if (root == null)
                {
                    Log.Write($"RPC response [{requestNumber}]\nurl: {url}\nFailed to parse JSON", Log.Level.Networking);
                    if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.FAILED_PARSING_JSON, "failed to parse JSON");
                }
                else
                if (root.HasNode("error"))
                {
                    var errorDesc = root["error"].GetString("message");
                    Log.Write($"RPC response [{requestNumber}]\nurl: {url}\nError node found: {errorDesc}", Log.Level.Networking);
                    if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.API_ERROR, errorDesc);
                }
                else
                if (root.HasNode("result"))
                {
                    var result = root["result"];

                    if (result.HasNode("error"))
                    {
                        // This is incorrect way of RPC error reporting,
                        // but it happens sometimes and should be handeled at least for now.
                        var errorDesc = result.GetString("error");
                        Log.Write($"RPC response [{requestNumber}]\nurl: {url}\nError node found (2): {errorDesc}", Log.Level.Networking);
                        if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.API_ERROR, errorDesc);
                    }
                    else
                    {
                        callback(result.AsString());
                    }
                }
                else
                {
                    if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.MALFORMED_RESPONSE, "malformed response");
                }
            }

            yield break;
        }
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="timeout"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <param name="callback"></param>
        /// <param name="parametersNode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IEnumerator RPCRequestEx(string url, string method, int timeout, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback,
            Action<DataNode> callback, DataNode parametersNode)
        {
            var jsonRpcData = DataNode.CreateObject(null);
            jsonRpcData.AddField("jsonrpc", "2.0");
            jsonRpcData.AddField("method", method);
            jsonRpcData.AddField("id", "1");
            jsonRpcData.AddNode(parametersNode);

            UnityWebRequest request;
            string json;

            try
            {
                json = JSONWriter.WriteToString(jsonRpcData);
            }
            catch (Exception e)
            {
                throw e;
            }

            var requestNumber = GetNextRequestNumber();
            Log.Write($"RPC request [{requestNumber}]\nurl: {url}\njson: {json}", Log.Level.Networking);

            request = new UnityWebRequest(url, "POST");
            byte[] bodyRaw = Encoding.UTF8.GetBytes(json);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            DateTime startTime = DateTime.Now;

            if (timeout > 0)
                request.timeout = timeout;

            yield return request.SendWebRequest();
            
            TimeSpan responseTime = DateTime.Now - startTime;

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Log.Write($"RPC error [{requestNumber}]\nurl: {url}\nResponse time: {responseTime.Seconds}.{responseTime.Milliseconds} sec\n{request.error}\nisNetworkError: {request.result == UnityWebRequest.Result.ConnectionError}\nisHttpError: {request.result == UnityWebRequest.Result.ProtocolError}\nresponseCode: {request.responseCode}", Log.Level.Networking);
                if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.WEB_REQUEST_ERROR, request.error + $"\nURL: {url}\nIs network error: {request.result == UnityWebRequest.Result.ConnectionError}\nIs HTTP error: {request.result == UnityWebRequest.Result.ProtocolError}\nResponse code: {request.responseCode}");
            }
            else
            {
                Log.Write($"RPC response [{requestNumber}]\nurl: {url}\nResponse time: {responseTime.Seconds}.{responseTime.Milliseconds} sec\n{request.downloadHandler.text}", Log.Level.Networking);
                DataNode root = null;

                try
                {
                    root = JSONReader.ReadFromString(request.downloadHandler.text);
                }
                catch(Exception e)
                {
                    Log.Write($"RPC response [{requestNumber}]\nurl: {url}\nFailed to parse JSON: " + e.Message, Log.Level.Networking);
                    if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.FAILED_PARSING_JSON, "Failed to parse JSON: " + e.Message);
                    yield break;
                }

                if (root == null)
                {
                    Log.Write($"RPC response [{requestNumber}]\nurl: {url}\nFailed to parse JSON", Log.Level.Networking);
                    if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.FAILED_PARSING_JSON, "failed to parse JSON");
                }
                else
                if (root.HasNode("error"))
                {
                    var errorDesc = root["error"].GetString("message");
                    Log.Write($"RPC response [{requestNumber}]\nurl: {url}\nError node found: {errorDesc}", Log.Level.Networking);
                    if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.API_ERROR, errorDesc);
                }
                else
                if (root.HasNode("result"))
                {
                    var result = root["result"];

                    if (result.HasNode("error"))
                    {
                        // This is incorrect way of RPC error reporting,
                        // but it happens sometimes and should be handeled at least for now.
                        var errorDesc = result.GetString("error");
                        Log.Write($"RPC response [{requestNumber}]\nurl: {url}\nError node found (2): {errorDesc}", Log.Level.Networking);
                        if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.API_ERROR, errorDesc);
                    }
                    else
                    {
                        callback(result);
                    }
                }
                else
                {
                    if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.MALFORMED_RESPONSE, "malformed response");
                }
            }

            yield break;
        }
        
        /// <summary>
        /// REST Request
        /// </summary>
        /// <param name="url"></param>
        /// <param name="timeout"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static IEnumerator RESTRequest(string url, int timeout, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback, Action<DataNode> callback)
        {
            UnityWebRequest request;

            var requestNumber = GetNextRequestNumber();
            Log.Write($"REST request [{requestNumber}]\nurl: {url}", Log.Level.Networking);

            request = new UnityWebRequest(url, "GET");
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            DateTime startTime = DateTime.Now;

            if (timeout > 0)
                request.timeout = timeout;
            
            yield return request.SendWebRequest();
            
            TimeSpan responseTime = DateTime.Now - startTime;

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Log.Write($"REST error [{requestNumber}]\nurl: {url}\nResponse time: {responseTime.Seconds}.{responseTime.Milliseconds} sec\n{request.error}\nisNetworkError: {request.result == UnityWebRequest.Result.ConnectionError}\nisHttpError: {request.result == UnityWebRequest.Result.ProtocolError}\nresponseCode: {request.responseCode}", Log.Level.Networking);
                if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.WEB_REQUEST_ERROR, request.error + $"\nURL: {url}\nIs network error: {request.result == UnityWebRequest.Result.ConnectionError}\nIs HTTP error: {request.result == UnityWebRequest.Result.ProtocolError}\nResponse code: {request.responseCode}");
            }
            else
            {
                DataNode root = null;
                try
                {
                    Log.Write($"REST response [{requestNumber}]\nurl: {url}\nResponse time: {responseTime.Seconds}.{responseTime.Milliseconds} sec\n{request.downloadHandler.text}", Log.Level.Networking);
                    root = JSONReader.ReadFromString(request.downloadHandler.text);
                }
                catch (Exception e)
                {
                    Log.Write(e.Message);
                }
                callback(root);
            }

            yield break;
        }

        public static IEnumerator RESTRequest(string url, string serializedJson, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback, Action<DataNode> callback)
        {
            UnityWebRequest request;

            var requestNumber = GetNextRequestNumber();
            Log.Write($"REST request (POST) [{requestNumber}]\nurl: {url}", Log.Level.Networking);

            request = new UnityWebRequest(url, "POST");

            byte[] data = Encoding.UTF8.GetBytes(serializedJson);
            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(data);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            DateTime startTime = DateTime.Now;
            yield return request.SendWebRequest();
            TimeSpan responseTime = DateTime.Now - startTime;
            
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Log.Write($"REST error [{requestNumber}]\nurl: {url}\nResponse time: {responseTime.Seconds}.{responseTime.Milliseconds} sec\n{request.error}\nisNetworkError: {request.result == UnityWebRequest.Result.ConnectionError}\nisHttpError: {request.result == UnityWebRequest.Result.ProtocolError}\nresponseCode: {request.responseCode}", Log.Level.Networking);
                if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.WEB_REQUEST_ERROR, request.error + $"\nURL: {url}\nIs network error: {request.result == UnityWebRequest.Result.ConnectionError}\nIs HTTP error: {request.result == UnityWebRequest.Result.ProtocolError}\nResponse code: {request.responseCode}");
            }
            else
            {
                DataNode root = null;
                try
                {
                    Log.Write($"REST response [{requestNumber}]\nurl: {url}\nResponse time: {responseTime.Seconds}.{responseTime.Milliseconds} sec\n{request.downloadHandler.text}", Log.Level.Networking);
                    root = JSONReader.ReadFromString(request.downloadHandler.text);
                }
                catch(Exception e)
                {
                    Log.Write(e.Message);
                }
                callback(root);
            }

            yield break;
        }

        /// <summary>
        /// PING
        /// </summary>
        /// <param name="url"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static IEnumerator Ping(string url, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback, Action<TimeSpan> callback)
        {
            UnityWebRequest request;

            Log.Write($"Ping url: {url}", Log.Level.Networking);

            request = new UnityWebRequest(url, "GET");
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

            DateTime startTime = DateTime.Now;
            yield return request.SendWebRequest();
            TimeSpan responseTime = DateTime.Now - startTime;

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Log.Write($"Ping error\nurl: {url}\nResponse time: {responseTime.Seconds}.{responseTime.Milliseconds} sec\n{request.error}\nisNetworkError: {request.result == UnityWebRequest.Result.ConnectionError}\nisHttpError: {request.result == UnityWebRequest.Result.ProtocolError}\nresponseCode: {request.responseCode}", Log.Level.Networking);
                if (errorHandlingCallback != null) errorHandlingCallback(EPHANTASMA_SDK_ERROR_TYPE.WEB_REQUEST_ERROR, request.error + $"\nURL: {url}\nIs network error: {request.result == UnityWebRequest.Result.ConnectionError}\nIs HTTP error: {request.result == UnityWebRequest.Result.ProtocolError}\nResponse code: {request.responseCode}");
            }
            else
            {
                Log.Write($"Ping response\nurl: {url}\nResponse time: {responseTime.Seconds}.{responseTime.Milliseconds} sec\n{request.downloadHandler.text}", Log.Level.Networking);
                callback(responseTime);
            }

            yield break;
        }
    }
}