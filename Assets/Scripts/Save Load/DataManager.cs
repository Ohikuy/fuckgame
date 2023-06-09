using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Newtonsoft.Json;
using System.IO;

[DefaultExecutionOrder(-100)]
public class DataManager : MonoBehaviour
{
    //����ģʽ
    public static DataManager instance;

    [Header("�¼�����")]
    public VoidEventSO saveDataEvent;
    public VoidEventSO loadDataEvent;

    public List<ISaveable> saveableList = new List<ISaveable>();
    private Data saveData;
    private string jsonFolder;

    private void Awake()
    {
        //����ģʽ��׼д����ȷ����������ֻ��һ��ʵ��
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        saveData = new Data();

        jsonFolder = Application.persistentDataPath + "/SAVE DATA/";
        ReadSavedData();
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

        var resultPath = jsonFolder + "data.sav";
        Debug.Log(resultPath);
        //json���л�
        var jsonData = JsonConvert.SerializeObject(saveData);

        if(!File.Exists(resultPath))
        {
            Directory.CreateDirectory(jsonFolder);
        }

        File.WriteAllText(resultPath, jsonData);
        //foreach (var item in saveData.characterPosDict)
        //{
        //    Debug.Log(item.Key + "  " + item.Value);
        //}
    }

    public void Load()
    {
        foreach (var savaable in saveableList)
        {
            savaable.LoadData(saveData);
        }
    }

    private void ReadSavedData()
    {
        var resultPath = jsonFolder + "data.sav";

        if (File.Exists(resultPath))
        {
            var stringData = File.ReadAllText(resultPath);
            var jsonData = JsonConvert.DeserializeObject<Data>(stringData);

            saveData = jsonData;
        }
    }
}
