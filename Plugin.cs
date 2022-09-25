using BepInEx;
using HarmonyLib;

namespace AntonHat
{
    [BepInPlugin(OinkyInfo.GUID, OinkyInfo.NAME, OinkyInfo.PLUGIN_VERSION)]
    internal class Plugin : BaseUnityPlugin
    {
        private readonly Harmony _harmony = new Harmony(OinkyInfo.GUID);

        private void Awake()
        {
            _harmony.PatchAll();

            // Create sprites
            Anton.Init();

        } // end Awake

    } // end class Plugin

    /* On the main menu, patch Anton */
    [HarmonyPatch(typeof(MainMenuController), "Start")]
    class PatchMainMenuController
    {
        [HarmonyPostfix]
        private static void Postfix(MainMenuController __instance)
        {
            CharacterSO anton = LoadedAssetsHandler.GetCharcater("Anton_CH");
            CustomTextures.SetSprite(ref anton.characterSprite, Anton.sprFront);
            CustomTextures.SetSprite(ref anton.characterBackSprite, Anton.sprBack);
            CustomTextures.SetSprite(ref anton.characterOWSprite, Anton.sprOW);

        } // end Postfix

    } // end class PatchMainMenuController

} // end namespace AntonHat
