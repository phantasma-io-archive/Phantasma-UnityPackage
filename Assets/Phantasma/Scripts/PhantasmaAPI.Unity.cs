using System;
using System.Collections;
using System.Globalization;

using UnityEngine;

using LunarLabs.Parser;

using Phantasma.Numerics;
using Phantasma.Cryptography;
using System.Text;
using Phantasma.Domain;
using Phantasma.Storage;

namespace Phantasma.SDK
{
	public enum EPHANTASMA_SDK_ERROR_TYPE
	{
		API_ERROR,
		WEB_REQUEST_ERROR,
		FAILED_PARSING_JSON,
		MALFORMED_RESPONSE
	}

	internal static class PhantasmaAPIUtils
	{
		internal static long GetInt64(this DataNode node, string name)
		{
			return node.GetInt64(name);
		}

		internal static bool GetBoolean(this DataNode node, string name)
		{
			return node.GetBool(name);
		}
	}

	public struct Balance
	{
		public string chain; //
		public string amount; //
		public string symbol; //
		public uint decimals; //
		public string[] ids; //

		public static Balance FromNode(DataNode node)
		{
			Balance result;

			result.chain = node.GetString("chain");
			result.amount = node.GetString("amount");
			result.symbol = node.GetString("symbol");
			result.decimals = node.GetUInt32("decimals");
			var ids_array = node.GetNode("ids");
			if (ids_array != null)
			{
				result.ids = new string[ids_array.ChildCount];
				for (int i = 0; i < ids_array.ChildCount; i++)
				{

					result.ids[i] = ids_array.GetNodeByIndex(i).AsString();
				}
			}
			else
			{
				result.ids = new string[0];
			}


			return result;
		}
	}

	public struct Interop
	{
		public string local; //
		public string external; //

		public static Interop FromNode(DataNode node)
		{
			Interop result;

			result.local = node.GetString("local");
			result.external = node.GetString("external");

			return result;
		}
	}

	public struct Platform
	{
		public string platform; //
		public string chain; //
		public string fuel; //
		public string[] tokens; //
		public Interop[] interop; //

		public static Platform FromNode(DataNode node)
		{
			Platform result;

			result.platform = node.GetString("platform");
			result.chain = node.GetString("chain");
			result.fuel = node.GetString("fuel");
			var tokens_array = node.GetNode("tokens");
			if (tokens_array != null)
			{
				result.tokens = new string[tokens_array.ChildCount];
				for (int i = 0; i < tokens_array.ChildCount; i++)
				{

					result.tokens[i] = tokens_array.GetNodeByIndex(i).AsString();
				}
			}
			else
			{
				result.tokens = new string[0];
			}

			var interop_array = node.GetNode("interop");
			if (interop_array != null)
			{
				result.interop = new Interop[interop_array.ChildCount];
				for (int i = 0; i < interop_array.ChildCount; i++)
				{

					result.interop[i] = Interop.FromNode(interop_array.GetNodeByIndex(i));

				}
			}
			else
			{
				result.interop = new Interop[0];
			}


			return result;
		}
	}

	public struct Swap
	{
		public string sourcePlatform; //
		public string sourceChain; //
		public string sourceHash; //
		public string sourceAddress; //
		public string destinationPlatform; //
		public string destinationChain; //
		public string destinationHash; //
		public string destinationAddress; //
		public string symbol; //
		public string value; //

		public static Swap FromNode(DataNode node)
		{
			Swap result;

			result.sourcePlatform = node.GetString("sourcePlatform");
			result.sourceChain = node.GetString("sourceChain");
			result.sourceHash = node.GetString("sourceHash");
			result.sourceAddress = node.GetString("sourceAddress");
			result.destinationPlatform = node.GetString("destinationPlatform");
			result.destinationChain = node.GetString("destinationChain");
			result.destinationHash = node.GetString("destinationHash");
			result.destinationAddress = node.GetString("destinationAddress");
			result.symbol = node.GetString("symbol");
			result.value = node.GetString("value");

			return result;
		}
	}

