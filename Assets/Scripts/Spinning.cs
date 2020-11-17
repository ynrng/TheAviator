using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Spinning : MonoBehaviour
{
    // Start is called before the first frame update
    public int spinSpeed = 30;
    public int spinAroundSpeed = 20;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -600 && transform.position.x < 0) {
            Destroy(gameObject);
        } else {
            transform.Rotate(Vector3.one * spinSpeed * Random.value * Time.deltaTime);
            // transform.RotateAround(Vector3.zero, Vector3.forward, spinAroundSpeed * Time.deltaTime);
            transform.RotateAround(Vector3.down * 600, Vector3.forward, spinAroundSpeed * Time.deltaTime);
        }
    }
}
