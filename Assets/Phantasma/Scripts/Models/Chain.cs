using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Chain
    {
        public string name; //
        public string address; //
        public string parentAddress; //
        public uint height; //
        public string organization;
        public string[] contracts; //

        public static Chain FromNode(DataNode node)
        {
            Chain result;

            result.name = node.GetString("name");
            result.address = node.GetString("address");
            result.parentAddress = node.GetString("parentAddress");
            result.height = node.GetUInt32("height");
            result.organization = node.GetString("organization");
            var contracts_array = node.GetNode("contracts");
            if (contracts_array != null)
            {
                result.contracts = new string[contracts_array.ChildCount];
                for (int i = 0; i < contracts_array.ChildCount; i++)
                {

                    result.contracts[i] = contracts_array.GetNodeByIndex(i).AsString();
                }
            }
            else
            {
                result.contracts = new string[0];
            }


            return result;
        }
    }
}