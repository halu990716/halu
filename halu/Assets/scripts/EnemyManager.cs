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

    // **
    private GameObject prefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // ** ���� ����Ǿ ��� ������ �� �ְ� ���ش�.
            DontDestroyOnLoad(gameObject);

            // ** �����Ǵ� Enemy�� ��Ƶ� ���� ��ü
            Parent = new GameObject("EnemyList");

            // ** Enemy�� ����� ���� ��ü
            prefab = Resources.Load("Prefabs/Enemy/Enemy") as GameObject;
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

            // ** Enemy �۵� ��ũ��Ʈ ����.
            Obj.AddComponent<EnemyController>();

            // ** Ŭ���� ��ġ�� �ʱ�ȭ.
            //Obj.transform.position = new Vector3(
            //    18.0f, Random.Range(-8.2f, -5.2f), 0.0f);

            Obj.transform.position = new Vector3(
                18.0f, -7.3f, 0.0f);

            // ** Ŭ���� �̸� �ʱ�ȭ
            Obj.transform.name = "Enemy";

            // ** Ŭ���� �������� ����.
            Obj.transform.parent = Parent.transform;


            // ** 1.5�� �޽�.
            yield return new WaitForSeconds(1.5f);
        }
    }
}
