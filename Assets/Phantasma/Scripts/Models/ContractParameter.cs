using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct ContractParameter
    {
        public string name;
        public string type;

        public static ContractParameter FromNode(DataNode node)
        {
            ContractParameter result = new ContractParameter();
            result.name = node.GetString("name");
            result.type = node.GetString("type");
            return result;
        }
    }
}