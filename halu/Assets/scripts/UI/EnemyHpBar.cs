using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHpBar : MonoBehaviour
{
    // ** ����ٴ� ��ü
    public GameObject Target;
    private Slider HPbar;

    // ** ������ġ ����
    private Vector3 offset;

    private void Awake()
    {
        Target = GameObject.Find("Boss");
        HPbar = GetComponent<Slider>();
    }

    private void Start()
    {
        // ** ��ġ ����
        offset = new Vector3(0.0f, 1.0f, 0.0f);

        HPbar.value = ControllerManager.GetInstance().HP;
        HPbar.maxValue = HPbar.value;

    }
    private void Update()
    {
        // ** WorldToScreenPoint = ������ǥ�� ī�޶� ��ǥ�� ��ȯ.
        // ** ����� �ִ� Ÿ���� ��ǥ�� ī�޶� ��ǥ�� ��ȯ�Ͽ�. UI�� �����Ѵ�.
        transform.position = Camera.main.WorldToScreenPoint(Target.transform.position + offset);

        HPbar.value = ControllerManager.GetInstance().HP;

        if (HPbar.value == 0)
        {
            Destroy(this.gameObject, 0.016f);
        }
    }
}
