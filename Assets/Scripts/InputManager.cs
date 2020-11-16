using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputManager : MonoBehaviour {

    void Awake()
    {
        if (!EnhancedTouchSupport.enabled) {
            EnhancedTouchSupport.Enable();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


    }
    // static public Vector2 lastTouchDelta()
    // {
    //     if (Touch.activeTouches.Count == 1) {
    //         return Touch.activeTouches[0].delta;
    //     }
    //     // return null;
    // }

    static public bool isTapping()
    {

        if (Touch.activeTouches.Count == 1) {
            // print("isTapping");
            return Touch.activeTouches[0].isTap;
        }
        // print("notTapping");
        return false;
    }


}
