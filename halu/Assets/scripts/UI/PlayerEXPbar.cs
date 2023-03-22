using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerEXPbar : MonoBehaviour
{
    private Slider EXPbar;
    private void Awake()
    {
        EXPbar = GetComponent<Slider>();
    }

    void Start()
    {
        EXPbar.value = 0;
        EXPbar.maxValue = 100;
    }


    void Update()
    {
        EXPbar.value = ControllerManager.GetInstance().Player_EXP;
    }
}
