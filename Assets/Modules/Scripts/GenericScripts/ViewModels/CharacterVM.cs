using Assets.Modules.Inventory;
using Assets.Modules.Managers;
using Assets.Modules.Utility;
using HutongGames.PlayMaker;
using SmoothMoves;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Modules.Models
{
     
    public class CharacterVM : MyViewModelBase
    {
        public InventoryContext _InventoryContext;
        public NguiRootContext InventoryView;

        public PlayerStatsContext _StatsContext;
        public NguiRootContext StatsView;

//        WeaponGenerator wg = new WeaponGenerator(3, 3, 3, 3, 3, 3, 1, 1);

        public override void GenerateContext()
        {
            _InventoryContext = new InventoryContext();


            /*
            _InventoryContext.AddItem(new Armor()
            {
                Name = "Boots of Haste",
                Slot = Slot.Boots,
                Icon = wg.GenerateWeapon("Sword"),
                Chest = "WOZOZEZK"
            });*/
            

            AddItem(new Weapon()
            {
                Name = "Boots of Haste",
                Slot = Slot.Boots,
                //Icon = wg.GenerateWeapon("Sword"),
                Blade="l_blade1",
                Guard="l_guard1" 
            });


            AddItem(new ChestArmor()
            {
                Name = "Boots of Haste",
                Slot = Slot.Boots,
                //Icon = wg.GenerateWeapon("Sword"),
                Chest="m_guard1"
            });


            /*

            _InventoryContext.AddItem(new Armor()
            {
                Name = "Boots of Haste",
                Slot = Slot.Boots,
                Icon = "GreenButton",
            });
            _InventoryContext.AddItem(new Armor()
            {
                Name = "Boots of Haste",
                Slot = Slot.Boots,
                Icon = "GreenButton",
            });
            _InventoryContext.AddItem(new Armor()
            {
                Name = "Boots of Haste",
                Slot = Slot.Boots,
                Icon = "GreenButton",

            });*/

            _InventoryContext.Gold = 400;

            InventoryView.SetContext(_InventoryContext);


            _StatsContext = new PlayerStatsContext();
            // All these values are for testing
             
            _StatsContext.pMaxHealth = 120;
            _StatsContext.pExperienceToLevel = 100;
            _StatsContext.pTitle = "Super Berserk";
            _StatsContext.pName = "Bryan";
            _StatsContext.pExperience = 33;
            _StatsContext.pMana = .77f;
            _StatsContext.pHealth = 85;
            _StatsContext.pStrength = 10;
            _StatsContext.pStatPoints = 15;
            StatsView.SetContext(_StatsContext);

        }


        /*
        #region Shoulders
        public readonly EZData.VariableContext<Armor> ShouldersEzVariableContext =
            new EZData.VariableContext<Armor>(null);
        public Armor Shoulders
        {
            get { return ShouldersEzVariableContext.Value; }
            set { ShouldersEzVariableContext.Value = value; }
        }
        #endregion

        #region Bracers
        public readonly EZData.VariableContext<Armor> BracersEzVariableContext =
            new EZData.VariableContext<Armor>(null);
        public Armor Bracers
        {
            get { return BracersEzVariableContext.Value; }
            set { BracersEzVariableContext.Value = value; }
        }
        #endregion

        #region Boots
        public readonly EZData.VariableContext<Armor> BootsEzVariableContext =
            new EZData.VariableContext<Armor>(null);
        public Armor Boots
        {
            get { return BootsEzVariableContext.Value; }
            set { BootsEzVariableContext.Value = value; }
        }
        #endregion
        */

 
        

        private EZData.VariableContext<Armor> GetSlot(EquipableItem item)
        {
          /*  switch (item.Slot)
            {
                case Slot.Shoulders: return ShouldersEzVariableContext;
                case Slot.Bracers: return BracersEzVariableContext;
                case Slot.Boots: return BootsEzVariableContext;
            }*/
            return null;
        }
         

        public void Toggle(BaseItem item)
        {

            
            if (item is Weapon)
            {
                Weapon tmp = (Weapon)item;
                //we want to equip the item we just clicked on, so the first step is to use the weapon properties of Weapon for the texture names.

                 
              //PlayerAC test =  FsmVariables.GlobalVariables.GetFsmGameObject(GlobalNames.Character).Value.GetComponent<PlayerAC>(); //   FsmVariables.GlobalVariables.GetFsmGameObject(GlobalNames.Character).Value.GetComponent<BoneAnimation>().SwapTexture("Blades", "l_blade1", "Blades",tmp.Blade);
            //  test.SwitchBlade(tmp.Blade);
               // FsmVariables.GlobalVariables.GetFsmGameObject(GlobalNames.Character).Value.GetComponent<BoneAnimation>().SwapTexture("Guards", "l_guard1", "Guards", tmp.Guard);
              //  FsmVariables.GlobalVariables.GetFsmGameObject(GlobalNames.Character).Value.GetComponent<BoneAnimation>().SwapTexture("Pommels", "pommel1", "Pommels", pommel);
               // FsmVariables.GlobalVariables.GetFsmGameObject(GlobalNames.Character).Value.GetComponent<BoneAnimation>().SwapTexture("Hilts", "hilt1", "Hilts", hilt);
             

            }
          
           
           
           


          //  if (Backpack.SelectedItem != null)
           //     ((BaseItem)Backpack.SelectedItem).IsSelected = false;

            //item was clicked, we need to deselected the other selected item

            /*
           var slot = GetSlot(item); // find a slot corresponding to the item
           if (slot.Value == item) // if item is equipped
           {
               slot.Value = null; // remove it from slot
               Backpack.Add(item); // and put to backpack
           }
           else // otherwise if item is not equipped
           {
               if (slot.Value != null)
                   Backpack.Add(slot.Value); // clear the slot if it was used
			
               Backpack.Remove(item); // take item from backpack
               slot.Value = item; // and put to the slot
           }*/


        }


        public void AddItem(BaseItem item)
        {
            item.OnToggle += Toggle;

            _InventoryContext.AddItem(item);
        }

        public void UnequipItem(EquipableItem item)
        {
            _InventoryContext.RemoveItem(item);

          /*  var slot = GetSlot(item);
            if (slot.Value == item)
                slot.Value = null;*/

            item.OnToggle -= Toggle;
        }
          
        public int GetGold()
        {
            return _InventoryContext.Gold;
        }

    }



}
