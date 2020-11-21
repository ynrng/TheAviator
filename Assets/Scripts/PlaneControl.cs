using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]// required component on the gameObject

// Use a separate gameplayInput component for setting up input.
public class PlaneControl : MonoBehaviour {

    public int tmp = 4;
    public float speed = .2f;
    // public GameObject projectile;

    // private bool m_Charging;

    public int forcePumpL = 4;
    private Rigidbody rb;

    private float horizontal = 0f;
    private float vertical = 0f;

    private Vector3 originPos = new Vector3(-0.6f, 2.2f, 0);
    private Vector3 flyingPos = new Vector3(-0.6f, 3.6f, 0);

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        updateRise();
    }

    // Update is called once per frame
    void Update()
    {

        switch (Aviator.status) {
            case AviatorStates.Start:
                updateRise();
                break;
            case AviatorStates.Flying:
                updateFlying();
                break;
            case AviatorStates.Falling:
                updateFall();
                break;
        }

    }

    void OnTriggerEnter(Collider other)
    {
        // todo uncomment
        // if (other.gameObject.tag == "stone") {
        //     Aviator.energy -= 10;
        //     rb.freezeRotation = true;
        //     // rb.AddForce(-forcePumpL, -forcePumpD, 0, ForceMode.Impulse);
        //     rb.AddExplosionForce(forcePumpL, other.transform.position, 1, -1, ForceMode.Impulse);
        //     rb.freezeRotation = false;
        //     Destroy(other.gameObject);
        // }

        // if (other.gameObject.tag == "coin") {
        //     // Interface.score += 10;
        //     Destroy(other.gameObject);
        // }

    }

    float normalize(float v, float vmin, float vmax, float tmin, float tmax)
    {
        var nv = Mathf.Clamp(v, vmin, vmax);
        var pc = (nv - vmin) / (vmax - vmin);
        var dt = tmax - tmin;
        var tv = tmin + (pc * dt);
        return tv;
    }
    void updateFlying()
    {

        // boundaries
        float targetY = normalize(Aviator.mousePos.y, -.75f, .75f, Aviator.planeDefaultHeight - Aviator.planeAmpHeight, Aviator.planeDefaultHeight + Aviator.planeAmpHeight);
        float targetX = normalize(Aviator.mousePos.x, -1f, 1f, -Aviator.planeAmpWidth * .7f, -Aviator.planeAmpWidth);

        // game.planeCollisionDisplacementX += game.planeCollisionSpeedX;
        // targetX += game.planeCollisionDisplacementX;

        // game.planeCollisionDisplacementY += game.planeCollisionSpeedY;
        // targetY += game.planeCollisionDisplacementY;

        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(targetX, targetY, 0),
            Time.deltaTime * Aviator.planeMoveSensivity
        );

        transform.rotation = Quaternion.FromToRotation(
            new Vector3(targetX, targetY, transform.position.y - targetY),
            transform.position
        );

        // // update velocity
        // rb.velocity = new Vector3(horizontal, vertical, 0) * speed;
        // transform.Rotate((Vector3)(Vector2.Lerp(transform.position, new Vector2(horizontal, vertical), 10f)));
    }

    #region public methods
    public void updateRise()
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        transform.position = new Vector3(
            -Aviator.planeAmpWidth,
            -Aviator.planeDefaultHeight,
            0
        );
        transform.rotation = Quaternion.identity;
    }

    public void updateFall()
    {
        if (transform.position.y <= -Aviator.planeDefaultHeight) {
            Aviator.status = AviatorStates.Start;
        } else {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(20, 20, -45),
                Time.deltaTime * 2
            );
            rb.AddForce(Vector3.down * 200, ForceMode.Force);
        }
    }
    #endregion
}
