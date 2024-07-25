
using _1Lab.Scripts.Managers.ProjectSettings.Loaders;

namespace _1Lab.Scripts.Managers.ProjectSettings
{
    public static class Project
    {
        public static readonly AssetLoader Loader = new(new ResourceLoader());
    }
}