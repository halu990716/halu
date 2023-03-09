using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // �Ѿ��� ���󰡴� �ӵ�
    private float Speed;

    private int hp;

    public GameObject fxPrefab;

    //private SpriteRenderer spriteRenderer;

    // �Ѿ��� ���ư����� ����
    public Vector3 Direction { get; set; }

   
    private void Start()
    {
        //spriteRenderer = this.GetComponent<SpriteRenderer>();
        // �ӵ� �ʱⰪ
        Speed = 10.0f;

        hp = 3;
    }

    void Update()
    {

        float Hor = Input.GetAxisRaw("Horizontal");
           // �������� �ӵ���ŭ ��ġ�� ����
        transform.position += Direction * Speed * Time.deltaTime;
        //spriteRenderer.flipX = (Hor < 0) ? true : false;
    }

    // �浿ü�� ���������� ���Ե� ������Ʈ�� �ٸ� �浹ü�� �浹�Ѵٸ� ����Ǵ� �Լ�.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //DestroyObject(this.gameObject);
        --hp;

        GameObject Obj = Instantiate(fxPrefab);

        GameObject camera = new GameObject("Camera Test");
        camera.AddComponent<CameraController>();

        Obj.transform.position = transform.position;

        DestroyObject(collision.transform.gameObject);

        if (hp == 0)
            DestroyObject(this.gameObject);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Debug.Log("BBBBB");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
       // print("CCCCCC");
    }
}
