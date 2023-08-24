//2023.8.9
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpAndRelease : MonoBehaviour
{
    public GameObject rightHandAnchor;

    [SerializeField] GameObject leftHandAnchor;
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
        rayObject.SetVertexCount(2); //�n�_�ƏI�_�ݒ�
        rayObject.SetPosition(0, leftHandAnchor.transform.position); //0�Ԗڂ̒��_������R���g���[���̈ʒu�ɐݒ�
        //1�Ԗڂ̒��_������R���g���[���̈ʒu����100m��ɐݒ�
        rayObject.SetPosition(1, leftHandAnchor.transform.position + leftHandAnchor.transform.forward * 100.0f);

        rayObject.SetWidth(0.01f, 0.01f); //���̑�����0.01�ɐݒ�
        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
            RaycastHit[] hits;
            hits = Physics.RaycastAll(leftHandAnchor.transform.position, leftHandAnchor.transform.forward, 100.0f);
            foreach (var hit in hits)
            {
                if (hit.collider.tag == "Key" /*|| hit.collider.tag == "Axe"*/)
                {
                    hit.collider.transform.parent = leftHandAnchor.transform;
                    hit.collider.transform.position = rayObject.transform.position;
                    break;
                }
            }
        }


        //���������̏���
        /*if (OVRInput.Get(OVRInput.Button.SecondaryHandTrigger, OVRInput.Controller.Touch))
        {
            RaycastHit[] hits;
            //�E��̈ʒu����O����0.01���a�̋��̃L���X�g���s���A���������I�u�W�F�N�g��RaycastHit�z��Ŏ擾
            hits = Physics.SphereCastAll(rightHandAnchor.transform.position, 0.01f, Vector3.forward);
            //���̃L���X�g�œ��������I�u�W�F�N�g�̏���
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

        //���������̏���
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
