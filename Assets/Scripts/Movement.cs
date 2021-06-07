using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    // CACHE - e.g. references for readability or speed
    // STATE - private instnace (member) variables

    [SerializeField] float mainThrust = 1500f;
    [SerializeField] float rotationThrust = 200f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    [SerializeField] ParticleSystem boostThrustParticles1;
    [SerializeField] ParticleSystem boostThrustParticles2;

    Rigidbody rb; 
    AudioSource audioSource;   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if ( Input.GetKey(KeyCode.Space))
        {
            StartThrust();
        }
        else
        {
            StopThrust();
        }
    }

    void StopThrust()
    {
        audioSource.Stop();
        boostThrustParticles1.Stop();
        boostThrustParticles2.Stop();
    }

    void StartThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!boostThrustParticles1.isPlaying)
        {
            boostThrustParticles1.Play();
        }
        if (!boostThrustParticles2.isPlaying)
        {
            boostThrustParticles2.Play();
        }
    }

    void ProcessRotation()
    {
        if ( Input.GetKey(KeyCode.A))
        {
            StartRightRotation();
        }
        else if ( Input.GetKey(KeyCode.D))
        {
            StartLeftRotation();
        }
        else
        {
            StopRotation();
        }

    }

    void StopRotation()
    {
        leftThrustParticles.Stop();
        rightThrustParticles.Stop();
    }

    void StartLeftRotation()
    {
        ApplyRotation(-rotationThrust);
      
        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }

    void StartRightRotation()
    {
        ApplyRotation(rotationThrust);
        
        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreezing roatation so the physics system can take over
    }

}
