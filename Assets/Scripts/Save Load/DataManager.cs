using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DataManager : MonoBehaviour
{
    //����ģʽ
    public static DataManager instance;

    [Header("�¼�����")]
    public VoidEventSO saveDataEvent;
    public VoidEventSO loadDataEvent;

    public List<ISaveable> saveableList = new List<ISaveable>();
    private Data saveData;

    private void Awake()
    {
        //����ģʽ��׼д����ȷ����������ֻ��һ��ʵ��
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        saveData = new Data();
    }

    private void OnEnable()
    {
        saveDataEvent.OnEventRaised += Save;
        loadDataEvent.OnEventRaised += Load;
    }

    private void OnDisable()
    {
        saveDataEvent.OnEventRaised -= Save;
        loadDataEvent.OnEventRaised -= Load;

    }

    private void Update()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            Load();
        }
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
