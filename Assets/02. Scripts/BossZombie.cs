using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class BossZombie : MonoBehaviour, IDamage
{
    [SerializeField]
    public ZombieData zombieData;   //���� ��ũ���ͺ� 

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
        attackDistance = 5f;
        //�ִ� ü�� �ʱ�ȭ
        maxHealth = zombieData.Health;
        //���� ü�� �ʱ�ȭ
        health = maxHealth;
    }

    private void OnEnable()
    {
        //��ȯ�Ǹ� �ݶ��̴��� ������ٵ� Ȱ��ȭ, �����ݶ��̴� ��Ȱ��ȭ, Walk�ִϸ��̼� ����
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
        //�÷��̾���� �Ÿ�
        float dist = Vector3.Distance(GameManager.instance.player.transform.position, transform.position);

        //�Ÿ��� ���� ��Ÿ����� ª����
        if (dist <= attackDistance)
        {
            //����
            anim.SetTrigger("Attack");
        }
    }

    public void GetDamage(float damage)
    {
        rb.AddForce(transform.position - GameManager.instance.player.transform.position * 0.1f, ForceMode.Impulse);
    }
}