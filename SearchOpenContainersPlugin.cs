using BepInEx;
using DrakiaXYZ.SearchOpenContainers.Patches;

namespace DrakiaXYZ.SearchOpenContainers
{
    [BepInPlugin("xyz.drakia.searchopencontainers", "DrakiaXYZ-SearchOpenContainers", "1.5.0")]
    [BepInDependency("com.SPT.core", "4.1.0")]
    public class SearchOpenContainersPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            new ContainerMenuPatch().Enable();
        }
    }
}
