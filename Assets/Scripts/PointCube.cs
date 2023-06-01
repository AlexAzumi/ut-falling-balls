using UnityEngine;

public class PointCube : MonoBehaviour
{
    [Header("References")]
    public MeshRenderer meshRenderer;
    public BoxCollider boxCollider;
    public ParticleSystem destroyParticles;
    [Header("Properties")]
    public string playerTag = "Player";
    public int pointsGiven = 1;

    private bool destroyed = false;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (destroyed && destroyParticles.isStopped)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == playerTag)
        {
            // Add point
            gameManager.AddPlayerPoints(pointsGiven);

            destroyParticles.Play();
            // Hide object
            meshRenderer.enabled = false;
            boxCollider.enabled = false;
            // Set to destroy when the particles are over
            destroyed = true;
        }
    }
}
