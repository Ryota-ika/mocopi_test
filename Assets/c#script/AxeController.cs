//�X���P�P���@��������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public float minRequiredSpeed = 2.0f;
    private Vector3 previousPosition;
    private float hitSpeed;
    
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
        if (other.gameObject.CompareTag("Wall"))
        {
            
            LateUpdate();
           
            //���x�������𖞂����ꍇ�A�ǂɃ_���[�W
            if (hitSpeed > minRequiredSpeed)
            {
                GetComponent<AudioSource>().Play();
                DestroyWall wall = other.gameObject.GetComponent<DestroyWall>();
                if (wall != null)
                {
                    wall.OnAxeHit(hitSpeed/*, controller*/);
                }
            }
        }
    }  
}
