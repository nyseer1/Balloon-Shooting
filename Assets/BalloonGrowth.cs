using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonGrowth : MonoBehaviour
{
    public float growRate = 0.1f;
    public float maxSize = 2.3f;

    private GameManager gameManager;

    void Start()
    {
        // Modern replacement for FindObjectOfType
        gameManager = Object.FindFirstObjectByType<GameManager>();

        // Grow the balloon every second
        InvokeRepeating(nameof(Grow), 1f, 1f);

        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("level2")){
                Debug.Log("faster growth");
                growRate = 0.2f;
                maxSize = 2f;
                
            }
        else if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName("level3")){
                Debug.Log("faster growth");
                growRate = 0.3f;
                maxSize = 2.5f;
                
            }
    }

    void Grow()
    {
        transform.localScale += Vector3.one * growRate;

        if (transform.localScale.x >= maxSize)
        {
            CancelInvoke(nameof(Grow));
            // When too big → no points, restart level
            Debug.Log("growth max reached");
            if (gameManager != null){
                Debug.Log("swap to level1");
                SceneManager.LoadScene("level1");
                gameManager.RestartLevel();

            }
                
            else
                Debug.LogWarning("GameManager not found!");
            Destroy(gameObject);
        }
    }

    public void Pop()
    {
        CancelInvoke(nameof(Grow));

        if (gameManager != null)
            gameManager.AddScore(transform.localScale.x);

        Destroy(gameObject);
    }

    public float getSize()
    {
        return transform.localScale.x;
    }
    
}
