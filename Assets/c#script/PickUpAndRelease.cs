//2023.8.9
using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

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
    private GameObject grabbedObject = null;
    private GameObject chestHinge;
    [SerializeField] private GameObject key;
    [SerializeField] private GameObject Animated_Chest_01;
    private Animator animator;

    [SerializeField] private bool leftTriggerReleased = true;
    [SerializeField] private bool rightTriggerReleased = true;

    public enum selectController
    {
        Left,
        Right
    }

    // Start is called before the first frame update
    void Start()
    {
        chestHinge = GameObject.Find("Chest_Hinge");
        animator = chestHinge.GetComponent<Animator>();
        
        //key = GameObject.Find("Key (1)");
        //Animated_Chest_01 = GameObject.Find("Animated_Chest_01 (1)");
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
        //selectController leftAxeController = selectController.Left;
        //selectController rightAxeController = selectController.Right;

        leftRayObject.SetVertexCount(2); //�n�_�ƏI�_�ݒ�
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

        //bool leftTouchController = OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        //bool rightTouchController = OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);
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

       /* if (leftTriggerReleased && rightTriggerReleased && grabbedObject != null)
        {
            ReleaseObject(grabbedObject);
        }

        if (rightTriggerPressed && !rightTriggerPressed && grabbedObject != null)
        {
            ReleaseObject(grabbedObject);
        }*/

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
                    grabbedObject.transform.localRotation = Quaternion.identity;

                }
                else if (rightRayHit && rightHit.collider.tag == "Axe" && rightTriggerPressed)
                {
                    grabbedObject = rightHit.collider.gameObject;
                    grabbedObject.transform.SetParent(rightHandAnchor.transform, true);
                    grabbedObject.transform.localPosition = Vector3.zero;
                    grabbedObject.transform.localRotation = Quaternion.identity;
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
        }

        /*leftTriggerReleased = !leftTriggerReleased;
        rightTriggerReleased = !rightTriggerReleased;*/
    }

    /*void ReleaseObject(GameObject obj)
    {
        obj.transform.SetParent(null);
        grabbedObject = null;
    }*/
}
