using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class DrawCylinder : MonoBehaviour {
    // how many segments do we want in between;
    public int segmentsHeight = 1; //
    public int segmentsRadial = 3;
    public bool isDiscrete = true;
    int radiusRound = 1;
    List<Vector3> vertices;
    List<int> triangles;

    // Start is called before the first frame update
    void Start()
    {
        makeMeshData();
        createMesh();
    }

    public List<int>[] groupSamePoint()
    {
        List<int>[] points = new List<int>[(segmentsHeight + 1) * segmentsRadial];
        // first is self;
        // points[0] = airplane;
        for (int j = 0; j <= segmentsHeight; j++) {
            for (int i = 0; i < segmentsRadial; i++) {
                int indexk = j * segmentsRadial + i;
                int zeroBalance = i % segmentsRadial == 0 ? segmentsRadial : 0;
                points[indexk] = new List<int>();
                // mind edge case
                if (j != segmentsHeight) {
                    // top line
                    points[indexk].Add((indexk + zeroBalance) * 6 - 3); // t r t
                    points[indexk].Add((indexk + zeroBalance) * 6 - 4); // t r t
                    points[indexk].Add((indexk) * 6); // t r b
                }
                if (j != 0) {
                    // bottome line
                    points[indexk].Add((indexk - segmentsRadial) * 6 + 1); // b r
                    points[indexk].Add((indexk - segmentsRadial) * 6 + 4); // b r
                    points[indexk].Add((indexk - segmentsRadial + zeroBalance) * 6 - 1); // b r
                }
            }
        }
        return points;
    }

    void makeMeshData()
    {

        //prepare
        List<Vector3> xzs = new List<Vector3>(segmentsRadial + 1);
        float tranferToCenterY = -segmentsHeight / 2;

        // from 0 to pi,
        float stepR = Mathf.PI * 2 / segmentsRadial;
        for (int i = 0; i < segmentsRadial; i++) {
            float angle = stepR * i;
            xzs.Add(new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radiusRound);
        }
        //connect the first with last
        //add back origin point so in for loop it dose not overflow
        xzs.Add(new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0)) * radiusRound);

        //
        //start generation
        vertices = new List<Vector3>();
        //faces: basicly from 0 to segmentsHeight*segmentsRound-1
        triangles = new List<int>();

        if (isDiscrete) {
            //faces: basicly from 0 to segmentsHeight*segmentsRound-1
            triangles = Enumerable.Range(0, (segmentsHeight * segmentsRadial) * 6).ToList();

            // only need to generate side
            for (int j = 0; j < segmentsHeight; j++) {
                for (int i = 0; i < segmentsRadial; i++) {
                    //order matters. points
                    vertices.Add(xzs[i] + Vector3.up * (j + tranferToCenterY));
                    vertices.Add(xzs[i] + Vector3.up * (j + 1 + tranferToCenterY));
                    vertices.Add(xzs[i + 1] + Vector3.up * (j + tranferToCenterY));

                    vertices.Add(xzs[i + 1] + Vector3.up * (j + tranferToCenterY));
                    vertices.Add(xzs[i] + Vector3.up * (j + 1 + tranferToCenterY));
                    vertices.Add(xzs[i + 1] + Vector3.up * (j + 1 + tranferToCenterY));
                }
            }
        } else {
            // only need to generate side
            for (int j = 0; j < segmentsHeight + 1; j++) {
                for (int i = 0; i < segmentsRadial; i++) {
                    //points, order matters
                    vertices.Add(xzs[i] + Vector3.up * (j + tranferToCenterY));
                }
            }

            // only need to generate side
            for (int j = 0; j < segmentsHeight; j++) {
                for (int i = 0; i < segmentsRadial; i++) {
                    //points. order matters
                    int indexk = j * segmentsRadial;
                    int iplus1 = (i + 1) % segmentsRadial; //for the last quad to reconnect to the 1st

                    triangles.Add(indexk + i);
                    triangles.Add(indexk + i + segmentsRadial);
                    triangles.Add(indexk + iplus1);//i + 1

                    triangles.Add(indexk + iplus1);
                    triangles.Add(indexk + i + segmentsRadial);
                    triangles.Add(indexk + iplus1 + segmentsRadial);
                }

            }
        }

    }

    void createMesh()
    {
        // to avoid add mesh multiple times; declare at top require;
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();

        // if (addStandardMaterial) {
        //     MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
        //     meshRenderer.material = new Material(Shader.Find("Standard"));
        // }

        meshFilter.mesh.Clear();
        meshFilter.mesh.vertices = vertices.ToArray();
        meshFilter.mesh.triangles = triangles.ToArray();

        meshFilter.mesh.RecalculateNormals();
        // gameObject.transform.localScale = Vector3.one * 1000;
    }

}
