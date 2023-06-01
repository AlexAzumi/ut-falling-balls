using UnityEngine;

public class RegularCube : MonoBehaviour
{
    [Header("Properties")]
    public string playerTag = "Player";
    public int livesRemoved = 1;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == playerTag)
        {
            // Add point
            gameManager.RemovePlayerLives(livesRemoved);
        }
    }
}
