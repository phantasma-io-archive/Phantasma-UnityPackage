using System.Collections.Generic;
using Phantasma.Core.Cryptography;
using Phantasma.Core.Domain;

namespace Phantasma.Plugins.Phantasma.Core.Domain
{
    public interface ITokenSwapper
    {
        Hash SettleSwap(string sourcePlatform, string destPlatform, Hash sourceHash);
        IEnumerable<ChainSwap> GetPendingSwaps(Address address);

        bool SupportsSwap(string sourcePlatform, string destPlatform);
    }
}
