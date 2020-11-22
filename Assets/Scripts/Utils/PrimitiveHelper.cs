/*
credit to https://answers.unity.com/questions/514293/changing-a-gameobjects-primitive-mesh.html
of https://answers.unity.com/questions/514293/changing-a-gameobjects-primitive-mesh.html
*/

using System.Collections.Generic;
using UnityEngine;
using Internal = UnityEngine.Internal;

public static class PrimitiveHelper {
    private static Dictionary<PrimitiveType, Mesh> primitiveMeshes = new Dictionary<PrimitiveType, Mesh>();
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

    public static Material SetMaterialTransparent(Material seaMa)
    {

        // TODO should Material be saved for future use;

        // [ref](https://docs.unity3d.com/2019.4/Documentation/Manual/StandardShaderMaterialParameterRenderingMode.html)
        #region set rendering mode to transparent
        seaMa.SetInt("_Mode", (int)BlendMode.Transparent); // note transparent mode cannot cast shadows;
        seaMa.SetOverrideTag("RenderType", "Transparent");
        seaMa.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        seaMa.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        seaMa.SetInt("_ZWrite", 0);
        seaMa.DisableKeyword("_ALPHATEST_ON");
        seaMa.DisableKeyword("_ALPHABLEND_ON");
        seaMa.EnableKeyword("_ALPHAPREMULTIPLY_ON");
        seaMa.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;
        return seaMa;
        #endregion
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