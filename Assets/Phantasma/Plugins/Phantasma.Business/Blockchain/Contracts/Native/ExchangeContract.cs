using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using Phantasma.Core.Cryptography;
using Phantasma.Core.Domain;
using Phantasma.Core.Numerics;
using Phantasma.Core.Types;
using Phantasma.Core.Utils;
using static Phantasma.Business.Blockchain.Contracts.Native.ExchangeOrderSide;
using static Phantasma.Business.Blockchain.Contracts.Native.ExchangeOrderType;

namespace Phantasma.Business.Blockchain.Contracts.Native
{
    public struct LPTokenContentROM : ISerializable
    {
        public string Symbol0;
        public string Symbol1;
        public BigInteger ID;

        public LPTokenContentROM(string Symbol0, string Symbol1, BigInteger ID)
        {
            this.Symbol0 = Symbol0;
            this.Symbol1 = Symbol1;
            this.ID = ID;
        }

        public void SerializeData(BinaryWriter writer)
        {
            writer.WriteVarString(Symbol0);
            writer.WriteVarString(Symbol1);
            writer.WriteBigInteger(ID);
        }

        public void UnserializeData(BinaryReader reader)
        {
            Symbol0 = reader.ReadVarString();
            Symbol1 = reader.ReadVarString();
            ID = reader.ReadBigInteger();
        }
    }

    public struct LPTokenContentRAM : ISerializable
    {
        public BigInteger Amount0;
        public BigInteger Amount1;
        public BigInteger Liquidity;
        public BigInteger ClaimedFeesSymbol0;
        public BigInteger ClaimedFeesSymbol1;

        public LPTokenContentRAM(BigInteger Amount0, BigInteger Amount1, BigInteger Liquidity)
        {
            this.Amount0 = Amount0;
            this.Amount1 = Amount1;
            this.Liquidity = Liquidity;
            ClaimedFeesSymbol0 = 0;
            ClaimedFeesSymbol1 = 0;
        }

        public void SerializeData(BinaryWriter writer)
        {
            writer.WriteBigInteger(Amount0);
            writer.WriteBigInteger(Amount1);
            writer.WriteBigInteger(Liquidity);
            writer.WriteBigInteger(ClaimedFeesSymbol0);
            writer.WriteBigInteger(ClaimedFeesSymbol1);
        }

        public void UnserializeData(BinaryReader reader)
        {
            Amount0 = reader.ReadBigInteger();
            Amount1 = reader.ReadBigInteger();
            Liquidity = reader.ReadBigInteger();
            ClaimedFeesSymbol0 = reader.ReadBigInteger();
            ClaimedFeesSymbol1 = reader.ReadBigInteger();
        }
    }

    public struct TradingVolume : ISerializable
    {
        public string Symbol0;
        public string Symbol1;
        public string Day;
        public BigInteger VolumeSymbol0;
        public BigInteger VolumeSymbol1;

        public TradingVolume(string Symbol0, string Symbol1, string Day, BigInteger VolumeSymbol0, BigInteger VolumeSymbol1)
        {
            this.Symbol0 = Symbol0;
            this.Symbol1 = Symbol1;
            this.Day = Day;
            this.VolumeSymbol0 = VolumeSymbol0;
            this.VolumeSymbol1 = VolumeSymbol1;
        }

        public void SerializeData(BinaryWriter writer)
        {
            writer.WriteVarString(Symbol0);
            writer.WriteVarString(Symbol1);
            writer.WriteVarString(Day);
            writer.WriteBigInteger(VolumeSymbol0);
            writer.WriteBigInteger(VolumeSymbol1);
        }

        public void UnserializeData(BinaryReader reader)
        {
            Symbol0 = reader.ReadVarString();
            Symbol1 = reader.ReadVarString();
            Day = reader.ReadVarString();
            VolumeSymbol0 = reader.ReadBigInteger();
            VolumeSymbol1 = reader.ReadBigInteger();
        }
    }

    public struct Pool : ISerializable
    {
        public string Symbol0; // Symbol
        public string Symbol1; // Pair
        public string Symbol0Address;
        public string Symbol1Address;
        public BigInteger Amount0;
        public BigInteger Amount1;
        public BigInteger FeeRatio;
        public BigInteger TotalLiquidity;
        public BigInteger FeesForUsersSymbol0;
        public BigInteger FeesForUsersSymbol1;
        public BigInteger FeesForOwnerSymbol0;
        public BigInteger FeesForOwnerSymbol1;


        public Pool(string Symbol0, string Symbol1, string Symbol0Address, string Symbol1Address, BigInteger Amount0, BigInteger Amount1, BigInteger FeeRatio, BigInteger TotalLiquidity)
        {
            this.Symbol0 = Symbol0;
            this.Symbol1 = Symbol1;
            this.Symbol0Address = Symbol0Address;
            this.Symbol1Address = Symbol1Address;
            this.Amount0 = Amount0;
            this.Amount1 = Amount1;
            this.FeeRatio = FeeRatio;
            this.TotalLiquidity = TotalLiquidity;
            FeesForUsersSymbol0 = 0;
            FeesForUsersSymbol1 = 0;
            FeesForOwnerSymbol0 = 0;
            FeesForOwnerSymbol1 = 0;
        }

