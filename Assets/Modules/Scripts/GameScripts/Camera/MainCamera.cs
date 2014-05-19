using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
 
	}
	
    void Awake()
    {
        //camera.orthographicSize = (1080 / 100f / 2.0f); // 100f is the PixelPerUnit that you have set on your sprite. Default is 100.
    }
	// Update is called once per frame
	void Update () {

      GetComponent<CameraController2D>().AddInfluence(new Vector3(9, 0, 0));
	}
}
