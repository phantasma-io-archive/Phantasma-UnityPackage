using LunarLabs.Parser;
using Phantasma.Core.Domain;

namespace Phantasma.SDK
{
    public struct Script
    {
        public Event[] events; //
        public string result; //
        public string[] results; //
        public Oracle[] oracles; //

        public static Script FromNode(DataNode node)
        {
            Script result;

            var events_array = node.GetNode("events");
            if (events_array != null)
            {
                result.events = new Event[events_array.ChildCount];
                for (int i = 0; i < events_array.ChildCount; i++)
                {

                    //result.events[i] = Event.FromNode(events_array.GetNodeByIndex(i));

                }
            }
            else
            {
                result.events = new Event[0];
            }

            result.result = node.GetString("result");

            var results_array = node.GetNode("results");
            if (results_array != null)
            {
                result.results = new string[results_array.ChildCount];
                for (int i = 0; i < results_array.ChildCount; i++)
                {

                    result.results[i] = results_array.GetNodeByIndex(i).Value;

                }
            }
            else
            {
                result.results = new string[0];
            }

            var oracles_array = node.GetNode("oracles");
            if (oracles_array != null)
            {
                result.oracles = new Oracle[oracles_array.ChildCount];
                for (int i = 0; i < oracles_array.ChildCount; i++)
                {

                    result.oracles[i] = Oracle.FromNode(oracles_array.GetNodeByIndex(i));

                }
            }
            else
            {
                result.oracles = new Oracle[0];
            }

            return result;
        }
    }
}