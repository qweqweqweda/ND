using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody rb;
    TrailRenderer trail;

    public float damage;

    private void Start()
    {
        trail = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Off());
    }

    private void OnEnable()
    {
        StartCoroutine(Off());
    }
    void Update()
    {
        //ȭ�� ����
        transform.Translate(Vector3.forward * 0.7f);
        trail.enabled = true;

        // �Ѿ��� �߻��� ��ġ�� ����
        Vector3 bulletOrigin = transform.position;
        Vector3 bulletDirection = transform.forward;

        // Raycast�� �̿��Ͽ� �浹 ����
        RaycastHit hit;
        float maxDistance = 1000; // �Ѿ� �ӵ��� ���� �ִ� �̵� �Ÿ� ����
        if (Physics.Raycast(bulletOrigin, bulletDirection, out hit, maxDistance))
        {
            // �浹�� ��ü�� ������
            GameObject hitObject = hit.collider.gameObject;

            // ���⼭ �浹 ó�� ������ �ۼ��մϴ�.
            // ���� ���, �浹�� ��ü�� IDamageable�� ������ ��쿡 �������� �ִ� ���� ������ ������ �� �ֽ��ϴ�.
            IDamage damageObj = hitObject.GetComponent<IDamage>();

            // IDamage �������̽��� ����ϰ� �ִ� ������Ʈ�϶�
            if (damageObj != null)
            {
                //GetDamage �޼��� ȣ��, Ʈ���Ϸ�����, ���ӿ�����Ʈ ��Ȱ��ȭ
                damageObj.GetDamage(damage);
                trail.enabled = false;
                gameObject.SetActive(false);
            }
            else
                StartCoroutine(Off());
        }
    }

    IEnumerator Off()   //2���� ��Ȱ��ȭ �ڷ�ƾ
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        trail.enabled = false;
        rb.velocity = Vector3.zero;
    }
}
