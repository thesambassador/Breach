using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Modules.Inventory
{
    public class Weapon : EquipableItem
    {

        public Weapon()
        {
            ItemType = "Weapon";
        }

        private readonly EZData.Property<string> _privateProperty1Property = new EZData.Property<string>();
        public EZData.Property<string> Property1Property { get { return _privateProperty1Property; } }
        public string Blade
        {
            get { return Property1Property.GetValue(); }
            set { Property1Property.SetValue(value); }
        }


        private readonly EZData.Property<string> _privateGuardProperty = new EZData.Property<string>();
        public EZData.Property<string> GuardProperty { get { return _privateGuardProperty; } }
        public string Guard
        {
            get { return GuardProperty.GetValue(); }
            set { GuardProperty.SetValue(value); }
        }

    }
}
