using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour,IInteractable
{
    public SpriteRenderer spriteRenderer;

    public Sprite darkSprite;
    public Sprite lightSprite;

    public bool isDone;



    private void OnEnable()
    {
        spriteRenderer.sprite = isDone ? lightSprite : darkSprite;
    }

    public void TriggerAction()
    {
        if (!isDone)
        {
            isDone = true;
            spriteRenderer.sprite = lightSprite;

            //TODO:±£´æÊý¾Ý
            this.gameObject.tag = "Untagged";
        }
    }
}
