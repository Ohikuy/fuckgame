
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Լ��where 
public class Singleton<T> : MonoBehaviour where T:Singleton<T>
{
    private static T instance;

    //�ⲿ�������õģ����ܱ����ĵģ�����ģʽ��
    public static T Instance
    {
        get 
        {
            return instance;
        }
    }
    //����������̳в�����д
   protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
            instance = (T)this;
    }
    
    //���ص�ǰ��������͵���ģʽ�Ƿ��Ѿ�������
    public static bool IsInitialized 
    {
        get { return instance != null; }
    }

    protected virtual void Ondestory()
    {
        if(instance == this)
        {
            instance = null;
        }
    }
}
