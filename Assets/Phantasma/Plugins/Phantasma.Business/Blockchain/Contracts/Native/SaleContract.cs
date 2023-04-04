using System;
using System.Linq;
using System.Numerics;
using Phantasma.Core.Cryptography;
using Phantasma.Core.Types;

namespace Phantasma.Business.Blockchain.Contracts.Native
{
    [Flags]
    public enum SaleFlags
    {
        None = 0,
        Whitelist = 1,
    }

    public enum SaleEventKind
    {
        Creation,
        SoftCap,
        HardCap,
        AddedToWhitelist,
        RemovedFromWhitelist,
        Distribution,
        Refund,
        PriceChange,
        Participation,
    }

    public struct SaleEventData
    {
        public Hash saleHash;
        public SaleEventKind kind;
    }

    public struct SaleInfo
    {
        public Address Creator;
        public string Name;
        public SaleFlags Flags;
        public Timestamp StartDate;
        public Timestamp EndDate;

        public string SellSymbol;
        public string ReceiveSymbol;
        public BigInteger Price;
        public BigInteger GlobalSoftCap;
        public BigInteger GlobalHardCap;
        public BigInteger UserSoftCap;
        public BigInteger UserHardCap;
    }

}
