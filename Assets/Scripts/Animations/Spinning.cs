using System.Collections;
using System.Collections.Generic;
using UnityEditor.iOS;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Spinning : MonoBehaviour
{
    bool isSpinningStopped = false;
    int forcePumpL = 10;//todo use common var
    List<GameObject> inUseGos = new List<GameObject>();//todo reduce reproduce new go
    ParticleSystem ps;
    void Start()
    {
        ps = gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -Aviator.seaRadius && transform.position.x < 0) {
            Destroy(gameObject);
        } else {
            float spinAroundSpeed = isSpinningStopped ? 0 : Aviator.speed;//* Time.deltaTime; // todo should be 20
            transform.Rotate(Vector3.one * spinAroundSpeed * Random.value * 3); // 3 is a scaler for the animation to show
            transform.RotateAround(Vector3.down * Aviator.seaRadius, Vector3.forward, spinAroundSpeed);
        }
    }
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "airplane") {
            isSpinningStopped = true;

            MeshRenderer meshRenderer = gameObject.GetComponent<MeshRenderer>();
            meshRenderer.enabled = false; // hide the original object then display the particles

            appendParticleSystem();

            if (gameObject.tag == "stone") {
                Aviator.removeEnergy();
                other.attachedRigidbody?.AddExplosionForce(forcePumpL, transform.position, 1, -1, ForceMode.Impulse);
            } else if (gameObject.tag == "coin") {
                Aviator.addEnergy();
            }

        }

    }

    void appendParticleSystem()
    {
        if (ps == null) {
            ps = gameObject.AddComponent<ParticleSystem>();
        }

        if (ps != null) {

            ps.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear); // Cannot set duration whilst Particle System is playing

            var main = ps.main;
            ps.name = "particle";
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
            //TODO why sometimes the instance is null
            Destroy(gameObject);
        }
    }

}
