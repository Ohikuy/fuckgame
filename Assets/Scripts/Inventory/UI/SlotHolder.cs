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
        //Bag里面的列表里的物品对应的序号，itemUI.bag 在上面拿到了inventoryData，找到格子同样序号的item
        var a = itemUI.Bag;
        //itemUI.Bag是 InventoryData_SO Bag里的数据，itemUI.Index这个是slot holder的序号，通过序号拿到Bag里的数据，存到ItemUI去
        var item = itemUI.Bag.items[itemUI.Index];
        itemUI.SetupItemUI(item.itemData, item.amount);
    }
}
