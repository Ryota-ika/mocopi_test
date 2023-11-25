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
    void Move()
    {
        float horizontalInput = 0;
        float verticalInput = 0;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            horizontalInput = Input.GetAxis("Horizontal");
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
        {
            verticalInput = Input.GetAxis("Vertical");
        }
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        Vector3 cameraFowerd = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;
        cameraFowerd.y = 0; // yŽ²‚ÌˆÚ“®‚Í–³Ž‹
        cameraRight.y = 0;
        Vector3 moveVec = (cameraFowerd * movement.z + cameraRight * movement.x);
        transform.position += moveVec;
    }
}
