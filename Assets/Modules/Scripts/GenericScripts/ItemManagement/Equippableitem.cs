using UnityEngine;
using System.Collections;

public class EquippableItem : EZData.Context {
    SlotType slotType;
    private EZData.StringProperty _nameProp = new EZData.StringProperty();
    public string Name
    {
        get { return _nameProp.GetValue(); }
        set { _nameProp.SetValue(value); }
    }

    private EZData.StringProperty _iconProp = new EZData.StringProperty();
    public string Icon
    {
        get { return _iconProp.GetValue(); }
        set { _iconProp.SetValue(value); }
    }

    private EZData.StringProperty _descriptionProp = new EZData.StringProperty();
    public string Description
    {
        get { return _descriptionProp.GetValue(); }
        set { _descriptionProp.SetValue(value); }
    }

    private EZData.IntProperty _costProp = new EZData.IntProperty();
    public int Cost
    {
        get { return _costProp.GetValue(); }
        set { _costProp.SetValue(value); }
    }

    public event System.Action<EquippableItem> OnClick;
    public void Click()
    {
        if (OnClick != null)
        {
            OnClick(this);
        }
    }

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
    Primary,
    Secondary,
    Armor,
    Accessory
}
