using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform[] mapObjects; //���� ���� �� �����Ǿ��ִ� �ʵ�

    public Queue<Transform> mapQ; // �ʵ��� ��Ƽ� ������ ť

    public float moveSpeed=1f;
    Transform tr;
    GameManager gameManager;
    void Start()
    {
        tr=GetComponent<Transform>();
        gameManager = GameManager.instance;

        mapQ = new Queue<Transform>(mapObjects); // ť �ʱ�ȭ
    }

    void Update()
    {
        Transform gm = mapQ.Peek(); // ť�� ��� ù��° ���(���� �տ� �ִ� ��)�� �����´�.
        if (gm.position.z <= -96)
        {
            mapQ.Dequeue();
            gm.gameObject.SetActive(false);

            gameManager.spawner.GetNextMap();
        }

        tr.Translate(Vector3.back * moveSpeed*Time.deltaTime,Space.World); // moveSpeed�� ���� ���� -z�� �̵�(�÷��̾�� ������ ���� ���� ȿ��)

        int distance = -Mathf.FloorToInt(tr.position.z); // map�� z��ǥ�� ������ �����ϰ� ����� �ٲ㼭 �Ÿ�������� ���
        gameManager.distanceScore=distance;
    }
}
