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
        ammo = itemData.Ammo;   //�Ѿ� ��
        damage = itemData.Damage;   // �� ������
        fireLateTime = itemData.FireTime;   //�߻� �ð�
        BulletType = itemData.BulletType;   //�Ѿ�
        ShellType = itemData.ShellType; //ź��
        //originSlider = slider.transform.position;
    }
    void Update()
    {
        fireTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && fireTimer >= fireLateTime && ammo >= 1)
        {
            Shoot();

            //�Ѿ� �� --
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
        //�ѱ� ȭ��
        fireLight.enabled = true;
        yield return new WaitForSeconds(0.1f);
        fireLight.enabled = false;
    }

    IEnumerator Slider()
    {
        while (true)
        {
            //�����̴� �ڷ� �Ѿ
            slider.transform.Translate(Vector3.right * Time.deltaTime * -1.5f);
            //�븮�� �ڷ� �Ѿ
            cocked.transform.localRotation = Quaternion.Euler(-90 * Time.deltaTime * -2.5f, -90f, 90f);
            //�����̴� �ִ�ġ�� �Ѿ����
            if(slider.transform.localPosition.x <= -0.3f)
            {
                //�����̴� �ִ���ġ�� ����
                slider.transform.localPosition = new Vector3(-0.3f, originSlider.y, originSlider.z);
                //�븮�� �ִ� ��ġ�� ����
                cocked.transform.localRotation = Quaternion.Euler(0, -90f, 90f);
                break;
            }
            yield return null;
        }

        //Ǯ�Ŵ������� ź�� ��������
        GameObject shell = GameManager.instance.ItemPoolManager.GetItemPools(ShellType);
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
            slider.transform.Translate(Vector3.right * Time.deltaTime * 1.5f);
            //�븮�� ������ �̵�
            cocked.transform.localRotation = Quaternion.Euler(0.1f * Time.deltaTime * 2.5f, -90f, 90f);
            //�����̴� �ִ�ġ�� ������ �̵�������
            if (slider.transform.localPosition.x >= originSlider.x)
            {
                //�븮�� �����·� ����
                cocked.transform.localRotation = Quaternion.Euler(-90f, -90f, 90f);
                //�����̴� �����·� ����
                slider.transform.localPosition = originSlider;
                yield break;
            }
            yield return null;
        }
    }

    void Shoot()
    {
        //Ǯ�Ŵ������� �Ѿ� ������
        GameObject bullet = GameManager.instance.ItemPoolManager.GetItemPools(BulletType);
        bullet.transform.position = firePos.transform.position;
        bullet.transform.rotation = firePos.transform.rotation;
        bullet.GetComponent<Bullet>().damage = damage;
    }

}
