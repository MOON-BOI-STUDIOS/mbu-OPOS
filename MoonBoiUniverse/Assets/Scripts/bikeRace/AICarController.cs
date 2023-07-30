using System.Collections;
using UnityEngine;

public class AICarController : MonoBehaviour
{
    public float speed = 10f; // Speed for vertical movement
    public float horizontalSpeed = 5f; // Speed for left and right movement
    public float moveDistance = 2f;

    public Rigidbody2D rb;
    public SpriteRenderer sr;
    private bool isMoving = false;
    private Vector3 targetPosition;
    public bool isColliding=false;
    public bool isDisdroyed=false;
    void Awake()
    {
        InitializeCar();
    }

    public enum Position { Left, Centre, Right } // Positions a car can be in
    public Position currentPosition; // Current position of the car

    public void InitializeCar()
    {
        int rand = Random.Range(0, 3);
        currentPosition = (Position)rand;

        switch (currentPosition)
        {
            case Position.Left:
                transform.position = new Vector3(-moveDistance, transform.position.y, transform.position.z);
                break;
            case Position.Centre:
                transform.position = new Vector3(0, transform.position.y, transform.position.z);
                break;
            case Position.Right:
                transform.position = new Vector3(moveDistance, transform.position.y, transform.position.z);
                break;
        }
    }

    void Update()
    {
        if (isColliding)
            return;

        speed = RaceGameManager.currentSpeed - 3;
        rb.velocity = new Vector2(0, speed);
    }

    public void ChangePath()
    {
        if (Random.value > 0.5)//50-50 chance of its moving
            return;

        if (!isMoving)
        {
            switch (currentPosition)
            {
                case Position.Left:
                    StartCoroutine(MoveRight());
                    currentPosition = Position.Centre;
                    break;
                case Position.Centre:
                    if (Random.value < 0.5f)
                    {
                        StartCoroutine(MoveLeft());
                        currentPosition = Position.Left;
                    }
                    else
                    {
                        StartCoroutine(MoveRight());
                        currentPosition = Position.Right;
                    }
                    break;
                case Position.Right:
                    StartCoroutine(MoveLeft());
                    currentPosition = Position.Centre;
                    break;
            }
        }
    }

    IEnumerator MoveLeft()
    {
        isMoving = true;
        targetPosition = transform.position + Vector3.left * moveDistance;

        StartCoroutine(HandleRotationLeft(moveDistance / horizontalSpeed));

        while (Mathf.Abs(transform.position.x - targetPosition.x) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, transform.position.y, transform.position.z), horizontalSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
        rb.velocity = new Vector2(0, speed); // Reset the Y-axis movement
    }
    IEnumerator MoveRight()
    {
        isMoving = true;
        targetPosition = transform.position + Vector3.right * moveDistance;

        StartCoroutine(HandleRotationRight(moveDistance / horizontalSpeed));

        while (Mathf.Abs(transform.position.x - targetPosition.x) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetPosition.x, transform.position.y, transform.position.z), horizontalSpeed * Time.deltaTime);
            yield return null;
        }

        isMoving = false;
        Debug.Log("IsFalse");
        rb.velocity = new Vector2(0, speed); // Reset the Y-axis movement
    }

    IEnumerator HandleRotationLeft(float movementTime)
    {
        float quarterTime = movementTime / 4f;
        float halfTime = movementTime / 2f;
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, 20); // Rotate 20 degrees to the left

        // First 25% of the time: Rotate left
        float time = 0f;
        while (time < quarterTime)
        {
            time += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, time / quarterTime);
            yield return null;
        }

        // Next 50% of the time: Wait
        yield return new WaitForSeconds(halfTime);

        // Last 25% of the time: Reset rotation
        time = 0f;
        while (time < quarterTime)
        {
            time += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(targetRotation, initialRotation, time / quarterTime);
            yield return null;
        }
    }

    IEnumerator HandleRotationRight(float movementTime)
    {
        float quarterTime = movementTime / 4f;
        float halfTime = movementTime / 2f;
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, -20); // Rotate 20 degrees to the right

        // First 25% of the time: Rotate right
        float time = 0f;
        while (time < quarterTime)
        {
            time += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, time / quarterTime);
            yield return null;
        }

        // Next 50% of the time: Wait
        yield return new WaitForSeconds(halfTime);

        // Last 25% of the time: Reset rotation
        time = 0f;
        while (time < quarterTime)
        {
            time += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(targetRotation, initialRotation, time / quarterTime);
            yield return null;
        }
    }

    public IEnumerator RotateCarRandomly(float rotationDuration)
    {
        // Choose a random angle between -360 and 360
        float targetAngle = Random.Range(-360, 360);
        Quaternion initialRotation = transform.rotation;
        Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);

        float startTime = Time.time;

        while (Time.time < startTime + rotationDuration)
        {
            float t = (Time.time - startTime) / rotationDuration;
            transform.rotation = Quaternion.Lerp(initialRotation, targetRotation, t);
            yield return null;
        }

        // Ensure the rotation is set to the target rotation
        transform.rotation = targetRotation;
    }

    public void StopMovement()
    {
        // Set speeds to 0
        speed = 0f;
        horizontalSpeed = 0f;

        // Stop all coroutines
        StopAllCoroutines();
    }

    public IEnumerator RepairCar()
    {
        yield return new WaitForSeconds(2f);
        transform.rotation = Quaternion.identity;
        horizontalSpeed = 8;//inspector value
        isDisdroyed = false;
        isColliding = false;
    }

}
