using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LevelData;
using Newtonsoft.Json;

public class Test : MonoBehaviour
{
    private JsonToLevelData jsonToLevelData;

    void Start()
    {
        var json = Resources.Load<TextAsset>("Data/Level1").text;
        jsonToLevelData = new JsonToLevelData(json);
    }
}
