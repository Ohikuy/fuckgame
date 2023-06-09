using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data 
{
    public string sceneToSave;

    public Dictionary<string, SerializeVector3> characterPosDict = new Dictionary<string, SerializeVector3>();
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
