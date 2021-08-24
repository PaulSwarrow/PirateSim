using Services.AI.Interfaces;
using Services.AI.Structure;

namespace DefaultNamespace
{
    public static class PropertyPathUtil
    {
        private const char Splitter = '/';
        public static void Parse(ref string path, out string name)
        {
            var index = path.IndexOf(Splitter);
            if (index == -1)
            {
                name = path;
                path = null;
            }
            else
            {
                name = path.Substring(0, index);
                path = path.Remove(0, index);
            }
        }
    }
}