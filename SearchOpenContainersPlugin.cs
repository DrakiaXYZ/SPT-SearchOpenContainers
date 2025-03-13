using BepInEx;
using DrakiaXYZ.SearchOpenContainers.Patches;

namespace DrakiaXYZ.SearchOpenContainers
{
    [BepInPlugin("xyz.drakia.searchopencontainers", "DrakiaXYZ-SearchOpenContainers", "1.3.0")]
    [BepInDependency("com.SPT.core", "3.11.0")]
    public class SearchOpenContainersPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            new ContainerMenuPatch().Enable();
        }
    }
}
