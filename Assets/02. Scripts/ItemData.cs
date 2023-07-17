using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Scripts Object/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("# Main Info")]
    public int ItemType;    //�� Ÿ��
    public int BulletType;  //�Ѿ� Ÿ��
    public int ShellType;   //ź�� Ÿ��
    //0�� - ����
    //1�� - ����
    //2�� - ����
    //3�� - AKM
    //4�� - ����ī

    [Header("# Item Data")]
    public int Ammo;    //�Ѿ˼�
    public float Damage; //�� ������
    public float FireTime; //�߻� �ӵ�
}
