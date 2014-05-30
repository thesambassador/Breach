using UnityEngine;
using System.Collections;
using SmoothMoves;

public class gunLook : MonoBehaviour {

    public BoneAnimation boneAnimation;
    public AnimationStateSM animationState;
    public Player playerRef;

    private Transform _armTransform;
    private Vector3 _armRotation;
    public Vector3 _mouseOffset;
    private Vector3 _transformScreenPosition;

	// Use this for initialization
	void Start () {
        _armTransform = boneAnimation.GetBoneTransform("arms");
        //playerRef = GetComponent<Player>();

	}
	
	// Update is called once per frame
    void Update()
    {
        SetAimFrame();

        

    }

	void SetAimFrame () {

        //get the angle between the mouse and the player's gun
        //0 = pointing straight to the right, 180 = pointing straight to the left
        Vector2 mousePos = Input.mousePosition;
        Vector2 playerScreenPos = Camera.main.WorldToScreenPoint(_armTransform.position);

        _mouseOffset = (mousePos - playerScreenPos);
        playerRef.aimVector = _mouseOffset.normalized;
        

        float aimZ = Mathf.Atan2(_mouseOffset.y, _mouseOffset.x) * Mathf.Rad2Deg;
        aimZ -= 90;

        aimZ = ConvertTo180Scale(aimZ);

        //flip the character if your mouse is to the right of the character, and we have to change the angle to have aiming straight upwards still equal 0
        Vector2 newScale = new Vector2(-1, 1);

        if (mousePos.x < playerScreenPos.x)
        {
            newScale.x = 1;
            
        }
        else
        {
            newScale.x = -1;
            aimZ -= 360;
            aimZ = Mathf.Abs(aimZ);

        }

        //Debug.Log(aimZ);

        boneAnimation["aim"].frame = aimZ;

        transform.localScale = newScale;

	}

    float ConvertTo180Scale(float rawAngle)
    {
        float returned = rawAngle;

        while (returned < 0)
        {
            returned += 360;
        }

        return returned;
    }
}
