using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct ContractMethod
    {
        public string name;
        public string returnType;
        public ContractParameter[] parameters;

        public static ContractMethod FromNode(DataNode node)
        {
            ContractMethod result = new ContractMethod();
            result.name = node.GetString("name");
            result.returnType = node.GetString("returnType");

            var parameters_array = node.GetNode("parameters");
            if (parameters_array != null)
            {
                result.parameters = new ContractParameter[parameters_array.ChildCount];
                for (int i = 0; i < parameters_array.ChildCount; i++)
                {
                    result.parameters[i] = ContractParameter.FromNode(parameters_array.GetNodeByIndex(i));
                }
            }
            else
            {
                result.parameters = new ContractParameter[0];
            }

            return result;
        }
    }
}