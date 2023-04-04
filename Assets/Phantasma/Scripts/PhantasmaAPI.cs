using System;
using System.Collections;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Phantasma.Core.Cryptography;
using Phantasma.Core.Domain;
using Phantasma.Core.Numerics;
using UnityEngine;
using Event = Phantasma.Core.Domain.Event;

namespace Phantasma.SDK
{
    public class PhantasmaAPI
    {
        /// <summary>
        /// Host needs to be an RPC call, i.e. http://127.0.0.1:7077/rpc
        /// </summary>
        public readonly string Host;

        public PhantasmaAPI(string host)
        {
            this.Host = host;
        }

        /// <summary>
        /// Returns the account name and balance of given address.
        /// </summary>
        /// <param name="addressText"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetAccount(string addressText, Action<Account> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getAccount", WebClient.DefaultTimeout, errorHandlingCallback, (node) => {
                var result = Account.FromNode(node);
                callback(result);
            }, addressText);
        }

        /// <summary>
        /// Returns the address that owns a given name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator LookUpName(string name, Action<string> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "lookUpName", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = node.Value;
                callback(result);
            }, name);
        }

        /// <summary>
        /// Returns the height of a chain.
        /// </summary>
        /// <param name="chainInput"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetBlockHeight(string chainInput, Action<int> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getBlockHeight", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = int.Parse(node.Value);
                callback(result);
            }, chainInput);
        }


        /// <summary>
        /// Returns the number of transactions of given block hash or error if given hash is invalid or is not found.
        /// </summary>
        /// <param name="blockHash"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetBlockTransactionCountByHash(string blockHash, Action<int> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getBlockTransactionCountByHash", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = int.Parse(node.Value);
                callback(result);
            }, blockHash);
        }

        /// <summary>
        /// Returns information about a block by hash.
        /// </summary>
        /// <param name="blockHash"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetBlockByHash(string blockHash, Action<Block> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequestJSON(Host, "getBlockByHash", WebClient.NoTimeout, errorHandlingCallback, (node) =>
            {
                var result = JsonUtility.FromJson<Block>(node);
                callback(result);
            }, blockHash);
        }


        /// <summary>
        /// Returns a serialized string, containing information about a block by hash.
        /// </summary>
        /// <param name="blockHash"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetRawBlockByHash(string blockHash, Action<string> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getRawBlockByHash", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = node.Value;
                callback(result);
            }, blockHash);
        }

        /// <summary>
        /// Returns information about a block by height and chain.
        /// </summary>
        /// <param name="chainInput"></param>
        /// <param name="height"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetBlockByHeight(string chainInput, uint height, Action<Block> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequestJSON(Host, "getBlockByHeight", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = JsonUtility.FromJson<Block>(node);
                callback(result);
            }, chainInput, height);
        }

        /// <summary>
        /// Returns a serialized string, in hex format, containing information about a block by height and chain.
        /// </summary>
        public IEnumerator GetRawBlockByHeight(string chainInput, uint height, Action<string> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getRawBlockByHeight", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = node.Value;
                callback(result);
            }, chainInput, height);
        }

        /// <summary>
        /// Returns the information about a transaction requested by a block hash and transaction index.
        /// </summary>
        /// <param name="blockHash"></param>
        /// <param name="index"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetTransactionByBlockHashAndIndex(string blockHash, int index, Action<Transaction> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequestJSON(Host, "getTransactionByBlockHashAndIndex", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = JsonUtility.FromJson<Transaction>(node);
                callback(result);
            }, blockHash, index);
        }

        /// <summary>
        /// Returns last X transactions of given address.
        /// This api call is paginated, multiple calls might be required to obtain a complete result 
        /// </summary>
        /// <param name="addressText"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetAddressTransactions(string addressText, uint page, uint pageSize, Action<Account, int, int> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getAddressTransactions", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var currentPage = node.GetInt32("page");
                var totalPages = node.GetInt32("totalPages");
                node = node.GetNode("result");
                var result = Account.FromNode(node);
                callback(result, currentPage, totalPages);
            }, addressText, page, pageSize);
        }

        /// <summary>
        /// Get number of transactions in a specific address and chain
        /// </summary>
        /// <param name="addressText"></param>
        /// <param name="chainInput"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetAddressTransactionCount(string addressText, string chainInput, Action<int> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getAddressTransactionCount", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = int.Parse(node.Value);
                callback(result);
            }, addressText, chainInput);
        }

        /// <summary>
        /// Allows to broadcast a signed operation on the network, but it&apos;s required to build it manually.
        /// </summary>
        /// <param name="txData"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator SendRawTransaction(string txData, Action<string> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "sendRawTransaction", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = node.Value;
                callback(result);
            }, txData);
        }

        /// <summary>
        /// Allows to invoke script based on network state, without state changes.
        /// </summary>
        /// <param name="chainInput"></param>
        /// <param name="scriptData"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator InvokeRawScript(string chainInput, string scriptData, Action<Script> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "invokeRawScript", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = Script.FromNode(node);

                foreach (var entry in result.events)
                {
                    if (entry.Kind == EventKind.Log)
                    {
                        var bytes = entry.Data;
                        var msg = Serialization.Unserialize<string>(bytes);
                        Debug.LogWarning("VMlog: " + msg);
                    }
                }

                callback(result);
            }, chainInput, scriptData);
        }

        /// <summary>
        /// Returns information about a transaction by hash.
        /// </summary>
        /// <param name="hashText"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetTransaction(string hashText, Action<Transaction> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getTransaction", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = Transaction.FromNode(node);

                foreach (var entry in result.events)
                {
                    if (entry.Kind == EventKind.Log)
                    {
                        var bytes = entry.Data;
                        var msg = Serialization.Unserialize<string>(bytes);
                        Debug.LogWarning("VMlog: " + msg);
                    }
                }

                callback(result);
            }, hashText);
        }

        /// <summary>
        /// Removes a pending transaction from the mempool.
        /// </summary>
        /// <param name="hashText"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator CancelTransaction(string hashText, Action<string> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "cancelTransaction", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = node.Value;
                callback(result);
            }, hashText);
        }

        /// <summary>
        /// Returns an array of all chains deployed in Phantasma.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetChains(Action<Chain[]> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getChains", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = new Chain[node.ChildCount];
                for (int i = 0; i < result.Length; i++)
                {
                    var child = node.GetNodeByIndex(i);
                    result[i] = Chain.FromNode(child);
                }
                callback(result);
            });
        }

        /// <summary>
        /// Returns an array of all chains deployed in Phantasma.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetOrganization(string ID, Action<Organization> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getOrganization", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = Organization.FromNode(node);
                callback(result);
            }, ID);
        }

        /// <summary>
        /// Return the Leaderboard for a specific address
        /// </summary>
        /// <param name="name"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetLeaderboard(string name, Action<Leaderboard> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequestJSON(Host, "getLeaderboard", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = JsonUtility.FromJson<Leaderboard>(node);
                callback(result);
            }, name);
        }

        private int tokensLoadedSimultaneously = 0;

        /// <summary>
        /// Returns info about a specific token deployed in Phantasma.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetToken(string symbol, Action<Token> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getToken", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = Token.FromNode(node);
                callback(result);
            }, symbol);
        }

        /// <summary>
        /// Returns an array of tokens deployed in Phantasma.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetTokens(Action<Token[]> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getTokens", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = new Token[node.ChildCount];
                for (int i = 0; i < result.Length; i++)
                {
                    var child = node.GetNodeByIndex(i);
                    result[i] = Token.FromNode(child);
                }
                callback(result);
            });
        }

        /// <summary>
        /// Returns data of a non-fungible token, in hexadecimal format.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="IDtext"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetTokenData(string symbol, string IDtext, Action<TokenData> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            while (tokensLoadedSimultaneously > 5)
            {
                yield return null;
            }
            tokensLoadedSimultaneously++;

            yield return WebClient.RPCRequest(Host, "getTokenData", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = TokenData.FromNode(node);
                callback(result);
            }, symbol, IDtext);

            tokensLoadedSimultaneously--;
        }

        /// <summary>
        /// Returns data of a non-fungible token, in hexadecimal format.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="IDtext"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetNFT(string symbol, string IDtext, Action<TokenData> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            while (tokensLoadedSimultaneously > 5)
            {
                yield return null;
            }
            tokensLoadedSimultaneously++;

            yield return WebClient.RPCRequest(Host, "getNFT", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = TokenData.FromNode(node);
                callback(result);
            }, symbol, IDtext);

            tokensLoadedSimultaneously--;
        }

        /// <summary>
        /// Returns data of a non-fungible tokens, in hexadecimal format.
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="IDtext"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetNFTs(string symbol, string[] IDtext, Action<TokenData[]> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            while (tokensLoadedSimultaneously > 5)
            {
                yield return null;
            }

            tokensLoadedSimultaneously++;

            yield return WebClient.RPCRequest(Host, "getNFTs", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = new TokenData[node.ChildCount];
                for (int i = 0; i < result.Length; i++)
                {
                    result[i] = TokenData.FromNode(node.GetNodeByIndex(i));
                }
                callback(result);
            }, symbol, IDtext);

            tokensLoadedSimultaneously--;
        }

        /// <summary>
        /// Returns the token balance for a specific address and token symbol
        /// </summary>
        /// <param name="account"></param>
        /// <param name="tokenSymbol"></param>
        /// <param name="chainInput"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetTokenBalance(string account, string tokenSymbol, string chainInput = "main", Action<Balance> callback = null, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getTokenBalance", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = Balance.FromNode(node);
                callback(result);
            }, account, tokenSymbol, chainInput);
        }

        /// <summary>
        /// Returns the number of active auctions.
        /// </summary>
        /// <param name="chainAddressOrName"></param>
        /// <param name="symbol"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetAuctionsCount(string chainAddressOrName, string symbol, Action<int> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getAuctionsCount", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = int.Parse(node.Value);
                callback(result);
            }, chainAddressOrName, symbol);
        }

        /// <summary>
        /// Returns the auctions available in the market.
        /// </summary>
        /// <param name="chainAddressOrName"></param>
        /// <param name="symbol"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetAuctions(string chainAddressOrName, string symbol, uint page, uint pageSize, Action<Auction[], int, int, int> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getAuctions", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var currentPage = node.GetInt32("page");
                var total = node.GetInt32("total");
                var totalPages = node.GetInt32("totalPages");
                node = node.GetNode("result");
                var result = new Auction[node.ChildCount];
                for (int i = 0; i < result.Length; i++)
                {
                    var child = node.GetNodeByIndex(i);
                    result[i] = Auction.FromNode(child);
                }
                callback(result, currentPage, total, totalPages);
            }, chainAddressOrName, symbol, page, pageSize);
        }


        /// <summary>
        /// Returns the auction for a specific token and ID
        /// </summary>
        /// <param name="chainAddressOrName"></param>
        /// <param name="symbol"></param>
        /// <param name="IDtext"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetAuction(string chainAddressOrName, string symbol, string IDtext, Action<Auction> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getAuction", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = Auction.FromNode(node);
                callback(result);
            }, chainAddressOrName, symbol, IDtext);
        }


        /// <summary>
        /// Returns info about a specific archive.
        /// </summary>
        /// <param name="hashText"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetArchive(string hashText, Action<Archive> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getArchive", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = Archive.FromNode(node);
                callback(result);
            }, hashText);
        }


        /// <summary>
        /// Writes the contents of an incomplete archive.
        /// </summary>
        /// <param name="hashText"></param>
        /// <param name="blockIndex"></param>
        /// <param name="blockContent"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator WriteArchive(string hashText, int blockIndex, string blockContent, Action<Boolean> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "writeArchive", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = Boolean.Parse(node.Value);
                callback(result);
            }, hashText, blockIndex, blockContent);
        }

        /// <summary>
        /// Returns the archive info
        /// </summary>
        /// <param name="hashText"></param>
        /// <param name="blockIndex"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator ReadArchive(string hashText, int blockIndex, Action<string> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "readArchive", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                string result = node.Value;
                callback(result);
            }, hashText, blockIndex);
        }

        /// <summary>
        /// Returns the ContractData
        /// </summary>
        /// <param name="contractName"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetContract(string contractName, Action<Contract> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getContract", WebClient.DefaultTimeout, errorHandlingCallback, (node) => {
                var result = Contract.FromNode(node);
                callback(result);
            }, DomainSettings.RootChainName, contractName);
        }

        /// <summary>
        /// Returns list of known peers.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetPeers(Action<Peer[]> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getPeers", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = new Peer[node.ChildCount];
                for (int i = 0; i < result.Length; i++)
                {
                    var child = node.GetNodeByIndex(i);
                    result[i] = Peer.FromNode(child);
                }
                callback(result);
            });
        }

        /// <summary>
        /// Writes a message to the relay network.
        /// </summary>
        /// <param name="receiptHex"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator RelaySend(string receiptHex, Action<Boolean> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "relaySend", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = Boolean.Parse(node.Value);
                callback(result);
            }, receiptHex);
        }

        /// <summary>
        /// Receives messages from the relay network.
        /// </summary>
        /// <param name="accountInput"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator RelayReceive(string accountInput, Action<Receipt[]> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "relayReceive", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = new Receipt[node.ChildCount];
                for (int i = 0; i < result.Length; i++)
                {
                    var child = node.GetNodeByIndex(i);
                    result[i] = Receipt.FromNode(child);
                }
                callback(result);
            }, accountInput);
        }

        /// <summary>
        /// Reads pending messages from the relay network.
        /// </summary>
        /// <param name="accountInput"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetEvents(string accountInput, Action<Event[]> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getEvents", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = new Event[node.ChildCount];
                for (int i = 0; i < result.Length; i++)
                {
                    var child = node.GetNodeByIndex(i);
                    result[i] = new Event().FromNode(node);
                }
                callback(result);
            }, accountInput);
        }

        /// <summary>
        /// Returns an array of available interop platforms.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetPlatforms(Action<Platform[]> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getPlatforms", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = new Platform[node.ChildCount];
                for (int i = 0; i < result.Length; i++)
                {
                    var child = node.GetNodeByIndex(i);
                    result[i] = Platform.FromNode(child);
                }
                callback(result);
            });
        }

        /// <summary>
        /// Returns an array of available validators.
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetValidators(Action<Validator[]> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getValidators", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = new Validator[node.ChildCount];
                for (int i = 0; i < result.Length; i++)
                {
                    var child = node.GetNodeByIndex(i);
                    result[i] = Validator.FromNode(child);
                }
                callback(result);
            });
        }

        /// <summary>
        /// Returns platform swaps for a specific address.
        /// </summary>
        /// <param name="sourcePlatform"></param>
        /// <param name="destPlatform"></param>
        /// <param name="hashText"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator SettleSwap(string sourcePlatform, string destPlatform, string hashText, Action<string> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "settleSwap", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = node.Value;
                callback(result);
            }, sourcePlatform, destPlatform, hashText);
        }

        /// <summary>
        /// Returns platform swaps for a specific address.
        /// </summary>
        /// <param name="accountInput"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetSwapsForAddress(string accountInput, Action<Swap[]> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getSwapsForAddress", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = new Swap[node.ChildCount];
                for (int i = 0; i < result.Length; i++)
                {
                    var child = node.GetNodeByIndex(i);
                    result[i] = Swap.FromNode(child);
                }
                callback(result);
            }, accountInput);
        }

        /// <summary>
        /// Returns the Lastest Sale Hash
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetLatestSaleHash(Action<string> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getLatestSaleHash", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = node.Value;
                callback(result);
            });
        }

        /// <summary>
        /// Returns a specific Sale
        /// </summary>
        /// <param name="hashText"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator GetSale(string hashText, Action<Crowdsale> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            yield return WebClient.RPCRequest(Host, "getSale", WebClient.NoTimeout, errorHandlingCallback, (node) => {
                var result = Crowdsale.FromNode(node);
                callback(result);
            }, hashText);
        }

        /// <summary>
        /// Sign and send a transaction with the Payload
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="nexus"></param>
        /// <param name="script"></param>
        /// <param name="chain"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator SignAndSendTransaction(PhantasmaKeys keys, string nexus, byte[] script, string chain, Action<string> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            return SignAndSendTransactionWithPayload(keys, nexus, script, chain, new byte[0], callback, errorHandlingCallback);
        }

        /// <summary>
        /// Sign and send a transaction with the Payload
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="nexus"></param>
        /// <param name="script"></param>
        /// <param name="chain"></param>
        /// <param name="payload"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <returns></returns>
        public IEnumerator SignAndSendTransactionWithPayload(PhantasmaKeys keys, string nexus, byte[] script, string chain, string payload, Action<string> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
        {
            return SignAndSendTransactionWithPayload(keys, nexus, script, chain, Encoding.UTF8.GetBytes(payload), callback, errorHandlingCallback);
        }

        /// <summary>
        /// Sign and send a transaction with the Payload
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="nexus"></param>
        /// <param name="script"></param>
        /// <param name="chain"></param>
        /// <param name="payload"></param>
        /// <param name="callback"></param>
        /// <param name="errorHandlingCallback"></param>
        /// <param name="customSignFunction"></param>
        /// <returns></returns>
        public IEnumerator SignAndSendTransactionWithPayload(IKeyPair keys, string nexus, byte[] script, string chain, byte[] payload, Action<string> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null, Func<byte[], byte[], byte[], byte[]> customSignFunction = null)
        {
            Log.Write("Sending transaction...");

            var tx = new Core.Domain.Transaction(nexus, chain, script, DateTime.UtcNow + TimeSpan.FromMinutes(20), payload);
            tx.Sign(keys, customSignFunction);

            yield return SendRawTransaction(Base16.Encode(tx.ToByteArray(true)), callback, errorHandlingCallback);
        }

        /// <summary>
        /// Returns if it's a valid private key
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static bool IsValidPrivateKey(string address)
        {
            return (address.StartsWith("L", false, CultureInfo.InvariantCulture) ||
                    address.StartsWith("K", false, CultureInfo.InvariantCulture)) && address.Length == 52;
        }

        /// <summary>
        /// Returns if it's a valid address
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static bool IsValidAddress(string address)
        {
            return address.StartsWith("P", false, CultureInfo.InvariantCulture) && address.Length == 45;
        }
    }
}