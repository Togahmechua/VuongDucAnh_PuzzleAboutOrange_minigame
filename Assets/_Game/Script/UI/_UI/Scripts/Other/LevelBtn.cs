using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelBtn : MonoBehaviour
{
    public int id;

    [Header("=====UIControl=====")]
    public Image img;
    public Sprite[] spr;
    public Text txt;
    public Button btn;
    public Image lockImg;

    [Header("=====Star=====")]
    public Image[] starImg;
    public Sprite[] starSpr;

    [SerializeField] private Animator anim;

    private void Start()
    {
        btn.onClick.AddListener(LoadLevel);
    }

    private void LoadLevel()
    {
        //AudioManager.Ins.PlaySFX(AudioManager.Ins.click);
        UIManager.Ins.TransitionUI<ChangeUICanvas, ChooseLevelCanvas>(0.6f,
           () =>
           {
               //LevelManager.Ins.LoadMapByID(id);
               UIManager.Ins.OpenUI<MainCanvas>();
           });
    }

    public void PlayAnim()
    {
        anim.Play(CacheString.TAG_LVBTN);
    }

    public void UpdateStarUI()
    {
        if (LevelManager.Ins == null || LevelManager.Ins.mapSO == null)
        {
            Debug.LogWarning("LevelManager or MapSO not set");
            return;
        }

        if (id >= 0 && id < LevelManager.Ins.mapSO.mapList.Count)
        {
            int star = LevelManager.Ins.mapSO.mapList[id].star;

            for (int i = 0; i < starImg.Length; i++)
            {
                starImg[i].gameObject.SetActive(true);

                if (i < star)
                {
                    starImg[i].sprite = starSpr[0];
                }
                else
                {
                    starImg[i].sprite = starSpr[1];
                }
            }
        }
        else
        {
            Debug.LogWarning("Level ID out of range: " + id);
        }
    }


    public void DisAbleStar()
    {

        for (int i = 0; i < starImg.Length; i++)
        {
            starImg[i].gameObject.SetActive(false);
        }
    }
}
