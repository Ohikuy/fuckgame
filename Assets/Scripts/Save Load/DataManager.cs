using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [Header("事件监听")]
    public VoidEventSO saveDataEvent;

    private List<ISaveable> saveableList = new List<ISaveable>();
    private Data saveData;

    private void Awake()
    {
        //单例模式标准写法：确保场景有且只有一个实例
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        saveData = new Data();
    }

    private void OnEnable()
    {
        saveDataEvent.OnEventRaised += Save;
    }

    private void OnDisable()
    {
        saveDataEvent.OnEventRaised -= Save;
    }

    private void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            Load();
        }
    }


    //保存在列表中，统一通知存储加载
    public void RegisterSaveData(ISaveable saveable)
    {
        //DataManager.instance.RegisterSaveData(saveable);

        if (!saveableList.Contains(saveable)) 
        {
            saveableList.Add(saveable);
        }
    }

    public void UnRegisterSaveData(ISaveable saveable)
    {
        //DataManager.instance.UnRegisterSaveData(saveable);
        saveableList.Remove(saveable);
    }

    public void Save()
    {
        foreach(var savaable in saveableList)
        {
            savaable.GetSaveData(saveData);
        }

        foreach (var item in saveData.characterPosDict)
        {
            Debug.Log(item.Key + "  " + item.Value);
        }
    }

    public void Load()
    {
        foreach (var savaable in saveableList)
        {
            savaable.LoadData(saveData);
        }
    }

}
