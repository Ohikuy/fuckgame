using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory",menuName ="Inventory/Inventory Data")]
public class InventoryData_SO : ScriptableObject
{
    public List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(ItemData_SO newItemData,int amount)
    {
        //如果没找到新建一个，如果找到了+amount
        bool found = false;
        //可堆叠
        if (newItemData.stackable)
        {
            foreach(var item in items)
            {
                //匹配上了
                if(item.itemData == newItemData)
                {
                    item.amount += amount;
                    found = true;
                    break;
                }
            }
            for(int i = 0; i < items.Count; i++)
            {
                if(items[i].itemData == null && !found)
                {
                    items[i].itemData = newItemData;
                    items[i].amount = amount;
                    break;
                }
            }
        }
    }
}

//要序列化才能在Inspector看见
[System.Serializable]
public class InventoryItem 
{
    public ItemData_SO itemData;
    public int amount;
}
