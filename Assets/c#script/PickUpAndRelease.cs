//2023.8.9
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
    private GameObject key;
    private GameObject Animated_Chest_01;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        chestHinge = GameObject.Find("Chest_Hinge");
        animator = chestHinge.GetComponent<Animator>();

        key = GameObject.Find("Key (1)");
        Animated_Chest_01 = GameObject.Find("Animated_Chest_01 (1)");
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
        leftRayObject.SetVertexCount(2); //�n�_�ƏI�_�ݒ�
        leftRayObject.SetPosition(0, leftHandAnchor.transform.position); //0�Ԗڂ̒��_������R���g���[���̈ʒu�ɐݒ�
        //1�Ԗڂ̒��_������R���g���[���̈ʒu����100m��ɐݒ�
        leftRayObject.SetPosition(1, leftHandAnchor.transform.position + leftHandAnchor.transform.forward * 3.0f);

        leftRayObject.SetWidth(0.01f, 0.01f); //���̑�����0.01�ɐݒ�

        rightRayObject.SetVertexCount(2);
        rightRayObject.SetPosition(0, rightHandAnchor.transform.position);
        rightRayObject.SetPosition(1, rightHandAnchor.transform.position + rightHandAnchor.transform.forward * 3.0f);
        rightRayObject.SetWidth(0.01f, 0.01f);

        
        //����R���g���[���[�̃��C�L���X�g
        Ray leftRay = new Ray(leftHandAnchor.transform.position, leftHandAnchor.transform.forward);
        RaycastHit leftHit;

        //�E��R���g���[���[�̃��C�L���X�g
        Ray rightRay = new Ray(rightHandAnchor.transform.position, rightHandAnchor.transform.forward);
        RaycastHit rightHit;

        bool leftTouchController = OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        bool rightTouchController = OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        bool leftRayHit = Physics.Raycast(leftRay, out leftHit, 3.0f);
        bool rightRayHit = Physics.Raycast(rightRay, out rightHit, 3.0f);

        if (!isBoxOpened)
        {
            if (leftRayHit && leftHit.collider.tag == "Key" && canGrabKey && leftTouchController)
            {
                canGrabKey = false;
                grabbedObject = leftHit.collider.gameObject;
                grabbedObject.transform.SetParent(leftHandAnchor.transform, true);
                grabbedObject.transform.localPosition = Vector3.zero;
                grabbedObject.transform.localRotation = Quaternion.identity;
            }
            else if (rightRayHit && rightHit.collider.tag == "Key" && canGrabKey && rightTouchController)
            {
                canGrabKey = false;
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
                if (leftRayHit && leftHit.collider.tag == "Axe" && leftTouchController)
                {
                    grabbedObject = leftHit.collider.gameObject;
                    grabbedObject.transform.SetParent(leftHandAnchor.transform, true);
                    grabbedObject.transform.localPosition = Vector3.zero;
                    grabbedObject.transform.localRotation = Quaternion.identity;

                }
                else if (rightRayHit && rightHit.collider.tag == "Axe" && rightTouchController)
                {
                    grabbedObject = rightHit.collider.gameObject;
                    grabbedObject.transform.SetParent(rightHandAnchor.transform, true);
                    grabbedObject.transform.localPosition = Vector3.zero;
                    grabbedObject.transform.localRotation = Quaternion.identity;
                }
            }
            else if (leftRayHit && leftHit.collider.tag == "torch" && leftTouchController)
            {
                grabbedObject = leftHit.collider.gameObject;
                grabbedObject.transform.SetParent(leftHandAnchor.transform, true);
                grabbedObject.transform.localPosition = Vector3.zero;
                grabbedObject.transform.localRotation = Quaternion.identity;
            }
            else if (rightRayHit && rightHit.collider.tag == "torch" && rightTouchController)
            {
                grabbedObject = rightHit.collider.gameObject;
                grabbedObject.transform.SetParent(rightHandAnchor.transform, true);
                grabbedObject.transform.localPosition = Vector3.zero;
                grabbedObject.transform.localRotation = Quaternion.identity;
            }
        }
    }
}
