using System;
using System.Collections.Generic;
using System.Numerics;
using Phantasma.Core.Cryptography;
using Phantasma.Core.Numerics;

namespace Phantasma.Core.Domain
{
    public static class DomainExtensions
    {
        public static bool IsFungible(this IToken token)
        {
            return token.Flags.HasFlag(TokenFlags.Fungible);
        }

        public static bool IsBurnable(this IToken token)
        {
            return token.Flags.HasFlag(TokenFlags.Burnable);
        }

        public static bool IsTransferable(this IToken token)
        {
            return token.Flags.HasFlag(TokenFlags.Transferable);
        }

        public static bool IsCapped(this IToken token)
        {
            return token.MaxSupply > 0;
        }

        public static T GetKind<T>(this Event evt)
        {
            return (T)(object)evt.Kind;
        }

        public static T GetContent<T>(this Event evt)
        {
            return Serialization.Unserialize<T>(evt.Data);
        }

        public static T DecodeCustomEvent<T>(this EventKind kind)
        {
            if (kind < EventKind.Custom)
            {
                throw new Exception("Cannot cast system event");
            }

            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new Exception("Can only cast event to other enum");
            }

            var intVal = ((int)kind - (int)EventKind.Custom);
            var temp = (T)Enum.Parse(type, intVal.ToString());
            return temp;
        }

        public static EventKind EncodeCustomEvent(Enum kind)
        {
            var temp = (EventKind)((int)Convert.ChangeType(kind, kind.GetTypeCode()) + (int)EventKind.Custom);
            return temp;
        }

        public static Address GetChainAddress(this IPlatform platform)
        {
            return Address.FromHash(platform.Name);
        }

        

        public static string GetOracleTransactionURL(string platform, string chain, Hash hash)
        {
            return $"interop://{platform}/{chain}/tx/{hash}";
        }

        public static string GetOracleBlockURL(string platform, string chain, Hash hash)
        {
            return $"interop://{platform}/{chain}/block/{hash}";
        }

        public static string GetOracleBlockURL(string platform, string chain, BigInteger height)
        {
            return $"interop://{platform}/{chain}/block/{height}";
        }

        public static string GetOracleNFTURL(string platform, string symbol, BigInteger tokenID)
        {
            return $"interop://{platform}/nft/{symbol}/{tokenID}";
        }

        public static string GetOracleFeeURL(string platform)
        {
            return $"fee://{platform}";
        }

        public static BigInteger GetBlockCount(this IArchive archive)
        {
            var total = (archive.Size / DomainSettings.ArchiveBlockSize);

            if (archive.Size % DomainSettings.ArchiveBlockSize != 0)
            {
                total++;
            }

            return total;
        }
    }
}
