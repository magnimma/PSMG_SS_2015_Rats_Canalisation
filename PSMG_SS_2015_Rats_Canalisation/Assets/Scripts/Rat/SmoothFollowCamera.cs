using UnityEngine;
using System.Collections;

public class SmoothFollowCamera : MonoBehaviour
{

    // The target we are following
    public Transform target;
    // The distance in the x-z plane to the target
    public float distance = 10.0f;
    // the height we want the camera to be above the target
    public float height = 5.0f;
    // How much we
    public float Damping = 4.0f;

    void LateUpdate()
    {
        // Early out if we don't have a target
        if (!target)
            return;

        Vector3 CameraFinalPosition = new Vector3();
        Vector3 DirPlayerToCamera = new Vector3();

        CameraFinalPosition = target.position - (target.forward * distance);
        CameraFinalPosition += Vector3.up * height;

        DirPlayerToCamera = CameraFinalPosition - target.position;
        DirPlayerToCamera.Normalize();

        Ray MyRay = new Ray(target.position, DirPlayerToCamera);
        RaycastHit HitInfo = new RaycastHit();
        Physics.Raycast(MyRay, out HitInfo, distance);
        if (HitInfo.collider != null)
        {
            if (HitInfo.distance < distance)
            {
                CameraFinalPosition += -DirPlayerToCamera * (distance - HitInfo.distance);
            }
        }

        transform.position = Vector3.Lerp(transform.position, CameraFinalPosition, Time.deltaTime * Damping);
        transform.LookAt(target);
    }
}