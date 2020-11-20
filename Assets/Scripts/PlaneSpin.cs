
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
        switch (Aviator.status) {
            case AviatorStates.Flying:
                transform.Rotate(transform.position.z + speed * Time.deltaTime, 0, 0);
                break;
            // case AviatorStates.End:
            // todo
            // break;
            default:
                break;
        }
    }
}
