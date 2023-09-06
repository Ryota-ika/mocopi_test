using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public float minRequiredSpeed = 650.0f;
    private bool isGrabbed = false;
    private Rigidbody axeRigidbody;
    private GameObject Axe;

    private Vector3 previousPosition;
    private float hitSpeed;
    // Start is called before the first frame update
    void Start()
    {
        /*Axe = GameObject.Find("SM_Woodaxe_Unity");
        axeRigidbody = Axe.GetComponent<Rigidbody>();*/
        previousPosition = transform.position;
        
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (*//*isGrabbed && *//*other.gameObject.CompareTag("Wall"))
        {
            float hitSpeed = axeRigidbody.velocity.magnitude;
            Debug.Log(hitSpeed);
            if (hitSpeed > minRequiredSpeed)
            {
                DestroyWall wall = other.gameObject.GetComponent<DestroyWall>();
                if (wall != null)
                {
                    wall.OnAxeHit(hitSpeed);
                }
            }
        }
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (/*isGrabbed &&*/ other.gameObject.CompareTag("Wall"))
        {
            Vector3 currentPosition = transform.position;
            Vector3 velocity = (currentPosition - previousPosition) / Time.deltaTime;
            previousPosition = currentPosition;

            hitSpeed = velocity.magnitude;
            Debug.Log(hitSpeed);
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