	public struct Stake
	{
		public string amount; //
		public uint time; //
		public string unclaimed; //

		public static Stake FromNode(DataNode node)
		{
			Stake result;

			result.amount = node.GetString("amount");
			result.time = node.GetUInt32("time");
			result.unclaimed = node.GetString("unclaimed");

			return result;
		}
	}

	public struct Account
	{
		public string address; //
		public string name; //
		public Stake stakes; //
		public string stake; //
		public string unclaimed;
		public string relay; //
		public string validator; //
		public Balance[] balances; //
		public Storage storage;
		public string[] txs;

		public static Account FromNode(DataNode node)
		{
			Account result;

			result.address = node.GetString("address");
			result.name = node.GetString("name");
			result.stakes = Stake.FromNode(node.GetNode("stakes"));
			result.stake = node.GetString("stake");
			result.unclaimed = node.GetString("unclaimed");
			result.relay = node.GetString("relay");
			result.validator = node.GetString("validator");
			result.storage = Storage.FromNode(node.GetNode("storage"));
			var balances_array = node.GetNode("balances");
			if (balances_array != null)
			{
				result.balances = new Balance[balances_array.ChildCount];
				for (int i = 0; i < balances_array.ChildCount; i++)
				{

					result.balances[i] = Balance.FromNode(balances_array.GetNodeByIndex(i));

				}
			}
			else
			{
				result.balances = new Balance[0];
			}

			var txs_array = node.GetNode("txs");
			if (balances_array != null)
			{
				result.txs = new string[txs_array.ChildCount];
				for (int i = 0; i < txs_array.ChildCount; i++)
				{
					result.txs[i] = txs_array.GetNodeByIndex(i).AsString();
				}
			}
			else
			{
				result.txs = new string[0];
			}

			return result;
		}
	}

	public struct Storage
	{
		public int available;
		public int used;
		public string avatar;

		public static Storage FromNode(DataNode node)
		{
			Storage result;
			result.available = node.GetInt32("available");
			result.used = node.GetInt32("used");
			result.avatar = node.GetString("avatar");

			return result;
		}
	}

	public struct ContractParameter
	{
		public string name;
		public string type;

		public static ContractParameter FromNode(DataNode node)
		{
			ContractParameter result = new ContractParameter();
			result.name = node.GetString("name");
			result.type = node.GetString("type");
			return result;
		}
	}

	public struct ContractMethod
	{
		public string name;
		public string returnType;
		public ContractParameter[] parameters;

		public static ContractMethod FromNode(DataNode node)
		{
			ContractMethod result = new ContractMethod();
			result.name = node.GetString("name");
			result.returnType = node.GetString("returnType");

			var parameters_array = node.GetNode("parameters");
			if (parameters_array != null)
			{
				result.parameters = new ContractParameter[parameters_array.ChildCount];
				for (int i = 0; i < parameters_array.ChildCount; i++)
				{
					result.parameters[i] = ContractParameter.FromNode(parameters_array.GetNodeByIndex(i));
				}
			}
			else
			{
				result.parameters = new ContractParameter[0];
			}

			return result;
		}
	}

	public struct ContractEvent
	{
		public int value;
		public string name;
		public string returnType;
		public string description;

		public static ContractEvent FromNode(DataNode node)
		{
			var result = new ContractEvent();
			result.value = node.GetInt32("value");
			result.name = node.GetString("name");
			result.returnType = node.GetString("returnType");
			result.description = node.GetString("description");
			return result;
		}
	}

	public struct Contract
	{
		public string name; //
		public string address; //
		public string script; //
		public ContractMethod[] methods;
		public ContractEvent[] events;

