using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
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

    void appendParticleSystem()
    {

        ParticleSystem ps = gameObject.AddComponent<ParticleSystem>();
        if (ps != null) {

            ps.Stop(); // Cannot set duration whilst Particle System is playing

            var main = ps.main;
            main.duration = .1f;
            main.loop = false;
            main.startLifetime = .4f;
            main.startSpeed = 10;
            main.startSize = new ParticleSystem.MinMaxCurve(.2f, .3f);
            main.startRotation = new ParticleSystem.MinMaxCurve(0, 90);
            main.playOnAwake = false;

            var emission = ps.emission;
            emission.enabled = true;
            emission.rateOverTime = 150;

            var shape = ps.shape;
            shape.enabled = true;
            shape.shapeType = ParticleSystemShapeType.Sphere;
            shape.radius = .0001f;
            shape.radiusThickness = 0;

            var size = ps.sizeOverLifetime;
            size.enabled = true;
            size.size = new ParticleSystem.MinMaxCurve(1, AnimationCurve.Linear(0, 1, 1, 0));

            ParticleSystemRenderer renderer = gameObject.GetComponent<ParticleSystemRenderer>();
            renderer.enabled = true;
            renderer.renderMode = ParticleSystemRenderMode.Mesh;
            renderer.mesh = Resources.GetBuiltinResource<Mesh>("Cube.fbx");

            // renderer.material = PrimitiveHelper.GetMaterialStandard();
            if (gameObject.tag == "stone") {
                renderer.material.color = AviatorColors.Red;
            } else if (gameObject.tag == "coin") {
                renderer.material.color = AviatorColors.Green;
            }

            ps.Play();

            Destroy(gameObject, ps.main.duration + ps.main.startLifetime.constantMax);

        } else {
            //FIXME why sometimes the instance is null
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "airplane") {

            spinAroundSpeed = 0;

            // ParticleSystem particleSystem = gameObject.GetComponent<ParticleSystem>();
            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
            meshRenderer.enabled = false;

            // particleSystem.Play();
            appendParticleSystem();

            if (gameObject.tag == "stone") {

                Aviator.energy -= Aviator.ennemyValue;
                Aviator.energy = Mathf.Max(0, Aviator.energy);
                if (Aviator.energy > 0) {
                    other.attachedRigidbody.AddExplosionForce(forcePumpL, transform.position, 1, -1, ForceMode.Impulse);
                }
            } else if (gameObject.tag == "coin") {

                Aviator.energy += Aviator.coinValue;
                Aviator.energy = Mathf.Min(Aviator.energy, 100);

            }

        }

    }
}
