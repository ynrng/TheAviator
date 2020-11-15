using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed = 100;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < 0 && transform.position.x < 0) {
            Destroy(gameObject);
        } else {
            transform.RotateAround(Vector3.zero, Vector3.forward, speed * Time.deltaTime);
        }
    }
}