		public static Contract FromNode(DataNode node)
		{
			Contract result;

			result.address = node.GetString("address");
			result.name = node.GetString("name");
			result.script = node.GetString("script");

			var methods_array = node.GetNode("methods");
			if (methods_array != null)
			{
				result.methods = new ContractMethod[methods_array.ChildCount];
				for (int i = 0; i < methods_array.ChildCount; i++)
				{
					result.methods[i] = ContractMethod.FromNode(methods_array.GetNodeByIndex(i));
				}
			}
			else
			{
				result.methods = new ContractMethod[0];
			}

			var event_array = node.GetNode("events");
			if (event_array != null)
			{
				result.events = new ContractEvent[event_array.ChildCount];
				for (int i = 0; i < event_array.ChildCount; i++)
				{
					result.events[i] = ContractEvent.FromNode(event_array.GetNodeByIndex(i));
				}
			}
			else
			{
				result.events = new ContractEvent[0];
			}

			return result;
		}
	}

	public struct Chain
	{
		public string name; //
		public string address; //
		public string parentAddress; //
		public uint height; //
		public string organization;
		public string[] contracts; //

		public static Chain FromNode(DataNode node)
		{
			Chain result;

			result.name = node.GetString("name");
			result.address = node.GetString("address");
			result.parentAddress = node.GetString("parentAddress");
			result.height = node.GetUInt32("height");
			result.organization = node.GetString("organization");
			var contracts_array = node.GetNode("contracts");
			if (contracts_array != null)
			{
				result.contracts = new string[contracts_array.ChildCount];
				for (int i = 0; i < contracts_array.ChildCount; i++)
				{

					result.contracts[i] = contracts_array.GetNodeByIndex(i).AsString();
				}
			}
			else
			{
				result.contracts = new string[0];
			}


			return result;
		}
	}

	public struct Organization
	{
		public string id;
		public string name;
		public string[] members;

		public static Organization FromNode(DataNode node)
		{
			var result = new Organization();
			result.id = node.GetString("id");
			result.name = node.GetString("name");

			var members_array = node.GetNode("members");
			if (members_array != null)
			{
				result.members = new string[members_array.ChildCount];
				for (int i = 0; i < members_array.ChildCount; i++)
				{

					result.members[i] = members_array.GetNodeByIndex(i).AsString();
				}
			}
			else
			{
				result.members = new string[0];
			}
			return result;
		}
	}

	public struct Event
	{
		public string address; //
		public string contract;
		public EventKind kind; //
		public string data; //

		public static Event FromNode(DataNode node)
		{
			Event result;

			result.address = node.GetString("address");
			result.contract = node.GetString("contract");
			result.kind = node.GetEnum<EventKind>("kind");
			result.data = node.GetString("data");

			return result;
		}

		public override string ToString()
		{
			return $"{kind} @ {address}";
		}
	}

	public struct Transaction
	{
		public string hash; //
		public string chainAddress; //
		public uint timestamp; //
		public int blockHeight; //
		public string blockHash; //
		public string script; //
		public string payload;
		public Event[] events; //
		public string result; //
		public string fee; //
		public Signature[] signatures;
		public uint expiration;
		//public int confirmations; //

		public static Transaction FromNode(DataNode node)
		{
			Transaction result;

			result.hash = node.GetString("hash");
			result.chainAddress = node.GetString("chainAddress");
			result.timestamp = node.GetUInt32("timestamp");
			result.blockHeight = node.GetInt32("blockHeight");
			result.blockHash = node.GetString("blockHash");
			result.script = node.GetString("script");
			result.payload = node.GetString("payload");
			var events_array = node.GetNode("events");
			if (events_array != null)
			{
				result.events = new Event[events_array.ChildCount];
				for (int i = 0; i < events_array.ChildCount; i++)
				{
					result.events[i] = Event.FromNode(events_array.GetNodeByIndex(i));
				}
			}
			else
			{
				result.events = new Event[0];
			}

			result.result = node.GetString("result");
			result.fee = node.GetString("fee");

			var signatures_array = node.GetNode("signatures");
			if (signatures_array != null)
			{
				result.signatures = new Signature[signatures_array.ChildCount];
				for (int i = 0; i < signatures_array.ChildCount; i++)
				{

					result.signatures[i] = Signature.FromNode(signatures_array.GetNodeByIndex(i));

				}
			}
			else
			{
				result.signatures = new Signature[0];
			}

			result.expiration = node.GetUInt32("expiration");

			return result;
		}
	}

