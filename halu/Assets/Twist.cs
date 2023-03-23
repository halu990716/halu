using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twist : MonoBehaviour
{
    public float Angle;
    public float Speed;

    private GameObject[] Bullet = new GameObject[2];
    private void Start()
    {
        GameObject ParentObj = new GameObject("Twist");


        Angle = 0.0f;
        Speed = 5.0f;

        Bullet[0] = new GameObject("TwistBullet");

        Bullet[0].AddComponent<MyGizmo>();

        Bullet[0].transform.position = new Vector3(
            transform.position.x,
            transform.position.y + 2.5f,
            0.0f);


        Bullet[1] = new GameObject("TwistBullet");

        Bullet[1].AddComponent<MyGizmo>();

        Bullet[1].transform.position = new Vector3(
            transform.position.x,
            transform.position.y - 2.5f,
            0.0f);
    }

    void Update()
    {
        Angle += 0.5f;

        Bullet[0].transform.position = new Vector3(
            1.0f,
            Mathf.Sin(Angle * Mathf.Deg2Rad), 0.0f);

        Bullet[1].transform.position = new Vector3(
            Mathf.Sin(-Angle * Mathf.Deg2Rad), 1.0f, 0.0f);

    }
}
