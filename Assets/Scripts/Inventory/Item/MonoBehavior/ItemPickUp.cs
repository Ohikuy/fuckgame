using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemData_SO itemData;
    private BoxCollider2D boxCollider2D;
    public InventoryManager inventoryManager;

    protected virtual void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(boxCollider2D, GameObject.Find("dina").GetComponent<CapsuleCollider2D>());
    }

    private void Start()
    {
        inventoryManager = GameObject.Find("Inventory Canvas").GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //将物品添加到背包
            inventoryManager.inventoryData.AddItem(itemData, itemData.itemAmount);
            inventoryManager.inventoryUI.RefreshUI();
            Destroy(gameObject);
        }
    }
}
