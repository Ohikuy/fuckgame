using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour
{
    public Image icon = null;
    public Text amount = null;

    //可读可写
    public InventoryData_SO Bag
    {
        get; set;
    }

    //因为是从零开始的，为避免错误排序设置为-1
    public int Index
    {
        get; set;
    } = -1;
    public void SetupItemUI(ItemData_SO item, int itemAmount)
    {
        if (item != null)
        {
            icon.sprite = item.itemIcon;
            var a = itemAmount;
            var b = amount;
            var c = itemAmount.ToString("00");
            //此处有bug
            amount.text = itemAmount.ToString("00");

            icon.gameObject.SetActive(true);
        }
        else
            icon.gameObject.SetActive(false);
    }
}
