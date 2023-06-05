using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStatBar playerStatBar;
    [Header("事件监听")]
    public CharacterEventSO healthEvent;
    public SceneLoadEventSO loadEvent;

    //注册事件
    private void OnEnable()
    {
        healthEvent.OnEventRaised += OnHealthEvent;
        loadEvent.LoadRequestEvent += OnLoadEvent;
    }

    //取消事件
    private void OnDisable()
    {
        healthEvent.OnEventRaised -= OnHealthEvent;
        loadEvent.LoadRequestEvent -= OnLoadEvent;
    }

    private void OnLoadEvent(GameSceneSO sceneToLoad,Vector3 b,bool c)
    {
        var isMenu = sceneToLoad.sceneType == SceneType.Menu;
        playerStatBar.gameObject.SetActive(!isMenu);
    }

    private void OnHealthEvent(Character character)
    {
        var persentage = character.currentHealth / character.maxHealth;
        playerStatBar.OnHealthChange(persentage);
    }
}
