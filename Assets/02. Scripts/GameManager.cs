using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance=null;

    //싱글톤 선언
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        poolManager.PoolSetting();
    }

    public PoolManager poolManager;
    public MapSpawner spawner;
    public PlayerController player;
    public Map map;

    public bool isGameOver;
    public int distanceScore;// 거리 기록
    void Start()
    {

        distanceScore = 0;// 시작 기록은 0
        isGameOver = false;

    }


}
