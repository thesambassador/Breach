using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules.Inventory
{

    public class ChestArmor : Armor
    {

        public ChestArmor()
        {
            ItemType = "Chest";
        }
        private readonly EZData.Property<string> _privateProperty1Property = new EZData.Property<string>();
        public EZData.Property<string> Property1Property { get { return _privateProperty1Property; } }
        public string Chest
        {
            get { return Property1Property.GetValue(); }
            set { Property1Property.SetValue(value); }
        }


    }

}
