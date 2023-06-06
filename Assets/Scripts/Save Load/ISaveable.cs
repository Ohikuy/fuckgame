using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable 
{
    DataDefinition GetDataID();
    void RegisterSaveData(ISaveable saveable);
    void UnRegisterSaveData(ISaveable saveable);

    void GetSaveData(Data data);
    void LoadData(Data data);
}
