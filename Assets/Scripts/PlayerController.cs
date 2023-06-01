using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    public GameManager gameManager;
    [Header("Properties")]
    public float changeLineSpeed = 0.5f;

    private int currentLineIndex = 0;
    private float currentLinePosition = 0.0f;
    private float startMovementTime = 0.0f;
    private float jorneyLength = 0.0f;
    private Vector3 startMovementPosition;

    private void Start()
    {
        // Set initial values
        currentLineIndex = gameManager.initialLineIndex;
        // Move ball to initial position
        currentLinePosition = gameManager.GetLineXPosition(currentLineIndex);
        transform.position = new Vector3(currentLinePosition, transform.position.y, transform.position.z);

        Debug.Log(string.Format("Start X position: {0}", currentLinePosition));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.W))
        {
            // Move to the left line
            if (currentLineIndex > 0)
            {
                currentLineIndex--;
                MoveBallToLine(currentLineIndex);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) ||  Input.GetKeyDown(KeyCode.D))
        {
            // Move to the right line
            if (currentLineIndex + 1 < gameManager.lineXPositions.Length)
            {
                currentLineIndex++;
                MoveBallToLine(currentLineIndex);
            }
        }

        if (transform.position.x != currentLinePosition)
        {
            float distCovered = (Time.time - startMovementTime) * changeLineSpeed;
            float fractionOfJorney = distCovered / jorneyLength;
            transform.position = Vector3.Lerp(startMovementPosition, new Vector3(currentLinePosition, transform.position.y, transform.position.z), fractionOfJorney);
        }
    }

    public void MoveBallToLine(int lineIndex)
    {
        // Get line X position
        currentLinePosition = gameManager.GetLineXPosition(lineIndex);
        // Set start time
        startMovementTime = Time.time;
        // Set start vector position
        startMovementPosition = transform.position;
        // Calculate the jorney length
        jorneyLength = Vector3.Distance(startMovementPosition, new Vector3(currentLinePosition, transform.position.y, transform.position.z));
    }
}
