using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed=10f;
    public bool testMode=true;
    
    public Rigidbody rb;
    public Collider coll;
    void Start()
    {
        rb= GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }


    void Update()
    {

        float xInput = Input.GetAxisRaw("Horizontal");

        if (testMode) // �׽�Ʈ ���� ���� ���� ���
        {
            float zInput = Input.GetAxisRaw("Vertical");
            Vector3 testVelocity = new Vector3(xInput * 10f, 0f, zInput*10f);
            rb.velocity = testVelocity;
            return;
        }

        Vector3 newVelocity = new Vector3(xInput*10f, 0f, playerSpeed);
        rb.velocity = newVelocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("ZOMBIE"))
        {
            print("�����浹");
            collision.collider.GetComponent<Zombie>().health -= 40;
        }
    }
}
