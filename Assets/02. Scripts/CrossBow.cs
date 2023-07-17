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
        ammo = itemData.Ammo;   //총알 수
        damage = itemData.Damage;   // 총 데미지
        fireLateTime = itemData.FireTime;   //발사 시간
        BulletType = itemData.BulletType;   //총알
        anim.SetBool("Holding", true);
    }
    void Update()
    {
        fireTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && fireTimer >= fireLateTime && ammo >= 1)
        {
            Shoot();
            //발사 애니메이션 실행
            anim.SetBool("Fire", true);
            //발사 시간 초기화
            fireTimer = 0;
        }
    }

    //애니메이션 이벤트에서 호출
    void Shoot()
    {
        //풀매니저에서 화살 가져오기
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
        //화살 다 쓰면 empty애니메이션에서 멈춤
        if (ammo <= 0)
            anim.SetBool("Holding", false);
    }
}
