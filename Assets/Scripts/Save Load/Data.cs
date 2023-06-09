using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data 
{
    public string sceneToSave;

    public Dictionary<string, SerializeVector3> characterPosDict = new Dictionary<string, SerializeVector3>();
    public Dictionary<string, float> floatSavedData = new Dictionary<string, float>();

    //工厂模式:别管GameSceneSO是怎么转成string类型的，只暴露函数给你，你运行就行了
    public void SaveGameScene(GameSceneSO saveScene)
    {
        sceneToSave = JsonUtility.ToJson(saveScene);
        Debug.Log(sceneToSave);
    }

    public GameSceneSO GetSavedScene()
    {
        //创建一个空的实例
        var newScene = ScriptableObject.CreateInstance<GameSceneSO>();
        //反序列化
        JsonUtility.FromJsonOverwrite(sceneToSave,newScene);

        return newScene;
    }
}

public class SerializeVector3
{
    public float x, y, z;
    public SerializeVector3(Vector3 pos)
    {
        this.x = pos.x;
        this.y = pos.y;
        this.z = pos.z;
    }

    public Vector3 ToVector3()
    {
        return new Vector3(x, y, z);
    }
}
