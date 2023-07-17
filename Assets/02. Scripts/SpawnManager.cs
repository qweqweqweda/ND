using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform ObjectSpawner; // �������� ��ġ

    float randomX; //���� x��ǥ�� �����ϱ� ���� ����
    Vector3 randomPosition; // ���� x��ǥ

    public float spawnTime;

    GameManager gameManager;

    int poolIndex; // ��ȯ�� ������ƮǮ �ε���

    void Start()
    {
        gameManager=GameManager.instance;
        spawnTime = 0.5f;

        StartCoroutine(SpawnObject());
    }

    IEnumerator SpawnObject()
    {
        while (true)
        {
            randomX = Random.Range(-15f, 15f); // ������Ʈ ��ȯ�� x��ǥ ����
            randomPosition = ObjectSpawner.position + Vector3.right * randomX; // ������Ʈ ��ȯ�� ��ġ

            poolIndex = Random.Range(6, 9); //�ӽ�  6 - ���� , 7 - ��ֹ�


            GameObject currObject = gameManager.poolManager.GetPools(poolIndex);

            currObject.transform.position = randomPosition; // ��ȯ��ġ 
            currObject.transform.parent = gameManager.map.transform; // map�� �ڽ�����

            yield return new WaitForSeconds(spawnTime);
        }


    }
}



