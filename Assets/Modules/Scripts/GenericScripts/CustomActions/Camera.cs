using Assets.Modules.Utility;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
  
    [ActionCategory("Camera")] 
    public class SetCameraToFollowPlayer : FsmStateAction
    { 

//        QuestVM vm;
        public override void OnEnter()
        {
            /* CameraController2D cam =  FsmVariables.GlobalVariables.GetFsmGameObject(GlobalNames.Camera ).Value.GetComponent<CameraController2D>();
             GameObject Target = FsmVariables.GlobalVariables.GetFsmGameObject(GlobalNames.Character ).Value;

           //  cam.initialTarget = Target.transform;

             if (Target != null)
             {
                 cam.AddTarget(Target.transform);
             }

            // cam.heightFromTarget = 5; 
               Finish(); 
           }  */
        }
    } 

    [ActionCategory("Camera")]
    public class AlignMenuOptionWithPlayer : FsmStateAction
    { 
//        QuestVM vm;
        public override void OnEnter()
        {
       /*     Camera cam = FsmVariables.GlobalVariables.GetFsmGameObject(GlobalNames.Camera).Value.GetComponent<Camera>();
            GameObject Target = FsmVariables.GlobalVariables.GetFsmGameObject(GlobalNames.Character).Value;
             
            Vector3 vec= cam.camera.WorldToScreenPoint(Target.transform.position);
             
            FsmVariables.GlobalVariables.GetFsmGameObject(GlobalNames.QuestChoicesGUI).Value.transform.localPosition = vec;
             
            Finish(); */
        }
         
    }


}