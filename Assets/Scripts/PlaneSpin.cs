
using UnityEngine;

public class PlaneSpin : MonoBehaviour
{
    // Start is called before the first frame update
    public int speed = 600;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (Interface.state)
        {
            case AviatorStates.Flying:
                transform.Rotate(0, 0, transform.position.z + speed * Time.deltaTime);
            break;
            // case AviatorStates.End:
            // todo
            // break;
            default:
            break;
        }
    }
}
