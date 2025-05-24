using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelCanvas : UICanvas
{
    [SerializeField] private SpawnLevel spawnLevel;
    [SerializeField] private Button backBtn;

    private bool flag;

    private void Awake()
    {
        StartCoroutine(Wait());
    }

    private void OnEnable()
    {
        if (!flag)
            return;
        spawnLevel.Check();
    }

    private void Start()
    {
        backBtn.onClick.AddListener(() =>
        {
            AudioManager.Ins.PlaySFX(AudioManager.Ins.click);
            UIManager.Ins.TransitionUI<ChangeUICanvas, ChooseLevelCanvas>(0.5f,
                () =>
                {
                    UIManager.Ins.OpenUI<StartCanvas>();
                });
        });
    }

    private IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
        spawnLevel.Check();
        flag = true;
    }
}
