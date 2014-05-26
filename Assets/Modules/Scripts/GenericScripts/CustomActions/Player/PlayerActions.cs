using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;

namespace HutongGames.Playmaker.Actions
{
    
    [ActionCategory("BreachActions")]
    [Tooltip("Figures whether the player is facing left or right and running left or right, then sets a variable to either -1 or 1")]
    
    public class SetPlayerAnimationSpeed :  FsmStateAction{

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

}