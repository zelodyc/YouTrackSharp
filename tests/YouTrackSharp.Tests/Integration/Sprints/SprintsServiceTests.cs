using System.IO;
using System.Reflection;
using JetBrains.Annotations;

namespace YouTrackSharp.Tests.Integration.Sprints
{
    [UsedImplicitly]
    public partial class SprintsServiceTests
    {
        private static string FullSprint => GetTextResource("YouTrackSharp.Tests.Resources.FullSprint.json");
        
        private static string GetTextResource(string name)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            using Stream stream = assembly.GetManifestResourceStream(name);
            if (stream == null)
            {
                return string.Empty;
            }

            using StreamReader reader = new StreamReader(stream);

            return reader.ReadToEnd();
        }
    }
}