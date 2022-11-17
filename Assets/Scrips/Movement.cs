using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem leftParticle;
    [SerializeField] ParticleSystem rightParticle;
    [SerializeField] ParticleSystem mainParticle;

    Rigidbody rocketrb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rocketrb = GetComponent<Rigidbody>();
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
        if (Input.GetKey(KeyCode.Space))
        {
            rocketrb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if(!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
            if (!mainParticle.isPlaying)
            {
            mainParticle.Play();
            }
        }
        else
        {
            audioSource.Stop();
            mainParticle.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            rightParticle.Stop();
            ApplyRotation(rotationThrust);
            if (!leftParticle.isPlaying)
            {
                leftParticle.Play();
            }
        }
        else if (Input.GetKey(KeyCode.D))
        {
            leftParticle.Stop();
            ApplyRotation(-rotationThrust);
            if (!rightParticle.isPlaying)
            {
                rightParticle.Play();
            }
        }
        else
        {
            leftParticle.Stop();
            rightParticle.Stop();
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rocketrb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rocketrb.freezeRotation = false; // unfreezing rotation
    }
}
