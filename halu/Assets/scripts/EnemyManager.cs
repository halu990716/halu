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

    private GameObject prefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            // ** 씬이 변경되어도 계속 유지될 수 있게 해준다.
            DontDestroyOnLoad(gameObject);

            prefab = Resources.Load("Prefabs/Enemy/Enemy") as GameObject;
        }
    }

    private IEnumerator Start()
    {
        while (true)
        {
            GameObject Obj = Instantiate(prefab);
            Obj.transform.position = new Vector3(
                18.0f, Random.Range(-8.2f, -5.2f), 0.0f);

            Obj.transform.name = "TEST";

            yield return new WaitForSeconds(1.5f);
        }
    }
}
