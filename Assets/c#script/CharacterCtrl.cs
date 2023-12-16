//2023�N9��16���@��������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CharacterCtrl : MonoBehaviour
{

    [SerializeField]
    private Transform targetPlayer;
    [SerializeField]
    private Transform naviPos;
    [SerializeField]
    float smoothTime = 0.5f;
    [SerializeField]
    float rotateTime;
    Vector3 velocity = Vector3.zero;
    [SerializeField]
    private GameObject centerEyeAnchor;
    [SerializeField]
    float raycastDistance = 1.0f;//���C�L���X�g�̋���
    // Start is called before the first frame update
    void Start()
    {
        //targetPlayer = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 ovrCameraMovement = centerEyeAnchor.transform.position - transform.position;

        float ovrCameraMovementMagnitude = ovrCameraMovement.magnitude;
        if (ovrCameraMovementMagnitude <= 1.0)
        {
        }*/

        /*Vector3 target_pos = naviPos.position;
        transform.position = Vector3.SmoothDamp(transform.position, target_pos, ref velocity, smoothTime);
        Vector3 loockPos = targetPlayer.position - transform.position;
        loockPos.y = 0;
        if (lookPos != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(loockPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateTime);
        }*/
        Vector3 targetPos = naviPos.position;

        //���C�L���X�g���g�p���ď�Q�����m�F
        RaycastHit hit;
        if (Physics.Raycast(transform.position,targetPos-transform.position,out hit,raycastDistance))
        {
            //��Q��������ꍇ�͕␳
            targetPos = hit.point - (targetPos - transform.position).normalized * 0.1f;
        }
        transform.position = Vector3.SmoothDamp(transform.position,targetPos,ref velocity,smoothTime);
        Vector3 lookPos = targetPlayer.position - transform.position;
        lookPos.y = 0;
        if (lookPos != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotateTime);
        }
    }
}
