using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.SimpleDragAndDrop.Scripts
{
    public class OpenArea : DragAndDropCellBase
    {
        public override void PlaceItemPosition(GameObject itemObj)
        {
            // Do nothing - keep where dropped
        }

        public override void HandlePreviousItem()
        {
            // Do nothing
        }
    }
}
