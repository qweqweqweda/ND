using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AKM : MonoBehaviour
{
    public ItemData itemData;

    public GameObject firePos;
    public ParticleSystem fireFx;
    public Light fireLight;
    public Rigidbody magazine;
    public GameObject Shell;
    public GameObject ShellOutPos;
    public GameObject Fx;

    private float fireTimer;
    float fireLateTime;

    public float damage;
    public int ammo;

    public int BulletType;
    public int ShellType;
    private void OnEnable()
    {
        ammo = itemData.Ammo;   //총알 수
        damage = itemData.Damage;   // 총 데미지
        fireLateTime = itemData.FireTime;   //발사 시간
        BulletType = itemData.BulletType;   //총알
        ShellType = itemData.ShellType; //탄피
        magazine.isKinematic = true;    //탄창 고정
    }
    void Update()
    {
        fireTimer += Time.deltaTime;

        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && fireTimer >= fireLateTime && ammo >= 1)
        {
            //풀매니저에서 총알 가져옴
            Shoot();

            //풀매니저에서 탄피 가져오기
            GameObject shell = GameManager.instance.poolManager.GetPools(ShellType);
            //탄피 리지드바디 가져오기
            Rigidbody shellRb = shell.GetComponent<Rigidbody>();
            shell.transform.position = Shell.transform.position;
            //랜덤한 반향으로 회전
            shell.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            //탄피 배출 위치을 위한 방향
            Vector3 dir = ShellOutPos.transform.position - shell.transform.position;
            //AddForce로 탄피 배출
            shellRb.AddForce(dir * 5f, ForceMode.Impulse);

            //총알 수 --
            ammo--;

            Fx.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 20));
            fireFx.Play();
            StartCoroutine(Flicker());
            fireTimer = 0f;

            if(ammo <= 0)
            {
                magazine.isKinematic = false;
            }
        }
    }

    IEnumerator Flicker()
    {
        //총구 화염
        fireLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        fireLight.enabled = false;
    }

    void Shoot()
    {
        //풀매니저에서 화살 가져오기
        GameObject bullet = GameManager.instance.poolManager.GetPools(BulletType);
        bullet.transform.position = firePos.transform.position;
        bullet.transform.rotation = firePos.transform.rotation;
        bullet.GetComponent<AKM_Bullet>().damage = damage;
    }
}
