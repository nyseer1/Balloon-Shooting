using UnityEngine;
using UnityEngine.SceneManagement;
public class PopOnContact : MonoBehaviour
{
    public AudioClip popSound;
    public AudioClip bounceSound;
    private AudioSource audioSource;
    private GameManager gameManager;
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Debug.Log("started");

        // Use the modern API to find the GameManager
        gameManager = Object.FindFirstObjectByType<GameManager>();

         // Get the SpriteRenderer component if not already assigned in the Inspector
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        // Make the sprite invisible
        spriteRenderer.enabled = true;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("entered");
        if (other.CompareTag("balloon"))
        {
            Debug.Log("pop balloon");
            // Play pop sound if available
            if (audioSource == null){
                Debug.Log("no audio");
            }
            if (audioSource != null && popSound != null)
            {
                Debug.Log("fart time");
                audioSource.PlayOneShot(popSound);
            }

            // Let the balloon handle its own pop logic
            BalloonGrowth balloon = other.GetComponent<BalloonGrowth>();
            if (balloon != null)
                balloon.Pop();
            
            Debug.Log("supposed to add score");
            gameManager.AddScore(balloon.getSize());

            // Destroy the pin
            spriteRenderer.enabled = false;
            Destroy(gameObject, 2f);

            if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("level1")){
                Debug.Log("swap to level2");
                SceneManager.LoadScene("level2");
            }
            else if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("level2")){
                Debug.Log("swap to level3");
                SceneManager.LoadScene("level3");
            }
            else{
                Debug.Log("swap to win screen");
                SceneManager.LoadScene("win_screen");

            }
        }


        if (other.CompareTag("trampoline"))
        {
            Debug.Log("hit trampoline");
            // Play pop sound if available
            if (audioSource == null){
                Debug.Log("no audio");
            }
            if (audioSource != null && bounceSound != null)
            {
                Debug.Log("bounce time");
                audioSource.PlayOneShot(bounceSound);
            }

            //flip the pin backwards
            // Vector3 scale = transform.localScale;
            // scale.y *= -1; // Flip horizontally (use scale.y *= -1 if you want vertical flip)
            // transform.localScale = -scale.y;
            PinMovement movement = GetComponent<PinMovement>();
            if (movement != null)
            {
                movement.direction = Vector3.down;
            }
        }
    }
}
