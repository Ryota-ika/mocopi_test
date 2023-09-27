using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRPositionRock : MonoBehaviour
{
    [SerializeField]
    Transform anchor;
    [SerializeField]
    Transform rightHandAnchor;
    [SerializeField]
    Transform leftHandAnchor;
    Vector3 rightCorectionValue;
    Vector3 leftCorectionValue;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 anchorPos = new Vector3(anchor.position.x, transform.position.y, anchor.position.z);
        transform.position = anchorPos;
    }
}
