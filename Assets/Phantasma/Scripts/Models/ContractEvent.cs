using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct ContractEvent
    {
        public int value;
        public string name;
        public string returnType;
        public string description;

        public static ContractEvent FromNode(DataNode node)
        {
            var result = new ContractEvent();
            result.value = node.GetInt32("value");
            result.name = node.GetString("name");
            result.returnType = node.GetString("returnType");
            result.description = node.GetString("description");
            return result;
        }
    }
}