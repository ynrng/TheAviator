using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawAirPlane : MonoBehaviour {
    // Start is called before the first frame update
    GameObject parent;
    void Start()
    {
        parent = new GameObject("AirPlane");
        parent.transform.parent = gameObject.transform;
        // todo remember to add rigidbody to parent

        createCockpit();
        // createCubeWithName("Cabin", new Vector3(60, 50, 50), Vector3.zero, AviatorColors.Red);
        createCubeWithName("Engine", new Vector3(20, 50, 50), Vector3.right * 40, AviatorColors.White);
        createCubeWithName("Tail", new Vector3(15, 20, 5), new Vector3(-35, 25, 0), AviatorColors.Red);
        createCubeWithName("SideWing", new Vector3(40, 8, 150), Vector3.zero, AviatorColors.Red);
        GameObject propeller = createCubeWithName("Propeller", new Vector3(20, 10, 10), Vector3.zero, AviatorColors.Brown);
        GameObject blade = createCubeWithName("Blades", new Vector3(1, 100, 20), new Vector3(8, 0, 0), AviatorColors.BrownDark);
        //    GameObject blade2 = Instantiate(blade1, )
        blade.transform.parent = propeller.transform;
        propeller.transform.position = new Vector3(50, 0, 0);
        propeller.AddComponent<PlaneSpin>();

        parent.transform.position = Vector3.up * 100;
        parent.transform.localScale = Vector3.one * .25f;

    }

    GameObject createCockpit()
    {

        GameObject engineGo = PrimitiveHelper.CreatePrimitive(PrimitiveType.Cube, false, "Cockpit");
        Mesh meshCube = engineGo.GetComponent<MeshFilter>().mesh;

        Vector3[] verticesTarget = meshCube.vertices;
        // different from 3.js, vertices remains the same with original coordinates, meaning cube x-axis is -0.5-0.5, so on;
        // it does not matter if transform or scale is applied; so in some way it is easy to calculate;
        for (int i = 0; i < meshCube.vertexCount; i++) {
            //i dont need to know the index of each point;
            //just calculate the cordinates and perform act
            if (verticesTarget[i].x < 0) {
                if (verticesTarget[i].y > 0) {
                    verticesTarget[i].y -= .2f;
                    if (verticesTarget[i].z > 0) {
                        verticesTarget[i].z -= .4f;
                    } else {
                        verticesTarget[i].z += .4f;
                    }
                } else {
                    verticesTarget[i].y += .6f;
                    if (verticesTarget[i].z > 0) {
                        verticesTarget[i].z -= .4f;
                    } else {
                        verticesTarget[i].z += .4f;
                    }
                }
            }
        }
        // remember to reassign
        meshCube.vertices = verticesTarget;
        // call to rerender
        meshCube.RecalculateNormals();

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

        //1) Create an empty GameObject with the required Components
        GameObject engineGo = PrimitiveHelper.CreatePrimitive(PrimitiveType.Cube, false, name);
        // Mesh meshCube =  PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Cube);

        engineGo.transform.localScale = localScale;
        engineGo.transform.position = position;
        engineGo.transform.parent = parent.transform;

        //9) Give it a Material
        // Material material = new Material(PrimitiveHelper.GetMaterialStandard());
        Material engineMa = engineGo.GetComponent<MeshRenderer>().material;
        // todo should Material be saved for future use;
        engineMa.SetColor("_Color", color);

        return engineGo;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
