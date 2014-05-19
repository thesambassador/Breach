using Assets.Modules.Inventory;
using Assets.Modules.Models;
using Assets.Modules.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory("Managers")]
    [Tooltip("Custom actions for writing quests")]
    public class GenerateRewardsFromMob : FsmStateAction
    {
        [RequiredField]
        public FsmGameObject MobThatWasKilled;
         
        public override void Reset()
        {
            MobThatWasKilled = null;   
        }

        public override void OnEnter()
        {
            LimbsHPProperty prop = MobThatWasKilled.Value.GetComponent<LimbsHPProperty>();

            //This is where we read the properties of the mob to determine what kind of loot it should drop(its % to drop rare loot, etc)
            CharacterVM vm = FsmVariables.GlobalVariables.GetFsmGameObject(GlobalNames.Character ).Value.GetComponent<CharacterVM>();

            vm.AddItem(new Armor()
            {
                Name = "GOTFROMGOBLIN",
                Slot = Slot.Boots,
            //    Icon = "YellowButton",
            });
            
             
            Finish();

        }
    }

}
