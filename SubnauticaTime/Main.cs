using HarmonyLib;
using QModManager.API.ModLoading;

namespace SubnauticaTimeBZ
{
    [QModCore]
    public class MainPatch
    {
        public static void FirstStart()
        {
            SecondStart();
        }
        [QModPatch]
        public static void SecondStart()
        {
            Harmony harmony = new Harmony("SubnauticaTimeBZ.mod");
            harmony.PatchAll();
        }
    }
}