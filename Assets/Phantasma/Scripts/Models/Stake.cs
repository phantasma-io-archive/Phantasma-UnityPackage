using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Stake
    {
        public string amount; //
        public uint time; //
        public string unclaimed; //

        public static Stake FromNode(DataNode node)
        {
            Stake result;

            result.amount = node.GetString("amount");
            result.time = node.GetUInt32("time");
            result.unclaimed = node.GetString("unclaimed");

            return result;
        }
    }
}