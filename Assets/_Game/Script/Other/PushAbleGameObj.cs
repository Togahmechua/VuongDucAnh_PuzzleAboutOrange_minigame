using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushAbleGameObj : MonoBehaviour
{
    [SerializeField] private List<GameObject> obstacleList = new List<GameObject>();
    [SerializeField] private List<PushAbleGameObj> pushAbleList = new List<PushAbleGameObj>();


    void Start()
    {
        //LoadObjList(LevelManager.Ins.level.GameObjList(), LevelManager.Ins.level.PushAbleGameObjList());
    }

    public void LoadObjList(List<GameObject> obstacleL, List<PushAbleGameObj> pushAbleL)
    {
        obstacleList.Clear();
        pushAbleList.Clear();

        obstacleList = obstacleL;
        pushAbleList = pushAbleL;
    }

    public bool Move(Vector2 direction)
    {
        if (Blocked(transform.position, direction))
        {
            return false;
        }
        else
        {
            transform.Translate(direction /** LevelManager.Ins.level.distancePerGrid*/);
            return true;
        }
    }


    public bool Blocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction /** LevelManager.Ins.level.distancePerGrid*/;

        for (int i = obstacleList.Count - 1; i >= 0; i--)
        {
            if (obstacleList[i] == null)
            {
                obstacleList.RemoveAt(i);
                continue;
            }

            if (obstacleList[i].transform.position.x == newPos.x && obstacleList[i].transform.position.y == newPos.y)
            {
                return true;
            }
        }

        for (int i = pushAbleList.Count - 1; i >= 0; i--)
        {
            if (pushAbleList[i] == null)
            {
                pushAbleList.RemoveAt(i);
                continue;
            }

            if (pushAbleList[i].transform.position.x == newPos.x && pushAbleList[i].transform.position.y == newPos.y)
            {
                return true;
            }
        }

        return false;
    }

}
