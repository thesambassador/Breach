using Assets.Modules.Inventory;
using Assets.Modules.Managers;
using Assets.Modules.Models;
using UnityEngine;
 

public enum Slot
{
	Shoulders = 0,
	Bracers = 1,
	Boots = 2,
};
  
public class InventoryContext : EZData.Context 
{
	
	#region Collection Backpack
	private readonly EZData.Collection<BaseItem> _privateBackpack =
		new EZData.Collection<BaseItem>(false);
	public EZData.Collection<BaseItem> Backpack { get { return _privateBackpack; } }
	#endregion


	#region Property Gold
	private readonly EZData.Property<int> _privateGoldProperty = new EZData.Property<int>();
	public EZData.Property<int> GoldProperty { get { return _privateGoldProperty; } }
	public int Gold
	{
		get { return GoldProperty.GetValue(); }
		set { GoldProperty.SetValue(value); }
	}
	#endregion
   

	#region Collection ArmorInBackpack

	public EZData.Collection<Armor> ArmorInBackpack
	{
		get
		{
			EZData.Collection<Armor> list = new EZData.Collection<Armor>();
			foreach (BaseItem item in Backpack.Items)
			{
				if (item is Armor)
				{
					list.Add((Armor)item);
				}
			}
			return list;
		}
	}
	#endregion

	#region Collection WeaponsInBackpack

	public EZData.Collection<Weapon> WeaponsInBackpack
	{
		get
		{
			EZData.Collection<Weapon> list = new EZData.Collection<Weapon>();
			foreach (BaseItem item in Backpack.Items)
			{
				if (item is Weapon)
				{
					list.Add((Weapon)item);
				}
			}
			return list;
		}
	}
	#endregion

	public void AddItem(BaseItem item)
	{
		Backpack.Add(item);
	} 
	public void RemoveItem(BaseItem item)
	{
		Backpack.Remove(item);
	}


	/*
	public override void GenerateContext()
	{
	 

		Context.Add(new Armor()
		{
			Name = "Boots of Haste",
			Slot = Slot.Boots,
			Icon = "GreenButton",
		});
		Context.Add(new Armor()
		{
			Name = "Boots of Haste",
			Slot = Slot.Boots,
			Icon = "GreenButton",
		});
		Context.Add(new Armor()
		{
			Name = "Boots of Haste",
			Slot = Slot.Boots,
			Icon = "GreenButton",
		});
		Context.Add(new Weapon()
		{
			Name = "Boots of Haste",
			Slot = Slot.Boots,
			Icon = "RedButton",
		});
		Context.Add(new Weapon()
		{
			Name = "Boots of Haste",
			Slot = Slot.Boots,
			Icon = "RedButton",
		});
		Views.SetContext(Context);
	}*/



}
