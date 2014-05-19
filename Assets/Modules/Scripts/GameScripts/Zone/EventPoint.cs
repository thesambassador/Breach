 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Modules.Managers.ZoneGenerator
{
    public enum TypeOfEvent
    {
        Town,
        Quest,
        Battle
    }
    public class EventPoint : MonoBehaviour
    {
        public EventPoint()
        {            
        }

        public void Initialize(GameObject obj, int PosX, TypeOfEvent type)
        {
            EventToActivate = obj;
            this.transform.position = new Vector3(PosX, 0, 0);
            PositionX = PosX;
            EventPointType = type;
        }
        public TypeOfEvent EventPointType;
       // public bool IsQuestEvent = false;
       // public bool IsTownEvent = false;
        //public bool IsBattleEvent = false;

        public GameObject EventToActivate;
        public int PositionX;
    }

}
