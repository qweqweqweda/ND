using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public Transform firePos;
    public ParticleSystem fireFx;
    public Light fireLight;
    public GameObject slider;
    public GameObject cocked;
    public GameObject Shell;
    public GameObject ShellOutPos;
    public GameObject Fx;

    private float fireTimer;
    float fireLateTime;
    
    Vector3 originSlider;

    public ItemData itemData;

    public float damage;
    public int ammo;
    public int BulletType;
    public int ShellType;

    private void Start()
    {
        originSlider = new Vector3(-0.07f, 0.15f, -0.05f);
    }
    private void OnEnable()
    {
        ammo = itemData.Ammo;   //총알 수
        damage = itemData.Damage;   // 총 데미지
        fireLateTime = itemData.FireTime;   //발사 시간
        BulletType = itemData.BulletType;   //총알
        ShellType = itemData.ShellType; //탄피
        //originSlider = slider.transform.position;
    }
    void Update()
    {
        fireTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && fireTimer >= fireLateTime && ammo >= 1)
        {
            Shoot();

            //총알 수 --
            ammo--;

            Fx.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 5));
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
        while (true)
        {
            //슬라이더 뒤로 넘어감
            slider.transform.Translate(Vector3.right * Time.deltaTime * -1.5f);
            //노리쇠 뒤로 넘어감
            cocked.transform.localRotation = Quaternion.Euler(-90 * Time.deltaTime * -2.5f, -90f, 90f);
            //슬라이더 최대치로 넘어갔으면
            if(slider.transform.localPosition.x <= -0.3f)
            {
                //슬라이더 최대위치로 고정
                slider.transform.localPosition = new Vector3(-0.3f, originSlider.y, originSlider.z);
                //노리쇠 최대 위치로 고정
                cocked.transform.localRotation = Quaternion.Euler(0, -90f, 90f);
                break;
            }
            yield return null;
        }

        //풀매니저에서 탄피 가져오기
        GameObject shell = GameManager.instance.ItemPoolManager.GetItemPools(ShellType);
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
            slider.transform.Translate(Vector3.right * Time.deltaTime * 1.5f);
            //노리쇠 앞으로 이동
            cocked.transform.localRotation = Quaternion.Euler(0.1f * Time.deltaTime * 2.5f, -90f, 90f);
            //슬라이더 최대치로 앞으로 이동했으면
            if (slider.transform.localPosition.x >= originSlider.x)
            {
                //노리쇠 원상태로 복구
                cocked.transform.localRotation = Quaternion.Euler(-90f, -90f, 90f);
                //슬라이더 원상태로 복구
                slider.transform.localPosition = originSlider;
                yield break;
            }
            yield return null;
        }
    }

    void Shoot()
    {
        //풀매니저에서 총알 가져옴
        GameObject bullet = GameManager.instance.ItemPoolManager.GetItemPools(BulletType);
        bullet.transform.position = firePos.transform.position;
        bullet.transform.rotation = firePos.transform.rotation;
        bullet.GetComponent<Bullet>().damage = damage;
    }

}
