using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class AddVertices : MonoBehaviour {
    // how many segments do we want in between;
    public int segmentsHeight = 2; //
    public int segmentsRound = 5;
    int stepH = 1;
    int radiusRound = 1;

    public List<Vector3> xzs; //todo make private
    public List<Vector3> vertices; //todo make private
    public List<int> triangles; //todo make private

    // Start is called before the first frame update
    void Start()
    {
        //
        //prepare
        // List<Vector3>
        xzs = new List<Vector3>(segmentsRound + 1);

        // from 0 to pi,
        float stepR = Mathf.PI * 2 / segmentsRound;
        for (int i = 0; i < segmentsRound; i++) {
            float angle = stepR * i;
            xzs.Add(new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radiusRound);
        }
        //connect the first with last
        //add back origin point so in for loop it dose not overflow
        xzs.Add(new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0)) * radiusRound);

        //
        //start generation
        // List<Vector3>
        vertices = new List<Vector3>();
        // List<int>
        // triangles = new List<int>();
        triangles = Enumerable.Range(0, (segmentsHeight * segmentsRound) * 6).ToList();

        // only need to generate side
        for (int j = 0; j < segmentsHeight; j++) {
            for (int i = 0; i < segmentsRound; i++) {
                //order matters
                //points
                vertices.Add(xzs[i] + Vector3.up * j);
                vertices.Add(xzs[i + 1] + Vector3.up * (j + 1));
                vertices.Add(xzs[i + 1] + Vector3.up * j);

                vertices.Add(xzs[i] + Vector3.up * j);
                vertices.Add(xzs[i] + Vector3.up * (j + 1));
                vertices.Add(xzs[i + 1] + Vector3.up * (j + 1));

                //faces: basicly from 0 to segmentsHeight*segmentsRound-1
                // int start = segmentsRound * j + i;
                // triangles.AddRange(Enumerable.Range(start * 3, 3));
            }

        }

        MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();

        MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        meshRenderer.material = new Material(Shader.Find("Standard"));

        meshFilter.mesh.Clear();
        meshFilter.mesh.vertices = vertices.ToArray();
        meshFilter.mesh.triangles = triangles.ToArray();

        meshFilter.mesh.RecalculateNormals();
        gameObject.transform.localScale = Vector3.one * 1000;

    }

    // Update is called once per frame
    void Update()
    {

    }
}

// segments = Mathf.Max(0, segments);//check to implement minus
// float segmentStep = 2f / (segments + 1);
// Mesh mesh = gameObject.GetComponent<MeshFilter>().mesh;

// Vector3[] verticesOrigin = mesh.vertices;
// List<Vector3> vertices = new List<Vector3>(verticesOrigin);
// int[] trianglesOrigin = mesh.triangles;
// List<int> triangles = new List<int>(trianglesOrigin);

// List<Vector3> xzs = vertices.FindAll(x => x.x != 0 && x.z != 0 && x.y == -1);//bottom surface
// xzs.Sort(delegate (Vector3 a, Vector3 b) {
//     // clockwise
//     return Mathf.Atan((float)a.z / a.x) - Mathf.Atan((float)b.z / b.x) <= 0 ? -1 : 1;
// });

// foreach (Vector3 xz in xzs) {
//     for (int i = 0; i < segments; i++) {
//         vertices.Add(xz + Vector3.up * segmentStep);
//     }
// }

// // int[] numbers = { 2, 3, 4, 5 };
// // var squaredNumbers = numbers.Select(x => x * x);
// // Console.WriteLine(string.Join(" ", squaredNumbers));
// // Output:
// // 4 9 16 25

// // generate extra faces