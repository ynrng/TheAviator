using System;
using UnityEngine;
using UnityEngine.UI;

public class Interface : MonoBehaviour
{
    static public int score = 0;
    static public int distance = 0;
    static public int energy = 30;
    static public AviatorState state = AviatorState.Start;

    private int level = 1;
    private Text[] texts;
    private Generator generator;
    private PlaneControl plane;

    // Start is called before the first frame update
    void Start()
    {
        // initVars();
        texts = GetComponentsInChildren<Text>();
        generator = GameObject.Find("Generators").GetComponent<Generator>();
        // plane = transform.parent.gameObject.GetComponentInChildren<PlaneControl>();
    }

    // Update is called once per frame
    void Update()
    {
        texts[0].text = String.Format("Level {0}, Score {1}/{2}", level, score, 100);
        // texts[1].text = String.Format("Level {0}, Score {1}/{2}", level, score, 100);
        texts[1].text = String.Format("Energy {0}/{1}", energy, 100);

        if (state == AviatorState.Start && Input.GetButtonDown("Jump")) {

            initVars();
            generator.startCoroutines();

            // new WaitForSeconds(1);

            state = AviatorState.Rising;

            // plane.rise();
        }
        if (state == AviatorState.Flying && energy <= 0) {
            state = AviatorState.Falling;
            generator.stopCoroutines();
            // plane.fall();
        }
    }

    void initVars()
    {
        score = 0;
        distance = 0;
        energy = 10;
        // state = AviatorState.Start;
        level = 1;
    }


}
