//2023.8.9
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAndRelease : MonoBehaviour
{
    public GameObject rightHandAnchor;

    [SerializeField] GameObject leftHandAnchor;
    [SerializeField] LineRenderer rayObject;

    public TresureChest tresureChest;
    private bool canGrabKey = true;
    public bool isBoxOpened = false;

    private GameObject grabbedObject = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        rayObject.SetVertexCount(2); //始点と終点設定
        rayObject.SetPosition(0, leftHandAnchor.transform.position); //0番目の頂点を左手コントローラの位置に設定
        //1番目の頂点を左手コントローラの位置から100m先に設定
        rayObject.SetPosition(1, leftHandAnchor.transform.position + leftHandAnchor.transform.forward * 100.0f);

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
            if (Physics.Raycast(leftHandAnchor.transform.position, leftHandAnchor.transform.forward, out hit, 100.0f))
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
                    }
                    else if (hit.collider.tag=="Box")
                    {
                        isBoxOpened = true;

                        hit.collider.enabled = false;
                    }
                }
                else
                {
                    if (grabbedObject != null /*&& grabbedObject.tag == "Key"*/)
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
    private void LateUpdate()
    {
        /*rayObject.SetVertexCount(2); //始点と終点設定
        rayObject.SetPosition(0, leftHandAnchor.transform.position); //0番目の頂点を左手コントローラの位置に設定
        //1番目の頂点を左手コントローラの位置から100m先に設定
        rayObject.SetPosition(1, leftHandAnchor.transform.position + leftHandAnchor.transform.forward * 100.0f);

        rayObject.SetWidth(0.01f, 0.01f); //線の太さを0.01に設定
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(leftHandAnchor.transform.position, leftHandAnchor.transform.forward, 100.0f);
            foreach (var hit in hits)
            {
                if (hit.collider.tag == "Key" *//*|| hit.collider.tag == "Axe"*//*)
                {
                    hit.collider.transform.parent = leftHandAnchor.transform;
                    hit.collider.transform.position = rayObject.transform.position;
                    break;
                }
            }
        }*/


        //引いた時の処理
        /*if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger, OVRInput.Controller.Touch))
        {
            RaycastHit[] hits;
            //右手の位置から前方に0.01半径の球体キャストを行い、当たったオブジェクトをRaycastHit配列で取得
            hits = Physics.SphereCastAll(rightHandAnchor.transform.position, 0.01f, Vector3.forward);
            //球体キャストで当たったオブジェクトの処理
            foreach(var hit in hits)
            {
                if (hit.collider.CompareTag("Key"))
                {
                    hit.collider.transform.parent = rightHandAnchor.transform;
                    hit.collider.transform.GetComponent<Rigidbody>().useGravity = false;
                    hit.collider.transform.GetComponent<Rigidbody>().isKinematic = true;
                    break;
                }
            }
        }*/

        //離した時の処理
        /*if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger,OVRInput.Controller.Touch))
        {
            for (int i = 0; i < rightHandAnchor.transform.childCount; i++)
            {
                var child = rightHandAnchor.transform.GetChild(i);
                if (child.CompareTag("Key"))
                {
                    child.parent = null;
                    child.GetComponent<Rigidbody>().useGravity = true;
                    child.GetComponent <Rigidbody>().isKinematic = false;
                }
            }
        }*/
    }
}
