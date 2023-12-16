//2023.12.16 ϋό΄ΑΎ
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
    [Header("Rayͺ½·ιC[")]
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
        //RbγΙΤ·
        yield return new WaitForSeconds(delayTime);

        Destroy(Animated_Chest_01);
        Animated_Chest_01.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateRay(leftHandAnchor,leftRayObject,ref leftTriggerPressed);
        UpdateRay(rightHandAnchor,rightRayObject,ref rightTriggerPressed);

        //σ ͺJ’Δ’Θ’κ
        if (!isBoxOpened)
        {
            //Άθ©EθΕIuWFNgπΝή
            TryGrabObject(leftHandAnchor,leftRayObject,leftTriggerPressed);
            TryGrabObject(rightHandAnchor,rightRayObject,rightTriggerPressed);
        }
        else
        {
            //ΝρΎIuWFNgπ
            HandleGrabbedObject(leftHandAnchor,leftRayObject,ref leftTriggerPressed);
            HandleGrabbedObject(rightHandAnchor,rightRayObject,ref rightTriggerPressed);
        }

        //t@N^OOΜR[h
        /*leftRayObject.SetVertexCount(2); //n_ΖI_έθ
        leftRayObject.SetPosition(0, leftHandAnchor.transform.position); //0ΤΪΜΈ_πΆθRg[ΜΚuΙέθ
        //1ΤΪΜΈ_πΆθRg[ΜΚu©η3mζΙέθ
        leftRayObject.SetPosition(1, leftHandAnchor.transform.position + leftHandAnchor.transform.forward * 3.0f);
        leftRayObject.SetWidth(0.01f, 0.01f); //όΜΎ³π0.01Ιέθ
        leftRayObject.material.color = Color.red;

        rightRayObject.SetVertexCount(2);
        rightRayObject.SetPosition(0, rightHandAnchor.transform.position);
        rightRayObject.SetPosition(1, rightHandAnchor.transform.position + rightHandAnchor.transform.forward * 3.0f);
        rightRayObject.SetWidth(0.01f, 0.01f);
        rightRayObject.material.color = Color.red;


        //ΆθRg[[ΜCLXg
        Ray leftRay = new Ray(leftHandAnchor.transform.position, leftHandAnchor.transform.forward);
        RaycastHit leftHit;

        //EθRg[[ΜCLXg
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

    //Rg[[ΜCπXV·ιΦ
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

        //CͺIuWFNgΙqbg΅Δ’ικAFπΟX
        if (rayHit && (hit.collider.tag == "Key" || hit.collider.tag == "Axe" || hit.collider.tag == "torch"))
        {
            rayObject.material.color = Color.blue;
        }

        //gK[{^ͺ³κΔ’ι©XV
        triggerPressed = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, handAnchor == leftHandAnchor ? OVRInput.Controller.LTouch : OVRInput.Controller.RTouch);
    }

    //IuWFNgπΝήπs€Φ
    void TryGrabObject(GameObject handAnchor,LineRenderer rayObject,bool triggerPressed)
    {
        Ray handRay = new Ray(handAnchor.transform.position,handAnchor.transform.forward);
        RaycastHit hit;

        //gK[{^ͺ³κΔ¨θACͺIuWFNgΙqbg΅Δ’ικ
        if (triggerPressed && Physics.Raycast(handRay,out hit,3.0f))
        {
            //IuWFNgͺKeyΕ ικ
            if (hit.collider.tag == "Key")
            {
                //IuWFNgπΝή
                grabbedObject = hit.collider.gameObject;
                grabbedObject.transform.SetParent(handAnchor.transform,true);
                grabbedObject.transform.localPosition = Vector3.zero;
                grabbedObject.transform.localRotation = Quaternion.identity;
            }
        }
    }

    //ΝρΎIuWFNgπ·ιΦ
    void HandleGrabbedObject(GameObject handAnchor, LineRenderer rayObject,ref bool triggerPressed)
    {
        Ray handRay = new Ray(handAnchor.transform.position, handAnchor.transform.forward);
        RaycastHit hit;
        //ΝρΕιIuWFNgͺΆέ·ικ
        if (grabbedObject != null && grabbedObject.tag == "Key")
        {
            //ΝρΕιIuWFNgͺKeyΕ ικ
            if (Physics.Raycast(handRay,out hit,3.0f) && hit.collider.tag == "Axe" && triggerPressed)
            {
                //IuWFNgπθΙΗ]³Ήι
                grabbedObject = hit.collider.gameObject;
                grabbedObject.transform.SetParent(handAnchor.transform,true);
                grabbedObject.transform.localPosition = Vector3.zero;
                grabbedObject.transform.localRotation = Quaternion.identity;
            }
        }
        //CͺIuWFNgΙqbg΅A»Μ^OͺtorchΕA©ΒgK[{^ͺ³κΔ’ικ
        else if (Physics.Raycast(handRay,out hit,3.0f) && hit.collider.tag == "torch" && triggerPressed)
        {
            //IuWFNgπθΙΗ]³ΉAρ]
            grabbedObject = hit.collider.gameObject;
            grabbedObject.transform.SetParent(handAnchor.transform,true);
            grabbedObject.transform.localPosition = Vector3.zero;
            grabbedObject.transform.localRotation = Quaternion.identity;
        }
    }
}
