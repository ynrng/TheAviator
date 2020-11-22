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

    }

    void buildString()
    {
        text.Clear();
        text.Append(String.Format("Level {0}\n", Aviator.level));
        text.Append(String.Format("Distance {0}\n", Aviator.distance));
        text.Append(String.Format("Energy {0}\n", Aviator.energy));
        needUpdate = true;

        Aviator.energy -= (int)(Aviator.speed * Time.deltaTime * Aviator.ratioSpeedEnergy);
        Aviator.energy = Mathf.Max(Aviator.energy, 0);
        if (Aviator.energy < 1) {
            Aviator.status = AviatorStates.Falling;
        }
    }

}
