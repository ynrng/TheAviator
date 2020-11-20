using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using System.Text;
using System.Linq;

public class Interface : MonoBehaviour
{

    private Text texts;
    private Generator generator;
    private PlaneControl plane;

    // Start is called before the first frame update
    void Start()
    {
        // initVars();
        texts = GetComponentInChildren<Text>();
        // generator = GameObject.Find("Generators").GetComponent<Generator>();
        // plane = transform.parent.gameObject.GetComponentInChildren<PlaneControl>();
    }

    // Update is called once per frame
    void Update()
    {
        // texts[0].text = String.Format("Level {0}, Score {1}/{2}", level, score, 100);
        // texts[1].text = String.Format("Level {0}, Score {1}/{2}", level, score, 100);

        StringBuilder text = new StringBuilder();
        text.Append(String.Format("Level {0}, Score {1}/{2}\n", Aviator.level, " ", 100));
        text.Append(String.Format("Energy {0}/{1}\n", Aviator.energy, 100));

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

}
