
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//约束where 
public class Singleton<T> : MonoBehaviour where T:Singleton<T>
{
    private static T instance;

    //外部可以引用的，不能被更改的（单例模式）
    public static T Instance
    {
        get 
        {
            return instance;
        }
    }
    //可以在子类继承并且重写
   protected virtual void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
            instance = (T)this;
    }
    
    //返回当前的这个泛型单例模式是否已经生成了
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
