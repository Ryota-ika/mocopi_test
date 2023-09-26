using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextMeshProBackGround : MonoBehaviour
{
    public float paddingTop;
    public float paddingBottom;
    public float paddingLeft;
    public float paddingRight;

    public Material material;

    private GameObject background;

    private TextMeshProUGUI textMeshProUGUI;
    // Start is called before the first frame update
    void Start()
    {
        this.textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        this.background.name = "background";
        this.background.transform.Rotate(-90,0,0);
        this.background.transform.SetParent(this.transform);
        if (material != null)
        {
            this.background.GetComponent<MeshRenderer>().material = material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var bounds = this.textMeshProUGUI.bounds;

        //•`‰æˆÊ’u‚ÌŒvŽZ
        {
            var pos = bounds.center;
            var hoseiX = -(paddingLeft / 2) + (paddingRight / 2);
            var hoseiY = -(paddingBottom / 2) + (paddingTop / 2);
            var hoseiZ = 0.01f;
            this.background.transform.localPosition = new Vector3(pos.x + hoseiX,pos.y + hoseiY,pos.z + hoseiZ);
        }

        //•`‰æƒTƒCƒY‚ÌŒvŽZ
        {
            var scale = bounds.extents;
            var hoseiW = (paddingLeft + paddingRight) / 10;
            var hoseiH = (paddingTop + paddingBottom) / 10;
            this.background.transform.localScale = new Vector3((scale.x / 10 * 2) * hoseiW, 1, (scale.y / 10 * 2) * hoseiH);
        }
    }
}
