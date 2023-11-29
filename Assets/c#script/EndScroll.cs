using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScrole : MonoBehaviour
{
    
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private float endPosition = 0.0f;
    [SerializeField]
    private float scrollSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        //staffRollPosition = rectTransform.anchoredPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (rectTransform.anchoredPosition.y < endPosition)
        {
            //エンドロールの位置を更新して、上昇させる
            Vector3 position = rectTransform.anchoredPosition;
            position.y += scrollSpeed * Time.deltaTime;
            rectTransform.anchoredPosition = position;
        }
    }
}
