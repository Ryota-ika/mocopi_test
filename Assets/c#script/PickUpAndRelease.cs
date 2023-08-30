//2023.8.9
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.WSA;

public class PickUpAndRelease : MonoBehaviour
{
    public GameObject rightHandAnchor;
    public float minRequiredSpeed = 5.0f;
    [SerializeField] GameObject leftHandAnchor;
    [SerializeField] LineRenderer rayObject;

    [SerializeField] private TresureChest tresureChest;
    private bool canGrabKey = true;
    public bool isBoxOpened = false;

    private GameObject grabbedObject = null;
    private GameObject chestHinge;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        chestHinge = GameObject.Find("Chest_Hinge");
        animator = chestHinge.GetComponent<Animator>();
    }

    private IEnumerator DelaydMethodCoroutine(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        tresureChest.OpenLid();
    }

    // Update is called once per frame
    void Update()
    {
        rayObject.SetVertexCount(2); //始点と終点設定
        rayObject.SetPosition(0, leftHandAnchor.transform.position); //0番目の頂点を左手コントローラの位置に設定
        //1番目の頂点を左手コントローラの位置から100m先に設定
        rayObject.SetPosition(1, leftHandAnchor.transform.position + leftHandAnchor.transform.forward * 3.0f);

        rayObject.SetWidth(0.01f, 0.01f); //線の太さを0.01に設定

        //if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        //{
        //    if (!isBoxOpened)
        //    {
        //        RaycastHit hit;
        //        //レイキャストを発射してヒットしたオブジェクトを取得
        //        if (Physics.Raycast(leftHandAnchor.transform.position,leftHandAnchor.transform.forward,out hit,100.0f))
        //        {
        //            if (hit.collider.tag == "Key" && canGrabKey)
        //            {
        //                canGrabKey = false;
        //                grabbedObject = hit.collider.gameObject;
        //                grabbedObject.transform.parent = leftHandAnchor.transform;
        //                grabbedObject.transform.position = rayObject.transform.position;
        //                //isBoxOpened = true;
        //            }
        //            else if (isBoxOpened == true)
        //            {
        //                isBoxOpened = true;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        if (grabbedObject != null && grabbedObject.tag == "Key")
        //        {
        //            RaycastHit hit;
        //            //ボックスが開かれた後、、別のオブジェクトに対してレイキャスト
        //            if (Physics.Raycast(leftHandAnchor.transform.position,leftHandAnchor.transform.forward,out hit,100.0f))
        //            {
        //                if (hit.collider.tag == "Axe")
        //                {
        //                    hit.collider.transform.parent = leftHandAnchor.transform;
        //                    hit.collider.transform.position = rayObject.transform.position;
        //                    //grabbedObject = null;//もう一度鍵を掴むための準備
        //                }
        //            }

        //    }
        //}
        //}

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            RaycastHit hit;
            //レイキャストを発射してヒットしたオブジェクトを取得
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
                        tresureChest.OpenLid();
                        //hit.collider.gameObject.SetActive(false);
                        //float delayTime = 3.0f;
                        //StartCoroutine(DelaydMethodCoroutine(delayTime));
                        isBoxOpened = true;
                    }
                }
                else
                {
                    if (grabbedObject != null && grabbedObject.tag == "Key")
                    {
                        if (hit.collider.tag == "Axe")
                        {
                            //grabbedObject = null;
                            hit.collider.transform.parent = leftHandAnchor.transform;
                            hit.collider.transform.position = rayObject.transform.position;
                            //grabbedObject = null;//もう一度鍵を掴むための準備
                        }
                        Debug.Log(hit.collider.tag);

                    }
                }

            }
        }
    }  
}
