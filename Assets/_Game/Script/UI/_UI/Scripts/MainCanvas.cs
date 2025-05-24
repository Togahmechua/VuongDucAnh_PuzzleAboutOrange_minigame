using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvas : UICanvas
{
    [SerializeField] private Button homeBtn;
    [SerializeField] private Button retryBtn;
    [SerializeField] private TimeCounter clock;

    private void OnEnable()
    {
        UIManager.Ins.mainCanvas = this;
    }

    private void Start()
    {
        homeBtn.onClick.AddListener(() =>
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.click);
            UIManager.Ins.TransitionUI<ChangeUICanvas, MainCanvas>(0.5f,
                () =>
                {
                    UIManager.Ins.OpenUI<ChooseLevelCanvas>();
                });
            
        });

        retryBtn.onClick.AddListener(() =>
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.click);
            UIManager.Ins.TransitionUI<ChangeUICanvas, MainCanvas>(0.5f,
                () =>
                {
                    UIManager.Ins.OpenUI<MainCanvas>();
                    LevelManager.Ins.LoadMapByID(LevelManager.Ins.curMapID);
                });
        });
    }

    public void StopCounter()
    {
        clock.StopCounter();
    }
}
