//９月７日
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public float minRequiredSpeed = 2f;
    private bool isGrabbed = false;
    private Rigidbody axeRigidbody;
    private GameObject Axe;

    private Vector3 previousPosition;
    private float hitSpeed;
    private bool firstFrame = true;
    // Start is called before the first frame update
    void Start()
    {
        //previousPosition = transform.position;   
    }
    private void LateUpdate()
    {
        //現在の位置と前フレームの位置の差から速度を計算
            Vector3 currentPosition = transform.position;
            Vector3 velocity = (currentPosition - previousPosition) / Time.deltaTime;
            //速度ベクトルの大きさを計算
            hitSpeed = velocity.magnitude;

            //現在の位置を前フレームの位置として保存
            previousPosition = currentPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (/*isGrabbed &&*/ other.gameObject.CompareTag("Wall"))
        {
            
            LateUpdate();
           
            //速度が条件を満たす場合、壁を破壊
            if (hitSpeed > minRequiredSpeed)
            {
                DestroyWall wall = other.gameObject.GetComponent<DestroyWall>();
                if (wall != null)
                {
                    wall.OnAxeHit(hitSpeed);
                }
            }
        }
    }

    /*public void Grab()
    {
        isGrabbed = true;
    }

    public void Release()
    {
        isGrabbed = false;
    }*/

    // Update is called once per frame
    void Update()
    {
        /*Vector3 velocity = (controllerTransform.position - transform.position) / Time.deltaTime;
        axeRigidbody.velocity = velocity * velocityMultiplier;*/

        /*Vector3 currentPosition = transform.position;
        Vector3 velocity = (currentPosition - previousPosition)/Time.deltaTime;

        hitSpeed = velocity.magnitude;

        previousPosition = currentPosition;

        if (hitSpeed >= minRequiredSpeed)
        {
            Destroy(this.gameObject);
        }*/
    }
}
