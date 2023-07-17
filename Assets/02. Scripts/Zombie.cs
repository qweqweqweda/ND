using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Zombie : MonoBehaviour, IDamage
{
    [SerializeField]
    public ZombieData zombieData;   //좀비 스크립터블 
    public GameObject box;     //공격 콜라이더

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
        attackDistance = GameManager.instance.map.moveSpeed * 2.5f;
        //최대 체력 초기화
        maxHealth = zombieData.Health;
        //현재 체력 초기화
        health = maxHealth;
        //공격 박스 비활성화
        box.SetActive(false);
    }

    private void OnEnable()
    {
        //소환되면 콜라이더와 리지드바디 활성화, 공격콜라이더 비활성화, Walk애니메이션 실행
        coll.enabled = true;
        rb.isKinematic = false;
        box.SetActive(false);
        anim.SetBool("Walk", true);
    }
    void Update()
    {
        //죽었으면 작동 X
        if(isDead) 
            return;

        //HeavyZombie일시 (walk애니메이션이 z축으로 계속 이동해서 강제로 뒤로 이동)
        if (zombieData.ZombieType == 2 && isWalk)
        {
            //뒤로 이동
            transform.position = new Vector3(originPos.x, originPos.y, transform.position.z + Time.deltaTime * 0.3f);
        }

        if(health <= 0)
        {
            dead();
        }

        //지나친 좀비들 비활성화
        if (gameObject.transform.position.z <= -64)
        {
            gameObject.SetActive(false);
        }
        
    }

    private void FixedUpdate()
    {
        //플레이어와의 거리
        float dist = Vector3.Distance(GameManager.instance.player.transform.position, transform.position);

        //거리가 공격 사거리보다 짧으면
        if(dist <= attackDistance)
        {
            //공격
            anim.SetTrigger("Attack");
        }    
    }

    void AttackOn()
    {
        //공격할때 공격콜라이더박스 활성화
        box.SetActive(true);
        isWalk = false;
    }

    void AttackOff()
    {
        //공격 끝나면 공격 콜라이더박스 비활성화
        box.SetActive(false);
        isWalk = true;
    }

    public void dead()
    {
        anim.SetTrigger("Dead");
        isDead = true;

        //콜라이더, 리지드바디 비활성화
        coll.enabled = false;
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //죽었으면 실행X
        if (isDead)
            return;

        //권총맞으면
        if (other.CompareTag("PISTOL"))
        {
            //체력 70감소
            health -= 70;
        }

        //석궁맞으면
        if (other.CompareTag("ARROW"))
        {
            //체력 100감소
            health -= 100;
        }

        //보스좀비일때
        if(zombieData.ZombieType == 3)
        {
            //권총이나 석궁맞으면
            if(other.CompareTag("PISTOL") || other.CompareTag("ARROW"))
            {
                //뒤로 넉백
                rb.AddForce(transform.position - GameManager.instance.player.transform.position * 0.1f, ForceMode.Impulse);
            }
        }
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        print(damage +  "만큼 데미지 받음");
    }
}
