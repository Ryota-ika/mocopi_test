//2023.12.16 ��������
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class PickUpAndRelease : MonoBehaviour
{
    public float minRequiredSpeed = 5.0f;
    [SerializeField] GameObject rightHandAnchor;
    [SerializeField] GameObject leftHandAnchor;
    [SerializeField] LineRenderer leftRayObject;
    [SerializeField] LineRenderer rightRayObject;

    [SerializeField] private TresureChest tresureChest;
    private bool canGrabKey = true;
    public bool isBoxOpened = false;
    [Header("Ray���������郌�C���[")]
    [SerializeField] LayerMask mask;
    public GameObject grabbedObject = null;
    private GameObject chestHinge;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject Animated_Chest_01;
    private Animator animator;

    [SerializeField] private bool leftTriggerReleased = true;
    [SerializeField] private bool rightTriggerReleased = true;

    private bool leftTriggerPressed;
    private bool rightTriggerPressed;

    // Start is called before the first frame update
    void Start()
    {
        chestHinge = GameObject.Find("Chest_Hinge");
        animator = chestHinge.GetComponent<Animator>();
    }

    private IEnumerator DelaydMethodCoroutine(float delayTime)
    {
        //�R�b��ɕԂ�
        yield return new WaitForSeconds(delayTime);

        Destroy(Animated_Chest_01);
        Animated_Chest_01.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRay(leftHandAnchor,leftRayObject,ref leftTriggerPressed);
        UpdateRay(rightHandAnchor,rightRayObject,ref rightTriggerPressed);

        //�󔠂��J���Ă��Ȃ��ꍇ
        if (!isBoxOpened)
        {
            //���肩�E��ŃI�u�W�F�N�g��͂�
            TryGrabObject(leftHandAnchor,leftRayObject,leftTriggerPressed);
            TryGrabObject(rightHandAnchor,rightRayObject,rightTriggerPressed);
        }
        else
        {
            //�͂񂾃I�u�W�F�N�g������
            HandleGrabbedObject(leftHandAnchor,leftRayObject,ref leftTriggerPressed);
            HandleGrabbedObject(rightHandAnchor,rightRayObject,ref rightTriggerPressed);
        }

        //���t�@�N�^�����O�O�̃R�[�h
        /*leftRayObject.SetVertexCount(2); //�n�_�ƏI�_�ݒ�
        leftRayObject.SetPosition(0, leftHandAnchor.transform.position); //0�Ԗڂ̒��_������R���g���[���̈ʒu�ɐݒ�
        //1�Ԗڂ̒��_������R���g���[���̈ʒu����3m��ɐݒ�
        leftRayObject.SetPosition(1, leftHandAnchor.transform.position + leftHandAnchor.transform.forward * 3.0f);
        leftRayObject.SetWidth(0.01f, 0.01f); //���̑�����0.01�ɐݒ�
        leftRayObject.material.color = Color.red;

        rightRayObject.SetVertexCount(2);
        rightRayObject.SetPosition(0, rightHandAnchor.transform.position);
        rightRayObject.SetPosition(1, rightHandAnchor.transform.position + rightHandAnchor.transform.forward * 3.0f);
        rightRayObject.SetWidth(0.01f, 0.01f);
        rightRayObject.material.color = Color.red;


        //����R���g���[���[�̃��C�L���X�g
        Ray leftRay = new Ray(leftHandAnchor.transform.position, leftHandAnchor.transform.forward);
        RaycastHit leftHit;

        //�E��R���g���[���[�̃��C�L���X�g
        Ray rightRay = new Ray(rightHandAnchor.transform.position, rightHandAnchor.transform.forward);
        RaycastHit rightHit;

        bool leftTriggerPressed = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        bool rightTriggerPressed = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        bool leftRayHit = Physics.Raycast(leftRay, out leftHit, 3.0f);
        bool rightRayHit = Physics.Raycast(rightRay, out rightHit, 3.0f);

        

        if (leftRayHit && (leftHit.collider.tag == "Key" || leftHit.collider.tag == "Axe" || leftHit.collider.tag == "touch"))
        {
            leftRayObject.material.color = Color.blue;
        }
        if (rightRayHit && (rightHit.collider.tag == "Key" || rightHit.collider.tag == "Axe" || rightHit.collider.tag == "touch"))
        {
            rightRayObject.material.color = Color.blue;
        }

        if (!isBoxOpened)
        {
            if (leftRayHit && leftHit.collider.tag == "Key" && canGrabKey && leftTriggerPressed)
            {
                //canGrabKey = false;
                grabbedObject = leftHit.collider.gameObject;
                grabbedObject.transform.SetParent(leftHandAnchor.transform, true);
                grabbedObject.transform.localPosition = Vector3.zero;
                grabbedObject.transform.localRotation = Quaternion.identity;
                
            }
            else if (rightRayHit && rightHit.collider.tag == "Key" && canGrabKey && rightTriggerPressed)
            {
                //canGrabKey = false;
                grabbedObject = rightHit.collider.gameObject;
                grabbedObject.transform.SetParent(rightHandAnchor.transform, true);
                grabbedObject.transform.localPosition = Vector3.zero;
                grabbedObject.transform.localRotation = Quaternion.identity;
            }
        }
        else
        {
            if (grabbedObject != null && grabbedObject.tag == "Key")
            {
                if (leftRayHit && leftHit.collider.tag == "Axe" && leftTriggerPressed)
                {
                    grabbedObject = leftHit.collider.gameObject;
                    grabbedObject.transform.SetParent(leftHandAnchor.transform, true);
                    grabbedObject.transform.localPosition = Vector3.zero;
                    grabbedObject.transform.localRotation = Quaternion.identity*Quaternion.AngleAxis(90f,transform.up);

                }
                else if (rightRayHit && rightHit.collider.tag == "Axe" && rightTriggerPressed)
                {
                    grabbedObject = rightHit.collider.gameObject;
                    grabbedObject.transform.SetParent(rightHandAnchor.transform, true);
                    grabbedObject.transform.localPosition = Vector3.zero;
                    grabbedObject.transform.localRotation = Quaternion.identity * Quaternion.AngleAxis(90f, transform.up);

                }
            }
            else if (leftRayHit && leftHit.collider.tag == "torch" && leftTriggerPressed)
            {
                grabbedObject = leftHit.collider.gameObject;
                grabbedObject.transform.SetParent(leftHandAnchor.transform, true);
                grabbedObject.transform.localPosition = Vector3.zero;
                grabbedObject.transform.localRotation = Quaternion.identity;
            }
            else if (rightRayHit && rightHit.collider.tag == "torch" && rightTriggerPressed)
            {
                grabbedObject = rightHit.collider.gameObject;
                grabbedObject.transform.SetParent(rightHandAnchor.transform, true);
                grabbedObject.transform.localPosition = Vector3.zero;
                grabbedObject.transform.localRotation = Quaternion.identity;
            }
        }*/
        
    }

    //�R���g���[���[�̃��C���X�V����֐�
    void UpdateRay(GameObject handAnchor, LineRenderer rayObject, ref bool triggerPressed)
    {
        rayObject.SetVertexCount(2);
        rayObject.SetPosition(0,handAnchor.transform.position);
        rayObject.SetPosition(1, handAnchor.transform.position + handAnchor.transform.forward * 3.0f);
        rayObject.SetWidth(0.01f,0.01f);
        rayObject.material.color = Color.red;

        Ray handRay = new Ray(handAnchor.transform.position,handAnchor.transform.forward);
        RaycastHit hit;

        bool rayHit = Physics.Raycast(handRay,out hit,3.0f);

        //���C���I�u�W�F�N�g�Ƀq�b�g���Ă���ꍇ�A�F��ύX
        if (rayHit && (hit.collider.tag == "Key" || hit.collider.tag == "Axe" || hit.collider.tag == "torch"))
        {
            rayObject.material.color = Color.blue;
        }

        //�g���K�[�{�^����������Ă��邩�X�V
        triggerPressed = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, handAnchor == leftHandAnchor ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch);
    }

    //�I�u�W�F�N�g��͂ޏ������s���֐�
    void TryGrabObject(GameObject handAnchor,LineRenderer rayObject,bool triggerPressed)
    {
        Ray handRay = new Ray(handAnchor.transform.position,handAnchor.transform.forward);
        RaycastHit hit;

        //�g���K�[�{�^����������Ă���A���C���I�u�W�F�N�g�Ƀq�b�g���Ă���ꍇ
        if (triggerPressed && Physics.Raycast(handRay,out hit,3.0f))
        {
            //�I�u�W�F�N�g��Key�ł���ꍇ
            if (hit.collider.tag == "Key")
            {
                //�I�u�W�F�N�g��͂�
                grabbedObject = hit.collider.gameObject;
                grabbedObject.transform.SetParent(handAnchor.transform,true);
                grabbedObject.transform.localPosition = Vector3.zero;
                grabbedObject.transform.localRotation = Quaternion.identity;
            }
        }
    }

    //�͂񂾃I�u�W�F�N�g����������֐�
    void HandleGrabbedObject(GameObject handAnchor, LineRenderer rayObject,ref bool triggerPressed)
    {
        Ray handRay = new Ray(handAnchor.transform.position, handAnchor.transform.forward);
        RaycastHit hit;
        //�͂�ł�I�u�W�F�N�g�����݂���ꍇ
        if (grabbedObject != null && grabbedObject.tag == "Key")
        {
            //�͂�ł�I�u�W�F�N�g��Key�ł���ꍇ
            if (Physics.Raycast(handRay,out hit,3.0f) && hit.collider.tag == "Axe" && triggerPressed)
            {
                //�I�u�W�F�N�g����ɒǏ]������
                grabbedObject = hit.collider.gameObject;
                grabbedObject.transform.SetParent(handAnchor.transform,true);
                grabbedObject.transform.localPosition = Vector3.zero;
                grabbedObject.transform.localRotation = Quaternion.identity;
            }
        }
        //���C���I�u�W�F�N�g�Ƀq�b�g���A���̃^�O��torch�ŁA���g���K�[�{�^����������Ă���ꍇ
        else if (Physics.Raycast(handRay,out hit,3.0f) && hit.collider.tag == "torch" && triggerPressed)
        {
            //�I�u�W�F�N�g����ɒǏ]�����A��]
            grabbedObject = hit.collider.gameObject;
            grabbedObject.transform.SetParent(handAnchor.transform,true);
            grabbedObject.transform.localPosition = Vector3.zero;
            grabbedObject.transform.localRotation = Quaternion.identity;
        }
    }
}
