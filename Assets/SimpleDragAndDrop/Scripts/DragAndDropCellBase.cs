using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

/// <summary>
/// Every item's cell must contain this script
/// </summary>
[RequireComponent(typeof(Image))]
public class DragAndDropCellBase : MonoBehaviour, IDropHandler
{
    public enum CellType
    {
        Swap,                                                               // Items will be swapped between cells
        DropOnly,                                                           // Item will be dropped into cell
        DragOnly,                                                           // Item will be dragged from this cell
        DragAndDrop,                                                        // Item will be dragged from this cell and dropped into cell
        UnlimitedSource                                                     // Item will be cloned and dragged from this cell
    }
    public CellType cellType = CellType.Swap;                               // Special type of this cell

    public struct DropDescriptor                                            // Struct with info about item's drop event
    {
        public DragAndDropCellBase sourceCell;                                  // From this cell item was dragged
        public DragAndDropCellBase destinationCell;                             // Into this cell item was dropped
        public DragAndDropItemBase  item;                                        // dropped item
    }

    public Color empty = new Color();                                       // Sprite color for empty cell
    public Color full = new Color();                                        // Sprite color for filled cell

    void OnEnable()
    {
        DragAndDropItemBase.OnItemDragStartEvent += OnAnyItemDragStart;         // Handle any item drag start
        DragAndDropItemBase.OnItemDragEndEvent += OnAnyItemDragEnd;             // Handle any item drag end
    }

    void OnDisable()
    {
        DragAndDropItemBase.OnItemDragStartEvent -= OnAnyItemDragStart;
        DragAndDropItemBase.OnItemDragEndEvent -= OnAnyItemDragEnd;
    }

    void Start()
    {
        SetBackgroundState(GetComponentInChildren<DragAndDropItemBase>() == null ? false : true);
    }

