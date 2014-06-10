using UnityEngine;
using System.Collections;

public class UISlotDragDropItem : UIDragDropItem  {
    public SlotType slotType;
    private bool _inSlot = false;

    private Transform originalList; //list that this was originally part of, return to this list if we get dragged into empty space from a slot

    protected override void Start()
    {
        base.Start();
        originalList = mTrans.parent;
    }

    //most of this is copied from the OnDragDropRelease of UIDragDropItem, but I added some checks to the target container's slot type
    protected override void OnDragDropRelease(GameObject surface)
    {
        mTouchID = int.MinValue;

        // Re-enable the collider
        if (mButton != null) mButton.isEnabled = true;
        else if (mCollider != null) mCollider.enabled = true;

        // Is there a droppable container?
        UIDragDropSlot container = surface ? NGUITools.FindInParents<UIDragDropSlot>(surface) : null;

        bool deleteClone = false;

        if (container != null && container.slotType == this.slotType) 
        {

            // Container found -- parent this object to the container
            mTrans.parent = (container.reparentTarget != null) ? container.reparentTarget : container.transform;

            Vector3 pos = new Vector3(0,0,0);
            pos.z = 0f;
            mTrans.localPosition = pos;
            this.cloneOnDrag = false;
            this._inSlot = true;
        }
        else
        {
            if(!cloneOnDrag){
            // No valid container under the mouse -- set the item's parent back to the items panel
                mTrans.parent = originalList;
            }
            else
            {
                deleteClone = true;
            }

            this._inSlot = false;
        }

        // Update the grid and table references
        mParent = mTrans.parent;
        mGrid = NGUITools.FindInParents<UIGrid>(mParent);
        mTable = NGUITools.FindInParents<UITable>(mParent);

        // Re-enable the drag scroll view script
        if (mDragScrollView != null)
            mDragScrollView.enabled = true;

        // Notify the widgets that the parent has changed
        NGUITools.MarkParentAsChanged(gameObject);

        if (mTable != null) mTable.repositionNow = true;
        if (mGrid != null) mGrid.repositionNow = true;

        if (deleteClone)
        {
            NGUITools.Destroy(gameObject);
        }
         
    }
	
}
