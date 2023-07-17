using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public Map entileMap;

    int mapPrefabIndex; // 다음 차례로 불러올 맵프리팝 인덱스
    int mapChangeDistance; // 맵을 변경할 거리

    GameManager gameManager;

    Vector3 mapSpawnPos = new Vector3(0, 0, 96);

    void Start()
    {
        gameManager = GameManager.instance;

        mapPrefabIndex = 0; // 기록은 0부터 시작
        mapChangeDistance = 100; //테스트를 위해 우선 100으로 설정
    }


    public void GetNextMap()
    {
        if(gameManager.distanceScore > mapChangeDistance) // 맵 변경 주기가 지났으면
        {
           // mapPrefabIndex++; //맵 프리팹 변경
        }

        GameObject nextMap=gameManager.poolManager.GetPools(mapPrefabIndex);
        nextMap.transform.position = mapSpawnPos;
        nextMap.transform.parent = entileMap.transform;

        //내일 이거
        entileMap.mapQ.Enqueue(nextMap.transform);

    }
}
