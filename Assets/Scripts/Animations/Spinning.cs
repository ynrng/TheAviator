using UnityEngine;

public class Spinning : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -Aviator.seaRadius && transform.position.x < 0) {
            Destroy(gameObject);
        } else {
            transform.Rotate(Vector3.one * Aviator.speed * Random.value * 3); // 3 is a scaler for the animation to show
            transform.RotateAround(Vector3.down * Aviator.seaRadius, Vector3.forward, Aviator.speed);
        }
    }
}
