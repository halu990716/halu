using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ** 움직이는 속도
    private float Speed;

    // 움직임을 저장하는 벡터
    private Vector3 Movement;

    // 플레이어의 Animator 구성요소를 받아오기 위해...
    private Animator animator;

    // 플레이어의 SpriteRenderer 구성요소를 받아오기 위해...
    private SpriteRenderer spriteRenderer;
    //private SpriteRenderer spriteRendererBullet;

    // [상태체크]
    private bool onAttack; // 공격상태
    private bool onHit; // 피격상태
    private bool onJump;
    private bool onclimbing;
    private bool ondaegurrrr;

    // 복사할 총알 원본
    public GameObject BulletPrefab;
    public GameObject BulletPrefabBack;

    // 복제할 
    public GameObject fxPrefab;



    public GameObject[] stageBack = new GameObject[7];

    // 복제된 총알의 저장공간.
    private List<GameObject> Bullets = new List<GameObject>();

    // 플레이어가 마지막으로 바라본 방향.
    private float Direction;

    private void Awake()
    {
        //  player 의 Animator를 받아온다.
        animator = this.GetComponent<Animator>();

        // player 의 spriteRenderer 받아온다.
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }

    //  유니티 기본 제공 함수
    //  초기값을 설정할 때 사용
    void Start()
    {
        //  속도를 초기화.
        Speed = 5.0f;

        //spriteRendererBullet = BulletPrefab.GetComponent<SpriteRenderer>();

        //bulletPrefabBack = Bullet.GetComponent<SpriteRenderer>();

        // 초기값 설정
        onAttack = false;

        onHit = false;

        Direction = 1.0f;

        for (int i = 0; i < 7; ++i)
            stageBack[i] = GameObject.Find(i.ToString());
    }

    //  유니티 기본 제공 함수
    //  프레임마다 반복적으로 실행되는 함수.
    void Update()
    {
        //  [실수 연산 IEEE754]

        // **  Input.GetAxis =     -1 ~ 1 사이의 값을 반환함. 
        float Hor = Input.GetAxisRaw("Horizontal"); // -1 or 0 or 1 셋중에 하나를 반환.
        float Ver = Input.GetAxis("Vertical"); // -1 ~ 1 까지 실수로 반환.

        // Hor이 0이라면 멈춰있는 상태이므로 예외처리를 해준다.
        if (Hor != 0)
            Direction = Hor;

        // 플레이어가 바라보고 있는 방향에 따라 이미지 플립 설정.
        if(Direction < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (Direction > 0)
            spriteRenderer.flipX = false;

        // 입력받은 값으로 플레이어를 움직인다.
        Movement = new Vector3(
            Hor * Time.deltaTime * Speed,
            0.0f,
            0.0f);

        
        // 좌측 컨트롤키를 입력한다면....
        if (Input.GetKey(KeyCode.LeftControl))
            // 공격
            OnAttack();

        // ** 좌측 시프트키를 입력한다면.....
        if (Input.GetKey(KeyCode.LeftShift))
            // ** 피격
            OnHit();

        // 스페이스바를 입력한다면....
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // 총알원본을 복제한다.
            GameObject Obj = Instantiate(BulletPrefab);

            //Obj.transform.name = "";
            //복제된 총알의 위치를 현재 플레이어의 위치로 초기화한다.
            Obj.transform.position = transform.position;

            //총알의 BulletController 스크립트를 받아온다
            BulletController Controller = Obj.AddComponent<BulletController>();

            // 총알 스크립트내부의 방향 변수를 현재 플레이어의 방향 변수로 설정 한다.
            Controller.Direction = new Vector3(Direction, 0.0f, 0.0f);

            //총알
            Controller.fxPrefab = fxPrefab;

            // 총알의 spriteRenderer를 받아온다.
            SpriteRenderer renderer = Obj.GetComponent<SpriteRenderer>();

            // 총알의 이미지 반전 상태를 플레이어의 이미지 반전 상태로 설정한다.
            renderer.flipY = spriteRenderer.flipX;

            //spriteRendererBullet.flipX = (Hor < 0) ? true : false;
           // Obj.transform.rotation.z = 90;

            // 모든 설정이 종료되었다면 저장소에 보관한다.
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
        // 플레이의 움직임에 따라 이동 모션을 실행 한다.
        animator.SetFloat("Speed", Hor);

        // 실제 플레이어를 움직인다.
        transform.position += Movement;
    }


    private void OnAttack()
    {
        // 이미 공격모션이 진행중이라면
        if (onAttack)
            // 함수를 종료시킨다.
            return;

        // 함수가 종료되지 않았다면...
        // 공격상태를 활성화 하고.
        onAttack = true;
        
        // 공격모션을 실행 시킨다.
        animator.SetTrigger("Attack");
    }

    private void SetAttack()
    {
        // 함수가 실행되면 공격모션이 비활성화 된다.
        // 함수는 애니메이션 클립의 이벤트 프레임으로 삽입됨.
        onAttack = false;
    }

    private void OnHit()
    {
        // 이미 피격모션이 진행중이라면
        if (onHit)
            // 함수를 종료시킨다.
            return;

        // 함수가 종료되지 않았다면...
        // 피격상태를 활성화 하고.
        onHit = true;

        // 피격모션을 실행 시킨다.
        animator.SetTrigger("Hit");
    }

    private void SetHit()
    {
        // 함수가 실행되면 피격모션이 비활성화 된다.
        // 함수는 애니메이션 클립의 이벤트 프레임으로 삽입됨.
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