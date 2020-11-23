using UnityEngine;

public class PlaneSpin : MonoBehaviour
{
    float scalerPropspellerSpinning = 500f;
    // Update is called once per frame
    void Update()
    {
        if (Aviator.status == AviatorStates.Flying) {
            transform.Rotate(Aviator.planeSpeed * scalerPropspellerSpinning, 0, 0);
        } else if (Aviator.status == AviatorStates.Falling) {
            // important to use localRotation because when the plane is falling. the plane self has an angle.
            // if use rotation, the 2nd time the prospeller will be tilted.
            transform.localRotation = Quaternion.Lerp(
                transform.localRotation,
                Quaternion.identity,
                Time.deltaTime // todo maybe add a scaler to make the process slower
            );
        }
    }
}
