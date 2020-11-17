using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO.Compression;
using UnityEditor;
using UnityEngine;

public class Generator : MonoBehaviour {
    // Start is called before the first frame update

    public GameObject[] prefebsNone = new GameObject[1];
    public GameObject[] prefebsCoin = new GameObject[1];
    public GameObject[] prefebsStone = new GameObject[1];

    private float currentX;
    private GameObject parent;

    private IEnumerator[] ienums = new IEnumerator[3];

    // private bool isGenerating = false;

    void Start()
    {

        // PrefabUtility.GetPrefabObject;
        // Instantiate(PrefabUtility.fi).
        // GameObject heli = Resources.Load<GameObject>("Prefabs/Heli");
        // Instantiate(heli);

        prefebsNone[0] = Resources.Load<GameObject>("Prefabs/Cube");
        prefebsCoin[0] = Resources.Load<GameObject>("Prefabs/Coin");
        prefebsStone[0] = Resources.Load<GameObject>("Prefabs/Stone");

        ienums[0] = generateCubes();
        ienums[1] = generateCoins();
        ienums[2] = generateStones();

        StartCoroutine(ienums[0]);

        // foreach (IEnumerator item in ienums) {
        //     StartCoroutine(item);
        //     // isGenerating = true;
        // }
    }


    IEnumerator generateCubes()
    {

        while (true) {

            // GameObject parent = new GameObject("Cloud");
            // var stepAngle = Math.PI * 2 / 20;
            float x = 600 + 150 + Random.Range(0f, 100f); // 100-250
            float z = 200 + Random.Range(0f, 200);//200-400
            float y = -600f;
            float scale = (1 + Random.Range(0, 1f)) * 20;
            for (int i = 0; i < 3 + Random.Range(0, 3); i++) {

                GameObject iCube =
                Instantiate(prefebsNone[Random.Range(0, prefebsNone.Length)],
                            new Vector3(x - Random.Range(0f, 10f), y + i * 25f, z - Random.Range(0f, 10f)),
                            Random.rotation
                            );
                iCube.transform.localScale *= (.5f + Random.Range(0, .5f)) * scale; //	var s = .1 + Math.random()*.9;
                iCube.transform.parent = gameObject.transform;
            }
            // parent.transform.localScale *= 1 + Random.Range(0, 2f);
            // parent.transform.Translate(Vector3.down * 600);
            // parent.transform.parent = gameObject.transform;

            yield return new WaitForSeconds(Random.Range(2, 4));
        }
    }

    IEnumerator generateCoins()
    {
        // Y:2.5-3.5
        // x: -3.5 screen left
        new WaitForSeconds(Random.Range(0, 3));
        while (true) {

            float x = Random.Range(3.4f, 4.5f);
            // if (Mathf.Abs(currentX - x) < 0.2) {
            //     currentX = 0;
            //     yield return new WaitForSeconds(1);
            // }
            // currentX = x;
            for (int i = 0; i < Random.Range(2, 8); i++) {

                GameObject iCube =
                Instantiate(prefebsCoin[Random.Range(0, prefebsCoin.Length)],
                    new Vector3(x, i * 0.2f, 0),
                    Random.rotation
                    );
                iCube.transform.parent = gameObject.transform;
            }
            yield return new WaitForSeconds(Random.Range(3, 5));
        }
    }

    IEnumerator generateStones()
    {
        // Y:2.5-3.5
        // x: -3.5 screen left
        new WaitForSeconds(Random.Range(0, 3));
        while (true) {

            float x = Random.Range(3.4f, 4.5f);
            if (Mathf.Abs(currentX - x) < 0.2) {
                currentX = 0;
                yield return new WaitForSeconds(1);
            }
            currentX = x;

            // GameObject parentCube = new GameObject("parentCube");
            // for (int i = 0; i < Random.Range(nums[0], nums[1]); i++)
            // {

            GameObject iCube =
            Instantiate(prefebsStone[Random.Range(0, prefebsStone.Length)],
                        new Vector3(x, 0, 0),
                        Random.rotation
                        );
            iCube.transform.parent = gameObject.transform;
            // }
            yield return new WaitForSeconds(Random.Range(4, 6));
        }
    }


    #region public methods
    public void stopCoroutines()
    {
        for (int i = 1; i < ienums.Length; i++) {
            StopCoroutine(ienums[i]);
        }
    }

    public void startCoroutines()
    {
        for (int i = 1; i < ienums.Length; i++) {
            StartCoroutine(ienums[i]);
        }
    }
    #endregion

}
