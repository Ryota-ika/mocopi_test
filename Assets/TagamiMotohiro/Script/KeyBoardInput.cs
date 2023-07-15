using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OculusSampleFramework;

public class KeyBoardInput : MonoBehaviour
{
    TouchScreenKeyboard keyboard;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            keyboard = TouchScreenKeyboard.Open("",TouchScreenKeyboardType.Default);
            Debug.Log(keyboard);
        }
    }
}
