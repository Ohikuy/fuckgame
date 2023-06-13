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
                if (eventData.pointerEnter.gameObject.GetComponent<SlotHolder>())
                    targetHolder = eventData.pointerEnter.gameObject.GetComponent<SlotHolder>();
                Debug.Log(eventData.pointerEnter.gameObject);
            }
        }
    }
}
