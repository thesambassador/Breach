using UnityEngine;
using System.Collections;

public class EquippableItem : BaseItem {
    SlotType slotType;
    public bool usable = false;

    //called when you equip this item
    public virtual void OnEquip(GameObject parent)
    {

    }

    //called when you unequip this item
    public virtual void OnUnequip(GameObject parent)
    {

    }

    //called when you have this item as your active item and you click
    public virtual void OnUse()
    {

    }


    //called each frame while this item is in your inventory
    public virtual void Update()
    {

    }

	
}

public enum SlotType
{
    Weapon,
    Armor,
    Accessory
}
