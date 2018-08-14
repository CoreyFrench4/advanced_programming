using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour
{
    public Transform target;
    public LayerMask ignoreLayers;

    
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public bool cameraCollision = false;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float rayDistance = 1000f;
    public Vector3 originalOffset;
    
    public Vector3 offset = new Vector3(0f, 1f, 0f);
    public float distance = 5f;
    private float x = 0.0f;
    private float y = 0.0f;
    public bool hidecursor = false;
    // Use this for initialization
    void Start()
    {
        originalOffset = transform.position - target.position;

        rayDistance = originalOffset.magnitude;

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        transform.SetParent(null);
    }
    public void FixedUpdate()
    {
        if (target)
        {
            if (cameraCollision)
            {
                Ray camRay = new Ray(target.position, -transform.position);

                RaycastHit hit;
                if (Physics.Raycast(camRay, out hit, rayDistance, ~ignoreLayers, QueryTriggerInteraction.Ignore))
                {
                    distance = hit.distance;
                    return;
                }

            }
            distance = originalOffset.magnitude;
        }
    }

    public void Look(float mouseX, float mouseY)
    {

        x += mouseX * xSpeed * Time.deltaTime;
        y -= mouseY * ySpeed * Time.deltaTime;

        y = ClampAngle(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        transform.rotation = rotation;

    }
    void LateUpdate()
    {
        if (target)
        {
            Vector3 localOffset = transform.TransformDirection(offset);
            transform.position = (target.position + localOffset) + -transform.forward * distance;
        }
    }


    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}