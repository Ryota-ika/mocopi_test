//９月１１日　高橋涼太
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
        if (other.gameObject.CompareTag("Wall"))
        {
            
            LateUpdate();
           
            //速度が条件を満たす場合、壁にダメージ
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
