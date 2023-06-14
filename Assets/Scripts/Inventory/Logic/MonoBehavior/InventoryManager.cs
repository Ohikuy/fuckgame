using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{

    //创建第三方数据
    public class DragData
    {
        public SlotHolder originalHolder;
        public RectTransform originalParent;
    }
    //获取背包的数据
    //TODO:最后添加模版用于保存数据
    [Header("Inventory Data")]
    public InventoryData_SO inventoryData;
    public InventoryData_SO actionData;
    public InventoryData_SO equipmentData;

    [Header("ContainerS")]
    public ContainerUI inventoryUI;
    public ContainerUI actionUI;
    public ContainerUI equipmentUI;

    [Header("Drag Canvas")]
    public Canvas dragCanvas;

    public DragData currentDrag;
    void Start()
    {
        //让背包和数据库连接，刷新图片
        inventoryUI.RefreshUI();
        //actionUI.RefreshUI();
        //equipmentUI.RefreshUI();
    }

    #region 检查拖拽物品是否在每一个slot范围内

    //判断鼠标的点是否在方格范围内
    public bool CheckInInventoryUI(Vector3 position)
    {
        for(int i = 0;i < inventoryUI.slotHolders.Length; i++)
        {
            RectTransform t = inventoryUI.slotHolders[i].transform as RectTransform;
            //把UI坐标（rectTransform.anchoredPosition）转换成屏幕坐标
            //Debug.Log("tScreenPosition:"+t.position);
            //Debug.Log("tScreemLocalPositon"+t.localPosition);
            //Debug.Log("tScreemAnchoredPosition" + t.anchoredPosition);
            //Debug.Log("vector3 position:"+position);
            //Debug.Log(position);

            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckInActionUI(Vector3 position)
    {
        for (int i = 0; i < actionUI.slotHolders.Length; i++)
        {
            RectTransform t = actionUI.slotHolders[i].transform as RectTransform;

            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
            {
                return true;
            }
        }
        return false;
    }

    public bool CheckInEquipmentUI(Vector3 position)
    {
        for (int i = 0; i < equipmentUI.slotHolders.Length; i++)
        {
            RectTransform t = equipmentUI.slotHolders[i].transform as RectTransform;

            if (RectTransformUtility.RectangleContainsScreenPoint(t, position))
            {
                return true;
            }
        }
        return false;
    }
    #endregion
}
