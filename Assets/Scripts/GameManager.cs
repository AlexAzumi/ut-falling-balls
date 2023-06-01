using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Player references")]
    public GameObject player;
    public Material[] damageStates;
    [Header("Player stats")]
    public int playerPoints = 0;
    public int playerLives = 3;
    [Header("UI references")]
    public TextMeshProUGUI pointsText;
    public GameObject gameOverScreen;
    [Header("Game references")]
    public GameObject pointCube;
    public GameObject regularCube;
    public AudioManager audioManager;
    [Header("Game properties")]
    public float initialGravity = -9.81f;
    public int initialLineIndex = 0;
    public float startYPosition = 0;
    public float spaceBetweenLines = 5.0f;
    public int numInitialLines = 5;
    public float[] lineXPositions;

    private int damageTaken = 0;
    private bool deadPlayer = false;
    private float currentYPosition = 0;

    private MeshRenderer playerMeshRender;
    private SphereCollider playerSphereCollider;
    private Rigidbody playerRigidbody;
    private ParticleSystem playerParticleSystem;

    public void Start()
    {
        // Get escential player components
        playerMeshRender = player.GetComponent<MeshRenderer>();
        playerSphereCollider = player.GetComponent<SphereCollider>();
        playerRigidbody = player.GetComponent<Rigidbody>();
        playerParticleSystem = player.GetComponent<ParticleSystem>();
        // Set properties
        Physics.gravity = new Vector3(0.0f, initialGravity, 0.0f);
        pointsText.text = playerPoints.ToString();
        // Set the initial lines
        for (int i = 0; i < numInitialLines; i++)
        {
            CreateNewLine();
        }
    }

    private void Update()
    {
        // Disable player after death
        if (deadPlayer && playerParticleSystem.isStopped)
        {
            player.SetActive(false);
        }
    }

    public float GetLineXPosition(int line)
    {
        return lineXPositions[line];
    }

    public void AddPlayerPoints(int points)
    {
        playerPoints += points;
        // Update UI
        pointsText.text = playerPoints.ToString();
        // Play SFX
        audioManager.PlayBounceSound();
        // Create a new line
        CreateNewLine();
    }

    public void RemovePlayerLives(int damage)
    {
        damageTaken += damage;

        if (damageTaken - 1 < playerLives)
        {
            // Update damage material
            if (damageTaken < damageStates.Length)
            {
                player.GetComponent<Renderer>().material = damageStates[damageTaken - 1];
            }
            else
            {
                player.GetComponent<Renderer>().material = damageStates[damageStates.Length - 1];
            }
            // Play SFX
            audioManager.PlayBounceSound();
        }
        else
        {
            KillPlayer();
        }
    }

    public void KillPlayer()
    {
        // Disable player
        deadPlayer = true;
        playerMeshRender.enabled = false;
        playerSphereCollider.enabled = false;
        playerRigidbody.useGravity = false;
        playerRigidbody.velocity = Vector3.zero;
        // Start death animation
        playerParticleSystem.Play();
        // Play SFX
        audioManager.PlayExplosionSound();
        // Show game over screen
        gameOverScreen.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    [ContextMenu("Create new line")]
    public void CreateNewLine()
    {
        int nextPointXIndex = Random.Range(0, lineXPositions.Length);
        float nextPointXPosition = lineXPositions[nextPointXIndex];

        Instantiate(pointCube, new Vector3(nextPointXPosition, currentYPosition, 0.0f), pointCube.transform.rotation);

        // Fill remaining positions with regular cubes
        for (int i = 0; i < lineXPositions.Length; i++)
        {
            if (i != nextPointXIndex)
            {
                Instantiate(regularCube, new Vector3(lineXPositions[i], currentYPosition, 0.0f), regularCube.transform.rotation);
            }
        }

        // Increment new line
        currentYPosition -= spaceBetweenLines;
    }
}
