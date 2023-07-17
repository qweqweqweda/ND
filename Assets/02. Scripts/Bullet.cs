using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;

    Rigidbody rb;
    TrailRenderer trailRenderer;
    private void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(Off());
    }

    private void OnEnable()
    {
        StartCoroutine(Off());
    }
    void Update()
    {
        //�Ѿ� �߻�
        transform.Translate(Vector3.forward * 1f);
        //Ʈ���� ������ Ȱ��ȭ
        trailRenderer.enabled = true;

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
                trailRenderer.enabled = false;
                gameObject.SetActive(false);
            }
            else
                StartCoroutine(Off());
        }
    }

    IEnumerator Off()
    {
        //2���� �Ѿ� ��Ȱ��ȭ
        yield return new WaitForSeconds(2f);
        trailRenderer.enabled=false;
        gameObject.SetActive(false);
        rb.velocity = Vector3.zero;
    }
}
