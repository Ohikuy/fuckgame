using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data 
{
    public string sceneToSave;

    public Dictionary<string, Vector3> characterPosDict = new Dictionary<string, Vector3>();
    public Dictionary<string, float> floatSavedData = new Dictionary<string, float>();

    //����ģʽ:���GameSceneSO����ôת��string���͵ģ�ֻ��¶�������㣬�����о�����
    public void SaveGameScene(GameSceneSO saveScene)
    {
        sceneToSave = JsonUtility.ToJson(saveScene);
        Debug.Log(sceneToSave);
    }

    public GameSceneSO GetSavedScene()
    {
        //����һ���յ�ʵ��
        var newScene = ScriptableObject.CreateInstance<GameSceneSO>();
        //�����л�
        JsonUtility.FromJsonOverwrite(sceneToSave,newScene);

        return newScene;
    }
}
