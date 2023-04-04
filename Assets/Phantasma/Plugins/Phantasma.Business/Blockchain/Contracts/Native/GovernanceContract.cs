using System.Numerics;

namespace Phantasma.Business.Blockchain.Contracts.Native
{
    public enum ConstraintKind
    {
        MaxValue,
        MinValue,
        GreatThanOther,
        LessThanOther,
        MustIncrease,
        MustDecrease,
        Deviation,
    }

    public struct ChainConstraint
    {
        public ConstraintKind Kind;
        public BigInteger Value;
        public string Tag;
    }

    public struct GovernancePair
    {
        public string Name;
        public BigInteger Value;
    }

}
