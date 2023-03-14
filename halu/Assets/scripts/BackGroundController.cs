using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    // BackGround �� ���ִ� ���������� �ֻ��� ��ü(�θ�)
    private Transform parent;

    // Sprite�� �����ϰ� �ִ� �������
    private SpriteRenderer spriteRenderer;

    // �̹���
    private Sprite sprite;


    // ��������
    private float endPoint;

    // ���� ����
    private float exitPoint;

    // �̹��� �̵��ӵ�
    public float Speed;

    // �÷��̾� ����
    private GameObject player;
    private PlayerController playerController;

    // ������ ����
    private Vector3 movemane;

    // �̹����� �߾� ��ġ�� ���������� ����� �� �ֵ��� �ϱ� ���� ���濪Ȱ
    private Vector3 offset = new Vector3(0.0f, 7.5f, 0.0f);

    private void Awake()
    {
        // �÷��̾��� �⺻������ �޾ƿ´�.
        player = GameObject.Find("Player").gameObject;

        // �θ�ü�� �޾ƿ´�.
        parent = GameObject.Find("BackGround").transform;

        // �̹����� ����ִ� ������Ҹ� �޾ƿ´�.
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ** �÷��̾� �̹����� ����ִ� ������Ҹ� �޾ƿ´�.
        playerController = player.GetComponent<PlayerController>();

    }
    void Start()
    {
        // ������ҿ� ���Ե� �̹����� �޾ƿ´�.
        sprite = spriteRenderer.sprite;

        // ���������� ����
        endPoint = sprite.bounds.size.x * 0.5f + transform.position.x;

        // ���������� ����
        exitPoint = -(sprite.bounds.size.x * 0.5f) + player.transform.position.x;
    }

    void Update()
    {
        // �̵����� ����
        movemane = new Vector3(
            Input.GetAxisRaw("Horizontal") * Time.deltaTime * Speed + offset.x, // singleton
            0.0f,
            0.0f );

        // �÷��̾ �ٶ󺸰� �ִ� ���⿡ ���� �б��.

       // ** singleton
        if (ControllerManager.GetInstance().DirRight)
        {// ** �����̵�
            transform.position -= movemane;
            endPoint -= movemane.x;
        }

       
        // �̵����� ����
       
        

        // ������ �̹��� ����
        if(player.transform.position.x + (sprite.bounds.size.x * 0.5f) + 1 > endPoint)
        {
            // �̹����� �����Ѵ�.
            GameObject Obj = Instantiate(this.gameObject);

            // ������ �̹����� �θ� �����Ѵ�.
            Obj.transform.parent = parent.transform;

            // ������ �̹����� �̸��� �����Ѵ�.
            Obj.transform.name = transform.name;

            // ������ �̹����� ��ġ�� �����Ѵ�.
            Obj.transform.position = new Vector3(
                endPoint + 25.0f,
                0.0f,0.0f);

            // ���������� �����Ѵ�
            endPoint += endPoint +25.0f;
        }

        // ���������� �����ϸ� �����Ѵ�.
        if(transform.position.x + (sprite.bounds.size.x * 0.5f) - 2 <  exitPoint)
        {
            Destroy(this.gameObject);
        }
    }
}