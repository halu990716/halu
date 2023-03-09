using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    // 총알이 날라가는 속도
    private float Speed;

    private int hp;

    public GameObject fxPrefab;

    //private SpriteRenderer spriteRenderer;

    // 총알이 날아가야할 방향
    public Vector3 Direction { get; set; }

   
    private void Start()
    {
        //spriteRenderer = this.GetComponent<SpriteRenderer>();
        // 속도 초기값
        Speed = 10.0f;

        hp = 3;
    }

    void Update()
    {

        float Hor = Input.GetAxisRaw("Horizontal");
           // 방향으로 속도만큼 위치를 변경
        transform.position += Direction * Speed * Time.deltaTime;
        //spriteRenderer.flipX = (Hor < 0) ? true : false;
    }

    // 충동체와 물리엔진이 포함된 오브젝트가 다른 충돌체와 충돌한다면 실행되는 함수.
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
