using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float Speed;
    private int HP;
    private Animator Anim;
    private Vector3 Movement;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }
    void Start()
    {
        Speed = 0.2f;
        Movement = new Vector3(1.0f, 0.0f, 0.0f);
        HP = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Movement = ControllerManager.GetInstance().DirRight ? 
            new Vector3(Speed + 1.0f,0.0f,0.0f) : new Vector3(Speed, 0.0f, 0.0f);
  
        transform.position -= Movement * Time.deltaTime;
        Anim.SetFloat("Speed", Movement.x);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            --HP;

            if (HP <= 0)
            {
                Anim.SetTrigger("Die");
                GetComponent<CapsuleCollider2D>().enabled = false;
            }
        }
    }

    private void ReleaseEnemy()
    {
        Destroy(gameObject, 0.016f);
    }
}
