
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DrawScene : MonoBehaviour {
    // Start is called before the first frame update
    public bool useFog = true;
    void Start()
    {

        //fog
        if (useFog) {
            RenderSettings.fogMode = FogMode.Linear;
            RenderSettings.fogColor = AviatorColors.Fog;
            RenderSettings.fogStartDistance = 100;
            RenderSettings.fogEndDistance = 950;
            RenderSettings.fog = useFog;  // for debug;
        }
        RenderSettings.ambientLight = AviatorColors.Light;
        QualitySettings.shadowDistance = 1500;
        QualitySettings.antiAliasing = 8;

        Camera camera = Camera.main;

        if (camera) {
            camera.fieldOfView = 50;
            camera.nearClipPlane = .1f;
            camera.farClipPlane = 1000;
            camera.orthographic = false;
            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = Color.clear;
            camera.transform.position = new Vector3(0, Aviator.planeDefaultHeight, -200); // coordinate system diff
        }

        // 	// Activate the anti-aliasing; this is less performant,
        // 	// but, as our project is low-poly based, it should be fine :)
        // 	antialias: true

        // // Enable shadow rendering
        // renderer.shadowMap.enabled = true;

        // // Listen to the screen: if the user resizes it
        // // we have to update the camera and the renderer size
        // window.addEventListener('resize', handleWindowResize, false);
    }

}
