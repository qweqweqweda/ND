using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public ItemData itemData;

    public GameObject firePos;
    public ParticleSystem fireFx;
    public Light fireLight;
    public GameObject slider;
    public GameObject Shell;
    public GameObject ShellOutPos;
    public GameObject Fx;

    private float fireTimer;
    float fireLateTime;

    Vector3 originSlider;

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
        originSlider = Vector3.zero;
    }
    void Update()
    {
        fireTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && fireTimer >= fireLateTime && ammo >= 1)
        {
            for(int i = 0; i < 8; i++)
            {
                float ram = Random.Range(-1f, 1f);
                //풀매니저에서 총알 가져옴
                GameObject bullet = GameManager.instance.poolManager.GetPools(BulletType);
                bullet.transform.position = firePos.transform.position;
                bullet.transform.rotation = Quaternion.Euler(firePos.transform.rotation.x - Random.Range(-0.8f, 0.8f), firePos.transform.rotation.y - Random.Range(-0.8f, 0.8f), firePos.transform.rotation.z - Random.Range(-0.8f, 0.8f));
                bullet.GetComponent<Bullet>().damage = damage;
            }
            

            //총알 수 --
            ammo--;

            Fx.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 20));
            fireFx.Play();
            StartCoroutine(Flicker());
            StartCoroutine(Slider());
            fireTimer = 0f;
        }
    }

    IEnumerator Flicker()
    {
        //총구 화염
        fireLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        fireLight.enabled = false;
    }

    IEnumerator Slider()
    {
        yield return new WaitForSeconds(0.2f);
        while (true)
        {
            //슬라이더 뒤로 넘어감
            slider.transform.Translate(Vector3.right * Time.deltaTime * 0.8f);
            
            //슬라이더 최대치로 넘어갔으면
            if (slider.transform.localPosition.x >= 0.09f)
            {
                //슬라이더 최대위치로 고정
                slider.transform.localPosition = new Vector3(0.09f, originSlider.y, originSlider.z);
               
                break;
            }
            yield return null;
        }

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

        //총알수 없으면 코루틴 탈출
        //슬라이더 뒤로 넘어간 상태로 정지
        if (ammo <= 0)
            yield break;

        while (true)
        {
            //슬라이더 앞으로 이동
            slider.transform.Translate(Vector3.right * Time.deltaTime * -0.8f);
            //슬라이더 최대치로 앞으로 이동했으면
            if (slider.transform.localPosition.x <= originSlider.x)
            {
                //슬라이더 원상태로 복구
                slider.transform.localPosition = originSlider;
                yield break;
            }
            yield return null;
        }
    }
}
