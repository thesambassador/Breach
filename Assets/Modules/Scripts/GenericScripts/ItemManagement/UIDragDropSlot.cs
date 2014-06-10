using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class UIDragDropSlot : UIDragDropContainer
{
    public SlotType slotType;

    //for slots, we're always going to want them to reparent to themself
    public void OnStart()
    {
        this.reparentTarget = this.transform;
    }


}

