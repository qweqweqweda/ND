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
        ammo = itemData.Ammo;   //�Ѿ� ��
        damage = itemData.Damage;   // �� ������
        fireLateTime = itemData.FireTime;   //�߻� �ð�
        BulletType = itemData.BulletType;   //�Ѿ�
        ShellType = itemData.ShellType; //ź��
        magazine.isKinematic = true;    //źâ ����
    }
    void Update()
    {
        fireTimer += Time.deltaTime;

        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButton(0)) && fireTimer >= fireLateTime && ammo >= 1)
        {
            //Ǯ�Ŵ������� �Ѿ� ������
            Shoot();

            //Ǯ�Ŵ������� ź�� ��������
            GameObject shell = GameManager.instance.poolManager.GetPools(ShellType);
            //ź�� ������ٵ� ��������
            Rigidbody shellRb = shell.GetComponent<Rigidbody>();
            shell.transform.position = Shell.transform.position;
            //������ �������� ȸ��
            shell.transform.rotation = Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360));
            //ź�� ���� ��ġ�� ���� ����
            Vector3 dir = ShellOutPos.transform.position - shell.transform.position;
            //AddForce�� ź�� ����
            shellRb.AddForce(dir * 5f, ForceMode.Impulse);

            //�Ѿ� �� --
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
        //�ѱ� ȭ��
        fireLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        fireLight.enabled = false;
    }

    void Shoot()
    {
        //Ǯ�Ŵ������� ȭ�� ��������
        GameObject bullet = GameManager.instance.poolManager.GetPools(BulletType);
        bullet.transform.position = firePos.transform.position;
        bullet.transform.rotation = firePos.transform.rotation;
        bullet.GetComponent<AKM_Bullet>().damage = damage;
    }
}
