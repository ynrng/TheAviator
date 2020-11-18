using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PilotHair : MonoBehaviour {

    public int index;
    // Start is called before the first frame update
    float angleHairs;
    Vector3 originScale;
    void Start()
    {
        originScale = gameObject.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localScale = originScale + Vector3.up * (.75f + Mathf.Cos(angleHairs + index / 3) * .25f - 1) * originScale.y;
        angleHairs += 10f * Time.deltaTime; // 0.16 per frame
    }

}
