using LunarLabs.Parser;
using Phantasma.Core.Domain;

namespace Phantasma.SDK
{
    public struct Transaction
    {
        public string hash; //
        public string chainAddress; //
        public uint timestamp; //
        public int blockHeight; //
        public string blockHash; //
        public string script; //
        public string payload;
        public Event[] events; //
        public string result; //
        public string fee; //
        public Signature[] signatures;
        public uint expiration;
        //public int confirmations; //

        public static Transaction FromNode(DataNode node)
        {
            Transaction result;

            result.hash = node.GetString("hash");
            result.chainAddress = node.GetString("chainAddress");
            result.timestamp = node.GetUInt32("timestamp");
            result.blockHeight = node.GetInt32("blockHeight");
            result.blockHash = node.GetString("blockHash");
            result.script = node.GetString("script");
            result.payload = node.GetString("payload");
            var events_array = node.GetNode("events");
            if (events_array != null)
            {
                result.events = new Event[events_array.ChildCount];
                for (int i = 0; i < events_array.ChildCount; i++)
                {
                    result.events[i] = new Event().FromNode(events_array.GetNodeByIndex(i));
                }
            }
            else
            {
                result.events = new Event[0];
            }

            result.result = node.GetString("result");
            result.fee = node.GetString("fee");

            var signatures_array = node.GetNode("signatures");
            if (signatures_array != null)
            {
                result.signatures = new Signature[signatures_array.ChildCount];
                for (int i = 0; i < signatures_array.ChildCount; i++)
                {

                    result.signatures[i] = Signature.FromNode(signatures_array.GetNodeByIndex(i));

                }
            }
            else
            {
                result.signatures = new Signature[0];
            }

            result.expiration = node.GetUInt32("expiration");

            return result;
        }
    }
}