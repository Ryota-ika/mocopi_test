using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class DebugUse_PlayerCtrl : MonoBehaviour
{
    [SerializeField]
    float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    [Conditional("UNITY_EDITOR")]
    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        Vector3 cameraFowerd = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraFowerd.y = 0; // yŽ²‚ÌˆÚ“®‚Í–³Ž‹
        cameraRight.y = 0;
        Vector3 moveVec = (cameraFowerd * movement.z + cameraRight * movement.x);
        transform.position += moveVec;
    }
}
