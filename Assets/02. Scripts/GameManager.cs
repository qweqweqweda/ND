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
        ItemPoolManager.PoolSetting();
    }

    public PoolManager poolManager;
    public MapSpawner spawner;
    public PlayerController player;
    public Map map;

    public bool isGameOver;
    public int distanceScore;// 거리 기록

    float currSpeed;

    [HideInInspector]
    public float gameOverTimer=0f;
    void Start()
    {

        distanceScore = 0;// 시작 기록은 0
        isGameOver = false;

    }
  public ItemPoolManager ItemPoolManager;

    public void GameOver()
    {
        currSpeed = map.moveSpeed;//게임오버 시의 맵 속도
        isGameOver = true;

    }
    private void Update()
    {
        if (isGameOver) // 게임 오버 시 3초에 걸쳐 맵 속도 감소
        {
            gameOverTimer += Time.deltaTime;
            float t = Mathf.Clamp01(gameOverTimer / 3);
            map.moveSpeed = currSpeed - Mathf.Lerp(0, currSpeed, t);
        }

    }


}
