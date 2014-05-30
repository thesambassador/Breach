using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace HutongGames.Playmaker.Actions
{
    
    [ActionCategory("BreachActions")]
    [Tooltip("Figures whether the player is facing left or right and running left or right, then sets a variable to either -1 or 1")]
    public class ActionSetPlayerAnimationSpeed :  FsmStateAction{

        [UIHint(UIHint.Variable)]
        [Tooltip("Store running direction here, 1 for moving forward, -1 for moving backwards")]
        public FsmFloat runningDir;

	    // Use this for initialization
	    public void Start () {
	        
	    }
	
	    // Update is called once per frame
        public override void OnUpdate()
        {
            Vector2 mousePos = Input.mousePosition;
            Vector2 playerScreenPos = Camera.main.WorldToScreenPoint(this.Owner.transform.position);

            float mouseX = mousePos.x;
            float playerX = playerScreenPos.x;

            bool keyRight = Input.GetKey(KeyCode.D);
            bool keyLeft = Input.GetKey(KeyCode.A);

            bool movingTowardsMouse = (keyRight && mouseX > playerX) || (keyLeft && mouseX < playerX);

            runningDir.Value = -1;

            if (movingTowardsMouse)
                runningDir.Value = 1;

            //Debug.Log("Test");

	    }
    }

    [ActionCategory("BreachActions")]
    [Tooltip("Makes the player move to the left or right depending on input, can change speed")]
    public class ActionMoveLeftRight : FsmStateAction
    {
        [UIHint(UIHint.Variable)]
        [Tooltip("Target run speed")]
        public FsmFloat runSpeed;

        [UIHint(UIHint.Variable)]
        [Tooltip("Max Force to acheive the target runspeed")]
        public FsmFloat maxForce;

        [UIHint(UIHint.Variable)]
        [Tooltip("How fast we get to the target velocity")]
        public FsmFloat proportionalConstant;

        [UIHint(UIHint.Variable)]
        [Tooltip("Initial speed that you jump off the ground at")]
        public FsmFloat jumpVelocity;

        [UIHint(UIHint.Variable)]
        [Tooltip("How much to dampen the Y velocity when you release the key")]
        public FsmFloat jumpDamping;

        [UIHint(UIHint.Variable)]
        [Tooltip("How much force to apply while you hold the jump key")]
        public FsmFloat holdForce;

        [UIHint(UIHint.Variable)]
        [Tooltip("How long you can hold the button before we stop applying force")]
        public FsmFloat holdTime;

        [UIHint(UIHint.Variable)]
        [Tooltip("The current amount of time left")]
        public FsmFloat currentHoldTime;

        public PlayMakerFSM animationFSM;
        

        public override void Awake()
        {
            Transform animationObject = Owner.transform.FindChild("PlayerAnimation");
            animationFSM = animationObject.GetComponent<PlayMakerFSM>();

            Fsm.HandleFixedUpdate = true;
        }

        public override void OnEnter()
        {
            if (Fsm.ActiveStateName == "Ground")
            {
                animationFSM.SendEvent("Land");
            }
            else if (Fsm.ActiveStateName == "Air")
            {
                animationFSM.SendEvent("Air");
            }
        }

        public override void OnUpdate()
        {
            if (currentHoldTime.Value > 0)
            {
                currentHoldTime.Value -= Time.deltaTime;
            }

           
            if (Fsm.ActiveStateName == "Ground")
            {
                 //handle jump movement
                if(Input.GetButtonDown("Jump")){
                    Owner.rigidbody.AddForce(new Vector2(0, jumpVelocity.Value), ForceMode.VelocityChange);
                    currentHoldTime.Value = holdTime.Value;
                    Fsm.Event("leftGround");
                    animationFSM.SendEvent("Jump");
                    //Owner.GetComponent<Player>().onGround = false;
                }

                float velX = Mathf.Abs(Owner.rigidbody.velocity.x);
                Debug.Log(velX);

                if (velX > 0.1f)
                {
                    animationFSM.SendEvent("Running");
                }
                else
                {
                    animationFSM.SendEvent("StopRunning");
                }
            }

            
        }

        public override void OnFixedUpdate()
        {
            //handle left and right movement
            float dir = Input.GetAxisRaw("Horizontal");
            Vector2 targetVelocity = new Vector2(dir * runSpeed.Value, 0);
            Vector2 force = MovementUtility.ProportionalForce(targetVelocity, Owner.rigidbody.velocity, new Vector2(-maxForce.Value,0), new Vector2(maxForce.Value, 0), proportionalConstant.Value, true, false);
            Owner.rigidbody.AddForce(force);

           
            

           

            //dampen jump velocity on key release if we're still moving up
            if (Owner.rigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
            {
                Vector2 playerVel = Owner.rigidbody.velocity;
                playerVel.y *= jumpDamping.Value;
                Owner.rigidbody.velocity = playerVel;
                currentHoldTime.Value = 0;
             
            }

            if (Fsm.ActiveStateName == "Air")
            {

                //apply extra force if we still have hold time left
                if (currentHoldTime.Value > 0 && Input.GetButton("Jump"))
                {
                    Owner.rigidbody.AddForce(new Vector2(0, holdForce.Value));
                }
            }

        }

    }


}