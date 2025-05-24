using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrangePieceController : MonoBehaviour
{
    public int pieceID;

    [SerializeField] private List<GameObject> obstacleList = new List<GameObject>();
    [SerializeField] private List<OrangePieceController> pushAbleList = new List<OrangePieceController>();


    void Start()
    {
        LoadObjList(LevelManager.Ins.level.GameObjList(), LevelManager.Ins.level.OrangePieceGameObjList());
    }

    public void LoadObjList(List<GameObject> obstacleL, List<OrangePieceController> pushAbleL)
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
            transform.Translate(direction * LevelManager.Ins.level.distancePerGrid);

            return true;
        }
    }

    public bool Blocked(Vector3 position, Vector2 direction)
    {
        Vector2 newPos = new Vector2(position.x, position.y) + direction * LevelManager.Ins.level.distancePerGrid;

        foreach (var obj in obstacleList)
        {
            if (obj.transform.position.x == newPos.x && obj.transform.position.y == newPos.y)
            {
                return true;
            }
        }

        foreach (OrangePieceController obj in pushAbleList)
        {
            if (obj == this) continue;

            if (obj.transform.position.x == newPos.x && obj.transform .position.y == newPos.y && obj.Blocked(obj.transform.position, direction))
            {
                return true;
            }
        }

        return false;
    }
}
