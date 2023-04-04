using System.Numerics;
using Phantasma.Core.Cryptography;

namespace Phantasma.Core.Domain
{
    public interface IOrganization
    {
        string ID { get; }
        string Name { get; }
        Address Address { get; }
        byte[] Script { get; }
        BigInteger Size { get; } // number of members
        Address[] GetMembers();
    }
}
