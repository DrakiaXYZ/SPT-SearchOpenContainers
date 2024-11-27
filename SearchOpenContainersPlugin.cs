using BepInEx;
using DrakiaXYZ.SearchOpenContainers.Patches;

namespace DrakiaXYZ.SearchOpenContainers
{
    [BepInPlugin("xyz.drakia.searchopencontainers", "DrakiaXYZ-SearchOpenContainers", "1.2.0")]
    [BepInDependency("com.SPT.core", "3.10.0")]
    public class SearchOpenContainersPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo("[SearchOpenContainers] Loading...");

            new ContainerMenuPatch().Enable();

            Logger.LogInfo("[SearchOpenContainers] Loaded!");
        }
    }
}
