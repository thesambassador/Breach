using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules.Inventory
{


    public class BaseItem : EZData.Context
    {

        #region Property Name
        private readonly EZData.Property<string> _privateNameProperty = new EZData.Property<string>();
        public EZData.Property<string> NameProperty { get { return _privateNameProperty; } }
        public string Name
        {
            get { return NameProperty.GetValue(); }
            set { NameProperty.SetValue(value); }
        }
        #endregion

      
	 
	private readonly EZData.Property<string> _privateProperty1Property = new EZData.Property<string>();
    public EZData.Property<string> Property1Property { get { return _privateProperty1Property; } }
    public string ItemType
	{
        get { return Property1Property.GetValue(); }
        set { Property1Property.SetValue(value); }
	}


  
         

        #region Property IsSelected
        private readonly EZData.Property<bool> _privateIsSelectedProperty = new EZData.Property<bool>();
        public EZData.Property<bool> IsSelectedProperty { get { return _privateIsSelectedProperty; } }
        public bool IsSelected
        {
            get { return IsSelectedProperty.GetValue(); }
            set { IsSelectedProperty.SetValue(value); }
        }
        #endregion




        #region Property Price
        private readonly EZData.Property<int> _privatePriceProperty = new EZData.Property<int>();
        public EZData.Property<int> PriceProperty { get { return _privatePriceProperty; } }
        public int Price
        {
            get { return PriceProperty.GetValue(); }
            set { PriceProperty.SetValue(value); }
        }
        #endregion



        public event System.Action<BaseItem> OnToggle;

        public void Toggle()
        {
            IsSelected = true;
            if (OnToggle != null)
                OnToggle(this);
        }

    }

}
