using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Zombie : MonoBehaviour, IDamage
{
    [SerializeField]
    public ZombieData zombieData;   //���� ��ũ���ͺ� 
    public GameObject box;     //���� �ݶ��̴�

    public float maxHealth; //�ִ� ü��
    public float health;    //���� ü��

    float attackDistance;   //���� ��Ÿ�

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
        //���� ��ġ
        originPos = transform.position;
        //���� �Ÿ� (�� �̵��ӵ��� ����Ͽ� ����)
        attackDistance = GameManager.instance.map.moveSpeed * 2.5f;
        //�ִ� ü�� �ʱ�ȭ
        maxHealth = zombieData.Health;
        //���� ü�� �ʱ�ȭ
        health = maxHealth;
        //���� �ڽ� ��Ȱ��ȭ
        box.SetActive(false);
    }

    private void OnEnable()
    {
        //��ȯ�Ǹ� �ݶ��̴��� ������ٵ� Ȱ��ȭ, �����ݶ��̴� ��Ȱ��ȭ, Walk�ִϸ��̼� ����
        coll.enabled = true;
        rb.isKinematic = false;
        box.SetActive(false);
        anim.SetBool("Walk", true);
    }
    void Update()
    {
        //�׾����� �۵� X
        if(isDead) 
            return;

        //HeavyZombie�Ͻ� (walk�ִϸ��̼��� z������ ��� �̵��ؼ� ������ �ڷ� �̵�)
        if (zombieData.ZombieType == 2 && isWalk)
        {
            //�ڷ� �̵�
            transform.position = new Vector3(originPos.x, originPos.y, transform.position.z + Time.deltaTime * 0.3f);
        }

        if(health <= 0)
        {
            dead();
        }

        //����ģ ����� ��Ȱ��ȭ
        if (gameObject.transform.position.z <= -64)
        {
            gameObject.SetActive(false);
        }
        
    }

    private void FixedUpdate()
    {
        //�÷��̾���� �Ÿ�
        float dist = Vector3.Distance(GameManager.instance.player.transform.position, transform.position);

        //�Ÿ��� ���� ��Ÿ����� ª����
        if(dist <= attackDistance)
        {
            //����
            anim.SetTrigger("Attack");
        }    
    }

    void AttackOn()
    {
        //�����Ҷ� �����ݶ��̴��ڽ� Ȱ��ȭ
        box.SetActive(true);
        isWalk = false;
    }

    void AttackOff()
    {
        //���� ������ ���� �ݶ��̴��ڽ� ��Ȱ��ȭ
        box.SetActive(false);
        isWalk = true;
    }

    public void dead()
    {
        anim.SetTrigger("Dead");
        isDead = true;

        //�ݶ��̴�, ������ٵ� ��Ȱ��ȭ
        coll.enabled = false;
        rb.isKinematic = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //�׾����� ����X
        if (isDead)
            return;

        //���Ѹ�����
        if (other.CompareTag("PISTOL"))
        {
            //ü�� 70����
            health -= 70;
        }

        //���ø�����
        if (other.CompareTag("ARROW"))
        {
            //ü�� 100����
            health -= 100;
        }

        //���������϶�
        if(zombieData.ZombieType == 3)
        {
            //�����̳� ���ø�����
            if(other.CompareTag("PISTOL") || other.CompareTag("ARROW"))
            {
                //�ڷ� �˹�
                rb.AddForce(transform.position - GameManager.instance.player.transform.position * 0.1f, ForceMode.Impulse);
            }
        }
    }

    public void GetDamage(float damage)
    {
        health -= damage;
        print(damage +  "��ŭ ������ ����");
    }
}
