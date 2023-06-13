using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType {BAG,WEAPON,ARMOR,ACTION}
public class SlotHolder : MonoBehaviour
{
    public SlotType slotType;
    public ItemUI itemUI;

    public void UpdateItem()
    {
        switch (slotType)
        {
            case SlotType.BAG:
                itemUI.Bag = InventoryManager.Instance.inventoryData;
                break;
            case SlotType.WEAPON:
                break;
            case SlotType.ARMOR:
                break;
            case SlotType.ACTION:
                break;
        }
        //Bag������б������Ʒ��Ӧ����ţ�itemUI.bag �������õ���inventoryData���ҵ�����ͬ����ŵ�item
        var a = itemUI.Bag;
        //itemUI.Bag�� InventoryData_SO Bag������ݣ�itemUI.Index�����slot holder����ţ�ͨ������õ�Bag������ݣ��浽ItemUIȥ
        var item = itemUI.Bag.items[itemUI.Index];
        itemUI.SetupItemUI(item.itemData, item.amount);
    }
}
