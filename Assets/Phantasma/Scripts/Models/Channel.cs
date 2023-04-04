using System;
using LunarLabs.Parser;

namespace Phantasma.SDK
{
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
}