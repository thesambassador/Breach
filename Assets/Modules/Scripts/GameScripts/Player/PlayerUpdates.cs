using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using Assets.Modules.Utility;

public class PlayerUpdates : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		FsmVariables.GlobalVariables.GetFsmFloat(GlobalNames.DistanceTraveled).Value = FsmVariables.GlobalVariables.GetFsmGameObject(GlobalNames.Character ).Value.transform.position.x;
	
	}
}
