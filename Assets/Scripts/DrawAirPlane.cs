using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawAirPlane : MonoBehaviour {
    GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        createAirplane();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject createAirplane()
    {
        parent = new GameObject("AirPlane");
        parent.transform.parent = gameObject.transform;

        // createCubeWithName("Cabin", new Vector3(80, 50, 50), Vector3.zero, AviatorColors.Red);
        createCabin();
        createCubeWithName("Engine", new Vector3(20, 50, 50), Vector3.right * 50, AviatorColors.White);
        createCubeWithName("Tail", new Vector3(15, 20, 5), new Vector3(-40, 20, 0), AviatorColors.Red);
        createCubeWithName("SideWing", new Vector3(30, 5, 120), Vector3.up * 15, AviatorColors.Red);
        createCubeWithName("Windshield", new Vector3(3, 15, 20), new Vector3(5, 27, 0), AviatorColors.WhiteTransparent);

        // GameObject propeller = createCubeWithName("Propeller", new Vector3(20, 10, 10), Vector3.zero, AviatorColors.Brown);
        GameObject propeller = createPropeller();
        GameObject blade1 = createCubeWithName("Blades", new Vector3(1, 80, 10), new Vector3(8, 0, 0), AviatorColors.BrownDark, propeller);
        GameObject blade2 = Instantiate(blade1, blade1.transform.parent, false);
        blade2.transform.localRotation = Quaternion.Euler(90, 0, 0);
        propeller.transform.position = Vector3.right * 60;

        GameObject wheelProtectR = createCubeWithName("WheelProtect", new Vector3(30, 15, 10), Vector3.zero, AviatorColors.Red);
        GameObject wheelTireR = createCubeWithName("wheelTire", new Vector3(24, 24, 4), Vector3.up * -8, AviatorColors.BrownDark, wheelProtectR);
        GameObject wheelAxisR = createCubeWithName("wheelAxis", new Vector3(10, 10, 6), Vector3.up * -8, AviatorColors.Brown, wheelProtectR);
        wheelProtectR.transform.position = new Vector3(25, -20, -25);

        GameObject wheelProtectL = Instantiate(wheelProtectR, wheelProtectR.transform.parent, false);
        wheelProtectL.transform.RotateAround(Vector3.right * wheelProtectL.transform.position.x, Vector3.up, 180);

        GameObject wheelTireB = Instantiate(wheelTireR, parent.transform, true);
        wheelTireB.transform.localScale = wheelTireB.transform.localScale * .5f;
        wheelTireB.transform.position = new Vector3(-35, -5, 0);

        GameObject suspension = createCubeWithName("Suspension", new Vector3(4, 20, 4), new Vector3(-30, 3, 0), AviatorColors.Red);
        suspension.transform.rotation = Quaternion.Euler(0, 0, -0.3f / Mathf.PI * 180);

        return parent;
    }

    Mesh messWithMesh(Mesh meshCube, Vector4 distance)
    {
        Vector3[] verticesTarget = meshCube.vertices;

        for (int i = 0; i < meshCube.vertexCount; i++) {
            if (verticesTarget[i].x < 0) {
                if (verticesTarget[i].y > 0) {
                    verticesTarget[i].y += distance[0];
                    if (verticesTarget[i].z > 0) {
                        verticesTarget[i].z += distance[2];
                    } else {
                        verticesTarget[i].z += distance[3];
                    }
                } else {
                    verticesTarget[i].y += distance[1];
                    if (verticesTarget[i].z > 0) {
                        verticesTarget[i].z += distance[2];
                    } else {
                        verticesTarget[i].z += distance[3];
                    }
                }
            }
        }

        meshCube.vertices = verticesTarget;
        meshCube.RecalculateNormals();

        return meshCube;
    }

    GameObject createPropeller()
    {
        GameObject cabin = createCubeWithName("Propeller", new Vector3(20, 10, 10), Vector3.zero, AviatorColors.Brown);
        Mesh meshCube = cabin.GetComponent<MeshFilter>().mesh;
        messWithMesh(meshCube, new Vector4(-.5f, .5f, -.5f, .5f));

        return cabin;
    }

    GameObject createCabin()
    {
        GameObject cabin = createCubeWithName("Cabin", new Vector3(80, 50, 50), Vector3.zero, AviatorColors.Red);
        Mesh meshCube = cabin.GetComponent<MeshFilter>().mesh;
        messWithMesh(meshCube, new Vector4(-.2f, .6f, -.4f, .4f));

        return cabin;
    }

    GameObject createCubeWithName(string name, Vector3 localScale, Vector3 position, Color color)
    {
        return createCubeWithName(name, localScale, position, color, parent);
    }
    GameObject createCubeWithName(string name, Vector3 localScale, Vector3 position, Color color, GameObject p)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = name;
        Material material = go.GetComponent<MeshRenderer>().material;
        material.color = color;
        if(color.a < 1) {
            SetMaterialTransparent(material);
        }
        go.transform.localScale = localScale;
        go.transform.position = position;
        go.transform.parent = p.transform;

        return go;
    }

    Material SetMaterialTransparent(Material ma)
    {
        // [ref](https://docs.unity3d.com/2019.4/Documentation/Manual/StandardShaderMaterialParameterRenderingMode.html)
        // https://github.com/Unity-Technologies/UnityCsReference/blob/master/Modules/AssetPipelineEditor/AssetPostprocessors/FBXMaterialDescriptionPreprocessor.cs
        // CreateFromStandardMaterial
        ma.SetInt("_Mode", (int)BlendMode.Transparent); // note transparent mode cannot cast shadows;
        ma.SetOverrideTag("RenderType", "Transparent");
        ma.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        ma.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        ma.SetInt("_ZWrite", 0);
        // ma.DisableKeyword("_ALPHATEST_ON");
        // ma.DisableKeyword("_ALPHABLEND_ON");
        ma.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        ma.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        return ma;
    }

}
