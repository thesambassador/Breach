using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class InventoryIconAC 
{

	// Use this for initialization
	void Start () {

       /* InitLiveAnimation();
        float s = this.gameObject.transform.localScale.x;
        UILabel label = this.gameObject.GetComponent<UILabel>();
      
        List<string> filteredlist = new List<string>();     
        string[] list = label.text.Split(';');
        filteredlist.AddRange(list);
        filteredlist.RemoveAll(str => String.IsNullOrEmpty(str));

        string itemtype = filteredlist[0];
        */
     

        //Our UILabel that we get the string from has ALL of the possible bindable fields from ANY baseitem. These fields will be blank if they aren't used. Example: the Weapon object
        //has the blade,guard, etc fields, which will contain strings for those texture names, and an ItemType, but its "armor" fields will be blank, as it is a weapon object and only set
        //the weapon related fields.
        //We then parse this string and remove all the unused empty strings, and we maintain our element order and use it below.
        //(ALL baseitems have an itemtype set in their constructors, and it is ALWAYS the first
        //element of the array), 
     /*  if(itemtype=="Weapon")
        {
          //  _liveAnimation.SwapTexture("Blades", "l_blade1", "Blades", filteredlist[1]);
          //  _liveAnimation.SwapTexture("Guards", "l_guard1", "Guards", filteredlist[2]);
         //      _liveAnimation.Play("Weapon");

        }
        else if(itemtype=="Chest")
        {
          //  _liveAnimation.SwapTexture("Guards", "l_guard1", "Guards", filteredlist[1]);
         //   _liveAnimation.Play("Chest");
        }
        */
 
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
