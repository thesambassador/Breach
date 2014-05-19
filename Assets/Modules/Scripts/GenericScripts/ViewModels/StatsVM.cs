using Assets.Modules.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Modules.GUI.Stats
{


    public class StatsVM : MyViewModelBase
    {
        public NguiRootContext CharacterStatsView { get; set; }
        

        /*
         public override void GenerateContext()
         {
                // All these values are for testing
                Context = new CharacterStats();
                Context.pMaxHealth = 120;
                Context.pExperienceToLevel = 100;
                Context.pTitle = "Super Berserk";
                Context.pName = "Bryan";
                Context.pExperience = 33;
                Context.pMana = .77f;
                Context.pHealth = 85;
                Context.pStrength = 10;
                Context.pStatPoints = 15;

                Views.SetContext(Context);
         }*/
    }


}
