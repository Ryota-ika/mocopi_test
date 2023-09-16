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
    [SerializeField] private GameObject Axe;
    public Rigidbody[] pieces;
    private OVRInput.Controller controller;
    //public float minRequireForce = 50.0f; //壁を壊す最低限の力
    
    // Start is called before the first frame update
    void Start()
    {
        //Axe = GameObject.Find("FantasyHammer");
        currentDurability = maxDurability;  //初期化
    }

    public void OnAxeHit(float hitSpeed/*, OVRInput.Controller controller*/)
    {
        //this.controller = controller;
        float damage = hitSpeed;
        DecreaseDurability(damage);
    }

    private void DecreaseDurability(float damage)
    {
        if (PickUpAndRelease.selectController.Left== PickUpAndRelease.selectController.Right)
        {
            StartCoroutine(Vibrate(duration: 0.5f, controller: OVRInput.Controller.LTouch));

        }
        else if (OVRInput.Get(OVRInput.RawButton.RHandTrigger))
        {
            StartCoroutine(Vibrate(duration: 0.5f, controller: OVRInput.Controller.RTouch));

        }

        //StartCoroutine(Vibrate(duration: 0.5f, controller: controller));
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
        //Axe.SetActive(false);
        //Destroy(Axe);
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
}
