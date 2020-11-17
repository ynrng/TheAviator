/*
credit to https://answers.unity.com/questions/514293/changing-a-gameobjects-primitive-mesh.html
of https://answers.unity.com/questions/514293/changing-a-gameobjects-primitive-mesh.html
*/

using System.Collections.Generic;
using UnityEngine;
using Internal = UnityEngine.Internal;

public static class PrimitiveHelper {
    private static Dictionary<PrimitiveType, Mesh> primitiveMeshes = new Dictionary<PrimitiveType, Mesh>();
    private static Material materialStandard = new Material(Shader.Find("Standard"));

    public static Material GetMaterialStandard()
    {
        // if (materialStandard) {
        //     materialStandard = new Material(Shader.Find("Standard"));
        // }
        return materialStandard;
    }

    public static GameObject CreatePrimitive(PrimitiveType type, bool withCollider)
    {
        return CreatePrimitive(type, withCollider, string.Empty);
    }

    public static GameObject CreatePrimitive(PrimitiveType type, bool withCollider, string name)
    {
        if (withCollider) { return GameObject.CreatePrimitive(type); }

        GameObject gameObject = new GameObject(name != string.Empty ? name : type.ToString());
        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
        meshFilter.sharedMesh = PrimitiveHelper.GetPrimitiveMesh(type);
        gameObject.AddComponent<MeshRenderer>();

        return gameObject;
    }

    public static Mesh GetPrimitiveMesh(PrimitiveType type)
    {
        if (!PrimitiveHelper.primitiveMeshes.ContainsKey(type)) {
            PrimitiveHelper.CreatePrimitiveMesh(type);
        }

        return PrimitiveHelper.primitiveMeshes[type];
    }

    private static Mesh CreatePrimitiveMesh(PrimitiveType type)
    {
        GameObject gameObject = GameObject.CreatePrimitive(type);
        Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
        GameObject.Destroy(gameObject);

        PrimitiveHelper.primitiveMeshes[type] = mesh;
        return mesh;
    }


}
