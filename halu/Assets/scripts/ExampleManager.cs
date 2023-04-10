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
// 회원가입
// 로그인


public class ExampleManager : MonoBehaviour
{

    string URL = "https://script.google.com/macros/s/AKfycbx3QlTyD01scP2y90VF8bE5vOcOys278puntrN7cey99ZpOJMxXjsvnpqK-jkg-tyVaAA/exec";

    IEnumerator Start()
    {
        // ** 요청을 하기위한 작업.

        MemberForm member = new MemberForm("성춘향", 22);

        WWWForm form = new WWWForm();

        form.AddField("Name", member.Name);
        form.AddField("Age", member.Age);

        using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
        {
            yield return request.SendWebRequest();

            // ** 응답에 대한 작업.

            print(request.downloadHandler.text);
        }
    }
}