	public struct Signature
	{
		public string Kind;
		public string Data;

		public static Signature FromNode(DataNode node)
		{
			Signature result;

			result.Kind = node.GetString("Kind");
			result.Data = node.GetString("Data");

			return result;
		}
	}

	public struct AccountTransactions
	{
		public string address; //
		public Transaction[] txs; //

		public static AccountTransactions FromNode(DataNode node)
		{
			AccountTransactions result;

			result.address = node.GetString("address");
			var txs_array = node.GetNode("txs");
			if (txs_array != null)
			{
				result.txs = new Transaction[txs_array.ChildCount];
				for (int i = 0; i < txs_array.ChildCount; i++)
				{

					result.txs[i] = Transaction.FromNode(txs_array.GetNodeByIndex(i));

				}
			}
			else
			{
				result.txs = new Transaction[0];
			}


			return result;
		}
	}

	public struct Block
	{
		public string hash; //
		public string previousHash; //
		public uint timestamp; //
		public uint height; //
		public string chainAddress; //
		public uint protocol; //
		public Transaction[] txs; //
		public string validatorAddress; //
		public string reward; //
		public Event[] events;
		public Oracle[] oracles;

		public static Block FromNode(DataNode node)
		{
			Block result;

			result.hash = node.GetString("hash");
			result.previousHash = node.GetString("previousHash");
			result.timestamp = node.GetUInt32("timestamp");
			result.height = node.GetUInt32("height");
			result.chainAddress = node.GetString("chainAddress");
			result.protocol = node.GetUInt32("protocol");
			var txs_array = node.GetNode("txs");
			if (txs_array != null)
			{
				result.txs = new Transaction[txs_array.ChildCount];
				for (int i = 0; i < txs_array.ChildCount; i++)
				{

					result.txs[i] = Transaction.FromNode(txs_array.GetNodeByIndex(i));

				}
			}
			else
			{
				result.txs = new Transaction[0];
			}

			result.validatorAddress = node.GetString("validatorAddress");
			result.reward = node.GetString("reward");

			var events_array = node.GetNode("events");
			if (events_array != null)
			{
				result.events = new Event[events_array.ChildCount];
				for (int i = 0; i < events_array.ChildCount; i++)
				{

					result.events[i] = Event.FromNode(events_array.GetNodeByIndex(i));

				}
			}
			else
			{
				result.events = new Event[0];
			}


			var oracles_array = node.GetNode("events");
			if (oracles_array != null)
			{
				result.oracles = new Oracle[oracles_array.ChildCount];
				for (int i = 0; i < oracles_array.ChildCount; i++)
				{

					result.oracles[i] = Oracle.FromNode(oracles_array.GetNodeByIndex(i));

				}
			}
			else
			{
				result.oracles = new Oracle[0];
			}


			return result;
		}
	}

	public class Token
	{
		public string symbol; //
		public string name; //
		public int decimals; //
		public string currentSupply; //
		public string maxSupply; //
		public string burnedSupply; //
		public string address; //
		public string owner; //
		public string flags; //
		public string script; //
		public TokenExternal[] external;
		public TokenSeries[] series;

