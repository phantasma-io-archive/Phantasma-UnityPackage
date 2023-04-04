using LunarLabs.Parser;

namespace Phantasma.SDK
{
	internal static class PhantasmaAPIUtils
	{
		internal static long GetInt64(this DataNode node, string name)
		{
			return node.GetInt64(name);
		}

		internal static bool GetBoolean(this DataNode node, string name)
		{
			return node.GetBool(name);
		}
	}
}