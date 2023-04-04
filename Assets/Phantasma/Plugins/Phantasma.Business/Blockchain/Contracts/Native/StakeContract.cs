using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Phantasma.Core.Types;

namespace Phantasma.Business.Blockchain.Contracts.Native
{
    public struct EnergyStake
    {
        public BigInteger stakeAmount;
        public Timestamp stakeTime;
    }

    public struct EnergyClaim
    {
        public BigInteger stakeAmount;
        public Timestamp claimDate;
        public bool isNew;
    }

    public struct VotingLogEntry
    {
        public Timestamp timestamp;
        public BigInteger amount;
    }

}
