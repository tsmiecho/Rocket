using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;

    AudioSource audioSource;
    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

   private void OnCollisionEnter(Collision other) {
       switch(other.gameObject.tag){
            case "Start":
                Debug.Log("Start");
                break;
            case "Finish":
                Invoke("LoadNextLevel", delay);
                break;
            default:
                StatCrashSquence();
                break;         

       }
   }

   void LoadNextLevel()
   {
        audioSource.PlayOneShot(success, 1);
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        if(activeScene > 0) 
        {
            SceneManager.LoadScene(0);
        } else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
   }

   void StatCrashSquence() 
   {
       GetComponent<Movement>().enabled = false;
       audioSource.PlayOneShot(crash, 1);
       Invoke("RelaodLevel", delay);
   }

   private void RelaodLevel() 
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
}
