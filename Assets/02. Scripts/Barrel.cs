using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public GameObject explosionEff; // ���� ����Ʈ
    Rigidbody rb;

   // public Texture exploredTexture; // ���� �� �巳�� �̹���
    MeshRenderer meshRenderer;

    public float exposionRadius; //���� �ݰ�
    AudioSource audioSource;
    public AudioClip expotionSfx;

    int hitCount=0;

    void Start()
    {
        rb=GetComponent<Rigidbody>();
        meshRenderer=GetComponent<MeshRenderer>();
        audioSource=GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) // ���� ���ݹ����� ���� 
    {
        if (other.CompareTag("PISTOL"))
        {
            hitCount++;
            if (hitCount == 1)
            {
                ExpBarrel();

            }
        }
    }

    void ExpBarrel()
    {
        GameObject effect = Instantiate(explosionEff, transform.position, Quaternion.identity);
        Destroy(effect,2f);

        IndirectDamage(transform.position);

        audioSource.PlayOneShot(expotionSfx,1f);
    }

    void IndirectDamage(Vector3 pos)
    {
        Collider[] colls = Physics.OverlapSphere(pos, exposionRadius,1<<8); // 8�� ���̾ 

        foreach(var coll in colls)
        {
            var _rb=coll.GetComponent<Rigidbody>();
            _rb.mass = 1f;
            _rb.AddExplosionForce(600f, pos, exposionRadius, 500f);
        }
    }

    private void Update()
    {
        if (transform.position.z < -64)
        {
            gameObject.SetActive(false);
        }
    }

}
