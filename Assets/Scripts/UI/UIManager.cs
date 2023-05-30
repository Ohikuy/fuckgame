using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("事件监听")]
    public CharacterEventSO healthEvent;

    //注册事件
    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
    }

    //取消事件
    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
    }

    private void OnHealthEvent(Character character)
    {
        var persentage = character.currentHealth / character.maxHealth;
        playerStatBar.OnHealthChange(persentage);
    }
}
