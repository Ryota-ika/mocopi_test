//2023.8.9
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class PickUpAndRelease : MonoBehaviour
{
    [SerializeField] GameObject rightHandAnchor;
    public float minRequiredSpeed = 5.0f;
    [SerializeField] GameObject leftHandAnchor;
    [SerializeField] LineRenderer rayObject;

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
        rayObject.SetVertexCount(2); //�n�_�ƏI�_�ݒ�
        rayObject.SetPosition(0, leftHandAnchor.transform.position); //0�Ԗڂ̒��_������R���g���[���̈ʒu�ɐݒ�
        //1�Ԗڂ̒��_������R���g���[���̈ʒu����100m��ɐݒ�
        rayObject.SetPosition(1, leftHandAnchor.transform.position + leftHandAnchor.transform.forward * 3.0f);

        rayObject.SetWidth(0.01f, 0.01f); //���̑�����0.01�ɐݒ�

      if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            RaycastHit hit;
            //���C�L���X�g�𔭎˂��ăq�b�g�����I�u�W�F�N�g���擾
            if (Physics.Raycast(leftHandAnchor.transform.position, leftHandAnchor.transform.forward, out hit, 3.0f))
            {
                if (!isBoxOpened)
                {
                    if (hit.collider.tag == "Key" && canGrabKey)
                    {
                        canGrabKey = false;
                        grabbedObject = hit.collider.gameObject;
                        grabbedObject.transform.parent = leftHandAnchor.transform;
                        grabbedObject.transform.position = rayObject.transform.position;
                        //isBoxOpened = true;
                        //hit.collider.enabled = false;
                    }
                    else if (hit.collider.tag == "Box" &&canGrabKey == false)
                    {
                        /*hit.collider.enabled = false;
                        if (hit.collider.enabled == false)
                        {

                        }*/
                        //Destroy(hit.collider.gameObject);
                        animator.SetBool("Open", true);
                        //Destroy(key);
                        key.SetActive(false);
                        //tresureChest.OpenLid();
                        //hit.collider.gameObject.SetActive(false);
                        float delayTime = 3.0f;
                        StartCoroutine(DelaydMethodCoroutine(delayTime));
                        //isBoxOpened = true;
                    }
                }
                else
                {
                    if (grabbedObject != null && grabbedObject.tag == "Key")
                    {
                        if (hit.collider.tag == "Axe")
                        {
                            //grabbedObject = null;
                            /*hit.collider.transform.parent = leftHandAnchor.transform;
                            hit.collider.transform.position = rayObject.transform.position;*/
                            //grabbedObject = null;//������x����͂ނ��߂̏���
                            grabbedObject = hit.collider.gameObject;
                            //leftanchor��e�Ƃ��Ďqobject�i���j���Q��
                            grabbedObject.transform.SetParent(leftHandAnchor.transform, true);
                            grabbedObject.transform.localPosition = Vector3.zero;
                            grabbedObject.transform.localRotation = Quaternion.identity;
                        }
                        /*Debug.Log(hit.collider.tag);*/
                        
                    }else if(hit.collider.tag == "torch")
                        {
                            grabbedObject = hit.collider.gameObject;
                            grabbedObject.transform.SetParent(leftHandAnchor.transform,true);
                            grabbedObject.transform.localPosition= Vector3.zero;
                            grabbedObject.transform.localRotation= Quaternion.identity;
                        }
                }

            }
        }
    }  
}
