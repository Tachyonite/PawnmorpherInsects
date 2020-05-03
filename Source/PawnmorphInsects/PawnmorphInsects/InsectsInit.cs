// InsectsInit.cs created by Iron Wolf for PawnmorphInsects on 05/03/2020 7:35 AM
// last updated 05/03/2020  7:35 AM

using System;
using HarmonyLib;
using Verse;

namespace PawnmorphInsects
{
    [StaticConstructorOnStartup]
    public static class InsectsInit
    {

        public const string HARMONY_ID = "tachyonite.pawnmorph.insects";

        static InsectsInit()
        {
            var har = new Harmony(HARMONY_ID);
            try
            {
                har.PatchAll(); 
            }
            catch (Exception e)
            {
                Log.Error($"caught {e.GetType()} while patching files for Pawnmorph Insects!");
            }

        }
    }
}