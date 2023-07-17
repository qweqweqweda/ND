using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform[] mapObjects; //게임 시작 시 생성되어있는 맵들

    public Queue<Transform> mapQ; // 맵들을 담아서 관리할 큐

    public float moveSpeed=1f;
    Transform tr;
    GameManager gameManager;
    void Start()
    {
        tr=GetComponent<Transform>();
        gameManager = GameManager.instance;

        mapQ = new Queue<Transform>(mapObjects); // 큐 초기화
    }

    void Update()
    {
        Transform gm = mapQ.Peek(); // 큐에 담긴 첫번째 요소(가장 앞에 있는 맵)를 가져온다.
        if (gm.position.z <= -96)
        {
            mapQ.Dequeue();
            gm.gameObject.SetActive(false);

            gameManager.spawner.GetNextMap();
        }

        tr.Translate(Vector3.back * moveSpeed*Time.deltaTime,Space.World); // moveSpeed에 맞춰 맵이 -z로 이동(플레이어는 앞으로 가는 듯한 효과)

        int distance = -Mathf.FloorToInt(tr.position.z); // map의 z좌표를 정수로 내림하고 양수로 바꿔서 거리기록으로 사용
        gameManager.distanceScore=distance;
    }
}
