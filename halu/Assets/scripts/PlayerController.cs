using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ** �����̴� �ӵ�
    private float Speed;

    [HideInInspector]public int HP;
    private float Cool;


    // �������� �����ϴ� ����
    private Vector3 Movement;

    // �÷��̾��� Animator ������Ҹ� �޾ƿ��� ����...
    private Animator animator;

    // �÷��̾��� SpriteRenderer ������Ҹ� �޾ƿ��� ����...
    private SpriteRenderer playerRenderer;
 

    // [����üũ]
    private bool onAttack; // ���ݻ���
    private bool onHit; // �ǰݻ���
    private bool onJump;
    private bool onclimbing;
    private bool ondaegurrrr;

   

    // ������ �Ѿ� ����
    private GameObject BulletPrefab;
    public GameObject BulletPrefabBack;

    // ������ 
    private GameObject fxPrefab;

    // ������ ��׶��� ����
    private GameObject backGround;


    //���� list�� ����
    //public GameObject[] stageBack = new GameObject[7];

    // ������ ��׶����� �������.
    public List<GameObject> stageBack = new List<GameObject>();

    //Dictionary<string, object>;
    //Dictionary<string, GameObject>;

    // ������ �Ѿ��� �������.
    private List<GameObject> Bullets = new List<GameObject>();


    // �÷��̾ ���������� �ٶ� ����.
    private float Direction;


    [Header("����")]
    // ** �÷��̾ �ٶ󺸴� ����

    [Tooltip("����")]
    public bool DirLeft;
    [Tooltip("������")]
    public bool DirRight;

    // �÷��̾ �ٶ󺸴� ����
    public bool Dir;
    //public bool DirLeft;
    //public bool DirRight;

    //void OnKeyUp

    private void Awake()
    {
        //  player �� Animator�� �޾ƿ´�.
        animator = this.GetComponent<Animator>();

        // player �� spriteRenderer �޾ƿ´�.
        playerRenderer = this.GetComponent<SpriteRenderer>();

        BulletPrefab = Resources.Load("Prefabs/Bullet") as GameObject;
        //fxPrefab = Resources.Load("Prefabs/FX/Smoke") as GameObject;
        fxPrefab = Resources.Load("Prefabs/FX/Hit") as GameObject;
        // ��׶���
        //backGround = Resources.Load("Prefabs/BackGround") as GameObject;
    }   

    //  ����Ƽ �⺻ ���� �Լ�
    //  �ʱⰪ�� ������ �� ���
    void Start()
    {
        //EnemyManager.Getinstance;

        //  �ӵ��� �ʱ�ȭ.
        Speed = 2.0f;

        Cool = 1.0f;

        // �ʱⰪ ����
        onAttack = false;

        onHit = false;

        Direction = 1.0f;

        DirLeft = false;
        DirRight = false;

        //for (int i = 0; i < 7; ++i)
        //    stageBack[i] = GameObject.Find(i.ToString());
    }

    //  ����Ƽ �⺻ ���� �Լ�
    //  �����Ӹ��� �ݺ������� ����Ǵ� �Լ�.
    void Update()
    {
        HP = ControllerManager.GetInstance().PlayerHP;
        //print(HP);

        Cool -= Time.deltaTime;
        //  [�Ǽ� ���� IEEE754]

        // **  Input.GetAxis =     -1 ~ 1 ������ ���� ��ȯ��. 
        float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 ���߿� �ϳ��� ��ȯ.
        float Ver = Input.GetAxisRaw("Vertical"); // -1 or 0 or 1 ���߿� �ϳ��� ��ȯ.

        // �Է¹��� ������ �÷��̾ �����δ�.
        Movement = new Vector3(
            Hor * Time.deltaTime * Speed,
            Ver * Time.deltaTime * Speed * 0.5f,
            0.0f);

        transform.position += new Vector3(0.0f, Movement.y, 0.0f);

        // Hor�� 0�̶�� �����ִ� �����̹Ƿ� ����ó���� ���ش�.
        if (Hor != 0)
            Direction = Hor;

        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            // ** �÷��̾� ��ǥ�� 0.1 ���� ������ �÷��̾ �����δ�.
            if (transform.position.x < 0.1f)
                transform.position += Movement;
            else
            {
                ControllerManager.GetInstance().DirRight = true;
                ControllerManager.GetInstance().DirLeft = false;
            }

        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            ControllerManager.GetInstance().DirRight = false;
            ControllerManager.GetInstance().DirLeft = true;

            // ** �÷��̾� ��ǥ�� -15.0 ���� Ŭ��...
            if (transform.position.x > -15)
             // ���� �÷��̾ �����δ�.
             transform.position += Movement;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            ControllerManager.GetInstance().DirRight = false;
            ControllerManager.GetInstance().DirLeft = false;

        }



        // �÷��̾ �ٶ󺸰� �ִ� ���⿡ ���� �̹��� �ø� ����.
        if (Direction < 0)
        {
            playerRenderer.flipX = DirLeft = true;
        }
        else if (Direction > 0)
        {
            playerRenderer.flipX = false;
            DirRight = true;
        }
        
        // ���� ��Ʈ��Ű�� �Է��Ѵٸ�....
        if (Input.GetKey(KeyCode.LeftControl))
            // ����
            OnAttack();

        // ** ���� ����ƮŰ�� �Է��Ѵٸ�.....
        if (Input.GetKey(KeyCode.LeftShift))
            // ** �ǰ�
            OnHit();

        // �����̽��ٸ� �Է��Ѵٸ�....
        if (Cool < 0)
        {
            Cool = 1.0f;
            // ����
            OnAttack();

            // �Ѿ˿����� �����Ѵ�.
            GameObject Obj = Instantiate(BulletPrefab);

            //Obj.transform.name = "";
            //������ �Ѿ��� ��ġ�� ���� �÷��̾��� ��ġ�� �ʱ�ȭ�Ѵ�.
            Obj.transform.position = transform.position;

            //�Ѿ��� BulletController ��ũ��Ʈ�� �޾ƿ´�
            BulletController Controller = Obj.AddComponent<BulletController>();

            // �Ѿ� ��ũ��Ʈ������ ���� ������ ���� �÷��̾��� ���� ������ ���� �Ѵ�.
            Controller.Direction = new Vector3(Direction, 0.0f, 0.0f);

            //�Ѿ� ��ũ��Ʈ������ FX Prefab�� �����Ѵ�.
            Controller.fxPrefab = fxPrefab;

            // �Ѿ��� spriteRenderer�� �޾ƿ´�.
            SpriteRenderer renderer = Obj.GetComponent<SpriteRenderer>();

            // �Ѿ��� �̹��� ���� ���¸� �÷��̾��� �̹��� ���� ���·� �����Ѵ�.
            renderer.flipY = playerRenderer.flipX;

            //spriteRendererBullet.flipX = (Hor < 0) ? true : false;
           // Obj.transform.rotation.z = 90;

            // ��� ������ ����Ǿ��ٸ� ����ҿ� �����Ѵ�.
            Bullets.Add(Obj);

            //stageBack.Add(Obj);
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
        //transform.position += Movement;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
            --HP;

        if (HP == 0)
            print("DIE");
    }
}