        public void SerializeData(BinaryWriter writer)
        {
            writer.WriteVarString(Symbol0);
            writer.WriteVarString(Symbol1);
            writer.WriteVarString(Symbol0Address);
            writer.WriteVarString(Symbol1Address);
            writer.WriteBigInteger(Amount0);
            writer.WriteBigInteger(Amount1);
            writer.WriteBigInteger(FeeRatio);
            writer.WriteBigInteger(TotalLiquidity);
            writer.WriteBigInteger(FeesForUsersSymbol0);
            writer.WriteBigInteger(FeesForUsersSymbol1);
            writer.WriteBigInteger(FeesForOwnerSymbol0);
            writer.WriteBigInteger(FeesForOwnerSymbol1);
        }

        public void UnserializeData(BinaryReader reader)
        {
            Symbol0 = reader.ReadVarString();
            Symbol1 = reader.ReadVarString();
            Symbol0Address = reader.ReadVarString();
            Symbol1Address = reader.ReadVarString();
            Amount0 = reader.ReadBigInteger();
            Amount1 = reader.ReadBigInteger();
            FeeRatio = reader.ReadBigInteger();
            TotalLiquidity = reader.ReadBigInteger();
            FeesForUsersSymbol0 = reader.ReadBigInteger();
            FeesForUsersSymbol1 = reader.ReadBigInteger();
            FeesForOwnerSymbol0 = reader.ReadBigInteger();
            FeesForOwnerSymbol1 = reader.ReadBigInteger();
        }
    }

    public struct LPHolderInfo : ISerializable
    {
        public BigInteger NFTID;
        public BigInteger UnclaimedSymbol0;
        public BigInteger UnclaimedSymbol1;
        public BigInteger ClaimedSymbol0;
        public BigInteger ClaimedSymbol1;

        public LPHolderInfo(BigInteger NFTID, BigInteger unclaimedSymbol0, BigInteger unclaimedSymbol1, BigInteger claimedSymbol0, BigInteger claimedSymbol1)
        {
            this.NFTID = NFTID;
            UnclaimedSymbol0 = unclaimedSymbol0;
            UnclaimedSymbol1 = unclaimedSymbol1;
            ClaimedSymbol0 = claimedSymbol0;
            ClaimedSymbol1 = claimedSymbol1;
        }

        public void SerializeData(BinaryWriter writer)
        {
            writer.WriteBigInteger(NFTID);
            writer.WriteBigInteger(UnclaimedSymbol0);
            writer.WriteBigInteger(UnclaimedSymbol1);
            writer.WriteBigInteger(ClaimedSymbol0);
            writer.WriteBigInteger(ClaimedSymbol1);
        }

        public void UnserializeData(BinaryReader reader)
        {
            NFTID = reader.ReadBigInteger();
            UnclaimedSymbol0 = reader.ReadBigInteger();
            UnclaimedSymbol1 = reader.ReadBigInteger();
            ClaimedSymbol0 = reader.ReadBigInteger();
            ClaimedSymbol1 = reader.ReadBigInteger();
        }
    }

    public enum ExchangeOrderSide
    {
        Buy,
        Sell
    }

    public enum ExchangeOrderType
    {
        OTC,
        Limit,              //normal limit order
        ImmediateOrCancel,  //special limit order, any unfulfilled part of the order gets cancelled if not immediately fulfilled
        Market,             //normal market order
        //TODO: FillOrKill = 4,         //Either gets 100% fulfillment or it gets cancelled , no partial fulfillments like in IoC order types
    }

    public struct ExchangeOrder
    {
        public readonly BigInteger Uid;
        public readonly Timestamp Timestamp;
        public readonly Address Creator;
        public readonly Address Provider;

        public readonly BigInteger Amount;
        public readonly string BaseSymbol;

        public readonly BigInteger Price;
        public readonly string QuoteSymbol;

        public readonly ExchangeOrderSide Side;
        public readonly ExchangeOrderType Type;

        public ExchangeOrder(BigInteger uid, Timestamp timestamp, Address creator, Address provider, BigInteger amount, string baseSymbol, BigInteger price, string quoteSymbol, ExchangeOrderSide side, ExchangeOrderType type)
        {
            Uid = uid;
            Timestamp = timestamp;
            Creator = creator;
            Provider = provider;

            Amount = amount;
            BaseSymbol = baseSymbol;

            Price = price;
            QuoteSymbol = quoteSymbol;

            Side = side;
            Type = type;
        }

        public ExchangeOrder(ExchangeOrder order, BigInteger newPrice, BigInteger newOrderSize)
        {
            Uid = order.Uid;
            Timestamp = order.Timestamp;
            Creator = order.Creator;
            Provider = order.Provider;

            Amount = newOrderSize;
            BaseSymbol = order.BaseSymbol;

            Price = newPrice;
            QuoteSymbol = order.QuoteSymbol;

            Side = order.Side;
            Type = order.Type;

        }
    }

    public struct TokenSwap
    {
        public Address buyer;
        public Address seller;
        public string baseSymbol;
        public string quoteSymbol;
        public BigInteger value;
        public BigInteger price;
    }

    public struct ExchangeProvider
    {
        public static readonly ExchangeProvider Null = new ExchangeProvider
        {
            address = Address.Null
        }; 
        public Address address;
        public string id;
        public string name;
        public BigInteger TotalFeePercent;
        public BigInteger FeePercentForExchange;
        public BigInteger FeePercentForPool;
        public Hash dapp;
    }
}
