using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;

[System.Serializable]
public class MemberForm
{
    public int index;
    public string name;
    public int age;
    public int gender;

    public MemberForm(int index, string name, int age, int gender)
    {
        this.index = index;
        this.name = name;
        this.age = age;
        this.gender = gender;
    }
}
// ȸ������
// �α���


public class ExampleManager : MonoBehaviour
{

    string URL = "https://script.google.com/macros/s/AKfycbx3QlTyD01scP2y90VF8bE5vOcOys278puntrN7cey99ZpOJMxXjsvnpqK-jkg-tyVaAA/exec";

    /*
    IEnumerator Start()
    {
        // ** ��û�� �ϱ����� �۾�.

        MemberForm member = new MemberForm("������", 22);

        WWWForm form = new WWWForm();

        form.AddField("Name", member.Name);
        form.AddField("Age", member.Age);

        using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
        {
            yield return request.SendWebRequest();

            // ** ���信 ���� �۾�.

            print(request.downloadHandler.text);
        }
    }
    */

    IEnumerator Start()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(URL))
        {
            yield return request.SendWebRequest();

            MemberForm json = JsonUtility.FromJson<MemberForm>(request.downloadHandler.text);

            // ** ���信 ���� �۾�.

            print(json.index);
            print(json.name);
            print(json.age);
            print(json.gender);
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene("progressScenes");
    }

}
