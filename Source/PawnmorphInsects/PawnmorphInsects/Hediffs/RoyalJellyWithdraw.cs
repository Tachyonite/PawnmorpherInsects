// RoyalJellyWithdraw.cs created by Iron Wolf for PawnmorphInsects on 06/22/2020 3:52 PM
// last updated 06/22/2020  3:52 PM

using Pawnmorph;
using Pawnmorph.TfSys;
using RimWorld;
using Verse;

namespace PawnmorphInsects.Hediffs
{
    public class RoyalJellyWithdraw : Hediff_Addiction
    {
        private int? _lastStage; 

        public override void Tick()
        {
            if (_lastStage != CurStageIndex)
            {
                _lastStage = CurStageIndex;
                if (_lastStage == 0)
                {
                    DoTf(); 
                }
            } 
            
        }

        private void DoTf()
        {
            var mutagen = MutagenDefOf.defaultMutagen;
            if (!mutagen.CanTransform(pawn)) return; 
            var request = new TransformationRequest(PawnKindDefOf.Megaspider, pawn); 
            //TODO thoughts & other stuff 
            var tfResult = mutagen.MutagenCached.Transform(request);
            if (tfResult != null)
            {
                var gComp = Find.World.GetComponent<PawnmorphGameComp>();
                gComp.AddTransformedPawn(tfResult); 
            }
        }
    }
}