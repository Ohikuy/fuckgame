using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDefinition : MonoBehaviour
{
    //ö�����Ϳ�������idΨһ
    public PersistentType persistentType;
    public string ID;

    //�����������仯֮��
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