		public static Token FromNode(DataNode node)
		{
			Token result = new Token();

			result.symbol = node.GetString("symbol");
			result.name = node.GetString("name");
			result.decimals = node.GetInt32("decimals");
			result.currentSupply = node.GetString("currentSupply");
			result.maxSupply = node.GetString("maxSupply");
			result.burnedSupply = node.GetString("burnedSupply");
			result.address = node.GetString("address");
			result.owner = node.GetString("owner");
			result.flags = node.GetString("flags");
			result.script = node.GetString("script");

			var series_array = node.GetNode("series");
			if (series_array != null)
			{
				result.series = new TokenSeries[series_array.ChildCount];
				for (int i = 0; i < series_array.ChildCount; i++)
				{
					result.series[i] = TokenSeries.FromNode(series_array.GetNodeByIndex(i));
				}
			}

			var external_array = node.GetNode("external");
			if (external_array != null)
			{
				result.external = new TokenExternal[external_array.ChildCount];
				for (int i = 0; i < external_array.ChildCount; i++)
				{
					result.external[i] = TokenExternal.FromNode(external_array.GetNodeByIndex(i));
				}
			}
			else
			{
				result.external = new TokenExternal[0];
			}

			return result;
		}
	}

	public struct TokenSeries
	{
		public int seriesID; //
		public string currentSupply; //
		public string maxSupply; //
		public string burnedSupply; //
		public string mode; //
		public string script; //

		public static TokenSeries FromNode(DataNode node)
		{
			TokenSeries result = new TokenSeries();

			result.seriesID = node.GetInt32("seriesID");
			result.currentSupply = node.GetString("currentSupply");
			result.maxSupply = node.GetString("maxSupply");
			result.burnedSupply = node.GetString("burnedSupply");
			result.mode = node.GetString("mode");

			return result;
		}

	}

	public struct TokenExternal
	{
		public string platform;
		public string hash;

		public static TokenExternal FromNode(DataNode node)
		{
			TokenExternal result = new TokenExternal();
			result.platform = node.GetString("platform");
			result.hash = node.GetString("hash");
			return result;
		}
	}

	public struct TokenData
	{
		public string ID; //
		public string series; //
		public string mint; //
		public string chainName; //
		public string ownerAddress; //
		public string creatorAddress; //
		public string ram; //
		public string rom; //
		public string status; //
		public Boolean forSale; //

		public static TokenData FromNode(DataNode node)
		{
			TokenData result;

			result.ID = node.GetString("ID");
			result.series = node.GetString("series");
			result.mint = node.GetString("mint");
			result.chainName = node.GetString("chainName");
			result.ownerAddress = node.GetString("ownerAddress");
			result.creatorAddress = node.GetString("creatorAddress");
			result.ram = node.GetString("ram");
			result.rom = node.GetString("rom");
			result.status = node.GetString("status");
			result.forSale = node.GetBoolean("forSale");

			return result;
		}
	}

	public struct TokenBalance
	{
		public string chain;
		public string amount;
		public string symbol;
		public int decimals;

		public static TokenBalance FromNode(DataNode node)
		{
			TokenBalance result = new TokenBalance();
			result.chain = node.GetString("chain");
			result.amount = node.GetString("amount");
			result.symbol = node.GetString("symbol");
			result.decimals = node.GetInt32("decimals");
			return result;
		}
	}

	public struct SendRawTx
	{
		public string hash; //
		public string error; //

		public static SendRawTx FromNode(DataNode node)
		{
			SendRawTx result;

			result.hash = node.GetString("hash");
			result.error = node.GetString("error");

			return result;
		}
	}

	public struct Auction
	{
		public string creatorAddress; //
		public string chainAddress; //
		public uint startDate; //
		public uint endDate; //
		public string baseSymbol; //
		public string quoteSymbol; //
		public string tokenId; //
		public string price; //
		public string endPrice; //
		public string extensionPeriod; //
		public string type; //
		public string rom; //
		public string ram; //
		public string listingFee; //
		public string currentWinner; //

