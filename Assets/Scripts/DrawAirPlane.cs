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
        //     // Create the cabin
        // MeshRenderer cabinM = cabinGo.GetComponent<MeshRenderer>();
        // MaterialPropertyBlock m = new MaterialPropertyBlock(); //common use
        // cabinM.GetPropertyBlock(m, 0);
        // m.SetColor("Standard", AviatorColors.Fog);

        //1) Create an empty GameObject with the required Components
        GameObject cabinGo = PrimitiveHelper.CreatePrimitive(PrimitiveType.Cube, false);
        // Mesh meshCube =  PrimitiveHelper.GetPrimitiveMesh(PrimitiveType.Cube);

        cabinGo.transform.localScale = new Vector3(60, 50, 50);
        cabinGo.transform.position = Vector3.zero;
        cabinGo.transform.parent = parent.transform;

        //9) Give it a Material
        Material standardMaterial = new Material(PrimitiveHelper.GetMaterialStandard());
        standardMaterial.SetColor("_Color", AviatorColors.Red); //green main color
        cabinGo.GetComponent<Renderer>().material = standardMaterial;
        Destroy(standardMaterial);


        // Material cubeMaterial = new Material(Shader.Find("Standard"));
        // cubeMaterial.SetColor("_Color", new Color(0f, 0.7f, 0f)); //green main color
        // _cube.GetComponent<Renderer>().material = cubeMaterial;

        //     // Create the engine
        //     var geomEngine = new THREE.BoxGeometry(20, 50, 50, 1, 1, 1);
        //     var matEngine = new THREE.MeshPhongMaterial({ color:Colors.white, shading: THREE.FlatShading});
        // var engine = new THREE.Mesh(geomEngine, matEngine);
        // engine.position.x = 40;
        // engine.castShadow = true;
        // engine.receiveShadow = true;
        // this.mesh.add(engine);

        // GameObject engine = GameObject.CreatePrimitive(PrimitiveType.Cube);
        // engine.transform.localScale = new Vector3(60, 50, 50);
        // engine.transform.position = Vector3.right * 40;
        // engine.transform.parent = gameObject.transform;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
