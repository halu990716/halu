    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private EnemyManager() { }
    private static EnemyManager instance = null;

    public static EnemyManager GetInstance
    {
        get
        {
            if (instance == null)
                return null;
            return instance;
        }
    }

    // ** Enemy�� �θ� ��ü
    private GameObject Parent;

    // ** ������
    private GameObject prefab;
    private GameObject HPprefab;

    // ** �÷��̾��� ���� �̵� �Ÿ�
    public float Distance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            Distance = 0.0f;

            // ** ���� ����Ǿ ��� ������ �� �ְ� ���ش�.
            DontDestroyOnLoad(gameObject);

            // ** �����Ǵ� Enemy�� ��Ƶ� ���� ��ü
            Parent = new GameObject("EnemyList");

            // ** Enemy�� ����� ���� ��ü
            prefab = Resources.Load("Prefabs/Enemy/Enemy") as GameObject;
            HPprefab = Resources.Load("Prefabs/HP") as GameObject;
        }
    }
    // ** �������ڸ��� Start�Լ��� �ڷ�ƾ �Լ��� ����
    private IEnumerator Start()
    {
        // ** 
        while (true)
        {
            // ** Enemy ������ü�� �����Ѵ�.
            GameObject Obj = Instantiate(prefab);

            // ** Enemy HP UI ����.
            GameObject Bar = Instantiate(HPprefab);

            // ** ������ UI�� ĵ������ ��ġ��Ų��. 2���� ���
            //Bar.transform.parent = GameObject.Find("EnemyHPCanvas").transform;
            Bar.transform.SetParent(GameObject.Find("EnemyHPCanvas").transform);

            // ** Enemy �۵� ��ũ��Ʈ ����.
            //Obj.AddComponent<EnemyController>();

            // ** Ŭ���� ��ġ�� �ʱ�ȭ.
            //Obj.transform.position = new Vector3(
            //    18.0f, Random.Range(-8.2f, -5.2f), 0.0f);

            Obj.transform.position = new Vector3(
                18.0f, -7.3f, 0.0f);


            // ** Ŭ���� �̸� �ʱ�ȭ
            Obj.transform.name = "Enemy";

            // ** Ŭ���� �������� ����.
            Obj.transform.parent = Parent.transform;

            // ** UI ��ü�� ����ִ� ��ũ��Ʈ�� ����.
            EnemyHpBar EnemhpBar = Bar.GetComponent<EnemyHpBar>();

            // ** ��ũ��Ʈ�� Target �� ���� ������ Enemy�� ����.
            EnemhpBar.Target = Obj;

            // ** 1.5�� �޽�.
            yield return new WaitForSeconds(1.5f);
        }
    }

    private void Update()
    {
        if(ControllerManager.GetInstance().DirRight)
        {
            Distance += Input.GetAxisRaw("Horizontal") * Time.deltaTime * 2;
        }
    }
}