		public static Auction FromNode(DataNode node)
		{
			Auction result;

			result.creatorAddress = node.GetString("creatorAddress");
			result.chainAddress = node.GetString("chainAddress");
			result.startDate = node.GetUInt32("startDate");
			result.endDate = node.GetUInt32("endDate");
			result.baseSymbol = node.GetString("baseSymbol");
			result.quoteSymbol = node.GetString("quoteSymbol");
			result.tokenId = node.GetString("tokenId");
			result.price = node.GetString("price");
			result.endPrice = node.GetString("endPrice");
			result.extensionPeriod = node.GetString("extensionPeriod");
			result.type = node.GetString("type");
			result.rom = node.GetString("rom");
			result.ram = node.GetString("ram");
			result.listingFee = node.GetString("listingFee");
			result.currentWinner = node.GetString("currentWinner");

			return result;
		}
	}

	public struct Oracle
	{
		public string url; //
		public string content; //

		public static Oracle FromNode(DataNode node)
		{
			Oracle result;

			result.url = node.GetString("url");
			result.content = node.GetString("content");

			return result;
		}
	}

	public struct Script
	{
		public Event[] events; //
		public string result; //
		public string[] results; //
		public Oracle[] oracles; //

		public static Script FromNode(DataNode node)
		{
			Script result;

			var events_array = node.GetNode("events");
			if (events_array != null)
			{
				result.events = new Event[events_array.ChildCount];
				for (int i = 0; i < events_array.ChildCount; i++)
				{

					result.events[i] = Event.FromNode(events_array.GetNodeByIndex(i));

				}
			}
			else
			{
				result.events = new Event[0];
			}

			result.result = node.GetString("result");

			var results_array = node.GetNode("results");
			if (results_array != null)
			{
				result.results = new string[results_array.ChildCount];
				for (int i = 0; i < results_array.ChildCount; i++)
				{

					result.results[i] = results_array.GetNodeByIndex(i).Value;

				}
			}
			else
			{
				result.results = new string[0];
			}

			var oracles_array = node.GetNode("oracles");
			if (oracles_array != null)
			{
				result.oracles = new Oracle[oracles_array.ChildCount];
				for (int i = 0; i < oracles_array.ChildCount; i++)
				{

					result.oracles[i] = Oracle.FromNode(oracles_array.GetNodeByIndex(i));

				}
			}
			else
			{
				result.oracles = new Oracle[0];
			}

			return result;
		}
	}

	public struct Archive
	{
		public string name; //
		public string hash; //
		public uint time; //
		public uint size; //
		public string encryption; //
		public int blockCount; //
		public int[] missingBlocks;
		public string[] owners; //

		public static Archive FromNode(DataNode node)
		{
			Archive result;

			result.name = node.GetString("name");
			result.hash = node.GetString("hash");
			result.time = node.GetUInt32("time");
			result.size = node.GetUInt32("size");
			result.encryption = node.GetString("encryption");
			result.blockCount = node.GetInt32("blockCount");
			var missingBlocks_array = node.GetNode("missingBlocks");
			if (missingBlocks_array != null)
			{
				result.missingBlocks = new int[missingBlocks_array.ChildCount];
				for (int i = 0; i < missingBlocks_array.ChildCount; i++)
				{

					result.missingBlocks[i] = missingBlocks_array.GetNodeByIndex(i).AsInt32();
				}
			}
			else
			{
				result.missingBlocks = new int[0];
			}

			var owners_array = node.GetNode("missingBlocks");
			if (owners_array != null)
			{
				result.owners = new string[owners_array.ChildCount];
				for (int i = 0; i < owners_array.ChildCount; i++)
				{

					result.owners[i] = owners_array.GetNodeByIndex(i).AsString();
				}
			}
			else
			{
				result.owners = new string[0];
			}


			return result;
		}
	}

	public struct ABIParameter
	{
		public string name; //
		public string type; //

		public static ABIParameter FromNode(DataNode node)
		{
			ABIParameter result;

			result.name = node.GetString("name");
			result.type = node.GetString("type");

			return result;
		}
	}

	public struct ABIMethod
	{
		public string name; //
		public string returnType; //
		public ABIParameter[] parameters; //

