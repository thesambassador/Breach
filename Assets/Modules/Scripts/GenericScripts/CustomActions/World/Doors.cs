using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
//using Assets.Modules.Models.Utility;
using Assets.Modules.Utility;
using SmoothMoves;
using HutongGames.PlayMaker;
using UnityEngine;
using Rotorz.Tile;
using Custom;

namespace HutongGames.PlayMaker.Actions
{

    [ActionCategory("WorldObjects_Doors")]
    public class RemoveDoorCollider : FsmStateAction
    {

        public override void OnEnter()
        {
            //  GameObject go = (GameObject)Object.Instantiate(SM_AnimationToAttach.Value,Owner.transform.position,Owner.transform.rotation);
            // go.transform.parent = Owner.transform; 
            BoxCollider2D ba = Owner.GetComponentInChildren<BoxCollider2D>();
            ba.enabled = false;


            Finish();

        }


    }

}
