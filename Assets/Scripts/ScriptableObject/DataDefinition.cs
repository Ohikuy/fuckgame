using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDefinition : MonoBehaviour
{
    //枚举类型可以限制id唯一
    public PersistentType persistentType;
    public string ID;

    //当数量发生变化之后
    private void OnValidate()
    {
        if (persistentType == PersistentType.ReadWrite)
        {
            if (ID == string.Empty)
                ID = System.Guid.NewGuid().ToString();
        }
        else
        {
            ID = string.Empty;
        }
    }

}
