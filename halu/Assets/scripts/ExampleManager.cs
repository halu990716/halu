using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class MemberForm
{
    public string Name;
    public int Age;

    public MemberForm(string name, int age)
    {
        this.Name = name;
        this.Age = age;
    }
}
// ȸ������
// �α���


public class ExampleManager : MonoBehaviour
{

    string URL = "https://script.google.com/macros/s/AKfycbx3QlTyD01scP2y90VF8bE5vOcOys278puntrN7cey99ZpOJMxXjsvnpqK-jkg-tyVaAA/exec";

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
}
