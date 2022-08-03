using JetBrains.Annotations;
using RimWorld;
using Pawnmorph;

#pragma warning disable 1591

namespace PawnmorphInsects
{
    /// <summary> DefOf class for commonly referenced Aspects. </summary>
    [DefOf]
    public static class PMI_AspectDefOf
    {
        /// <summary>
        /// Aspect for the insect mind
        /// </summary>
        [UsedImplicitly(ImplicitUseKindFlags.Assign), NotNull]
        public static AspectDef PMI_HiveMinded;

        // ReSharper disable once NotNullMemberIsNotInitialized
        static PMI_AspectDefOf()
        {
            DefOfHelper.EnsureInitializedInCtor(typeof(PMI_AspectDefOf));
        }
    }
}
