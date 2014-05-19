
using SmoothMoves;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

    //gets sm_ of the owner and saves it to the local variable inputed
    [ActionCategory("SmoothMoves")]
    public class SmoothMoves_LoadAnimationController : FsmStateAction
    {
        //[RequiredField]
       // public FsmGameObject ControllerOwner;

      
        [RequiredField]
        public FsmGameObject LiveAnimationOutput;

         

        public override void OnEnter()
        { 
          //  GameObject go = (GameObject)Object.Instantiate(SM_AnimationToAttach.Value,Owner.transform.position,Owner.transform.rotation);
           // go.transform.parent = Owner.transform; 
            GameObject  ba= Owner.GetComponentInChildren<BoneAnimation>().gameObject;
            LiveAnimationOutput.Value = ba;
             
            Finish();

        }

        public override void OnExit()
        {

        }

    }

}