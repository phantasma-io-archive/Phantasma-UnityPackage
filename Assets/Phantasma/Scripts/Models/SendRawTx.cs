using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct SendRawTx
    {
        public string hash; //
        public string error; //

        public static SendRawTx FromNode(DataNode node)
        {
            SendRawTx result;

            result.hash = node.GetString("hash");
            result.error = node.GetString("error");

            return result;
        }
    }
}