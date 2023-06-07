using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data 
{
    public string sceneToSave;

    public Dictionary<string, Vector3> characterPosDict = new Dictionary<string, Vector3>();
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
