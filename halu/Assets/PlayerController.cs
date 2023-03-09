using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ** �����̴� �ӵ�
    private float Speed;

    // �������� �����ϴ� ����
    private Vector3 Movement;

    // �÷��̾��� Animator ������Ҹ� �޾ƿ��� ����...
    private Animator animator;

    // �÷��̾��� SpriteRenderer ������Ҹ� �޾ƿ��� ����...
    private SpriteRenderer spriteRenderer;
    //private SpriteRenderer spriteRendererBullet;

    // [����üũ]
    private bool onAttack; // ���ݻ���
    private bool onHit; // �ǰݻ���
    private bool onJump;
    private bool onclimbing;
    private bool ondaegurrrr;

    // ������ �Ѿ� ����
    public GameObject BulletPrefab;
    public GameObject BulletPrefabBack;

    // ������ 
    public GameObject fxPrefab;



    public GameObject[] stageBack = new GameObject[7];

    // ������ �Ѿ��� �������.
    private List<GameObject> Bullets = new List<GameObject>();

    // �÷��̾ ���������� �ٶ� ����.
    private float Direction;

    private void Awake()
    {
        //  player �� Animator�� �޾ƿ´�.
        animator = this.GetComponent<Animator>();

        // player �� spriteRenderer �޾ƿ´�.
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    //  ����Ƽ �⺻ ���� �Լ�
    //  �ʱⰪ�� ������ �� ���
    void Start()
    {
        //  �ӵ��� �ʱ�ȭ.
        Speed = 5.0f;

        //spriteRendererBullet = BulletPrefab.GetComponent<SpriteRenderer>();

        //bulletPrefabBack = Bullet.GetComponent<SpriteRenderer>();

        // �ʱⰪ ����
        onAttack = false;

        onHit = false;

        Direction = 1.0f;

        for (int i = 0; i < 7; ++i)
            stageBack[i] = GameObject.Find(i.ToString());
    }

    //  ����Ƽ �⺻ ���� �Լ�
    //  �����Ӹ��� �ݺ������� ����Ǵ� �Լ�.
    void Update()
    {
        //  [�Ǽ� ���� IEEE754]

        // **  Input.GetAxis =     -1 ~ 1 ������ ���� ��ȯ��. 
        float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 ���߿� �ϳ��� ��ȯ.
        float Ver = Input.GetAxis("Vertical"); // -1 ~ 1 ���� �Ǽ��� ��ȯ.

        // Hor�� 0�̶�� �����ִ� �����̹Ƿ� ����ó���� ���ش�.
        if (Hor != 0)
            Direction = Hor;

        // �÷��̾ �ٶ󺸰� �ִ� ���⿡ ���� �̹��� �ø� ����.
        if(Direction < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (Direction > 0)
            spriteRenderer.flipX = false;

        // �Է¹��� ������ �÷��̾ �����δ�.
        Movement = new Vector3(
            Hor * Time.deltaTime * Speed,
            0.0f,
            0.0f);

        
        // ���� ��Ʈ��Ű�� �Է��Ѵٸ�....
        if (Input.GetKey(KeyCode.LeftControl))
            // ����
            OnAttack();

        // ** ���� ����ƮŰ�� �Է��Ѵٸ�.....
        if (Input.GetKey(KeyCode.LeftShift))
            // ** �ǰ�
            OnHit();

        // �����̽��ٸ� �Է��Ѵٸ�....
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // �Ѿ˿����� �����Ѵ�.
            GameObject Obj = Instantiate(BulletPrefab);

            //Obj.transform.name = "";
            //������ �Ѿ��� ��ġ�� ���� �÷��̾��� ��ġ�� �ʱ�ȭ�Ѵ�.
            Obj.transform.position = transform.position;

            //�Ѿ��� BulletController ��ũ��Ʈ�� �޾ƿ´�
            BulletController Controller = Obj.AddComponent<BulletController>();

            // �Ѿ� ��ũ��Ʈ������ ���� ������ ���� �÷��̾��� ���� ������ ���� �Ѵ�.
            Controller.Direction = new Vector3(Direction, 0.0f, 0.0f);

            //�Ѿ�
            Controller.fxPrefab = fxPrefab;

            // �Ѿ��� spriteRenderer�� �޾ƿ´�.
            SpriteRenderer renderer = Obj.GetComponent<SpriteRenderer>();

            // �Ѿ��� �̹��� ���� ���¸� �÷��̾��� �̹��� ���� ���·� �����Ѵ�.
            renderer.flipY = spriteRenderer.flipX;

            //spriteRendererBullet.flipX = (Hor < 0) ? true : false;
           // Obj.transform.rotation.z = 90;

            // ��� ������ ����Ǿ��ٸ� ����ҿ� �����Ѵ�.
            Bullets.Add(Obj);   

        }
           // OnJump();
           
        if (Input.GetKey(KeyCode.UpArrow))
            Onclimbing();

        if (Input.GetKey(KeyCode.LeftAlt))
            Ondaegurrrr();

        /*
        if (Input.GetKey(KeyCode.Q))
            OnIdel();
        */
        // �÷����� �����ӿ� ���� �̵� ����� ���� �Ѵ�.
        animator.SetFloat("Speed", Hor);

        // ���� �÷��̾ �����δ�.
        transform.position += Movement;
    }


    private void OnAttack()
    {
        // �̹� ���ݸ���� �������̶��
        if (onAttack)
            // �Լ��� �����Ų��.
            return;

        // �Լ��� ������� �ʾҴٸ�...
        // ���ݻ��¸� Ȱ��ȭ �ϰ�.
        onAttack = true;
        
        // ���ݸ���� ���� ��Ų��.
        animator.SetTrigger("Attack");
    }

    private void SetAttack()
    {
        // �Լ��� ����Ǹ� ���ݸ���� ��Ȱ��ȭ �ȴ�.
        // �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
        onAttack = false;
    }

    private void OnHit()
    {
        // �̹� �ǰݸ���� �������̶��
        if (onHit)
            // �Լ��� �����Ų��.
            return;

        // �Լ��� ������� �ʾҴٸ�...
        // �ǰݻ��¸� Ȱ��ȭ �ϰ�.
        onHit = true;

        // �ǰݸ���� ���� ��Ų��.
        animator.SetTrigger("Hit");
    }

    private void SetHit()
    {
        // �Լ��� ����Ǹ� �ǰݸ���� ��Ȱ��ȭ �ȴ�.
        // �Լ��� �ִϸ��̼� Ŭ���� �̺�Ʈ ���������� ���Ե�.
        onHit = false;
    }

    private void OnJump()
    {
        if (onJump)
            return;

        onJump = true;
        animator.SetTrigger("Jump");
    }

    private void SetJump()
    {
        onJump = false;
    }

    private void Onclimbing()
    {
        if (onclimbing)
            return;

        onclimbing = true;
        animator.SetTrigger("climbing");
        onJump = false;
    }

    private void Setclimbing()
    {
        onclimbing = false;
    }

    private void Ondaegurrrr()
    {
        if (ondaegurrrr)
            return;

        ondaegurrrr = true;
        animator.SetTrigger("daegurrrr");
    }

    private void Setdaegurrrr()
    {
        ondaegurrrr = false;
    }

    /*
    private void OnIdel()
    {
        if (onHit)
            return;
        onIdel = true;
        onHit = false;
        animator.SetTrigger("Idel");
    }
    */
}