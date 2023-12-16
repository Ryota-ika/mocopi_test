using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScroll : MonoBehaviour
{
    Vector3 staffRollPosition;
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private float endPos;
    // Start is called before the first frame update
    void Start()
    {
        staffRollPosition = rectTransform.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (rectTransform.anchoredPosition.y < endPos)
        {
            staffRollPosition.y += 100*Time.deltaTime;
            rectTransform.anchoredPosition = staffRollPosition;
        }
    }
}
