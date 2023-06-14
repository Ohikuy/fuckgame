using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//ȷ�������汾Ҳ����ȷ���ItemUI
[RequireComponent(typeof(ItemUI))]
//��ק�Ŀ�ʼ����ק�Ĺ��̣��Լ���ק
public class DragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    ItemUI currentItemUI;
    //��ǰ�ĸ���
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
        //��¼ԭʼ����

        //�϶����︸����dragCanvas
        transform.SetParent(InventoryManager.Instance.dragCanvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //�������λ���ƶ�
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //������Ʒ ��������    
        //�ж��Ƿ��ڸ��ӵķ�Χ��
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (InventoryManager.Instance.CheckInInventoryUI(eventData.position) || InventoryManager.Instance.CheckInEquipmentUI(eventData.position) || InventoryManager.Instance.CheckInActionUI(eventData.position))
            {
                Debug.Log("in change");
                if (eventData.pointerEnter.gameObject.GetComponent<SlotHolder>()) 
                {
                    Debug.Log("���õ�slotHolder");
                    targetHolder = eventData.pointerEnter.gameObject.GetComponent<SlotHolder>();

                }

                else
                {
                    Debug.Log("�����õ�slotHolder");
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

        //��������Ч��
        RectTransform t = transform as RectTransform;

        t.offsetMax = -Vector2.one * 5;
        t.offsetMin = Vector2.one * 5;
    }

    public void SwapItem()
    {
        var targetItem = targetHolder.itemUI.Bag.items[targetHolder.itemUI.Index];
        var tempItem = currentHolder.itemUI.Bag.items[currentHolder.itemUI.Index];

        bool isSameItem = tempItem.itemData == targetItem.itemData;

        //���������Ʒ��ͬ�ҿɶѵ� �ͼ�����
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
