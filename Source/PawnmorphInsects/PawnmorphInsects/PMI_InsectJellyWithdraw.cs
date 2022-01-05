﻿// PMI_InsectJellyWithdraw.cs created by Iron Wolf for PawnmorphInsects on 09/08/2020 12:29 PM
// last updated 09/08/2020  12:29 PM

using System.Linq;
using Pawnmorph;
using Pawnmorph.TfSys;
using RimWorld;
using Verse;
using Verse.Sound;

namespace PawnmorphInsects
{
    public class PMI_InsectJellyWithdraw : Hediff_Addiction
    {
        bool firstTime = true;

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Values.Look<bool>(ref this.firstTime, "firstTime");
        }

        public override void Tick()
        {
            base.Tick();
            Need_Chemical need = this.Need;
            if (firstTime && need != null && need.CurCategory == DrugDesireCategory.Withdrawal)
            {

                Name colonistName = this.pawn.Name;
                var tfRequest = new TransformationRequest(PawnKindDefOf.Megaspider, pawn);

                var tfPawn = MutagenDefOf.defaultMutagen.MutagenCached.Transform(tfRequest);
                if (tfPawn == null)
                {
                    Log.Error($"unable to transform pawn {colonistName}");
                    firstTime = false;
                    return;

                }

                Find.World.GetComponent<PawnmorphGameComp>().AddTransformedPawn(tfPawn);
                var megaspider = tfPawn.TransformedPawns.First();


                string text = "TransformationLetter".Translate(colonistName).CapitalizeFirst();
                Find.LetterStack.ReceiveLetter("TransformationLLabel".Translate(colonistName).CapitalizeFirst(), text, LetterDefOf.NegativeEvent, megaspider, null, null);
            }
            this.firstTime = false;
        }
    }
}