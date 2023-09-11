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
    [Header("Rayが反応するレイヤー")]
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
        //３秒後に返す
        yield return new WaitForSeconds(delayTime);

        Destroy(Animated_Chest_01);
        Animated_Chest_01.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        leftRayObject.SetVertexCount(2); //始点と終点設定
        leftRayObject.SetPosition(0, leftHandAnchor.transform.position); //0番目の頂点を左手コントローラの位置に設定
        //1番目の頂点を左手コントローラの位置から100m先に設定
        leftRayObject.SetPosition(1, leftHandAnchor.transform.position + leftHandAnchor.transform.forward * 3.0f);

        leftRayObject.SetWidth(0.01f, 0.01f); //線の太さを0.01に設定

        rightRayObject.SetVertexCount(2);
        rightRayObject.SetPosition(0, rightHandAnchor.transform.position);
        rightRayObject.SetPosition(1, rightHandAnchor.transform.position + rightHandAnchor.transform.forward * 3.0f);
        rightRayObject.SetWidth(0.01f, 0.01f);

        /*bool leftTouchController = OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        bool rightTouchController = OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);
        RaycastHit hit;
        bool leftRay = Physics.Raycast(leftHandAnchor.transform.position, leftHandAnchor.transform.forward, out hit, 3.0f);
        bool rightRay = Physics.Raycast(rightHandAnchor.transform.position, rightHandAnchor.transform.forward, out hit, 3.0f);
        //レイキャストを発射してヒットしたオブジェクトを取得
        if (leftRay)
        {
            //Debug.Log(hit.collider.name);
            if (!isBoxOpened)
            {
                if (hit.collider.tag == "Key" && canGrabKey)
                {
                    if (leftTouchController)
                    {
                        canGrabKey = false;
                        grabbedObject = hit.collider.gameObject;
                        grabbedObject.transform.parent = leftHandAnchor.transform;
                        grabbedObject.transform.position = leftRayObject.transform.position;
                        //isBoxOpened = true;
                        //hit.collider.enabled = false;
                    }
                    else if(rightTouchController)
                    {
                        canGrabKey = false;
                        grabbedObject = hit.collider.gameObject;
                        grabbedObject.transform.parent = rightHandAnchor.transform;
                        grabbedObject.transform.position = rightRayObject.transform.position;
                        //isBoxOpened = true;
                        //hit.collider.enabled = false;
                    }

                }
                else if (hit.collider.tag == "Box" && canGrabKey == false)
                {
                    if (leftTouchController || rightTouchController)
                    {
                        *//*hit.collider.enabled = false;
                        if (hit.collider.enabled == false)
                        {

                        }*//*
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
            }
            else
            {
                if (grabbedObject != null && grabbedObject.tag == "Key")
                {
                    if (hit.collider.tag == "Axe")
                    {
                        if(leftTouchController)
                        {
                            //grabbedObject = null;
                            *//*hit.collider.transform.parent = leftHandAnchor.transform;
                            hit.collider.transform.position = rayObject.transform.position;*//*
                            //grabbedObject = null;//もう一度鍵を掴むための準備
                            grabbedObject = hit.collider.gameObject;
                            //leftanchorを親として子object（斧）を参照
                            grabbedObject.transform.SetParent(leftHandAnchor.transform, true);
                            grabbedObject.transform.localPosition = Vector3.zero;
                            grabbedObject.transform.localRotation = Quaternion.identity;
                        }else if (rightTouchController)
                        {
                            //grabbedObject = null;
                            *//*hit.collider.transform.parent = leftHandAnchor.transform;
                            hit.collider.transform.position = rayObject.transform.position;*//*
                            //grabbedObject = null;//もう一度鍵を掴むための準備
                            grabbedObject = hit.collider.gameObject;
                            //leftanchorを親として子object（斧）を参照
                            grabbedObject.transform.SetParent(rightHandAnchor.transform, true);
                            grabbedObject.transform.localPosition = Vector3.zero;
                            grabbedObject.transform.localRotation = Quaternion.identity;
                        }
                        
                    }
                    *//*Debug.Log(hit.collider.tag);*//*

                }
                else if (hit.collider.tag == "torch")
                {
                    if (leftTouchController)
                    {
                        grabbedObject = hit.collider.gameObject;
                        grabbedObject.transform.SetParent(leftHandAnchor.transform, true);
                        grabbedObject.transform.localPosition = Vector3.zero;
                        grabbedObject.transform.localRotation = Quaternion.identity;
                    }else if (rightTouchController)
                    {
                        grabbedObject = hit.collider.gameObject;
                        grabbedObject.transform.SetParent(rightHandAnchor.transform, true);
                        grabbedObject.transform.localPosition = Vector3.zero;
                        grabbedObject.transform.localRotation = Quaternion.identity;
                    }
                    
                }
            }


        }*/



        /*if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            RaycastHit hit;
            bool rightRay = Physics.Raycast(rightHandAnchor.transform.position, rightHandAnchor.transform.forward, out hit, 3.0f);
            //レイキャストを発射してヒットしたオブジェクトを取得
            if (rightRay)
            {
                Debug.Log(hit.collider.name);
                if (!isBoxOpened)
                {
                    if (hit.collider.tag == "Key" && canGrabKey)
                    {
                        canGrabKey = false;
                        grabbedObject = hit.collider.gameObject;
                        grabbedObject.transform.parent = rightHandAnchor.transform;
                        grabbedObject.transform.position = rightRayObject.transform.position;
                        //isBoxOpened = true;
                        //hit.collider.enabled = false;
                    }
                    else if (hit.collider.tag == "Box" && canGrabKey == false)
                    {
                        *//*hit.collider.enabled = false;
                        if (hit.collider.enabled == false)
                        {

                        }*//*
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
                            *//*hit.collider.transform.parent = leftHandAnchor.transform;
                            hit.collider.transform.position = rayObject.transform.position;*//*
                            //grabbedObject = null;//もう一度鍵を掴むための準備
                            grabbedObject = hit.collider.gameObject;
                            //leftanchorを親として子object（斧）を参照
                            grabbedObject.transform.SetParent(rightHandAnchor.transform, true);
                            grabbedObject.transform.localPosition = Vector3.zero;
                            grabbedObject.transform.localRotation = Quaternion.identity;
                        }
                        *//*Debug.Log(hit.collider.tag);*//*

                    }
                    else if (hit.collider.tag == "torch")
                    {
                        grabbedObject = hit.collider.gameObject;
                        grabbedObject.transform.SetParent(rightHandAnchor.transform, true);
                        grabbedObject.transform.localPosition = Vector3.zero;
                        grabbedObject.transform.localRotation = Quaternion.identity;
                    }
                }


            }
        }*/
        //左手コントローラーのレイキャスト
        Ray leftRay = new Ray(leftHandAnchor.transform.position, leftHandAnchor.transform.forward);
        RaycastHit leftHit;

        //右手コントローラーのレイキャスト
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
