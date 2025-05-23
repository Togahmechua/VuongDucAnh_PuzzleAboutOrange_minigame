using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LooseCanvas : UICanvas
{
    [Header("---Other Button---")]
    [SerializeField] private Button retryBtn;
    [SerializeField] private Button menuBtn;


    private bool isClick;

    private void OnEnable()
    {
        //AudioManager.Ins.PlaySFX(AudioManager.Ins.loose);
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }


    private void Start()
    {
        retryBtn.onClick.AddListener(() =>
        {
            //AudioManager.Ins.PlaySFX(AudioManager.Ins.click);

            UIManager.Ins.TransitionUI<ChangeUICanvas, LooseCanvas>(0.5f,
                () =>
                {
                    UIManager.Ins.OpenUI<MainCanvas>();
                    LevelManager.Ins.LoadMapByID(LevelManager.Ins.curMapID);
                });
        });

        menuBtn.onClick.AddListener(() =>
        {
            //AudioManager.Ins.PlaySFX(AudioManager.Ins.click);

            UIManager.Ins.TransitionUI<ChangeUICanvas, LooseCanvas>(0.5f,
                () =>
                {
                    LevelManager.Ins.DespawnMap();
                    UIManager.Ins.OpenUI<ChooseLevelCanvas>();
                });
        });
    }
}
