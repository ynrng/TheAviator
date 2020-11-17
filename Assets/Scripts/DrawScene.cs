using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
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
        // RenderSettings.fogMode = FogMode.Exponential;
        // RenderSettings.fogColor = AviatorColors.Fog;
        // RenderSettings.fogStartDistance = 250;
        // RenderSettings.fogEndDistance = 700;
        // RenderSettings.fog = useFog;  // for debug;


        // Camera camera = gameObject.GetComponent<Camera>();

        // if (camera) {
        //     camera.transform.position = new Vector3(0, 200, -100); // coordinate system diff
        //     camera.orthographic = false;
        //     camera.fieldOfView = 60;
        //     // camera.aspect =
        //     camera.nearClipPlane = 1;
        //     camera.farClipPlane = 1000;
        //     camera.clearFlags = CameraClearFlags.SolidColor;
        //     camera.backgroundColor = Color.clear;
        // }

        // // Allow transparency to show the gradient background
        // // we defined in the CSS
        // alpha: true,

        // 	// Activate the anti-aliasing; this is less performant,
        // 	// but, as our project is low-poly based, it should be fine :)
        // 	antialias: true

        // // Define the size of the renderer; in this case,
        // // it will fill the entire screen
        // renderer.setSize(WIDTH, HEIGHT);

        // // Enable shadow rendering
        // renderer.shadowMap.enabled = true;

        // // Add the DOM element of the renderer to the
        // // container we created in the HTML
        // container = document.getElementById('world');
        // container.appendChild(renderer.domElement);

        // // Listen to the screen: if the user resizes it
        // // we have to update the camera and the renderer size
        // window.addEventListener('resize', handleWindowResize, false);


    }

    // Update is called once per frame
    void Update()
    {

    }
}
