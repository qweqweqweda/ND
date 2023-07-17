using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public GameObject explosionEff; // 폭파 이펙트
    Rigidbody rb;

   // public Texture exploredTexture; // 폭파 후 드럼통 이미지
    MeshRenderer meshRenderer;

    public float exposionRadius; //폭발 반경
    AudioSource audioSource;
    public AudioClip expotionSfx;

    int hitCount=0;

    void Start()
    {
        rb=GetComponent<Rigidbody>();
        meshRenderer=GetComponent<MeshRenderer>();
        audioSource=GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other) // 무기 공격받으면 터짐 
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
        Collider[] colls = Physics.OverlapSphere(pos, exposionRadius,1<<8); // 8번 레이어만 

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
