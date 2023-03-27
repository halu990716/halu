using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class DataFormTest
{
    public string name;
    public string lv;

    public DataFormTest(string _name, string _lv)
    {
        name = _name;
        lv = _lv;
    }
}

public class DataTest
{
    //private DataTest() { }
    //private static DataTest Instance = null;

    //public static DataTest GetInstance()
    //{
    //    if (Instance == null)
    //        Instance = new DataTest();
    //    return Instance;
    //}

    //public TextAsset JsonData = Resources.Load<TextAsset>("savaFile/Data");
    //DataForm form = JsonUtility.FromJson<DataForm>(JsonData.ToString());

    //public int value = int.Parse(form.age);
    //public string userName = form.name;
}