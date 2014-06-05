using UnityEngine;
using System.Collections;

public class UISlotDragDropItem : UIDragDropItem  {
    public SlotType slotType;

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

            Vector3 pos = mTrans.localPosition;
            pos.z = 0f;
            mTrans.localPosition = pos;
        }
        else
        {
            if(!cloneOnDrag){
            // No valid container under the mouse -- revert the item's parent
                mTrans.parent = mParent;
            }
            else
            {
                deleteClone = true;
            }
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
