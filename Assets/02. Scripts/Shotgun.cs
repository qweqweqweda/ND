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
        ammo = itemData.Ammo;   //�Ѿ� ��
        damage = itemData.Damage;   // �� ������
        fireLateTime = itemData.FireTime;   //�߻� �ð�
        BulletType = itemData.BulletType;   //�Ѿ�
        ShellType = itemData.ShellType; //ź��
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
                //Ǯ�Ŵ������� �Ѿ� ������
                GameObject bullet = GameManager.instance.poolManager.GetPools(BulletType);
                bullet.transform.position = firePos.transform.position;
                bullet.transform.rotation = Quaternion.Euler(firePos.transform.rotation.x - Random.Range(-0.8f, 0.8f), firePos.transform.rotation.y - Random.Range(-0.8f, 0.8f), firePos.transform.rotation.z - Random.Range(-0.8f, 0.8f));
                bullet.GetComponent<Bullet>().damage = damage;
            }
            

            //�Ѿ� �� --
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
        //�ѱ� ȭ��
        fireLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        fireLight.enabled = false;
    }

    IEnumerator Slider()
    {
        yield return new WaitForSeconds(0.2f);
        while (true)
        {
            //�����̴� �ڷ� �Ѿ
            slider.transform.Translate(Vector3.right * Time.deltaTime * 0.8f);
            
            //�����̴� �ִ�ġ�� �Ѿ����
            if (slider.transform.localPosition.x >= 0.09f)
            {
                //�����̴� �ִ���ġ�� ����
                slider.transform.localPosition = new Vector3(0.09f, originSlider.y, originSlider.z);
               
                break;
            }
            yield return null;
        }

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

        //�Ѿ˼� ������ �ڷ�ƾ Ż��
        //�����̴� �ڷ� �Ѿ ���·� ����
        if (ammo <= 0)
            yield break;

        while (true)
        {
            //�����̴� ������ �̵�
            slider.transform.Translate(Vector3.right * Time.deltaTime * -0.8f);
            //�����̴� �ִ�ġ�� ������ �̵�������
            if (slider.transform.localPosition.x <= originSlider.x)
            {
                //�����̴� �����·� ����
                slider.transform.localPosition = originSlider;
                yield break;
            }
            yield return null;
        }
    }
}
