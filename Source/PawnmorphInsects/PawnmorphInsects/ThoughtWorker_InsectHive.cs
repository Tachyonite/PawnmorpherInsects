using RimWorld;
using Verse;
using Pawnmorph;

namespace PawnmorphInsects
{
	/// <summary>
	/// Worker for relational thoughts inside an insectmorph hive.
	/// </summary>
    class ThoughtWorker_InsectHive : ThoughtWorker
    {
		/// <summary>
		/// Tells if two pawns have an insect mind aspect.
		/// </summary>
		/// <param name="p">The pawn</param>
		/// <param name="other">The other pawn</param>
		/// <returns>True if the two pawns have the aspect, false otherwise.</returns>
		protected override ThoughtState CurrentSocialStateInternal(Pawn p, Pawn other)
		{
			var aspectTrackerPawn = p.GetAspectTracker();
			var aspectTrackerOther = other.GetAspectTracker();

			if (!p.RaceProps.Humanlike || !other.RaceProps.Humanlike)
				return false;

			//No aspects.
			if (aspectTrackerPawn == null || aspectTrackerOther == null)
				return false;

			if (!RelationsUtility.PawnsKnowEachOther(p, other))
				return false;

			if (aspectTrackerPawn.Contains(PMI_AspectDefOf.PMI_HiveMinded) && aspectTrackerOther.Contains(PMI_AspectDefOf.PMI_HiveMinded))
				return true;

			return false;
		}
	}
}
