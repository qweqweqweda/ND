using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossBow : MonoBehaviour
{
    private float fireTimer;
    float fireLateTime;

    public Transform firePos;
    public GameObject animArrow;

    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public ItemData itemData;

    public float damage;
    public int ammo;
    public int BulletType;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        ammo = itemData.Ammo;   //�Ѿ� ��
        damage = itemData.Damage;   // �� ������
        fireLateTime = itemData.FireTime;   //�߻� �ð�
        BulletType = itemData.BulletType;   //�Ѿ�
        anim.SetBool("Holding", true);
    }
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && fireTimer >= fireLateTime && ammo >= 1)
        {
            Shoot();
            //�߻� �ִϸ��̼� ����
            anim.SetBool("Fire", true);
            //�߻� �ð� �ʱ�ȭ
            fireTimer = 0;
        }
    }

    //�ִϸ��̼� �̺�Ʈ���� ȣ��
    void Shoot()
    {
        //Ǯ�Ŵ������� ȭ�� ��������
        GameObject arrow = GameManager.instance.ItemPoolManager.GetItemPools(BulletType);
        arrow.transform.position = firePos.transform.position;
        arrow.transform.rotation = firePos.transform.rotation;
        arrow.GetComponent<Arrow>().damage = damage;
        animArrow.SetActive(false);
        ammo--;
    }

    void readyFire()
    {
        anim.SetBool("Fire", false);
    }

    void getArrow()
    {
        animArrow.SetActive(true);
    }

    void emptyArrow()
    {
        //ȭ�� �� ���� empty�ִϸ��̼ǿ��� ����
        if (ammo <= 0)
            anim.SetBool("Holding", false);
    }
}
