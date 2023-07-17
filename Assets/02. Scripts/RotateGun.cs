using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGun : MonoBehaviour
{
    void Update()
    {
        //마우스 포인트 위치값
        Vector3 gunPoint = GameManager.instance.player.gunPoint;

        // 총구의 방향을 계산합니다.
        Vector3 direction = gunPoint - transform.position;
        direction.Normalize();

        // 방향 벡터를 총구의 회전 값으로 변환합니다.
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
