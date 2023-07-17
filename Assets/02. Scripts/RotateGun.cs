using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGun : MonoBehaviour
{
    void Update()
    {
        //���콺 ����Ʈ ��ġ��
        Vector3 gunPoint = GameManager.instance.player.gunPoint;

        // �ѱ��� ������ ����մϴ�.
        Vector3 direction = gunPoint - transform.position;
        direction.Normalize();

        // ���� ���͸� �ѱ��� ȸ�� ������ ��ȯ�մϴ�.
        transform.rotation = Quaternion.LookRotation(direction);
    }
}
