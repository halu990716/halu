using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    private Text ui;
    public GameObject Player;
    public GameObject test;

    void Start()
    {
        ui = GetComponent<Text>();

        Vector3 Direction = (Player.transform.position - test.transform.position).normalized;

        transform.position += Direction * Time.deltaTime * 2.0f;
    }

    void Update()
    {
        float x = Player.transform.position.x - test.transform.position.x;
        float y = Player.transform.position.y - test.transform.position.y;

        //float distance = Mathf.Sqrt((x * x) + (y * y));

        // ** ���ⱸ�ϴ� ����
        /*
        Vector3 Direction = new Vector3(
            Player.transform.position.x - test.transform.position.x,
            Player.transform.position.y - test.transform.position.y,
            0.0f);

        Direction.Normalize();
        */

        // ** ���� ���ϴ� ����2
        /*
        Vector3 Direction = Player.transform.position - test.transform.position;
        Direction.Normalize();
        */

        // ** ���� ���ϴ� ����3
        //Vector3 Direction = (Player.transform.position - test.transform.position).normalized;

        //transform.position += Direction * Time.deltaTime * 2.0f;


        //float distance = Vector3.Distance(
        //    Player.transform.position,
        //    test.transform.position);

        //ui.text = distance.ToString();

        //if (distance > 5.0f)
        //{
        //    test.GetComponent<MyGizmo>().color = Color.green;
        //}
        //else
        //    test.GetComponent<MyGizmo>().color = Color.red;

    }
}
