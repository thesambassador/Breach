using UnityEngine;
using System.Collections;

public class ArmoryContext : EZData.Context{
    //ALL items that could potentially show up in the items tab
    private readonly EZData.Collection<EquippableItem> _availableItems = new EZData.Collection<EquippableItem>(false);
    public EZData.Collection<EquippableItem> AvailableItems { get { return _availableItems; } }

    //Item that is currently selected in the items list or maybe an equipped item, shows description in the info panel
    public readonly EZData.VariableContext<EquippableItem> SelectedItemProperty = new EZData.VariableContext<EquippableItem>(null);
    public EquippableItem SelectedItem
    {
        get { return SelectedItemProperty.Value; }
        set { SelectedItemProperty.Value = value; }
    }

    public readonly EZData.StringProperty SelectedItemDescriptionProp = new EZData.StringProperty();
    public string SelectedItemDescription
    {
        get { return SelectedItemDescriptionProp.GetValue(); }
        set { 
            SelectedItemDescriptionProp.SetValue(value);
        }
    }

    public Transform SelectionDisplay;

    public ArmoryContext(Transform selectionDisplay)
    {
        EquippableItem item = new EquippableItem();
        item.Description = "This is some text about the item!";
        SelectItem(item);

        

        //SelectionDisplay = selectionDisplay;
    }

    //Adds an item to the available items
    public void AddItem(EquippableItem item){
        AvailableItems.Add(item);
        item.OnClick += SelectItem;
    }

    public void SelectItem(EquippableItem item)
    {
        SelectedItem = item;
        SelectedItemDescriptionProp.SetValue(item.Description);
        //SelectedItem.Description = item.Description;

        Debug.Log(SelectedItemDescription);
    }



}