		public static ABIMethod FromNode(DataNode node)
		{
			ABIMethod result;

			result.name = node.GetString("name");
			result.returnType = node.GetString("returnType");
			var parameters_array = node.GetNode("parameters");
			if (parameters_array != null)
			{
				result.parameters = new ABIParameter[parameters_array.ChildCount];
				for (int i = 0; i < parameters_array.ChildCount; i++)
				{

					result.parameters[i] = ABIParameter.FromNode(parameters_array.GetNodeByIndex(i));

				}
			}
			else
			{
				result.parameters = new ABIParameter[0];
			}


			return result;
		}
	}

	public struct ABIContract
	{
		public string name; //
		public ABIMethod[] methods; //

		public static ABIContract FromNode(DataNode node)
		{
			ABIContract result;

			result.name = node.GetString("name");
			var methods_array = node.GetNode("methods");
			if (methods_array != null)
			{
				result.methods = new ABIMethod[methods_array.ChildCount];
				for (int i = 0; i < methods_array.ChildCount; i++)
				{

					result.methods[i] = ABIMethod.FromNode(methods_array.GetNodeByIndex(i));

				}
			}
			else
			{
				result.methods = new ABIMethod[0];
			}

			return result;
		}
	}

	public struct Channel
	{
		public string creatorAddress; //
		public string targetAddress; //
		public string name; //
		public string chain; //
		public uint creationTime; //
		public string symbol; //
		public string fee; //
		public string balance; //
		public Boolean active; //
		public int index; //

		public static Channel FromNode(DataNode node)
		{
			Channel result;

			result.creatorAddress = node.GetString("creatorAddress");
			result.targetAddress = node.GetString("targetAddress");
			result.name = node.GetString("name");
			result.chain = node.GetString("chain");
			result.creationTime = node.GetUInt32("creationTime");
			result.symbol = node.GetString("symbol");
			result.fee = node.GetString("fee");
			result.balance = node.GetString("balance");
			result.active = node.GetBoolean("active");
			result.index = node.GetInt32("index");

			return result;
		}
	}

	public struct Receipt
	{
		public string nexus; //
		public string channel; //
		public string index; //
		public uint timestamp; //
		public string sender; //
		public string receiver; //
		public string script; //

		public static Receipt FromNode(DataNode node)
		{
			Receipt result;

			result.nexus = node.GetString("nexus");
			result.channel = node.GetString("channel");
			result.index = node.GetString("index");
			result.timestamp = node.GetUInt32("timestamp");
			result.sender = node.GetString("sender");
			result.receiver = node.GetString("receiver");
			result.script = node.GetString("script");

			return result;
		}
	}

	public struct Port
	{
		public string name;
		public int port;

		public static Port FromNode(DataNode node)
		{
			var result = new Port();
			result.name = node.GetString("name");
			result.port = node.GetInt32("port");
			return result;
		}
	}

	public struct Peer
	{
		public string url; //
		public string version; //
		public string flags; //
		public string fee; //
		public uint pow; //
		public Port[] ports;

		public static Peer FromNode(DataNode node)
		{
			Peer result;

			result.url = node.GetString("url");
			result.version = node.GetString("version");
			result.flags = node.GetString("flags");
			result.fee = node.GetString("fee");
			result.pow = node.GetUInt32("pow");

			var ports_array = node.GetNode("methods");
			if (ports_array != null)
			{
				result.ports = new Port[ports_array.ChildCount];
				for (int i = 0; i < ports_array.ChildCount; i++)
				{

					result.ports[i] = Port.FromNode(ports_array.GetNodeByIndex(i));

				}
			}
			else
			{
				result.ports = new Port[0];
			}

			return result;
		}
	}

	public struct Validator
	{
		public string address; //
		public string type; //

		public static Validator FromNode(DataNode node)
		{
			Validator result;

			result.address = node.GetString("address");
			result.type = node.GetString("type");

			return result;
		}
	}

