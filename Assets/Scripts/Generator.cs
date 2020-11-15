using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO.Compression;
using UnityEngine;

public class Generator : MonoBehaviour {
    // Start is called before the first frame update

    public GameObject[] prefebsNone;
    public GameObject[] prefebsCoin;
    public GameObject[] prefebsStone;

    private float currentX;

    private IEnumerator[] ienums = new IEnumerator[3];

    // private bool isGenerating = false;

    void Start()
    {
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
        // Y:2.5-3.5
        // x: -3.5 screen left
        new WaitForSeconds(Random.Range(0, 3));
        while (true) {

            // GameObject parentCube = new GameObject("parentCube");
            float x = Random.Range(3f, 5f);
            float z = Random.Range(0f, 2f);
            for (int i = 0; i < Random.Range(1, 6); i++) {

                GameObject iCube =
                Instantiate(prefebsNone[Random.Range(0, prefebsNone.Length)],
                            new Vector3(x, i * 0.15f, z),
                            Random.rotation
                            );
                iCube.transform.parent = gameObject.transform;
            }
            yield return new WaitForSeconds(Random.Range(1, 4));
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
