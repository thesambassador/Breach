using UnityEngine;
using System.Collections;

public class ArmoryContext : EZData.Context{
    //ALL items that could potentially show up in the items tab
    private readonly EZData.Collection<EquippableItem> _availableItems = new EZData.Collection<EquippableItem>(false);
    public EZData.Collection<EquippableItem> AvailableItems { get { return _availableItems; } }

    //Item that is currently selected in the items list or maybe an equipped item, shows description in the info panel
    //private readonly EZData.VariableContext<EquippableItem> _selectedItemProperty = new EZData.VariableContext<EquippableItem>(null);
    public readonly EZData.VariableContext<EquippableItem> SelectedItemEzVariableContext = new EZData.VariableContext<EquippableItem>(null);
    public EquippableItem SelectedItem
    {
        get { return SelectedItemEzVariableContext.Value; }
        set { SelectedItemEzVariableContext.Value = value; }
    }



    public Transform selectionDisplay;
    public UIGrid grid;

    public ArmoryContext(Transform selectionDisplay, UIGrid grid)
    {
        EquippableItem item = new EquippableItem();
        item.Description = "initial text";
        SelectItem(item);

        this.selectionDisplay = selectionDisplay;
        this.grid = grid;

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
        
        

    }



}
