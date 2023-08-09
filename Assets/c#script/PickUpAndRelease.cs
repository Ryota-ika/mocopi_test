using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAndRelease : MonoBehaviour
{
    public GameObject rightHandAnchor;

    [SerializeField] GameObject leftController;
    [SerializeField] LineRenderer rayObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void LateUpdate()
    {
        rayObject.SetVertexCount(2);
        rayObject.SetPosition(0,leftController.transform.position);
        rayObject.SetPosition(1,leftController.transform.position+leftController.transform.forward*100.0f);

        rayObject.SetWidth(0.01f, 0.01f);
        if (OVRInput.GetDown(OVRInput.RawButton.Y))
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(leftController.transform.position, leftController.transform.forward, 100.0f);
            foreach (var hit in hits)
            {
                if (hit.collider.tag == "Key")
                {
                    hit.collider.transform.parent = leftController.transform;
                    hit.collider.transform.position = rayObject.transform.position;
                    break;
                }
            }
        }


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
        if (OVRInput.GetUp(OVRInput.Button.SecondaryHandTrigger,OVRInput.Controller.Touch))
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
        }
    }
}
