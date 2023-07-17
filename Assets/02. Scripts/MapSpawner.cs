using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public Map entileMap;

    int mapPrefabIndex; // ���� ���ʷ� �ҷ��� �������� �ε���
    int mapChangeDistance; // ���� ������ �Ÿ�

    GameManager gameManager;

    Vector3 mapSpawnPos = new Vector3(0, 0, 96);

    void Start()
    {
        gameManager = GameManager.instance;

        mapPrefabIndex = 0; // ����� 0���� ����
        mapChangeDistance = 100; //�׽�Ʈ�� ���� �켱 100���� ����
    }


    public void GetNextMap()
    {
        if(gameManager.distanceScore > mapChangeDistance) // �� ���� �ֱⰡ ��������
        {
           // mapPrefabIndex++; //�� ������ ����
        }

        GameObject nextMap=gameManager.poolManager.GetPools(mapPrefabIndex);
        nextMap.transform.position = mapSpawnPos;
        nextMap.transform.parent = entileMap.transform;

        //���� �̰�
        entileMap.mapQ.Enqueue(nextMap.transform);

    }
}
