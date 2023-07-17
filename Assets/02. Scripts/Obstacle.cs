using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public bool canCollide=false;
    // Start is called before the first frame update
    void Start()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("PLAYER"))
        {
            GameManager.instance.map.moveSpeed = 0;
            GameManager.instance.isGameOver = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //����ģ ������Ʈ ��Ȱ��ȭ
        if (gameObject.transform.position.z <= -64)
        {
            gameObject.SetActive(false);
        }
    }

}
