using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float Speed;
    private int HP;
    private Animator Anim;
    private Vector3 Movement;
    public GameObject Player;


    private int SkillWaitingTime;
    private int AttackWaitingTime;
    private float AttackTimer;
    private float SkillTimer;

    private bool Run;

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }
    void Start()
    {
        Speed = 0.2f;
        Movement = new Vector3(1.0f, 0.0f, 0.0f);
        HP = 3;

        SkillWaitingTime = 10;
        AttackWaitingTime = 2;
        AttackTimer = 0.0f;
        SkillTimer = 0.0f;

        Run = true;

        Player = GameObject.Find("Player").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Run)
        {
            Movement = ControllerManager.GetInstance().DirRight ?
                new Vector3(Speed + 1.0f, 0.0f, 0.0f) : new Vector3(Speed, 0.0f, 0.0f);

            transform.position -= Movement * Time.deltaTime;
            Anim.SetFloat("Speed", Movement.x);

            attack();
        }
        //else
        //{
        //    Movement = new Vector3(2.0f, 0.0f, 0.0f);
        //    transform.position -= Movement * Time.deltaTime;
        //    Anim.SetFloat("Speed", Movement.x);
        //}
    }

    private void runOff()
    {
        Run = false;
    }
    private void runOn()
    {
        Run = true;
    }

    private void attack()
    {
        float x = Player.transform.position.x - transform.position.x;
        float y = Player.transform.position.y - transform.position.y;

        float distance = Mathf.Sqrt((x * x) + (y * y));

        SkillTimer += Time.deltaTime;
        AttackTimer += Time.deltaTime;

        if (distance < 1.0f)
        {
            if (AttackWaitingTime < AttackTimer)
            {
                Anim.SetTrigger("Attack");


                

                AttackTimer = 0.0f;
            }
        }

        else if (distance < 5.0f)
        {

            if (SkillWaitingTime < SkillTimer)
            {
                Anim.SetTrigger("Skill");
                SkillTimer = 0.0f;
            }
        }

       
        
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
