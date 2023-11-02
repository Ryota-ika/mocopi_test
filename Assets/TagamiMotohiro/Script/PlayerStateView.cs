using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStateView : MonoBehaviour
{
    [SerializeField]
    int viewPlayerNum;
    [SerializeField]
    TextMeshProUGUI stateText;
    // Update is called once per frame
    void Update()
    {
        bool stanbyState = StartRoomKey.GetPlayerStanby(viewPlayerNum);
        if (stanbyState)
		{
            stateText.text = viewPlayerNum.ToString() + "P: €”õŠ®—¹!";
		}else
		{
            stateText.text = viewPlayerNum.ToString() + "P: €”õ’†c";
        }
    }
}
