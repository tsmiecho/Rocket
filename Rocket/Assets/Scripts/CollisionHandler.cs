using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delay = 1f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;

    AudioSource audioSource;
    Movement movement;
    bool isTransitioning = false;
    bool collisonEnabled = true;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        movement = GetComponent<Movement>();
    }

    private void Update() {
        if(Input.GetKey(KeyCode.C)) 
        {
            collisonEnabled = !collisonEnabled;
        }
        if (Input.GetKey(KeyCode.L))
        {
            NextLevel();
        }
    }

   private void OnCollisionEnter(Collision other) {
        if(isTransitioning) 
        {
           return;
        }

        switch(other.gameObject.tag)
        {
            case "Start":
                break;
            case "Finish":
                StatSuccessSquence();
                break;
            default:
                if(collisonEnabled)
                    StatCrashSquence();
                break;         
        }
   }

   void StatSuccessSquence()
   {
        audioSource.Stop();
        isTransitioning = true;
        movement.enabled = false;
        audioSource.PlayOneShot(success, 1);
        successParticle.Play();
        Invoke("NextLevel", delay);
   }

   private void NextLevel() 
   {
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
       audioSource.Stop();
       isTransitioning = true;
       movement.enabled = false;
       audioSource.PlayOneShot(crash, 1);
       crashParticle.Play();
       Invoke("RelaodLevel", delay);
   }

   private void RelaodLevel() 
   {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
}
