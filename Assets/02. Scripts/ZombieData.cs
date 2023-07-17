using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Zombie", menuName = "Scripts Object/ZombieData")]
public class ZombieData : ScriptableObject
{
    [Header("# Main Info")]
    public int ZombieType;  //�����Ǵ� ���� Ÿ��
    //0�� - �Ϲ�����
    //1�� - �߹�������
    //2�� - ��ũ����
    //3�� - ��������

    [Header("# Enemy Data")]
    public float Health;    //ü��
    //�Ϲ����� - 40
    //�߹������� - 70
    //��ũ���� - 150
    //�������� - 1000000
}
