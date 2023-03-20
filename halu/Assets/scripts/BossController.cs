using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    const int STATE_WALK = 1;
    const int STATE_ATTACK = 2;
    const int STATE_SLIDE = 3;


    private GameObject Target;

    private Animator Anim;

    // ** �÷��̾��� SpriteRenderer ������Ҹ� �޾ƿ�������...
    private SpriteRenderer renderer;

    private Vector3 Movement;
    private Vector3 EndPoint;

    private float CoolDown;
    private float Speed;
    private int HP;
    private float Distance;
    private float slideSpeed;

    private float fTime;

    private bool slide;
    private bool Attack;
    private bool Walk;
    private bool active;
    private bool slideJump;

    private int choice;

    private void Awake()
    {
        Target = GameObject.Find("Player");

        Anim = GetComponent<Animator>();

        renderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        CoolDown = 0.5f;
        Speed = 1.0f;
        HP = 30000;
        slideSpeed = 6.0f;

        fTime = CoolDown;

        active = false;

        slide = false;
        slideJump = true;
        Attack = false;
        Walk = false;
    }

    void Update()
    {
        //float result = Target.transform.position.x - transform.position.x;

        //if (result < 0.0f)
        //{
        //    renderer.flipX = true;
        //}
        //else if (result > 0.0f)
        //    renderer.flipX = false;

        if (ControllerManager.GetInstance().DirRight)
            transform.position -= new Vector3(2.0f, 0.0f, 0.0f) * Time.deltaTime;

        Distance = Vector3.Distance(EndPoint, transform.position);

        fTime -= Time.deltaTime;

        if (fTime < 0.0f)
        {
            if (!active)
            {
                //StartCoroutine(onCooldown());
                active = true;
                choice = onController();
            }
            else
            {
                switch (choice)
                {
                    case STATE_WALK:
                        onWalk();
                        break;

                    case STATE_ATTACK:
                        onAttack();
                        break;

                    case STATE_SLIDE:
                        onSlide();
                        break;
                }
            }
            fTime = CoolDown;
        }
        if (Walk)
        {
            upWalk();
        }
        else if (slide) 
        {
            upSkillAttack();
            if (slideJump)
            {
                Anim.SetTrigger("slide Jump");
                slideJump = false;
                //print("if");

            }
        }
    }

    private int onController()
    {
        // ** �ൿ ���Ͽ� ���� ������ �߰� �մϴ�.

        {
            // ** �ʱ�ȭ
            if (Walk)
            {
                Movement = new Vector3(0.0f, 0.0f, 0.0f);
                Anim.SetFloat("Speed", Movement.x);
                Walk = false;

                bossXY();
            }

            if (slide)
            {
                Movement = new Vector3(0.0f, 0.0f, 0.0f);
                slide = false;
                slideJump = true;
                bossXY();

                Anim.SetTrigger("Idel");

            }

            if (Attack)
            {
                Attack = false;

            }
        }
        //

        // ** ���� �������� ���ϴ� ������ �÷��̾��� ��ġ�� ������������ ����
        EndPoint = Target.transform.position;

        //
        EndPoint = new Vector3(
            EndPoint.x,
            EndPoint.y + 1.0f,
            EndPoint.z);

        // ** [return]
        // ** 0 : ����            Attack
        // ** 1 : �̵�             walk
        // ** 2 : �����̵�      SkillAttack
        return Random.Range(STATE_WALK, STATE_SLIDE + 1);
    }

    private void bossXY()
    {
        float result = Target.transform.position.x - transform.position.x;

        if (result < 0.0f)
            renderer.flipX = true;
        else if (result > 0.0f)
            renderer.flipX = false;
    }

    private IEnumerator onCooldown()
    {
        float fTime = CoolDown;

        while(fTime > 0.0f)
        {
            fTime -= Time.deltaTime;
            yield return null;
        }
    }

    private void onCool()
    {
        float fTime = CoolDown;

        while (fTime > 0.0f)
        {
            fTime -= Time.deltaTime;
        }

        active = true;
    }

    private void onAttack()
    {
        //print("onAttack");

        Anim.SetTrigger("Attack");
        Attack = true;

        active = false;
    }

    private void onWalk()
    {
        //print("onWalk");
        Walk = true;
        active = false;
        bossXY();
    }

    private void onSlide()
    {
        //print("onSlide");
        slide = true;
        active = false;
        bossXY();
        //Attack = true;

    }

    private void upWalk()
    {
        float Distance = Vector3.Distance(EndPoint, transform.position);


        if (Distance > 1.5f)
        {
            Vector3 Direction = (EndPoint - transform.position).normalized;

            Movement = new Vector3(
                Speed * Direction.x,
                Speed * Direction.y,
                0.0f);

            transform.position += Movement * Time.deltaTime;

            Anim.SetFloat("Speed", Mathf.Abs(Movement.x));

            fTime = CoolDown;
        }
    }

    private void upSkillAttack()
    {
        float Distance = Vector3.Distance(EndPoint, transform.position);

        if (Distance > 0.5f)
        {
            Vector3 Direction = (EndPoint - transform.position).normalized;

            Movement = new Vector3(
                slideSpeed * Direction.x,
                slideSpeed * Direction.y,
                0.0f);

            transform.position += Movement * Time.deltaTime;

            //Anim.SetBool("slide", true);

            fTime = CoolDown;
        }
    }

    private void OnSlide()
    {
        Anim.SetTrigger("slide");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            //print("HIT");
            HP = HP - ControllerManager.GetInstance().BulletDamage;

            if (HP <= 0)
            {
                Destroy(gameObject, 0.016f);
            }
        }
    }
}

