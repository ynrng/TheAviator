using UnityEngine;

public class DrawAirPlane : MonoBehaviour {
    // Start is called before the first frame update
    GameObject parent;
    void Start()
    {
        createAirplane();
    }

    // Update is called once per frame
    void Update()
    {

    }

    #region drawing functions

    public GameObject createAirplane(){
        parent = new GameObject("AirPlane");
        parent.transform.parent = gameObject.transform;

        createCabin();
        // createCubeWithName("Cabin", new Vector3(80, 50, 50), Vector3.zero, AviatorColors.Red);
        createCubeWithName("Engine", new Vector3(20, 50, 50), Vector3.right * 50, AviatorColors.White);
        createCubeWithName("Tail", new Vector3(15, 20, 5), new Vector3(-40, 20, 0), AviatorColors.Red);
        createCubeWithName("SideWing", new Vector3(30, 5, 120), Vector3.up * 15, AviatorColors.Red);
        createCubeWithName("Windshield", new Vector3(3, 15, 20), new Vector3(5, 27, 0), AviatorColors.WhiteTransparent);

        GameObject propeller = createPropeller();
        // GameObject propeller = createCubeWithName("Propeller", new Vector3(20, 10, 10), Vector3.zero, AviatorColors.Brown);
        GameObject blade1 = createCubeWithName("Blades", new Vector3(1, 80, 10), new Vector3(8, 0, 0), AviatorColors.BrownDark, propeller);
        GameObject blade2 = Instantiate(blade1, blade1.transform.parent, false);
        blade2.transform.localRotation = Quaternion.Euler(90, 0, 0);
        propeller.transform.position = new Vector3(60, 0, 0);
        propeller.AddComponent<PlaneSpin>();

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

        //DrawPilot
        createPilot();

        //after all parts drawn;
        parent.AddComponent<PlaneControl>();
        parent.tag = "airplane";

        return parent;
    }

    public GameObject createPropeller()
    {
        GameObject propeller = PrimitiveHelper.CreatePrimitive(PrimitiveType.Cube, false, "Propeller");
        Mesh meshCube = propeller.GetComponent<MeshFilter>().mesh;
        messWithCube(meshCube, new Vector4(-.5f, .5f, -.5f, .5f));

        // Material: assign color
        Material engineMa = propeller.GetComponent<MeshRenderer>().material;
        // engineMa.SetColor("_Color", AviatorColors.Brown);
        engineMa.color = AviatorColors.Brown;

        propeller.transform.localScale = new Vector3(20, 10, 10);
        // engineGo.transform.position = position;
        propeller.transform.parent = parent.transform;

        return propeller;
    }
    public GameObject createPilot()
    {

        GameObject pilotGo = new GameObject("Pilot");

        createCubeWithName("Body", Vector3.one * 15, new Vector3(2, -12, 0), AviatorColors.Brown, pilotGo);
        createCubeWithName("Face", Vector3.one * 10, Vector3.zero, AviatorColors.Pink, pilotGo);

        // MeshLambertMaterial => consider Shader: Mobile/Diffuse
        createCubeWithName("GlassR", Vector3.one * 5, new Vector3(6, 0, -3), AviatorColors.Brown, pilotGo);
        createCubeWithName("GlassL", Vector3.one * 5, new Vector3(6, 0, 3), AviatorColors.Brown, pilotGo);
        createCubeWithName("GlassA", new Vector3(6, 0, 3), Vector3.zero, AviatorColors.Brown, pilotGo);

        createCubeWithName("EarL", new Vector3(2, 3, 3), new Vector3(0, 0, -6), AviatorColors.Pink, pilotGo);
        createCubeWithName("EarR", new Vector3(2, 3, 3), new Vector3(0, 0, 6), AviatorColors.Pink, pilotGo);

        // hairs
        GameObject hairsGo = new GameObject("Hairs");
        createCubeWithName("SideL", new Vector3(12, 4, 2), new Vector3(8 - 6, -2, 6), AviatorColors.Brown, hairsGo);
        createCubeWithName("SideR", new Vector3(12, 4, 2), new Vector3(8 - 6, -2, -6), AviatorColors.Brown, hairsGo);
        createCubeWithName("Back", new Vector3(2, 8, 10), new Vector3(-1, -4, 0), AviatorColors.Brown, hairsGo);

        GameObject topGo = new GameObject("Top");

        int startPosZ = -4;
        int startPosX = -4;
        for (int i = 0; i < 12; i++) {
            GameObject hairMvGo = createCubeWithName("Hair", Vector3.one * 4,
            new Vector3(startPosX + (i / 3) * 4, 0, startPosZ + (i % 3) * 4), AviatorColors.Brown, topGo);
            PilotHair pilotHair = hairMvGo.AddComponent<PilotHair>();
            pilotHair.index = i;
        }

        topGo.transform.parent = hairsGo.transform;

        hairsGo.transform.position = new Vector3(-5, 5, 0);
        hairsGo.transform.parent = pilotGo.transform;
        // hairs end

        pilotGo.transform.position = new Vector3(-10, 27, 0);
        pilotGo.transform.parent = parent.transform;

        return pilotGo;

    }

    Mesh messWithCube(Mesh meshCube, Vector4 distance)
    {
        Vector3[] verticesTarget = meshCube.vertices;
        // different from 3.js, vertices remains the same with original coordinates, meaning cube x-axis is -0.5-0.5, so on;
        // it does not matter if transform or scale is applied; so in some way it is easy to calculate;
        for (int i = 0; i < meshCube.vertexCount; i++) {
            //i dont need to know the index of each point;
            //just calculate the cordinates and perform act
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
        // remember to reassign
        meshCube.vertices = verticesTarget;
        // call to rerender
        meshCube.RecalculateNormals();

        return meshCube;
    }

    GameObject createCabin()
    {

        GameObject engineGo = PrimitiveHelper.CreatePrimitive(PrimitiveType.Cube, false, "Cabin");
        Mesh meshCube = engineGo.GetComponent<MeshFilter>().mesh;
        messWithCube(meshCube, new Vector4(-.2f, .6f, -.4f, .4f));

        // Material: assign color
        Material engineMa = engineGo.GetComponent<MeshRenderer>().material;
        engineMa.SetColor("_Color", AviatorColors.Red);

        engineGo.transform.localScale = new Vector3(80, 50, 50);
        // engineGo.transform.position = position;
        engineGo.transform.parent = parent.transform;

        return engineGo;
    }

    GameObject createCubeWithName(string name, Vector3 localScale, Vector3 position, Color color)
    {
        return createCubeWithName(name, localScale, position, color, parent);
    }

    // Material seaMa = seaMr.material;
    // seaMa.SetColor("_Color", AviatorColors.Blue);
    // PrimitiveHelper.SetMaterialTransparent(seaMa);

    GameObject createCubeWithName(string name, Vector3 localScale, Vector3 position, Color color, GameObject p)
    {

        //1) Create an empty GameObject with the required Components
        GameObject engineGo = PrimitiveHelper.CreatePrimitive(PrimitiveType.Cube, false, name);
        // Mesh meshCube =  PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Cube);

        //9) Give it a Material
        Material engineMa = engineGo.GetComponent<MeshRenderer>().material;
        // todo should Material be saved for future use;
        // but definately create a instance for same color on the same render; like in primitivehelper;
        if (color.a < 1) {
            PrimitiveHelper.SetMaterialTransparent(engineMa);
        }
        engineMa.SetColor("_Color", color);

        engineGo.transform.localScale = localScale;
        engineGo.transform.position = position;
        engineGo.transform.parent = p.transform;

        return engineGo;
    }

        #endregion

}
