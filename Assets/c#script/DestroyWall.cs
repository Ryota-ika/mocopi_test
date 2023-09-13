//９月１日
using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{
    //public GameObject crackedWall;  
    public float maxDurability = 10.0f;  //壁の最大耐久度
    public float currentDurability;　　//現在の耐久度
    private GameObject Axe;
    public Rigidbody[] pieces;
    //public float minRequireForce = 50.0f; //壁を壊す最低限の力
    
    // Start is called before the first frame update
    void Start()
    {
        Axe = GameObject.Find("FantasyHammer");
        currentDurability = maxDurability;  //初期化
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
        /*bool leftController = OVRInput.Get(OVRInput.Button.One,OVRInput.Controller.LTouch);
        bool rightController = OVRInput.Get(OVRInput.Button.One,OVRInput.Controller.RTouch);
        if (leftController)
        {
        }
        else if (rightController)
        {
            OVRInput.SetControllerVibration(0.1f,0.1f,OVRInput.Controller.RTouch);
        }
            OVRInput.SetControllerVibration(0.1f,0.1f,OVRInput.Controller.LTouch);*/
        StartCoroutine(Vibrate(duration: 0.5f, controller: OVRInput.Controller.LTouch));
        StartCoroutine(Vibrate(duration: 0.5f, controller: OVRInput.Controller.RTouch));
        Debug.Log(damage);
        currentDurability -= damage;

        if (currentDurability <= 0)
        {
            DestroyWallObject();
        }
    }

    private static IEnumerator Vibrate(float duration = 0.1f,float frequency = 0.5f,float amplitude = 0.1f,OVRInput.Controller controller = OVRInput.Controller.Active)
    {
        OVRInput.SetControllerVibration(frequency,amplitude,controller);

        yield return new WaitForSeconds(duration);

        OVRInput.SetControllerVibration(0,0,controller);
    }

    void DestroyWallObject()
    {
        //壁を壊す処理（アニメーションの再生やモデルの変更
        //this.gameObject.SetActive(false);
        transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        foreach (Rigidbody item in pieces) {
            item.freezeRotation = false;
            item.constraints = FreezeCancellation(); 
        }
        StartCoroutine(InvokeDestroy(3));
        Axe.SetActive(false);
        Destroy(Axe);
    }
    IEnumerator InvokeDestroy( float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
        this.gameObject.SetActive(false);
    }
    RigidbodyConstraints FreezeCancellation()
    {
        RigidbodyConstraints c = RigidbodyConstraints.None;
        c = RigidbodyConstraints.FreezePositionX;
        c = RigidbodyConstraints.FreezePositionY;
        c = RigidbodyConstraints.FreezePositionZ;
        return c;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
	private void OnTriggerEnter(Collider other)
	{
        Debug.Log("当たった");
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
