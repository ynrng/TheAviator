using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DrawScene : MonoBehaviour {
    // Start is called before the first frame update
    public bool useFog = true;
    void Start()
    {
        // SceneView.sceneViews.
        // Scene scene = SceneManager.GetActiveScene();

        // bg
        // GameObject goSky = GameObject.FindGameObjectWithTag("sky");

        //fog
        if (useFog) {
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogColor = AviatorColors.Fog;
            RenderSettings.fogStartDistance = 100;
            RenderSettings.fogEndDistance = 1000;
            RenderSettings.fog = useFog;  // for debug;
        }
        RenderSettings.ambientLight = AviatorColors.Light;
        QualitySettings.shadowDistance = 1500;
        QualitySettings.antiAliasing = 8;

        Camera camera = gameObject.GetComponent<Camera>();

        // if (camera) {
        camera.transform.position = new Vector3(0, 100, -200); // coordinate system diff
        camera.orthographic = false;
        camera.fieldOfView = 60;
        // camera.aspect =
        camera.nearClipPlane = 1;
        camera.farClipPlane = 1000;
        camera.clearFlags = CameraClearFlags.SolidColor;
        camera.backgroundColor = Color.clear;
        // }

        // 	// Activate the anti-aliasing; this is less performant,
        // 	// but, as our project is low-poly based, it should be fine :)
        // 	antialias: true

        // // Enable shadow rendering
        // renderer.shadowMap.enabled = true;

        // // Listen to the screen: if the user resizes it
        // // we have to update the camera and the renderer size
        // window.addEventListener('resize', handleWindowResize, false);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
