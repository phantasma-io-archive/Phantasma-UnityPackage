/*using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Block
    {
        public string hash; //
        public string previousHash; //
        public uint timestamp; //
        public uint height; //
        public string chainAddress; //
        public uint protocol; //
        public Transaction[] txs; //
        public string validatorAddress; //
        public string reward; //
        public Event[] events;
        public Oracle[] oracles;

        public static Block FromNode(DataNode node)
        {
            Block result;

            result.hash = node.GetString("hash");
            result.previousHash = node.GetString("previousHash");
            result.timestamp = node.GetUInt32("timestamp");
            result.height = node.GetUInt32("height");
            result.chainAddress = node.GetString("chainAddress");
            result.protocol = node.GetUInt32("protocol");
            var txs_array = node.GetNode("txs");
            if (txs_array != null)
            {
                result.txs = new Transaction[txs_array.ChildCount];
                for (int i = 0; i < txs_array.ChildCount; i++)
                {

                    result.txs[i] = Transaction.FromNode(txs_array.GetNodeByIndex(i));

                }
            }
            else
            {
                result.txs = new Transaction[0];
            }

            result.validatorAddress = node.GetString("validatorAddress");
            result.reward = node.GetString("reward");

            var events_array = node.GetNode("events");
            if (events_array != null)
            {
                result.events = new Event[events_array.ChildCount];
                for (int i = 0; i < events_array.ChildCount; i++)
                {

                    result.events[i] = Event.FromNode(events_array.GetNodeByIndex(i));

                }
            }
            else
            {
                result.events = new Event[0];
            }


            var oracles_array = node.GetNode("events");
            if (oracles_array != null)
            {
                result.oracles = new Oracle[oracles_array.ChildCount];
                for (int i = 0; i < oracles_array.ChildCount; i++)
                {

                    result.oracles[i] = Oracle.FromNode(oracles_array.GetNodeByIndex(i));

                }
            }
            else
            {
                result.oracles = new Oracle[0];
            }


            return result;
        }
    }
}*/