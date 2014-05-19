using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Assets.Modules.Utility;

public class ScrollScript : MonoBehaviour {

    public GameObject CharacterToFollow;
	public float speed = .2f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
      //  if (CharacterToFollow != null)
       // {

        renderer.material.mainTextureOffset = new Vector2(FsmVariables.GlobalVariables.GetFsmFloat(GlobalNames.DistanceTraveled).Value * speed/1000, 0f);
      //  }
	}
}
