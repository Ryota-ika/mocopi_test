//2024.1.19 ��������
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectController : MonoBehaviour
{
    public float minRequiredSpeed = 5.0f;
    [SerializeField] GameObject rightHandAnchor;
    [SerializeField] GameObject leftHandAnchor;
    [SerializeField] LineRenderer leftRayObject;
    [SerializeField] LineRenderer rightRayObject;

    [Header("Ray���������郌�C���[")]
    [SerializeField] LayerMask mask;

    private bool leftTriggerPressed;
    private bool rightTriggerPressed;

    // Update is called once per frame
    void Update()
    {
        UpdateRay(leftHandAnchor, leftRayObject, ref leftTriggerPressed);
        UpdateRay(rightHandAnchor, rightRayObject, ref rightTriggerPressed);


        TryGrabObject(leftHandAnchor, leftRayObject, leftTriggerPressed);
        TryGrabObject(rightHandAnchor, rightRayObject, rightTriggerPressed);
        
    }
    public void OnClick()
    {
        SceneManager.LoadScene("MainGame(Smart)");
    }

    //�R���g���[���[�̃��C���X�V����֐�
    void UpdateRay(GameObject handAnchor, LineRenderer rayObject, ref bool triggerPressed)
    {
        rayObject.SetVertexCount(2);
        rayObject.SetPosition(0, handAnchor.transform.position);
        rayObject.SetPosition(1, handAnchor.transform.position + handAnchor.transform.forward * 10.0f);
        rayObject.SetWidth(0.01f, 0.01f);
        rayObject.material.color = Color.red;

        Ray handRay = new Ray(handAnchor.transform.position, handAnchor.transform.forward);
        RaycastHit hit;

        bool rayHit = Physics.Raycast(handRay, out hit, 10.0f);

        //���C���I�u�W�F�N�g�Ƀq�b�g���Ă���ꍇ�A�F��ύX
        if (rayHit && (hit.collider.tag == "Button"))
        {
            rayObject.material.color = Color.blue;
        }

        //�g���K�[�{�^����������Ă��邩�X�V
        triggerPressed = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, handAnchor == leftHandAnchor ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch);
    }

    //�I�u�W�F�N�g�̃N���b�N�������s���֐�
    void TryGrabObject(GameObject handAnchor, LineRenderer rayObject, bool triggerPressed)
    {
        Ray handRay = new Ray(handAnchor.transform.position, handAnchor.transform.forward);
        RaycastHit hit;

        //�g���K�[�{�^����������Ă���A���C���I�u�W�F�N�g�Ƀq�b�g���Ă���ꍇ
        if (triggerPressed && Physics.Raycast(handRay, out hit, 10.0f))
        {
            //�I�u�W�F�N�g��Button�ł���ꍇ
            if (hit.collider.tag == "Button")
            {
                OnClick();
            }
        }
    }
}
