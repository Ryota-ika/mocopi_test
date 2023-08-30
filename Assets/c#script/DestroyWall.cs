using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    //public GameObject crackedWall;  
    public float maxDurability = 100.0f;  //�ǂ̍ő�ϋv�x
    public float currentDurability;�@�@//���݂̑ϋv�x
    //public float minRequireForce = 50.0f; //�ǂ��󂷍Œ���̗�
    
    // Start is called before the first frame update
    void Start()
    {
        currentDurability = maxDurability;  //������
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Axe")
        {
            *//*float hitForce = other.attachedRigidbody.velocity.magnitude;

            if (hitForce >= minRequireForce)
            {
                DecreaseDurability(hitForce);
            }*//*
            Rigidbody axeRigidBody = other.attachedRigidbody;

            if (axeRigidBody != null)
            {
                float hitSpeed = axeRigidBody.velocity.magnitude;

                if (hitSpeed >= minRequireForce)
                {
                    DecreaseDurability(hitSpeed);
                }
            }
        }
    }*/

    public void OnAxeHit(float hitSpeed)
    {
        float damage = hitSpeed;
        DecreaseDurability(damage);
    }

    private void DecreaseDurability(float damage)

    {
        currentDurability -= damage;

        if (currentDurability <= 0)
        {
            Destroy();
        }
    }

    void Destroy()
    {
        //�ǂ��󂷏����i�A�j���[�V�����̍Đ��⃂�f���̕ύX�j
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Key��������");
        if (collision.gameObject.tag == "Key")
        {
            Destroy(this.gameObject,0.2f);
        }
    }*/
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "Axe")
    //    {
    //        Destroy(this.gameObject, 0.2f);
    //    }
    //}
}
