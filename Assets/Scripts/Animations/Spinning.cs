using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Spinning : MonoBehaviour
{
    int spinAroundSpeed;
    int forcePumpL = 10;

    void Start()
    {
        spinAroundSpeed = Aviator.SpinningSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -Aviator.seaRadius && transform.position.x < 0) {
            Destroy(gameObject);
        } else {
            transform.Rotate(Vector3.one * Aviator.SpinningSpeed * Random.value * Time.deltaTime);
            transform.RotateAround(Vector3.down * Aviator.seaRadius, Vector3.forward, spinAroundSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "airplane") {

            spinAroundSpeed = 0;

            ParticleSystem particleSystem = gameObject.GetComponent<ParticleSystem>();
            MeshRenderer mesh = gameObject.GetComponent<MeshRenderer>();
            mesh.enabled = false;

            particleSystem.Play();

            Destroy(gameObject, particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);

            if (gameObject.tag == "stone") {

                Aviator.energy -= Aviator.ennemyValue;
                Aviator.energy = Mathf.Max(0, Aviator.energy);
                if (Aviator.energy > 0) {
                    other.attachedRigidbody.AddExplosionForce(forcePumpL, transform.position, 1, -1, ForceMode.Impulse);
                }
            }

            if (gameObject.tag == "coin") {

                Aviator.energy += Aviator.coinValue;
                Aviator.energy = Mathf.Min(Aviator.energy, 100);

            }

        }

    }
}
