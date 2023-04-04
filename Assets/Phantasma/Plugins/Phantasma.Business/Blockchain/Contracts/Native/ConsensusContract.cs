using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using Phantasma.Core.Cryptography;
using Phantasma.Core.Cryptography.ECDsa;
using Phantasma.Core.Cryptography.EdDSA;
using Phantasma.Core.Domain;
using Phantasma.Core.Types;
using Phantasma.Core.Utils;

namespace Phantasma.Business.Blockchain.Contracts.Native
{
    public enum ConsensusMode
    {
        Unanimity,
        Majority,
        Popularity,
        Ranking,
    }

    public enum PollState
    {
        Inactive,
        Active,
        Consensus,
        Failure,
        Finished
    }

    public struct PollChoice : ISerializable
    {
        public byte[] value;

        public PollChoice(byte[] value)
        {
            this.value = value;
        }

        public void SerializeData(BinaryWriter writer)
        {
            writer.WriteByteArray(value);
        }

        public void UnserializeData(BinaryReader reader)
        {
            value = reader.ReadByteArray();
        }
    }

    public struct PollValue : ISerializable
    {
        public byte[] value;
        public BigInteger ranking;
        public BigInteger votes;

        public PollValue(byte[] value, BigInteger ranking, BigInteger votes)
        {
            this.value = value;
            this.ranking = ranking;
            this.votes = votes;
        }

        public void SerializeData(BinaryWriter writer)
        {
            writer.WriteByteArray(value);
            writer.WriteBigInteger(ranking);
            writer.WriteBigInteger(votes);
        }

        public void UnserializeData(BinaryReader reader)
        {
            value = reader.ReadByteArray();
            ranking = reader.ReadBigInteger();
            votes = reader.ReadBigInteger();
        }
    }

    public struct PollVote : ISerializable
    {
        public BigInteger index;
        public BigInteger percentage;

        public PollVote(BigInteger index, BigInteger percentage)
        {
            this.index = index;
            this.percentage = percentage;
        }

        public void SerializeData(BinaryWriter writer)
        {
            writer.WriteBigInteger(index);
            writer.WriteBigInteger(percentage);
        }

        public void UnserializeData(BinaryReader reader)
        {
            index = reader.ReadBigInteger();
            percentage = reader.ReadBigInteger();
        }
    }

    public struct ConsensusPoll : ISerializable
    {
        public string subject;
        public string organization;
        public ConsensusMode mode;
        public PollState state;
        public PollValue[] entries;
        public BigInteger round;
        public Timestamp startTime;
        public Timestamp endTime;
        public BigInteger choicesPerUser;
        public BigInteger totalVotes;
        public Timestamp consensusTime;

        public ConsensusPoll(string subject, string organization, ConsensusMode mode, PollState state, PollValue[] entries, BigInteger round, Timestamp startTime, Timestamp endTime, BigInteger choicesPerUser, BigInteger totalVotes, Timestamp consensusTime)
        {
            this.subject = subject;
            this.organization = organization;
            this.mode = mode;
            this.state = state;
            this.entries = entries;
            this.round = round;
            this.startTime = startTime;
            this.endTime = endTime;
            this.choicesPerUser = choicesPerUser;
            this.totalVotes = totalVotes;
            this.consensusTime = consensusTime;
        }

        public void SerializeData(BinaryWriter writer)
        {
            writer.WriteVarString(subject);
            writer.WriteVarString(organization);
            writer.Write((byte)mode);
            writer.Write((byte)state);
            writer.Write(entries.Length);
            foreach (var entry in entries)
            {
                entry.SerializeData(writer);
            }
            writer.WriteBigInteger(round);
            writer.WriteTimestamp(startTime);
            writer.WriteTimestamp(endTime);
            writer.WriteBigInteger(choicesPerUser);
            writer.WriteBigInteger(totalVotes);
            writer.WriteTimestamp(consensusTime);
        }

        public void UnserializeData(BinaryReader reader)
        {
            subject = reader.ReadVarString();
            organization = reader.ReadVarString();
            mode = (ConsensusMode)reader.ReadByte();
            state = (PollState)reader.ReadByte();
            var count = reader.ReadInt32();
            entries = new PollValue[count];
            for (int i = 0; i < count; i++)
            {
                entries[i].UnserializeData(reader);
            }
            round = reader.ReadBigInteger();
            startTime = reader.ReadTimestamp();
            endTime = reader.ReadTimestamp();
            choicesPerUser = reader.ReadBigInteger();
            totalVotes = reader.ReadBigInteger();
            consensusTime = reader.ReadTimestamp();
        }
    }

    public struct PollPresence : ISerializable
    {
        public string subject;
        public BigInteger round;

        public PollPresence(string subject, BigInteger round)
        {
            this.subject = subject;
            this.round = round;
        }

        public void SerializeData(BinaryWriter writer)
        {
            writer.WriteVarString(subject);
            writer.WriteBigInteger(round);
        }

        public void UnserializeData(BinaryReader reader)
        {
            subject = reader.ReadVarString();
            round = reader.ReadBigInteger();
        }
    }
    
    
}
