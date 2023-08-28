using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    public GameObject crackedWall;
    public float maxDurability = 100.0f;
    public float currentDurability;
    public float minRequireForce = 50.0f; //壁を壊す最低限の力
    
    // Start is called before the first frame update
    void Start()
    {
        currentDurability = maxDurability;
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Axe"))
        {
            float hitForce = collision.relativeVelocity.magnitude;

            if(hitForce >= minRequireForce)
            {
                DecreaseDurability(hitForce);
            }
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Axe")
        {
            float hitForce = other.attachedRigidbody.velocity.magnitude;

            if (hitForce >= minRequireForce)
            {
                DecreaseDurability(hitForce);
            }
        }
    }

    private void DecreaseDurability(float force)
    {
        currentDurability -= force;

        if (currentDurability <= 0)
        {
            Destroy();
        }
    }

    void Destroy()
    {
        //壁を壊す処理（アニメーションの再生やモデルの変更）
        Destroy(crackedWall);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   /* private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Key当たった");
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
