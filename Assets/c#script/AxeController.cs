//�X���V��
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
        //���݂̈ʒu�ƑO�t���[���̈ʒu�̍����瑬�x���v�Z
            Vector3 currentPosition = transform.position;
            Vector3 velocity = (currentPosition - previousPosition) / Time.deltaTime;
            //���x�x�N�g���̑傫�����v�Z
            hitSpeed = velocity.magnitude;

            //���݂̈ʒu��O�t���[���̈ʒu�Ƃ��ĕۑ�
            previousPosition = currentPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (/*isGrabbed &&*/ other.gameObject.CompareTag("Wall"))
        {
            
            LateUpdate();
           
            //���x�������𖞂����ꍇ�A�ǂ�j��
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
