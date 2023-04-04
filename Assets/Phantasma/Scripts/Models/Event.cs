using LunarLabs.Parser;
using Phantasma.Core.Domain;
using Phantasma.Core.Numerics;

namespace Phantasma.SDK
{
    public static class EventExtensions
    {
        public static Event FromNode( this Event Event, DataNode node)
        {

            var Address = Core.Cryptography.Address.FromText(node.GetString("address"));
            var Contract = node.GetString("contract");
            var Kind = node.GetEnum<EventKind>("kind");
            var Data = node.GetString("data");
            
            Event result = new Event(Kind, Address, Contract, Base16.Decode(Data));


            return result;
        }

        public static string ToString(this Event Event)
        {
            return $"{Event.Kind} @ {Event.Address}";
        }
    }
}