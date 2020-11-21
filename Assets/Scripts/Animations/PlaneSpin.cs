
using UnityEngine;

public class PlaneSpin : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed = 1000;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Aviator.status == AviatorStates.Flying) {
                transform.Rotate(transform.position.z + speed * Time.deltaTime, 0, 0);

        } else {
            // transform.Rotate(transform.position.z + speed * Time.deltaTime, 0, 0);
            // gameObject
        }
    }
}
