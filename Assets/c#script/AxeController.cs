using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public float minRequiredSpeed = 5.0f;
    private bool isGrabbed = false;
    private Rigidbody axeRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        axeRigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (/*isGrabbed && */other.gameObject.CompareTag("Wall"))
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
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 velocity = (controllerTransform.position - transform.position) / Time.deltaTime;
        axeRigidbody.velocity = velocity * velocityMultiplier;*/
    }
}
