using UnityEngine;
using System.Collections;

public class EquippableItem : EZData.Context {
    //What type of slot this goes into
    SlotType slotType;

    //name of the item
    private EZData.StringProperty _nameProp = new EZData.StringProperty();
    public EZData.StringProperty NameProperty { get { return _nameProp; } }
    public string Name
    {
        get { return NameProperty.GetValue(); }
        set { NameProperty.SetValue(value); }
    }

    //Name of the icon in the icon atlas
    private EZData.StringProperty _iconProp = new EZData.StringProperty();
    public EZData.StringProperty IconProperty { get { return _iconProp; } }
    public string Icon
    {
        get { return IconProperty.GetValue(); }
        set { IconProperty.SetValue(value); }
    }

    //Description of the item
    private EZData.StringProperty _descriptionProp = new EZData.StringProperty();
    public EZData.StringProperty DescriptionProperty { get { return _descriptionProp; } }
    public string Description
    {
        get { return DescriptionProperty.GetValue(); }
        set { DescriptionProperty.SetValue(value); }
    }

    //Cost of the item (may not be used)
    private EZData.IntProperty _costProp = new EZData.IntProperty();
    public EZData.IntProperty CostProperty { get { return _costProp; } }
    public int Cost
    {
        get { return CostProperty.GetValue(); }
        set { CostProperty.SetValue(value); }
    }

    //Action when you click the item in the armory gui
    public event System.Action<EquippableItem> OnClick;
    public void Click()
    {
        if (OnClick != null)
        {
            OnClick(this);
        }
    }

    //Option that specifies whether this is an item that can be selected and used or not.
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
