using UnityEngine;
using System.Collections;

public class CameraTransition : MonoBehaviour
{
    public Transform targetPoint;    // The point the camera will look at and move towards
    public Transform floorPoint;     // The point used to determine the floor's altitude
    public float moveSpeed = 1f;     // Speed at which the camera moves towards the point
    public float stutterIntensity = 0.5f;  // Intensity of the stutter effect
    public float fallDuration = 1f;  // Duration of the fall
    public float rotationAngle = 90f; // How much the camera rotates to the left when falling
    public float getCloseDuration = 3f;
    public float stopDistance = 0.5f; // Distance to stop short of the target point

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    void Start()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;

        // Step 0: Instantly rotate the camera towards the target point
        transform.rotation = Quaternion.LookRotation(targetPoint.position - transform.position);

        // Start the transition sequence
        StartCoroutine(TransitionSequence());
    }

    IEnumerator TransitionSequence()
    {
        // Step 1: Move towards the target point, but stop a little short
        float elapsedTime = 0f;

        // Calculate the final position that is slightly away from the target point
        Vector3 directionToTarget = (targetPoint.position - initialPosition).normalized;
        Vector3 targetPosition = targetPoint.position - directionToTarget * stopDistance;

        while (elapsedTime < getCloseDuration)
        {
            transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / getCloseDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Step 2: Apply a stutter effect
        for (int i = 0; i < 5; i++)
        {
            transform.position += Random.insideUnitSphere * stutterIntensity;
            yield return new WaitForSeconds(0.05f);
        }

        // Step 3 & 4: Simultaneously fall to the left and towards the floor
        Quaternion fallRotation = Quaternion.Euler(transform.eulerAngles.x + 30f, transform.eulerAngles.y, transform.eulerAngles.z - rotationAngle);
        Vector3 fallToFloorPosition = new Vector3(transform.position.x, floorPoint.position.y, transform.position.z);
        elapsedTime = 0f;

        while (elapsedTime < fallDuration)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, fallRotation, elapsedTime / fallDuration);
            transform.position = Vector3.Lerp(transform.position, fallToFloorPosition, elapsedTime / fallDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}
