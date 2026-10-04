using System;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using UnityEngine;

namespace NOCockpitZoom
{
    [BepInPlugin(PluginGuid, PluginName, PluginVersion)]
    public class Plugin : BaseUnityPlugin
    {
        internal const string PluginGuid = "com.roque.NOCockpitZoom";
        internal const string PluginName = "NOCockpitZoom";
        internal const string PluginVersion = "0.1.0";

        // The game's own cockpit clamp (CameraCockpitState.minFOV/maxFOV). The config can only
        // narrow it, never widen it.
        internal const float GameMin = 20f;
        internal const float GameMax = 120f;

        internal static ManualLogSource? Log;
        internal static ConfigEntry<float>? MinFov;
        internal static ConfigEntry<float>? MaxFov;

        private void Awake()
        {
            Log = Logger;
            var range = new AcceptableValueRange<float>(GameMin, GameMax);
            MinFov = Config.Bind("Cockpit zoom", "Min FOV", GameMin,
                new ConfigDescription("Narrowest field of view in degrees (furthest zoom in). The game allows 20-120.", range));
            MaxFov = Config.Bind("Cockpit zoom", "Max FOV", GameMax,
                new ConfigDescription("Widest field of view in degrees (furthest zoom out). The game allows 20-120.", range));

            try { new Harmony(PluginGuid).PatchAll(typeof(Plugin).Assembly); }
            catch (Exception e) { Log.LogWarning($"[{PluginName}] Harmony patch failed to apply: {e.Message}"); }
        }
    }

    // UpdateState clamps cockpit zoom to the instance's private minFOV/maxFOV every frame, so
    // writing them just before it runs is all it takes, and config edits apply live.
    [HarmonyPatch(typeof(CameraCockpitState), nameof(CameraCockpitState.UpdateState))]
    internal static class CameraCockpitState_UpdateState_Patch
    {
        private static readonly AccessTools.FieldRef<CameraCockpitState, float> MinFov =
            AccessTools.FieldRefAccess<CameraCockpitState, float>("minFOV");
        private static readonly AccessTools.FieldRef<CameraCockpitState, float> MaxFov =
            AccessTools.FieldRefAccess<CameraCockpitState, float>("maxFOV");

        private static void Prefix(CameraCockpitState __instance)
        {
            float min = Plugin.MinFov!.Value, max = Plugin.MaxFov!.Value;
            // A min set above the max would make the game's Mathf.Clamp return the max anyway;
            // ordering them keeps the range the player clearly meant.
            MinFov(__instance) = Mathf.Min(min, max);
            MaxFov(__instance) = Mathf.Max(min, max);
        }
    }
}
