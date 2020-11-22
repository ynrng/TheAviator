using System.Collections;
using UnityEngine;

public class Generator : MonoBehaviour {

    public GameObject[] prefebsNone = new GameObject[1];
    public GameObject[] prefebsCoin = new GameObject[1];
    public GameObject[] prefebsStone = new GameObject[1];

    private IEnumerator[] ienums = new IEnumerator[3];

    bool isCoroutineRunning = false;

    // Start is called before the first frame update
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
    }

    void Update()
    {
        if (Aviator.status == AviatorStates.Flying) {
            if (!isCoroutineRunning) {
                startCoroutines();
            }
        } else if (isCoroutineRunning) {
            stopCoroutines();
        }
    }

    IEnumerator generateCubes()
    {

        while (true) {

            // GameObject parent = new GameObject("Cloud");
            // var stepAngle = Math.PI * 2 / 20;
            float x = Aviator.seaRadius + 150 + Random.Range(0f, 200f); // 100-250
            float z = 100 + Random.Range(0, 600);//100-790
            float y = -Aviator.seaRadius;
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
            // parent.transform.Translate(Vector3.up * AviatorVariables.Center);
            // parent.transform.parent = gameObject.transform;

            yield return new WaitForSeconds(Random.Range(2, 4));
        }
    }

    IEnumerator generateCoins()
    {
        while (true) {
            float x = randomX();
            float amplitude = 10 + Random.Range(0f, 5);

            for (float i = 0; i < 5 + Random.Range(0, 5); i++) {
                GameObject iCube =
                Instantiate(prefebsCoin[Random.Range(0, prefebsCoin.Length)],
                            new Vector3(x + Mathf.Sin(i) * 2.5f,
                            -Aviator.seaRadius + i * amplitude, 0),
                            Random.rotation
                            );
                iCube.transform.localScale = Vector3.one * 5f;
                iCube.transform.parent = gameObject.transform;
            }
            yield return new WaitForSeconds(Random.Range(3, 5));
        }
    }

    float randomX() => Aviator.seaRadius + Aviator.planeDefaultHeight + Random.Range(-1f, 1f) * (Aviator.planeAmpHeight - 20);

    IEnumerator generateStones()
    {
        new WaitForSeconds(Random.Range(0, 3));
        while (true) {
            float x = randomX(); // 100-250
            float amplitude = 10 + Random.Range(0f, 5);

            for (int i = 0; i < Aviator.level; i++) {
                GameObject iCube =
                Instantiate(prefebsStone[Random.Range(0, prefebsStone.Length)],
                            new Vector3(x + Mathf.Sin(i) * amplitude, -Aviator.seaRadius + i * amplitude, 0),
                            Random.rotation
                            );
                iCube.transform.localScale = Vector3.one * 8;
                iCube.transform.parent = gameObject.transform;
            }
            yield return new WaitForSeconds(Random.Range(4, 6));
        }
    }

    #region public methods
    public void stopCoroutines()
    {
        for (int i = 1; i < ienums.Length; i++) {
            StopCoroutine(ienums[i]);
        }

        isCoroutineRunning = false;
    }

    public void startCoroutines()
    {
        for (int i = 1; i < ienums.Length; i++) {
            StartCoroutine(ienums[i]);
        }

        isCoroutineRunning = true;
    }
    #endregion

}
