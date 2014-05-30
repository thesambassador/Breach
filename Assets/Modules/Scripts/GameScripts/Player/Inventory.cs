using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
    public List<BaseItem> items;
    public EquippableItem activeItem;

    public GameObject parentObject;

    //initialize list, but since we're empty there isn't any active item
    void Start()
    {
        parentObject = this.gameObject;
        items = new List<BaseItem>();
        GenGun("gun1", Color.blue, true, .15f);
        GenGun("gun2", Color.green, true, .05f);
        GenGun("gun3", Color.red, false, .1f);
    }

    //temporary function to test stuff
    void GenGun(string atlasName, Color color, bool auto, float cooldown)
    {
        Weapon weapon = new Weapon();

        weapon.bulletSpeed = 20;
        weapon.bulletLife = 1;
        weapon.automatic = auto;
        weapon.cooldown = cooldown;
        weapon.atlasName = atlasName;
        weapon.usable = true;
        weapon.projectile = Resources.Load("DynamicPrefabs/basicBullet");
        weapon.bulletColor = color;

        AddItem(weapon);
    }

    void Update()
    {
        foreach (BaseItem item in items)
        {
            if (item is EquippableItem)
            {
                (item as EquippableItem).Update();
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (activeItem != null)
            {
                activeItem.OnUse();
            }
        }

        float scrollAxis = Input.GetAxisRaw("Mouse ScrollWheel");

        if (scrollAxis > 0)
        {
            CycleToNextUsableItem();
        }
        else if (scrollAxis < 0)
        {
            CycleToNextUsableItem(-1);
        }
        
    }

    public void AddItem(BaseItem item){
        items.Add(item);

        //If we don't have anything equipped, and the added item is equippable and usable, equip it
        //Todo: add slot support, equip to empty suitable slots here
        if (activeItem == null)
        {
            if (item is EquippableItem)
            {
                EquippableItem equippable = item as EquippableItem;
                if (equippable.usable)
                {
                    EquipItem(equippable);
                }
            }
        }
    }

    //You should only equip items that are in your inventory... so they should be added first?  might be confusing later
    public void EquipItem(EquippableItem item)
    {
        if (activeItem != null)
        {
            UnequipItem(activeItem);
        }
        item.OnEquip(parentObject);
        activeItem = item;
    }

    public void UnequipItem(EquippableItem item)
    {
        item.OnUnequip(parentObject);
        activeItem = null;
        //todo: when we have slots, this won't necessarily set activeItem to null
    }

    //switch to the next equippable item in your inventory, dir = 1 for forward in list, -1 for backwards
    public void CycleToNextUsableItem(int dir = 1)
    {
        if (items.Count <= 1) return;

        int startingIndex = items.IndexOf(activeItem);

        for (int i = startingIndex + dir; i != startingIndex; i+=dir)
        {
            //loop back to start of list if we're at the end
            if (i >= items.Count) i = 0;
            else if (i < 0) i = items.Count - 1;

            //ignore items that aren't equippable or usable
            if (items[i] is EquippableItem)
            {
                EquippableItem item = items[i] as EquippableItem;
                if (item.usable && activeItem != item)
                {
                    EquipItem(item);
                    break;
                }
            }
        }
    }




	
}
