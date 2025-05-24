using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] private Text txt;

    private bool isCounting = true;
    private Coroutine countdownCoroutine;
    private int currentTime;

    private void OnEnable()
    {
        isCounting = true;
        countdownCoroutine = StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        int countdownTime = 45;
        currentTime = countdownTime;

        while (currentTime >= 0 && isCounting)
        {
            txt.text = $"00 : {currentTime:00}";
            yield return new WaitForSeconds(1f);
            currentTime--;
        }

        if (currentTime < 0 && isCounting)
        {
            LevelManager.Ins.timesUp = true;
            UIManager.Ins.CloseUI<MainCanvas>();
            UIManager.Ins.OpenUI<LooseCanvas>();
        }
    }

    public void StopCounter()
    {
        isCounting = false;

        if (countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
        }

        int star = ReturnStar();
        LevelManager.Ins.level.curStar = star;

        SaveStar(LevelManager.Ins.curMapID, star);
    }

    private void SaveStar(int levelID, int star)
    {
        string key = "Star_" + levelID;

        int oldStar = PlayerPrefs.GetInt(key, 0);
        Debug.Log("OLd Star : " + oldStar);
        if (star > oldStar)
        {
            Debug.Log("A");
            PlayerPrefs.SetInt(key, star);
            PlayerPrefs.Save();
        }

        LevelManager.Ins.mapSO.LoadStar();
    }

    public int ReturnStar()
    {
        if (currentTime > 30)
            return 3;
        else if (currentTime > 10)
            return 2;
        else
            return 1;
    }
}
