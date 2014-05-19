using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
//using Assets.Modules.Models.Utility;
using Assets.Modules.Utility;

namespace HutongGames.PlayMaker.Actions
{

   
    [ActionCategory("Inventory")]
    public class AddWeaponToPlayerInventory : FsmStateAction
    {
        [RequiredField]
        public FsmInt GridSize;


        public override void OnEnter()
        {
            
            Finish();
        }
    }


 
}
