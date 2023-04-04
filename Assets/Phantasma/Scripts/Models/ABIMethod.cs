using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct ABIMethod
    {
        public string name; //
        public string returnType; //
        public ABIParameter[] parameters; //

        public static ABIMethod FromNode(DataNode node)
        {
            ABIMethod result;

            result.name = node.GetString("name");
            result.returnType = node.GetString("returnType");
            var parameters_array = node.GetNode("parameters");
            if (parameters_array != null)
            {
                result.parameters = new ABIParameter[parameters_array.ChildCount];
                for (int i = 0; i < parameters_array.ChildCount; i++)
                {

                    result.parameters[i] = ABIParameter.FromNode(parameters_array.GetNodeByIndex(i));

                }
            }
            else
            {
                result.parameters = new ABIParameter[0];
            }


            return result;
        }
    }
}