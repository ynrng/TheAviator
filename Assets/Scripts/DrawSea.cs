using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlendMode {
    Opaque,
    Cutout,
    Fade,   // Old school alpha-blending mode, fresnel does not affect amount of transparency
    Transparent // Physically plausible transparency mode, implemented as alpha pre-multiply
}

public class DrawSea : MonoBehaviour {
    // Start is called before the first frame update
    // GameObject parent;
    void Start()
    {

        createSky();
        createSea();

    }

    // Update is called once per frame
    void Update()
    {

    }
    #region drawing
    GameObject createSky()
    {
        GameObject skyGo = PrimitiveHelper.CreatePrimitive(PrimitiveType.Quad, false, "Sky");
        // Mesh meshCube =  PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Cube);

        skyGo.transform.position = new Vector3(0, 200, 400);
        skyGo.transform.localScale = new Vector3(1300, 1000, 1);
        skyGo.transform.parent = gameObject.transform;

        //9) Give it a Material
        // Material material = new Material(PrimitiveHelper.GetMaterialStandard());
        Material skyMa = new Material(Shader.Find("Aviator/LinearGradientColor"));
        skyMa.SetColor("_color1", AviatorColors.Sky);
        skyMa.SetColor("_color2", AviatorColors.Fog);
        skyGo.GetComponent<MeshRenderer>().material = skyMa;

        return skyGo;
    }

    GameObject createSea()
    {// parent = new GameObject("Sea");
        // parent.transform.parent = gameObject.transform;

        //1) Create an empty GameObject with the required Components
        GameObject seaGo = PrimitiveHelper.CreatePrimitive(PrimitiveType.Cylinder, false, "Sea");
        // Mesh meshCube =  PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Cube);

        seaGo.transform.position = Vector3.up * -600;
        seaGo.transform.Rotate(Vector3.right * -90);
        seaGo.transform.localScale = new Vector3(1200, 400, 1200);
        seaGo.transform.parent = gameObject.transform;

        //9) Give it a Material
        // Material material = new Material(PrimitiveHelper.GetMaterialStandard());
        // Material seaMa = new Material(seaGo.GetComponent<MeshRenderer>().material);
        Material seaMa = seaGo.GetComponent<MeshRenderer>().material;
        seaMa.SetColor("_Color", AviatorColors.Blue);
        // todo should Material be saved for future use;

        // [ref](https://docs.unity3d.com/2019.4/Documentation/Manual/StandardShaderMaterialParameterRenderingMode.html)
        #region set rendering mode to transparent
        seaMa.SetInt("_Mode", (int)BlendMode.Transparent);
        seaMa.SetOverrideTag("RenderType", "Transparent");
        seaMa.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        seaMa.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        seaMa.SetInt("_ZWrite", 0);
        seaMa.DisableKeyword("_ALPHATEST_ON");
        seaMa.DisableKeyword("_ALPHABLEND_ON");
        seaMa.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        seaMa.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        #endregion
        // seaGo.GetComponent<MeshRenderer>().material = seaMa;

        return seaGo;
    }
    #endregion

}
