using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct ABIContract
    {
        public string name; //
        public ABIMethod[] methods; //

        public static ABIContract FromNode(DataNode node)
        {
            ABIContract result;

            result.name = node.GetString("name");
            var methods_array = node.GetNode("methods");
            if (methods_array != null)
            {
                result.methods = new ABIMethod[methods_array.ChildCount];
                for (int i = 0; i < methods_array.ChildCount; i++)
                {

                    result.methods[i] = ABIMethod.FromNode(methods_array.GetNodeByIndex(i));

                }
            }
            else
            {
                result.methods = new ABIMethod[0];
            }

            return result;
        }
    }
}