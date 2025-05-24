using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinCanvas : UICanvas
{
    [Header("---Other Button---")]
    [SerializeField] private Button nextBtn;
    [SerializeField] private Button menuBtn;
    [SerializeField] private Button retryBtn;

    [Header("---Star Display---")]
    [SerializeField] private Image[] startImg;
    [SerializeField] private Sprite[] yellowAndBlackSpr;

    private bool isClick;

    private void OnEnable()
    {
        AudioManager.Ins.PlaySFX(AudioManager.Ins.win);
        UIManager.Ins.winCanvas = this;
        Time.timeScale = 0f;
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
    }


    private void Start()
    {
        nextBtn.onClick.AddListener(() =>
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.click);
            LevelManager.Ins.curMapID++;

            if (LevelManager.Ins.curMapID < LevelManager.Ins.mapSO.mapList.Count)
            {
                // Load the next level
                UIManager.Ins.TransitionUI<ChangeUICanvas, WinCanvas>(0.6f,
                () =>
                {
                    LevelManager.Ins.LoadMapByID(LevelManager.Ins.curMapID);
                    UIManager.Ins.CloseUI<WinCanvas>();
                    UIManager.Ins.OpenUI<MainCanvas>();
                });

            }
            else
            {
                // Reached the last level
                Debug.Log("All levels completed!");
                UIManager.Ins.TransitionUI<ChangeUICanvas, WinCanvas>(0.6f,
                () =>
                {
                    LevelManager.Ins.DespawnMap();
                    UIManager.Ins.CloseUI<WinCanvas>();
                    UIManager.Ins.CloseUI<MainCanvas>();
                    UIManager.Ins.OpenUI<ChooseLevelCanvas>();
                });
            }
        });

        menuBtn.onClick.AddListener(() =>
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.click);

            UIManager.Ins.TransitionUI<ChangeUICanvas, WinCanvas>(0.6f,
                () =>
                {
                    LevelManager.Ins.DespawnMap();
                    UIManager.Ins.OpenUI<ChooseLevelCanvas>();
                });


        });

        retryBtn.onClick.AddListener(() =>
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.click);
           
            UIManager.Ins.TransitionUI<ChangeUICanvas, WinCanvas>(0.6f,
                () =>
                {
                    UIManager.Ins.OpenUI<MainCanvas>();
                    LevelManager.Ins.LoadMapByID(LevelManager.Ins.curMapID);
                });
        });
    }

    public void DisplayStar(int curStar)
    {
        for (int i = 0; i < startImg.Length; i++)
        {
            startImg[i].sprite = i < curStar ? yellowAndBlackSpr[0] : yellowAndBlackSpr[1];
        }
    }
}