using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int id;
    public float distancePerGrid;
    [HideInInspector] public int curStar;

    [SerializeField] private Transform obstacleHolder;
    [SerializeField] private Transform pushAbleHolder;

    private List<GameObject> obstacleList = new List<GameObject>();
    private List<OrangePieceController> orangePieceList = new List<OrangePieceController>();

    private bool isWin;

    private void OnEnable()
    {
        SwipeDetection.CheckWin += CheckWinCondition;
    }

    private void OnDisable()
    {
        SwipeDetection.CheckWin -= CheckWinCondition;
    }

    private void Start()
    {
        LoadList();
    }

    private void Update()
    {
        if (!LevelManager.Ins.isWin) return;

        if (id == LevelManager.Ins.curMapID &&
            !LevelManager.Ins.mapSO.mapList[LevelManager.Ins.curMapID].isWon)
        {
            LevelManager.Ins.mapSO.mapList[LevelManager.Ins.curMapID].isWon = true;
            SaveWinState(LevelManager.Ins.curMapID);
            Debug.Log("Map " + LevelManager.Ins.curMapID + " is won.");
            LevelManager.Ins.curMap++;
        }

        SetCurMap();
    }


    private void SetCurMap()
    {
        PlayerPrefs.SetInt("CurrentMap", LevelManager.Ins.curMap);
        PlayerPrefs.Save();
    }

    private void SaveWinState(int mapIndex)
    {
        string key = "MapWin_" + mapIndex;
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
        LevelManager.Ins.mapSO.LoadWinStates();
    }

    #region CheckWinCondition

    private void CheckWinCondition()
    {
        if (CheckFormedOrange() && !isWin)
        {
            isWin = true;
            Debug.Log("Win!");

            StartCoroutine(IEWin());
        }
        else
        {
            Debug.Log("Wrong Pos!");
        }
    }

    private IEnumerator IEWin()
    {
        LevelManager.Ins.isWin = true;
        UIManager.Ins.mainCanvas.StopCounter();
        foreach (OrangePieceController or in orangePieceList)
        {
            if (or != null)
            {
                SwipeDetection sw = or.GetComponent<SwipeDetection>();
                if (sw != null)
                {
                    sw.enabled = false;
                }
            }
        }

        yield return new WaitForSeconds(1f);

        UIManager.Ins.CloseUI<MainCanvas>();
        UIManager.Ins.OpenUI<WinCanvas>().DisplayStar(curStar);
    }

    public bool CheckFormedOrange()
    {
        if (orangePieceList.Count != 4) return false;

        Dictionary<int, Vector2> positions = new Dictionary<int, Vector2>();

        foreach (var p in orangePieceList)
        {
            Vector2 pos = new Vector2(p.transform.position.x, p.transform.position.y);
            positions[p.pieceID] = pos;
        }

        if (!positions.ContainsKey(1) || !positions.ContainsKey(2) ||
            !positions.ContainsKey(3) || !positions.ContainsKey(4))
            return false;

        Vector2 pos1 = positions[1];
        Vector2 pos2 = positions[2];
        Vector2 pos3 = positions[3];
        Vector2 pos4 = positions[4];

        float d = distancePerGrid;

        bool isOrangeFormed =
            AreApproximatelyEqual(pos2, pos1 + Vector2.right * d) &&
            AreApproximatelyEqual(pos3, pos1 + Vector2.down * d) &&
            AreApproximatelyEqual(pos4, pos1 + (Vector2.right + Vector2.down) * d);

        return isOrangeFormed;
    }

    private bool AreApproximatelyEqual(Vector2 a, Vector2 b, float tolerance = 0.01f)
    {
        return Mathf.Abs(a.x - b.x) < tolerance && Mathf.Abs(a.y - b.y) < tolerance;
    }



    #endregion

    #region GetList
    public List<GameObject> GameObjList() => obstacleList;
    public List<OrangePieceController> OrangePieceGameObjList() => orangePieceList;

    public void LoadList()
    {
        obstacleList.Clear();
        orangePieceList.Clear();

        LoadObjectsFromHolder(obstacleHolder, obstacleList);
        LoadComponentsFromHolder(pushAbleHolder, orangePieceList);
    }

    private void LoadObjectsFromHolder(Transform holder, List<GameObject> list)
    {
        for (int i = 0; i < holder.childCount; i++)
        {
            GameObject obj = holder.GetChild(i).gameObject;
            if (obj != null)
            {
                list.Add(obj);
            }
        }
    }
    private void LoadComponentsFromHolder<T>(Transform holder, List<T> list) where T : Component
    {
        for (int i = 0; i < holder.childCount; i++)
        {
            T component = holder.GetChild(i).GetComponent<T>();
            if (component != null)
            {
                list.Add(component);
            }
        }
    }
    #endregion
}
