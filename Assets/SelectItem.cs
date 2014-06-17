using UnityEngine;
using System.Collections;

public class SelectItem : MonoBehaviour {

    private Transform _selectionDisplay;

    public void Awake()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("Selection");
        _selectionDisplay = obj.transform;
    }


    public void OnClick()
    {
        _selectionDisplay.position = this.transform.position;
    }
	
}
