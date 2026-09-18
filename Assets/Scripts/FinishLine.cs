using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float restartDelay = 1f;
    [SerializeField] ParticleSystem finishLikeParticles;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnTriggerEnter2D(Collider2D collision)
    {
        int layerIndex = LayerMask.NameToLayer("Player");
        if(collision.gameObject.layer == layerIndex)
        {
            print("Finish line crossed");
            finishLikeParticles.Play();
            Invoke("reloadScene",restartDelay);
            
        }
        
    }
    void reloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
