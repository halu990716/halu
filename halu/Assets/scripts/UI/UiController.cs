using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class UiController : MonoBehaviour
{
    public GameObject SkillCanvas;
    public GameObject TestCanvas;
    public bool SkillCanvasActive;

    private void Awake()
    {
        //SkillCanvas = GameObject.Find("SkillCanvas");
    }
    void Start()
    {
        //
        SkillCanvasActive = false;
        SkillCanvas.SetActive(SkillCanvasActive);
    }

    public void onSkillCanvasActive()
    {
        SkillCanvasActive = !SkillCanvasActive;
        SkillCanvas.SetActive(SkillCanvasActive);
        TestCanvas.SetActive(SkillCanvasActive);
    }

    public void onTitle()
    {
        SceneManager.LoadScene("Main");
    }
}
