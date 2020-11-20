
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

[RequireComponent(typeof(Rigidbody))]// required component on the gameObject
public class PlaneControl : MonoBehaviour
{
    public int speed = 5;
    public int forcePumpL = 4;
    public int tmp = 4;
    private Rigidbody rb;

    private float horizontal = 0f;
    private float vertical = 0f;

    private Vector3 originPos = new Vector3(-0.6f, 2.2f, 0);
    private Vector3 flyingPos = new Vector3(-0.6f, 3.6f, 0);

    private Quaternion originRot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ;

        originRot = transform.rotation;
        transform.position = originPos;
        // originPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        switch (Aviator.status) {
            case AviatorStates.Rising:
                rise();
                break;
            case AviatorStates.Flying:
                updateFlying();
                break;
            case AviatorStates.Falling:
                fall();
                break;
        }

    }

    void updateFlying()
    {
        // legacy input sys
        // GetAxisRaw has changes from 0 to 1 or -1 immediately, so with no steps.
        // if (Input.GetAxisRaw("Vertical") != 0) {
        //     vertical = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        // }

        // else if (Input.GetAxisRaw("Mouse Y") != 0) {
        //     vertical = Input.GetAxis("Mouse Y") * speed * 5 * Time.deltaTime;
        // }
        // else {

        if (Touch.activeTouches.Count == 1 && Touch.activeTouches[0].isInProgress) {
            rb.useGravity = false;
            vertical = Touch.activeTouches[0].delta.y * speed * Time.deltaTime;
        } else {
            // to stop when press released
            rb.useGravity = true;
            vertical = 0f;
        }

        if (transform.position.x != originPos.x) {
            horizontal = originPos.x - transform.position.x;
        }

        // boundaries
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -1.3f, 0f),
                            Mathf.Clamp(transform.position.y, 3.2f, 4.5f),
                            0);

        // update velocity
        rb.velocity = new Vector3(horizontal, vertical, 0);
        // rb.rotation = originRot;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "stone") {
            Aviator.energy -= 10;
            rb.freezeRotation = true;
            // rb.AddForce(-forcePumpL, -forcePumpD, 0, ForceMode.Impulse);
            rb.AddExplosionForce(forcePumpL, other.transform.position, 1, -1, ForceMode.Impulse);
            rb.freezeRotation = false;
            Destroy(other.gameObject);
        }

        if (other.gameObject.tag == "coin") {
            // Interface.score += 10;
            Destroy(other.gameObject);
        }

    }

    #region public methods
    public void rise()
    {
        if (transform.position.y > flyingPos.y) {
            rb.velocity = Vector3.zero;
            transform.position = flyingPos;
            Aviator.status = AviatorStates.Flying;
        } else {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            rb.AddForce(Vector3.up * 6);
        }
    }

    public void fall()
    {
        // rb.velocity = Vector3.zero;
        rb.useGravity = false;
        if (transform.position.y < originPos.y) {

            rb.velocity = Vector3.zero;
            transform.position = originPos;
            Aviator.status = AviatorStates.Start;

            // transform.position = new Vector3(originPos.x, );

        } else {
            // rb.constraints = RigidbodyConstraints.FreezePositionX;
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            rb.AddForce(Vector3.down * 6);

            // rb.AddTorque(new Vector3(0, 0, 45), ForceMode.Acceleration);

            // new WaitForSeconds(1);

        }
    }
    #endregion

}
