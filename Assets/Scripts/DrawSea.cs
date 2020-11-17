using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSea : MonoBehaviour {
    // Start is called before the first frame update
    GameObject parent;
    Material material;
    void Start()
    {
        parent = new GameObject("AirPlane");
        parent.transform.parent = gameObject.transform;

        createCubeWithName("Cabin", new Vector3(60, 50, 50), Vector3.zero, AviatorColors.Red);
        createCubeWithName("Engine", new Vector3(20, 50, 50), Vector3.right * 40, AviatorColors.White);
        createCubeWithName("Tail", new Vector3(15, 20, 5), new Vector3(-35, 25, 0), AviatorColors.Red);
        createCubeWithName("SideWing", new Vector3(40, 8, 150), Vector3.zero, AviatorColors.Red);
        GameObject propeller = createCubeWithName("Propeller", new Vector3(20, 10, 10), Vector3.zero, AviatorColors.Brown);
        GameObject blade = createCubeWithName("Blades", new Vector3(1, 100, 20), new Vector3(8, 0, 0), AviatorColors.BrownDark);
        //    GameObject blade2 = Instantiate(blade1, )
        blade.transform.parent = propeller.transform;
        propeller.transform.position = new Vector3(50, 0, 0);
        propeller.AddComponent<PlaneSpin>();

        parent.transform.position = Vector3.up * 200; // 100
        parent.transform.localScale = Vector3.one * .25f;

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
