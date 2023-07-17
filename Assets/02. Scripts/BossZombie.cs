using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BossZombie : MonoBehaviour, IDamage
{
    [SerializeField]
    public ZombieData zombieData;   //좀비 스크립터블 

    public float maxHealth; //최대 체력
    public float health;    //현재 체력

    float attackDistance;   //공격 사거리

    float time;

    bool isDead = false;
    bool isWalk = true;

    Collider coll;
    Rigidbody rb;
    Vector3 originPos;

    Animator anim;

    private void Awake()
    {
        coll = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }
    void Start()
    {
        //스폰 위치
        originPos = transform.position;
        //공격 거리 (맵 이동속도에 비례하여 증가)
        attackDistance = 5f;
        //최대 체력 초기화
        maxHealth = zombieData.Health;
        //현재 체력 초기화
        health = maxHealth;
    }

    private void OnEnable()
    {
        //소환되면 콜라이더와 리지드바디 활성화, 공격콜라이더 비활성화, Walk애니메이션 실행
        coll.enabled = true;
        rb.isKinematic = false;
        anim.SetBool("Walk", true);
    }
    void Update()
    {

        transform.position = new Vector3(originPos.x, originPos.y, transform.position.z + Time.deltaTime * 0.5f);

        transform.position = new Vector3(GameManager.instance.player.transform.position.x, originPos.y, transform.position.z);
    }

    private void FixedUpdate()
    {
        //플레이어와의 거리
        float dist = Vector3.Distance(GameManager.instance.player.transform.position, transform.position);

        //거리가 공격 사거리보다 짧으면
        if (dist <= attackDistance)
        {
            //공격
            anim.SetTrigger("Attack");
        }
    }

    public void GetDamage(float damage)
    {
        rb.AddForce(transform.position - GameManager.instance.player.transform.position * 0.1f, ForceMode.Impulse);
    }
}