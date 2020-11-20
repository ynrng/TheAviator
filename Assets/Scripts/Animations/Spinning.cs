using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Spinning : MonoBehaviour
{
    // Start is called before the first frame update
    public int spinSpeed = 30;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -Aviator.seaRadius && transform.position.x < 0) {
            Destroy(gameObject);
        } else {
            if (spinSpeed > 0) {
                transform.Rotate(Vector3.one * spinSpeed * Random.value * Time.deltaTime);
            }
            // transform.RotateAround(Vector3.zero, Vector3.forward, spinAroundSpeed * Time.deltaTime);
            transform.RotateAround(Vector3.down * Aviator.seaRadius, Vector3.forward, Aviator.SpinningSpeed * Time.deltaTime);
        }
    }
}