	public struct Leaderboard
	{

		public string name;
		public LeaderboardRow[] rows;

		public struct LeaderboardRow
		{
			public Address address;
			public BigInteger score;

			public static LeaderboardRow FromNode(DataNode node)
			{
				LeaderboardRow result;
				result.address = Address.FromText(node.GetString("address"));
				result.score = node.GetInt32("value");
				return result;
			}
		}

		public static Leaderboard FromNode(DataNode node)
		{
			Leaderboard result;
			result.name = node.GetString("name");
			var rows_array = node.GetNode("rows");
			if (rows_array != null)
			{
				result.rows = new LeaderboardRow[rows_array.ChildCount];
				for (int i = 0; i < rows_array.ChildCount; i++)
				{
					result.rows[i] = LeaderboardRow.FromNode(rows_array.GetNodeByIndex(i));
				}
			}
			else
			{
				result.rows = new LeaderboardRow[0];
			}

			return result;
		}
	}

	public struct Crowdsale
	{
		public string hash;
		public string name;
		public string creator;
		public string flags;
		public uint startDate;
		public uint endDate;
		public string sellSymbol;
		public string receiveSymbol;
		public uint price;
		public string globalSoftCap;
		public string globalHardCap;
		public string userSoftCap;
		public string userHardCap;

		public static Crowdsale FromNode(DataNode node)
		{
			var result = new Crowdsale();
			result.hash = node.GetString("hash");
			result.name = node.GetString("name");
			result.creator = node.GetString("creator");
			result.flags = node.GetString("flags");
			result.startDate = node.GetUInt32("startDate");
			result.endDate = node.GetUInt32("endDate");
			result.sellSymbol = node.GetString("sellSymbol");
			result.receiveSymbol = node.GetString("receiveSymbol");
			result.price = node.GetUInt32("price");
			result.globalSoftCap = node.GetString("globalSoftCap");
			result.globalHardCap = node.GetString("globalHardCap");
			result.userSoftCap = node.GetString("userSoftCap");
			result.userHardCap = node.GetString("userHardCap");
			return result;
		}
	}

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
			yield return WebClient.RPCRequest(Host, "getBlockByHash", WebClient.NoTimeout, errorHandlingCallback, (node) => {
				var result = Block.FromNode(node);
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
			yield return WebClient.RPCRequest(Host, "getBlockByHeight", WebClient.NoTimeout, errorHandlingCallback, (node) => {
				var result = Block.FromNode(node);
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
			yield return WebClient.RPCRequest(Host, "getTransactionByBlockHashAndIndex", WebClient.NoTimeout, errorHandlingCallback, (node) => {
				var result = Transaction.FromNode(node);
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
		public IEnumerator GetAddressTransactions(string addressText, uint page, uint pageSize, Action<AccountTransactions, int, int> callback, Action<EPHANTASMA_SDK_ERROR_TYPE, string> errorHandlingCallback = null)
		{
			yield return WebClient.RPCRequest(Host, "getAddressTransactions", WebClient.NoTimeout, errorHandlingCallback, (node) => {
				var currentPage = node.GetInt32("page");
				var totalPages = node.GetInt32("totalPages");
				node = node.GetNode("result");
				var result = AccountTransactions.FromNode(node);
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
					if (entry.kind == EventKind.Log)
					{
						var bytes = Base16.Decode(entry.data);
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
					if (entry.kind == EventKind.Log)
					{
						var bytes = Base16.Decode(entry.data);
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
			yield return WebClient.RPCRequest(Host, "getLeaderboard", WebClient.NoTimeout, errorHandlingCallback, (node) => {
				var result = Leaderboard.FromNode(node);
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
					result[i] = Event.FromNode(child);
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

			var tx = new Blockchain.Transaction(nexus, chain, script, DateTime.UtcNow + TimeSpan.FromMinutes(20), payload);
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