using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]// required component on the gameObject
public class PlaneControl : MonoBehaviour {

    public int tmp = 4;
    public float speed = .2f;
    // public gameObject projectile;

    // private bool m_Charging;

    public int forcePumpL = 10;
    private Rigidbody rb;
    private BoxCollider bc;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        rb.drag = .5f;

        bc = GetComponent<BoxCollider>();
        bc.center = new Vector3(10, 0, 0);
        bc.size = new Vector3(120, 70, 80);

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

        float horizontal = targetX - transform.position.x;
        float vertical = targetY - transform.position.y;

        transform.position = Vector3.Lerp(
            transform.position,
            new Vector3(targetX, targetY, 0),
            Time.deltaTime * Aviator.planeMoveSensivity
        );

        transform.rotation = Quaternion.FromToRotation(
            new Vector3(targetX, targetY, transform.position.y - targetY),
            transform.position
        );

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
