using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager
{
    private ControllerManager() { }
    private static ControllerManager Instance = null;

    public static ControllerManager GetInstance()
    {
        if (Instance == null)
            Instance = new ControllerManager();
        return Instance;
    }

    public bool DirLeft;
    public bool DirRight;

    public float BulletSpeed = 10.0f;
    public int BulletDamage = 1;
    public int Player_HP = 100;
    public int Player_EXP = 0;

    public int HP = 3;
}
