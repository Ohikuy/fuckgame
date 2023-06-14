using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//确保其他版本也会正确添加ItemUI
[RequireComponent(typeof(ItemUI))]
//拖拽的开始，拖拽的过程，以及拖拽
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    ItemUI currentItemUI;
    //当前的格子
    SlotHolder currentHolder;

    SlotHolder targetHolder;

    void Awake()
    {
        currentItemUI = GetComponent<ItemUI>();
        currentHolder = GetComponentInParent<SlotHolder>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        InventoryManager.Instance.currentDrag = new InventoryManager.DragData();
        InventoryManager.Instance.currentDrag.originalHolder = GetComponent<SlotHolder>();
        InventoryManager.Instance.currentDrag.originalParent = (RectTransform)transform.parent;
        //记录原始数据

        //拖动到达父级的dragCanvas
        transform.SetParent(InventoryManager.Instance.dragCanvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //跟随鼠标位置移动
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //放下物品 交换数据    
        //判断是否在格子的范围内
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (InventoryManager.Instance.CheckInInventoryUI(eventData.position) || InventoryManager.Instance.CheckInEquipmentUI(eventData.position) || InventoryManager.Instance.CheckInActionUI(eventData.position))
            {
                Debug.Log("in change");
                if (eventData.pointerEnter.gameObject.GetComponent<SlotHolder>()) 
                {
                    Debug.Log("能拿到slotHolder");
                    targetHolder = eventData.pointerEnter.gameObject.GetComponent<SlotHolder>();

                }

                else
                {
                    Debug.Log("不能拿到slotHolder");
                    var a = eventData.pointerEnter.gameObject;
                    targetHolder = eventData.pointerEnter.gameObject.GetComponentInParent<SlotHolder>();
                    Debug.Log(eventData.pointerEnter.gameObject.name);

                }
                    

                switch (targetHolder.slotType)
                {
                    case SlotType.BAG:
                        SwapItem();
                        break;
                    case SlotType.WEAPON:
                        break;
                    case SlotType.ARMOR:
                        break;
                    case SlotType.ACTION:
                        break;
                }

                currentHolder.UpdateItem();
                targetHolder.UpdateItem();
                
            }
        }
        transform.SetParent(InventoryManager.Instance.currentDrag.originalParent);

        //格子吸附效果
        RectTransform t = transform as RectTransform;

        t.offsetMax = -Vector2.one * 5;
        t.offsetMin = Vector2.one * 5;
    }

    public void SwapItem()
    {
        var targetItem = targetHolder.itemUI.Bag.items[targetHolder.itemUI.Index];
        var tempItem = currentHolder.itemUI.Bag.items[currentHolder.itemUI.Index];

        bool isSameItem = tempItem.itemData == targetItem.itemData;

        //如果两个物品相同且可堆叠 就加数量
        if(isSameItem && targetItem.itemData.stackable)
        {
            targetItem.amount += tempItem.amount;
            tempItem.itemData = null;
            tempItem.amount = 0;
        }
        else
        {
            currentHolder.itemUI.Bag.items[currentHolder.itemUI.Index] = targetItem;
            targetHolder.itemUI.Bag.items[targetHolder.itemUI.Index] = tempItem;
        }
    }
}
