//2023.12.16 髙橋涼太
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
    [Header("Rayが反応するレイヤー")]
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
        //３秒後に返す
        yield return new WaitForSeconds(delayTime);

        Destroy(Animated_Chest_01);
        Animated_Chest_01.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRay(leftHandAnchor,leftRayObject,ref leftTriggerPressed);
        UpdateRay(rightHandAnchor,rightRayObject,ref rightTriggerPressed);

        //宝箱が開いていない場合
        if (!isBoxOpened)
        {
            //左手か右手でオブジェクトを掴む
            TryGrabObject(leftHandAnchor,leftRayObject,leftTriggerPressed);
            TryGrabObject(rightHandAnchor,rightRayObject,rightTriggerPressed);
        }
        else
        {
            //掴んだオブジェクトを処理
            HandleGrabbedObject(leftHandAnchor,leftRayObject,ref leftTriggerPressed);
            HandleGrabbedObject(rightHandAnchor,rightRayObject,ref rightTriggerPressed);
        }

        //リファクタリング前のコード
        /*leftRayObject.SetVertexCount(2); //始点と終点設定
        leftRayObject.SetPosition(0, leftHandAnchor.transform.position); //0番目の頂点を左手コントローラの位置に設定
        //1番目の頂点を左手コントローラの位置から3m先に設定
        leftRayObject.SetPosition(1, leftHandAnchor.transform.position + leftHandAnchor.transform.forward * 3.0f);
        leftRayObject.SetWidth(0.01f, 0.01f); //線の太さを0.01に設定
        leftRayObject.material.color = Color.red;

        rightRayObject.SetVertexCount(2);
        rightRayObject.SetPosition(0, rightHandAnchor.transform.position);
        rightRayObject.SetPosition(1, rightHandAnchor.transform.position + rightHandAnchor.transform.forward * 3.0f);
        rightRayObject.SetWidth(0.01f, 0.01f);
        rightRayObject.material.color = Color.red;


        //左手コントローラーのレイキャスト
        Ray leftRay = new Ray(leftHandAnchor.transform.position, leftHandAnchor.transform.forward);
        RaycastHit leftHit;

        //右手コントローラーのレイキャスト
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

    //コントローラーのレイを更新する関数
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

        //レイがオブジェクトにヒットしている場合、色を変更
        if (rayHit && (hit.collider.tag == "Key" || hit.collider.tag == "Axe" || hit.collider.tag == "torch"))
        {
            rayObject.material.color = Color.blue;
        }

        //トリガーボタンが押されているか更新
        triggerPressed = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, handAnchor == leftHandAnchor ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch);
    }

    //オブジェクトを掴む処理を行う関数
    void TryGrabObject(GameObject handAnchor,LineRenderer rayObject,bool triggerPressed)
    {
        Ray handRay = new Ray(handAnchor.transform.position,handAnchor.transform.forward);
        RaycastHit hit;

        //トリガーボタンが押されており、レイがオブジェクトにヒットしている場合
        if (triggerPressed && Physics.Raycast(handRay,out hit,3.0f))
        {
            //オブジェクトがKeyである場合
            if (hit.collider.tag == "Key")
            {
                //オブジェクトを掴む
                grabbedObject = hit.collider.gameObject;
                grabbedObject.transform.SetParent(handAnchor.transform,true);
                grabbedObject.transform.localPosition = Vector3.zero;
                grabbedObject.transform.localRotation = Quaternion.identity;
            }
        }
    }

    //掴んだオブジェクトを処理する関数
    void HandleGrabbedObject(GameObject handAnchor, LineRenderer rayObject,ref bool triggerPressed)
    {
        Ray handRay = new Ray(handAnchor.transform.position, handAnchor.transform.forward);
        RaycastHit hit;
        //掴んでるオブジェクトが存在する場合
        if (grabbedObject != null && grabbedObject.tag == "Key")
        {
            //掴んでるオブジェクトがKeyである場合
            if (Physics.Raycast(handRay,out hit,3.0f) && hit.collider.tag == "Axe" && triggerPressed)
            {
                //オブジェクトを手に追従させる
                grabbedObject = hit.collider.gameObject;
                grabbedObject.transform.SetParent(handAnchor.transform,true);
                grabbedObject.transform.localPosition = Vector3.zero;
                grabbedObject.transform.localRotation = Quaternion.identity;
            }
        }
        //レイがオブジェクトにヒットし、そのタグがtorchで、かつトリガーボタンが押されている場合
        else if (Physics.Raycast(handRay,out hit,3.0f) && hit.collider.tag == "torch" && triggerPressed)
        {
            //オブジェクトを手に追従させ、回転
            grabbedObject = hit.collider.gameObject;
            grabbedObject.transform.SetParent(handAnchor.transform,true);
            grabbedObject.transform.localPosition = Vector3.zero;
            grabbedObject.transform.localRotation = Quaternion.identity;
        }
    }
}
