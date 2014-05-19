using UnityEngine;
using System.Collections;

namespace HutongGames.PlayMaker.Actions
{
 


    [ActionCategory("Managers")]
    public class GenerateEvent : FsmStateAction
    {
        [RequiredField]
        public FsmEvent BattleEvent;

        [RequiredField]
        public FsmEvent TownEvent;

        [RequiredField]
        public FsmEvent QuestEvent;

        public override void OnEnter()
        {

            //we determine our probabilities here for generating events, as well as any other inputs that could affect what events get generated(gear/potions,etc)
            System.Random rand = new System.Random();
            int count = rand.Next(0, 10);
            if (count <= 3)
            {
                Fsm.Event(BattleEvent);
            }
            if (count >= 4 && count <= 7)
            {
                Fsm.Event(TownEvent);
            }
            if (count >= 8)
            {
                Fsm.Event(QuestEvent);
            }


        }
    }


	[ActionCategory("Managers")]
	[Tooltip("Custom actions for writing quests")]
	public class IfAllBoolAreTrue : FsmStateAction
	{
		[RequiredField]
		public FsmEvent IfTrue;

		[RequiredField]
		public FsmEvent IfFalse;

		[RequiredField]
		public FsmBool[] BoolsToCheckForTrue;

		public override void Reset()
		{
			BoolsToCheckForTrue = null;
		}

		public override void OnEnter()
		{ 
			foreach(FsmBool b in BoolsToCheckForTrue)
			{
				if(b.Value == false)
				{
					Fsm.Event(IfFalse);
				}
			}
			//all were true, so go to the true event
			Fsm.Event (IfTrue);

		 
		}
	}


    [ActionCategory("Managers")]
    [Tooltip("Custom actions for writing quests")]
    public class IfAllBoolAreFalse : FsmStateAction
    {
        [RequiredField]
        public FsmEvent IfTrue;

        [RequiredField]
        public FsmEvent IfFalse;

        [RequiredField]
        public FsmBool[] BoolsToCheckForTrue;

        public override void Reset()
        {
            BoolsToCheckForTrue = null;
        }

        public override void OnEnter()
        {
            foreach (FsmBool b in BoolsToCheckForTrue)
            {
                if (b.Value == true)
                {
                    Fsm.Event(IfFalse);
                }
            }
            //all were true, so go to the true event
            Fsm.Event(IfTrue);


        }
    }

}


	
 