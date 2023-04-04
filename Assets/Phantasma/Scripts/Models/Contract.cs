using LunarLabs.Parser;

namespace Phantasma.SDK
{
    public struct Contract
    {
        public string name; //
        public string address; //
        public string script; //
        public ContractMethod[] methods;
        public ContractEvent[] events;

        public static Contract FromNode(DataNode node)
        {
            Contract result;

            result.address = node.GetString("address");
            result.name = node.GetString("name");
            result.script = node.GetString("script");

            var methods_array = node.GetNode("methods");
            if (methods_array != null)
            {
                result.methods = new ContractMethod[methods_array.ChildCount];
                for (int i = 0; i < methods_array.ChildCount; i++)
                {
                    result.methods[i] = ContractMethod.FromNode(methods_array.GetNodeByIndex(i));
                }
            }
            else
            {
                result.methods = new ContractMethod[0];
            }

            var event_array = node.GetNode("events");
            if (event_array != null)
            {
                result.events = new ContractEvent[event_array.ChildCount];
                for (int i = 0; i < event_array.ChildCount; i++)
                {
                    result.events[i] = ContractEvent.FromNode(event_array.GetNodeByIndex(i));
                }
            }
            else
            {
                result.events = new ContractEvent[0];
            }

            return result;
        }
    }
}