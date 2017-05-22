using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.SimpleDragAndDrop.Scripts
{
    public class OpenArea : DragAndDropCellBase
    {
        public override void PlaceItemPosition(GameObject itemObj, Vector2 dropPosition)
        {
            itemObj.transform.localPosition = new Vector3(dropPosition.x, dropPosition.y, itemObj.transform.localPosition.z);
        }

        public override void HandlePreviousItem()
        {
            // Do nothing
        }
    }
}
