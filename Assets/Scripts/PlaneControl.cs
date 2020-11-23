using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]// required component on the gameObject
public class PlaneControl : MonoBehaviour {

    public int tmp = 4;//todo delete
    private Rigidbody rb;
    private BoxCollider bc;
    new Camera camera;

    // Start is called before the first frame update
    void Start()
    {

        transform.localScale = Vector3.one * .25f;

        rb = GetComponent<Rigidbody>();
        rb.drag = .5f; // so it will stop after hit a stone;
        rb.useGravity = false;

        bc = GetComponent<BoxCollider>();
        // use simple collider instead of mesh to avoid extra cpu use
        bc.center = new Vector3(10, 0, 0);//approximately
        bc.size = new Vector3(120, 70, 80);//approximately

        camera = Camera.main;

        resetTransform();
    }

    // Update is called once per frame
    void Update()
    {

        switch (Aviator.status) {
            case AviatorStates.Start:
                resetTransform();
                break;
            case AviatorStates.Flying:
                updateFlying();
                break;
            case AviatorStates.Falling:
                updateFall();
                break;
        }

    }

    void updateFlying()
    {

        // boundaries
        float targetY = Aviator.normalize(Aviator.mousePos.y, -.75f, .75f, Aviator.planeDefaultHeight - Aviator.planeAmpHeight, Aviator.planeDefaultHeight + Aviator.planeAmpHeight);
        float targetX = Aviator.normalize(Aviator.mousePos.x, -1f, 1f, -Aviator.planeAmpWidth * .7f, -Aviator.planeAmpWidth);

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

        updateCamera();
    }

    void updateCamera()
    {
        camera.fieldOfView = Aviator.normalize(Aviator.mousePos.x, -1, 1, 40, 60);
        camera.ResetProjectionMatrix();
        camera.transform.position += Vector3.up * ((transform.position.y - camera.transform.position.y) * Aviator.cameraSensivity * Time.deltaTime);
    }

    void resetTransform()
    {
        // so it will always stay on the same plane to be able to hit stones and coins;
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        rb.velocity = Vector3.zero;

        transform.position = new Vector3(
            -Aviator.planeAmpWidth,
            -Aviator.planeDefaultHeight,
            0
        );
        transform.rotation = Quaternion.identity;
    }

    void updateFall()
    {
        if (transform.position.y <= -Aviator.planeDefaultHeight) {
            Aviator.status = AviatorStates.Start;
        } else {
            transform.rotation = Quaternion.Lerp(
                transform.rotation,
                Quaternion.Euler(20, 20, -45),
                Time.deltaTime * 2
            );
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.AddForce(Vector3.down * 200, ForceMode.Force);
        }
    }

}
