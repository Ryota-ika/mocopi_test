using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TresureChest : MonoBehaviour
{
    public float openAngle = 90f;
    // Start is called before the first frame update
   

    public void OpenLid()
    {
        Quaternion targetRotation = Quaternion.Euler(0f, openAngle, 0f);
        transform.rotation = targetRotation;
    }

    public void CloseLid()
    {
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, 0f);
        transform.rotation = targetRotation;
    }
}