    /// <summary>
    /// On any item drag start need to disable all items raycast for correct drop operation
    /// </summary>
    /// <param name="item"> dragged item </param>
    private void OnAnyItemDragStart(DragAndDropItemBase  item)
    {
        DragAndDropItemBase  myItem = GetComponentInChildren<DragAndDropItemBase>(); // Get item from current cell
        if (myItem != null)
        {
            myItem.MakeRaycast(false);                                      // Disable item's raycast for correct drop handling
            if (myItem == item)                                             // If item dragged from this cell
            {
                // Check cell's type
                switch (cellType)
                {
                    case CellType.DropOnly:
                        DragAndDropItemBase.icon.SetActive(false);              // Item will not be dropped
                        break;
                    case CellType.UnlimitedSource:
                        // Nothing to do
                        break;
                    default:
                        item.MakeVisible(false);                            // Hide item in cell till dragging
                        SetBackgroundState(false);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// On any item drag end enable all items raycast
    /// </summary>
    /// <param name="item"> dragged item </param>
    private void OnAnyItemDragEnd(DragAndDropItemBase  item)
    {
        DragAndDropItemBase  myItem = GetComponentInChildren<DragAndDropItemBase>(); // Get item from current cell
        if (myItem != null)
        {
            if (myItem == item)
            {
                SetBackgroundState(true);
            }
            myItem.MakeRaycast(true);                                       // Enable item's raycast
        }
        else
        {
            SetBackgroundState(false);
        }
    }

    /// <summary>
    /// Item is dropped in this cell
    /// </summary>
    /// <param name="data"></param>
    public void OnDrop(PointerEventData data)
    {
        if (DragAndDropItemBase.icon != null)
        {
            if (DragAndDropItemBase.icon.activeSelf == true)                    // If icon inactive do not need to drop item in cell
            {
                DragAndDropItemBase  item = DragAndDropItemBase.draggedItem;
                DragAndDropCellBase sourceCell = DragAndDropItemBase.sourceCell;
                DropDescriptor desc = new DropDescriptor();
                if ((item != null) && (sourceCell != this))
                {
                    switch (sourceCell.cellType)                            // Check source cell's type
                    {
                        case CellType.UnlimitedSource:
                            string itemName = item.name;
                            item = Instantiate(item);                       // Clone item from source cell
                            item.name = itemName;
                            break;
                        default:
                            // Nothing to do
                            break;
                    }
                    switch (cellType)                                       // Check this cell's type
                    {
                        case CellType.Swap:
                            DragAndDropItemBase  currentItem = GetComponentInChildren<DragAndDropItemBase>();
                            switch (sourceCell.cellType)
                            {
                                case CellType.Swap:
                                    SwapItems(sourceCell, this);            // Swap items between cells
                                    // Fill event descriptor
                                    desc.item = item;
                                    desc.sourceCell = sourceCell;
                                    desc.destinationCell = this;
                                    // Send message with DragAndDrop info to parents GameObjects
                                    StartCoroutine(NotifyOnDragEnd(desc));
                                    if (currentItem != null)
                                    {
                                        // Fill event descriptor
                                        desc.item = currentItem;
                                        desc.sourceCell = this;
                                        desc.destinationCell = sourceCell;
                                        // Send message with DragAndDrop info to parents GameObjects
                                        StartCoroutine(NotifyOnDragEnd(desc));
                                    }
                                    break;
                                default:
                                    PlaceItem(item.gameObject, data.position);             // Place dropped item in this cell
                                    // Fill event descriptor
                                    desc.item = item;
                                    desc.sourceCell = sourceCell;
                                    desc.destinationCell = this;
                                    // Send message with DragAndDrop info to parents GameObjects
                                    StartCoroutine(NotifyOnDragEnd(desc));
                                    break;
                            }
                            break;
                        case CellType.DropOnly:
                        case CellType.DragAndDrop:
                            PlaceItem(item.gameObject, data.position);                     // Place dropped item in this cell
                            // Fill event descriptor
                            desc.item = item;
                            desc.sourceCell = sourceCell;
                            desc.destinationCell = this;
                            // Send message with DragAndDrop info to parents GameObjects
                            StartCoroutine(NotifyOnDragEnd(desc));
                            break;
                        default:
                            // Nothing to do
                            break;
                    }
                }
                if (item.GetComponentInParent<DragAndDropCellBase>() == null)   // If item have no cell after drop
                {
                    Destroy(item.gameObject);                               // Destroy it
                }
            }
        }
    }

    /// <summary>
    /// Change cell's sprite color on item put/remove
    /// </summary>
    /// <param name="condition"> true - filled, false - empty </param>
    private void SetBackgroundState(bool condition)
    {
        GetComponent<Image>().color = condition ? full : empty;
    }

    /// <summary>
    /// Delete item from this cell
    /// </summary>
    public void RemoveItem()
    {
        foreach (DragAndDropItemBase  item in GetComponentsInChildren<DragAndDropItemBase>())
        {
            Destroy(item.gameObject);
        }
        SetBackgroundState(false);
    }

    /// <summary>
    /// Put new item in this cell
    /// </summary>
    /// <param name="itemObj"> New item's object with DragAndDropItemBase  script </param>
    public void PlaceItem(GameObject itemObj, Vector2 dropPosition)
    {
        HandlePreviousItem();
        if (itemObj != null)
        {
            itemObj.transform.SetParent(transform, false);
            PlaceItemPosition(itemObj, new Vector2(dropPosition.x - transform.position.x, dropPosition.y - transform.position.y)); 
            DragAndDropItemBase  item = itemObj.GetComponent<DragAndDropItemBase>();
            if (item != null)
            {
                item.MakeRaycast(true);
            }
            SetBackgroundState(true);
        }
    }

    public virtual void HandlePreviousItem()
    {
        RemoveItem();                                                       // Remove current item from this cell
    }

    public virtual void PlaceItemPosition(GameObject itemObj, Vector2 dropPosition)
    {
        itemObj.transform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// Get item from this cell
    /// </summary>
    /// <returns> Item </returns>
    public DragAndDropItemBase  GetItem()
    {
        return GetComponentInChildren<DragAndDropItemBase>();
    }

    /// <summary>
    /// Swap items between to cells
    /// </summary>
    /// <param name="firstCell"> Cell </param>
    /// <param name="secondCell"> Cell </param>
    public void SwapItems(DragAndDropCellBase firstCell, DragAndDropCellBase secondCell)
    {
        if ((firstCell != null) && (secondCell != null))
        {
            DragAndDropItemBase  firstItem = firstCell.GetItem();                // Get item from first cell
            DragAndDropItemBase  secondItem = secondCell.GetItem();              // Get item from second cell
            if (firstItem != null)
            {
                // Place first item into second cell
                firstItem.transform.SetParent(secondCell.transform, false);
                firstItem.transform.localPosition = Vector3.zero;
                secondCell.SetBackgroundState(true);
            }
            if (secondItem != null)
            {
                // Place second item into first cell
                secondItem.transform.SetParent(firstCell.transform, false);
                secondItem.transform.localPosition = Vector3.zero;
                firstCell.SetBackgroundState(true);
            }
        }
    }

    private IEnumerator NotifyOnDragEnd(DropDescriptor desc)
    {
        // Wait end of drag operation
        while (DragAndDropItemBase.draggedItem != null)
        {
            yield return new WaitForEndOfFrame();
        }
        // Send message with DragAndDrop info to parents GameObjects
        gameObject.SendMessageUpwards("OnItemPlace", desc, SendMessageOptions.DontRequireReceiver);
    }
}
