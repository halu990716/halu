using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

[System.Serializable]
class DataForm
{
    public string name;
    public string age;
    public string damege;
    public string EXP;

    public DataForm(string _name, string _age, string _damege, string _EXP)
    {
        name = _name;
        age = _age;
        damege = _damege;
        EXP = _EXP;
    }
}

public class DataManager : MonoBehaviour
{
    private string userName;
    public int value;
    public int damege;
    public int exp;

    void Start()
    {
        var JsonData = Resources.Load<TextAsset>("saveFile/Data");
        DataForm form = JsonUtility.FromJson<DataForm>(JsonData.ToString());

        value = int.Parse(form.age);
        userName = form.name;
        damege = int.Parse(form.damege);
        exp = int.Parse(form.EXP);

        //ControllerManager.GetInstance().BulletDamage = damege;
        //ControllerManager.GetInstance().Player_EXP = exp;

        //print(userName + " : " + value + "\n데미지 : " + damege + "   경험치 : " + exp);

        StartCoroutine(Up());
    }

        
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            ++value;
            print(value);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            --value;
            print(value);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            damege = ControllerManager.GetInstance().BulletDamage;
            exp = ControllerManager.GetInstance().Player_EXP;
            SaveData(userName, value.ToString(), damege.ToString(), exp.ToString());
        }
    }

    IEnumerator Up()
    {
        yield return new WaitForSeconds(1.0f);

        ControllerManager.GetInstance().BulletDamage = damege;
        ControllerManager.GetInstance().Player_EXP = exp;
        print(userName + " : " + value + "\n데미지 : " + damege + "   경험치 : " + exp);
    }


    public void SaveData(string _name, string _age, string _damege, string _EXP)
    {
        DataForm form = new DataForm(_name, _age, _damege, _EXP);

        string JsonData = JsonUtility.ToJson(form);

        FileStream fileStream = new FileStream(
            Application.dataPath + "/Resources/saveFile/Data.json", FileMode.Create);

        byte[] data = Encoding.UTF8.GetBytes(JsonData);

        fileStream.Write(data, 0, data.Length);
        fileStream.Close();

        print(userName + " : " + value + "\n데미지 : " + damege + "   경험치 : " + exp);

    }
}