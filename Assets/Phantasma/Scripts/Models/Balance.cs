using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Balance
    {
        public string chain; //
        public string amount; //
        public string symbol; //
        public uint decimals; //
        public string[] ids; //

        public static Balance FromNode(DataNode node)
        {
            Balance result;

            result.chain = node.GetString("chain");
            result.amount = node.GetString("amount");
            result.symbol = node.GetString("symbol");
            result.decimals = node.GetUInt32("decimals");
            var ids_array = node.GetNode("ids");
            if (ids_array != null)
            {
                result.ids = new string[ids_array.ChildCount];
                for (int i = 0; i < ids_array.ChildCount; i++)
                {

                    result.ids[i] = ids_array.GetNodeByIndex(i).AsString();
                }
            }
            else
            {
                result.ids = new string[0];
            }


            return result;
        }
    }
}