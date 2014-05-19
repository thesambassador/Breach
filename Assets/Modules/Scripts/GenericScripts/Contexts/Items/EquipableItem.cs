using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Modules.Inventory
{

    public class EquipableItem : BaseItem
    {

        #region Property Slot
        private readonly EZData.Property<int> _privateSlotProperty = new EZData.Property<int>();
        public EZData.Property<int> SlotProperty { get { return _privateSlotProperty; } }
        public Slot Slot
        {
            get { return (Slot)SlotProperty.GetValue(); }
            set { SlotProperty.SetValue((int)value); }
        }
        #endregion

    }
}
