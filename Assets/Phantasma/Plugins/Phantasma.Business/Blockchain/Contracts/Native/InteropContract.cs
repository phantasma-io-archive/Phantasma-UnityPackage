using System.Numerics;
using Phantasma.Core.Cryptography;
using Phantasma.Core.Types;

namespace Phantasma.Business.Blockchain.Contracts.Native
{
    public enum InteropTransferStatus
    {
        Unknown,
        Pending,
        Confirmed
    }

    public struct InteropWithdraw
    {
        public Hash hash;
        public Address destination;
        public string transferSymbol;
        public BigInteger transferAmount;
        public string feeSymbol;
        public BigInteger feeAmount;
        public Timestamp timestamp;
    }

    public struct InteropHistory
    {
        public Hash sourceHash;
        public string sourcePlatform;
        public string sourceChain;
        public Address sourceAddress;

        public Hash destHash;
        public string destPlatform;
        public string destChain;
        public Address destAddress;

        public string symbol;
        public BigInteger value;
    }

}
