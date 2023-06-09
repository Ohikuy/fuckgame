using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public ItemData_SO itemData;
    public BoxCollider2D boxCollider2D;

    protected virtual void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        Physics2D.IgnoreCollision(boxCollider2D, GameObject.Find("dina").GetComponent<CapsuleCollider2D>());
    }

        private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //TODO:将物品添加到背包

            //加入背包
            Destroy(gameObject);
        }
    }
}
