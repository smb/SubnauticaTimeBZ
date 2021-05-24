using HarmonyLib;
using QModManager.API.ModLoading;
using UnityEngine;

namespace SubnauticaTimeBZ
{
    class Time
    {
        [HarmonyPatch(typeof(uGUI_PowerIndicator))]
        [HarmonyPatch("UpdatePower")]
        internal class uGUI_PowerIndicator_UpdatePower_Patch
        {
            [QModPostPatch]
            public static void Postfix(uGUI_PowerIndicator __instance, bool ___initialized, int ___cachedPower, int ___cachedMaxPower)
            {
                // only when initialized and in game
                if (___initialized && uGUI.isMainLevel && !uGUI.isIntro)
                {
                    bool powerIndicatorEnabled = __instance.text.enabled;
                    string newText;

                    float dayScalar = DayNightCycle.main.GetDayScalar();
                    float num = dayScalar * 24f;
                    float f = num % 1f * 60f;
                    string timeText = Language.main.GetFormat<int, int>("ControlRoomTime", Mathf.FloorToInt(num), Mathf.FloorToInt(f));

                    if (!powerIndicatorEnabled)
                    {
                        // replace power text
                        __instance.text.enabled = true;
                        __instance.text.color = Color.white;

                        newText = timeText;
                    }
                    else
                    {
                        // append to power text - need to re-generate string because text is not set when cached value was used.
                        string powerText = Language.main.GetFormat<int, int>("HUDPowerStatus", ___cachedPower, ___cachedMaxPower);
                        newText = powerText + " - " + timeText;
                    }
                    __instance.text.text = newText;
                }
            }
        }
    }
}
