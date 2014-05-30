using UnityEngine;
using System.Collections;
using SmoothMoves;

public class Player : MonoBehaviour
{

    public bool onGround;
    public Transform[] footRays;

    public PlayMakerFSM playerControlFSM;
    public BoneAnimation playerBoneAnimation;

    public Vector2 aimVector;

    private float _minTime = .2f; //to avoid triggering leftGround too often
    private float _currentTime = 1;


    void Awake()
    {
        playerControlFSM = GetComponent<PlayMakerFSM>();
        playerBoneAnimation = this.transform.FindChild("PlayerAnimation").GetComponent<BoneAnimation>();
        aimVector = new Vector2();
    }
	

	void Update () {
        UpdateOnGround();

        

	}


    //just cast some rays down from our 3 points, if one of them hits the "ground" layer we set "onGround" to true;
    private void UpdateOnGround(){
        int layerMask = 1 << 12; //layer 12 is the ground tile

        _currentTime += Time.deltaTime;


        foreach (Transform rayStart in footRays)
        {
            if (Physics.Raycast(rayStart.position, Vector3.down, 0.06f, layerMask))
            {
                //if we weren't on the ground before, send the "landing" event
                if (_currentTime > _minTime && !onGround)
                {
                    playerControlFSM.SendEvent("landedGround");
                    _currentTime = 0;
                    onGround = true;
                    Debug.Log("landedGround");
                }

                return;
            }
        }
        
        //if we were on the ground before, send the "go to air state" event
        if (_currentTime > _minTime)
        {
            if (onGround)
            {
                Debug.Log("leftGround");
                _currentTime = 0;
                playerControlFSM.SendEvent("leftGround");
                onGround = false;
            }
            
        }

    }

    public void SetWeaponAtlasByName(string name)
    {
        playerBoneAnimation.SwapBoneTexture("gun", "gunAtlas", "noGun", "gunAtlas", name);
    }


}
