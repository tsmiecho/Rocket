using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust;
    [SerializeField] float rotationThrust;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem rightThrusterParticle;
    [SerializeField] ParticleSystem leftThrusterParticle;
    Rigidbody rb;
    AudioSource audioSource;

    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
        ProcessSound();
    }

    void ProcessInput()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.forward * rotationThrust * Time.deltaTime);
            if (!leftThrusterParticle.isPlaying)
                leftThrusterParticle.Play();
            rb.freezeRotation = false;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb.freezeRotation = true;
            transform.Rotate(Vector3.back * rotationThrust * Time.deltaTime);
            if (!rightThrusterParticle.isPlaying)
                rightThrusterParticle.Play();
            rb.freezeRotation = false;
        }
    }

    void ProcessSound()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine, 1);
            }
            if(!mainEngineParticle.isPlaying) 
            {
                mainEngineParticle.Play();
            }
        }
        else {
            audioSource.Stop();
        }
    }
}
