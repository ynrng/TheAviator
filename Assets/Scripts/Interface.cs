using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Text;
using System.Linq;

public class Interface : MonoBehaviour
{

    private Text texts;
    StringBuilder text = new StringBuilder();
    bool needUpdate = false;
    void Start()
    {
        texts = GetComponentInChildren<Text>();
        buildString();
    }

    // Update is called once per frame
    void Update()
    {
        switch (Aviator.status) {
            case AviatorStates.Start:
                if (needUpdate) {
                    text.Append("CLICK TO REPLAY");
                    needUpdate = false;
                }
                break;
            case AviatorStates.Flying:
                buildString();
                break;
        }
        texts.text = text.ToString();

        // if (Aviator.status == AviatorStates.Start) {

        //     // generator.startCoroutines();

        //     // new WaitForSeconds(1);

        //     Aviator.status = AviatorStates.Rising;

        //     // plane.rise();
        // }
        // if (Aviator.status == AviatorStates.Flying && Aviator.energy <= 0) {
        //     Aviator.status = AviatorStates.Falling;
        //     // generator.stopCoroutines();
        //     // plane.fall();
        // }
    }

    void buildString()
    {
        text.Clear();
        text.Append(String.Format("Level {0}\n", Aviator.level));
        text.Append(String.Format("Distance {0}\n", Aviator.distance));
        text.Append(String.Format("Energy {0}\n", Aviator.energy));
        needUpdate = true;
    }

}
