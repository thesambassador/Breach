using Assets.Modules.Utility;
using SmoothMoves;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{

     public static class AIUtility
     {

         public static bool EntityWithinRangeOfPlayer(int RangeToBeWithin,GameObject A)
         {

            float Distance = Math.Abs(FsmVariables.GlobalVariables.GetFsmGameObject(GlobalNames.Character).Value.transform.position.x - A.transform.position.x);
            if (Distance <= RangeToBeWithin)
            {
               return true;
            }
            else
            {
               return false;
            }    
  
         }
          

     }

     
    
    [ActionCategory("AI")]
    public class Mob_Move : FsmStateAction
    {
       

        public float moveForce = 365f;			// Amount of force added to move the player left and right.
        public float maxSpeed = 5f;
        public bool facingRight = true;			// For determining which way the player is currently facing.

        Rigidbody2D rigidBody2D;


        public override void OnEnter()
        {

          rigidBody2D = Owner.GetComponent<Rigidbody2D>();

          //BoneAnimation ba =  Owner.GetComponentInChildren<BoneAnimation>();
         // ba.Play("Standing");
          //  anim = ba["BlockHigh"];
          //Finish();
             
        }
 
         

        public override void OnUpdate()
        {
              
            float h = Owner.transform.localScale.x;
           
            if (h * rigidBody2D.velocity.x < maxSpeed)
            {
                // ... add a force to the player.
                rigidBody2D.AddForce(Vector2.right * h * moveForce);
                Debug.Log("ADDING FORCE");
            }
            // If the player's horizontal velocity is greater than the maxSpeed...
            if (Mathf.Abs(rigidBody2D.velocity.x) > maxSpeed)
            {
                // ... set the player's velocity to the maxSpeed in the x axis. 
                rigidBody2D.velocity = new Vector2(Mathf.Sign(rigidBody2D.velocity.x) * maxSpeed, rigidBody2D.velocity.y);
                Debug.Log("SETTING VELOCITY");
            }

        }
    }

     

    [ActionCategory("AI")]
    public class Mob_MeleeAttack : FsmStateAction
    {

        public override void OnEnter()
        {
            BoneAnimation ba = Owner.GetComponentInChildren<BoneAnimation>();
            ba.CrossFade("Slash"); 
            
          //  Finish();
        } 

    }


    [ActionCategory("AI")]
    public class Mob_BlockPlayerAttack : FsmStateAction
    {
        [Tooltip("Event to send when the animation is finished playing. NOTE: Not sent with Loop or PingPong wrap modes!")]
        public FsmEvent finishEvent;


        private SmoothMoves.AnimationStateSM anim;
       
        public override void OnEnter()
        {
          BoneAnimation ba =  Owner.GetComponentInChildren<BoneAnimation>();
          ba.Play("BlockHigh");
          anim = ba["BlockHigh"];
            
        }
         
       

        public override void OnUpdate()
        {
            
            if (!anim.enabled || (anim.wrapMode == WrapMode.ClampForever && anim.time > anim.length))
            {
                Fsm.Event(finishEvent);
                Finish();
            }

         
        }

    }

    [ActionCategory("AI")]
    public class Mob_PostBlockState : FsmStateAction
    {
     
        public override void OnEnter()
        {
            BoneAnimation ba = Owner.GetComponentInChildren<BoneAnimation>();
           // ba.Play("Standing");
          
            Finish();
        }

     



    }


    [ActionCategory("AI")]
    public class Mob_LookAtPlayer : FsmStateAction
    {
         
        public override void OnEnter()
        {
           
          //  Finish();
        }

        public override void OnUpdate()
        {
           
          //  base.OnUpdate();

            float mobx = Owner.transform.position.x;
            float playerx = FsmVariables.GlobalVariables.GetFsmGameObject(GlobalNames.Character).Value.transform.position.x;
            Vector3 theScale = Owner.transform.localScale;
            if(playerx > mobx)
            {
                theScale.x = 1;
            } 
            else
            {
                theScale.x = -1;
            }
            Owner.transform.localScale = theScale;
        }


    }

    [ActionCategory("AI")]
    public class Mob_WithinDistanceToPlayer : FsmStateAction
    {

        [RequiredField]
        public FsmInt DistanceLessThan;

         
        public FsmEvent IfTrue;
         
        public FsmEvent IfFalse;



        public override void OnEnter()
        {
            
        }

        public override void OnUpdate()
        {

            bool result = AIUtility.EntityWithinRangeOfPlayer(DistanceLessThan.Value, Owner);
            if (result)
            {
                Fsm.Event(IfTrue);
            }
            else
            {
                Fsm.Event(IfFalse);
            }    

           
        }


    }

}