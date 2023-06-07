using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable 
{
    DataDefinition GetDataID();
    void RegisterSaveData();
    void UnRegisterSaveData();


    void GetSaveData(Data data);
    void LoadData(Data data);
}
