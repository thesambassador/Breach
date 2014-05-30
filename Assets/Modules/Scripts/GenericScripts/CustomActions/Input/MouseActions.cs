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

    [ActionCategory("CustomInput_Mouse")]
    public class PlaceComposition : FsmStateAction
    {

        // Reference to the composition definition manager asset.
      //  [RequiredField]
      // public GameObject compositionManager;

        [RequiredField]
        public TileCompositionManager compositionManager;

        [RequiredField]
        public FsmInt CurrentTileX;
        [RequiredField]
        public FsmInt CurrentTileY;
 
        public override void OnEnter()
        {
             TileSystem tileSystem = FsmVariables.GlobalVariables.GetFsmGameObject("g_tile_TileSystem").Value.GetComponentAs<TileSystem>();
             var composition = compositionManager.Compositions[0];                        
             TileIndex newindex = new TileIndex(CurrentTileY.Value, CurrentTileX.Value);
             tileSystem.PaintComposition(newindex, composition);
            
            //now that we have placed the comp. we need to do calculations to see what tiles to replace. WE pass the type of composition we placed so we know what tiles to remove,
            //since we have to predefine which tiles we want to remove on a room.I.e the bottom left 2 of a room so we can place a door.
             tileSystem.ConnectRooms(newindex,"Room");


             Finish();
        }
 

    }



    [ActionCategory("CustomInput_Mouse")]
    public class MouseMoved : FsmStateAction
    {          
        [RequiredField]         
        public FsmEvent MouseMovedEvent;

        public override void OnUpdate()
        {
            if (Input.GetAxis("Mouse X") < 0)
            {
                Fsm.Event(MouseMovedEvent);
            }
            if (Input.GetAxis("Mouse X") > 0)
            {
                Fsm.Event(MouseMovedEvent);
            }
            if (Input.GetAxis("Mouse Y") < 0)
            {
                Fsm.Event(MouseMovedEvent);
            }
            if (Input.GetAxis("Mouse Y") > 0)
            {
                Fsm.Event(MouseMovedEvent);
            }
             
        }  

    }


    [ActionCategory("CustomInput_Mouse")]
    public class CheckForMouseTilePositionChange : FsmStateAction
    {
        [RequiredField]
        public FsmEvent TilePositionChanged;
        [RequiredField]
        public FsmEvent NoChange;

        [RequiredField]
        public FsmInt CurrentTileX;
        [RequiredField]
        public FsmInt CurrentTileY;

        public override void OnEnter()
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);             
            Vector3 vec = Camera.main.ScreenToWorldPoint(mousePosition);

            int x = Math.Abs((int)vec.x / 1);
            int y = Math.Abs((int)vec.y / 1);

            if (CurrentTileX.Value != x || CurrentTileY.Value != y)
            {
               
                    CurrentTileX.Value = x;
                    CurrentTileY.Value = y;

                   
                    Fsm.Event(TilePositionChanged);
                
            }
            else
            {
                Fsm.Event(NoChange);
            }
            Finish();
        }

    }



    [ActionCategory("CustomInput_Mouse")]
    public class CalculateIfTileGroupIsPlacable : FsmStateAction
    {
        [RequiredField]
        public FsmEvent Placable;
        [RequiredField]
        public FsmEvent NotPlacable;

        [RequiredField]
        public FsmInt CurrentTileX;
        [RequiredField]
        public FsmInt CurrentTileY;

        public override void OnEnter()
        {
            TileSystem tileSystem = FsmVariables.GlobalVariables.GetFsmGameObject("g_tile_TileSystem").Value.GetComponentAs<TileSystem>();
            
            TileIndex newindex = new TileIndex(CurrentTileY.Value, CurrentTileX.Value);

            if(tileSystem.IsGroupPlacable(newindex))
            {
                Fsm.Event(Placable);
            }
            else
            {
                Fsm.Event(NotPlacable);
            }

            Finish();
        }

    }
    [ActionCategory("CustomInput_Mouse")]
    public class DrawTileGroupOnUserMouse : FsmStateAction
    {
     
        public override void OnEnter()
        {
            Texture2D tex = (Texture2D)Resources.Load("Art/Character/images", typeof(Texture2D));

          //  FsmVariables.GlobalVariables.GetFsmGameObject("g_tile_DisplayedCompositionOnMouse").Value.GetComponentAs<UnityEngine.Sprite>();

            
             //need to do this so we dont spawn a shitload of objects while moving our mouse
            var tmp = TileCompositionUtility.TileCompGraphic.GetComponent("SpriteRenderer");
          if( tmp == null)
              TileCompositionUtility.TileCompGraphic.AddComponent("SpriteRenderer"); 
             

            
            Rect rec = new Rect(0, 0, 225, 225);
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            UnityEngine.Sprite.Destroy(FsmVariables.GlobalVariables.GetFsmObject("g_tile_DisplayedCompositionOnMouse").Value);
            FsmVariables.GlobalVariables.GetFsmObject("g_tile_DisplayedCompositionOnMouse").Value =(UnityEngine.Object) UnityEngine.Sprite.Create(tex, rec, pivot, 100);

            SpriteRenderer myRenderer = TileCompositionUtility.TileCompGraphic.GetComponent<SpriteRenderer>();
            myRenderer.sprite = (UnityEngine.Sprite) FsmVariables.GlobalVariables.GetFsmObject("g_tile_DisplayedCompositionOnMouse").Value;
  
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);             
            Vector3 vec = Camera.main.ScreenToWorldPoint(mousePosition);
          
            myRenderer.transform.position = new Vector2(vec.x , vec.y);
            


            Finish();
             
        }
     

    }
    [ActionCategory("CustomInput_Mouse")]
    public class EraseSpriteTileGroupOnUserMouse : FsmStateAction
    {

        public override void OnEnter()
        {
            //we delete the old display tiles on the mouse            
            UnityEngine.Sprite.Destroy(FsmVariables.GlobalVariables.GetFsmObject("g_tile_DisplayedCompositionOnMouse").Value);
            Finish();

        }


    }

}
