
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collisionHandler : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    // CACHE - e.g. references for readability or speed
    // STATE - private instance (member) variables

    [SerializeField] float lvlEndDelay = 2f;
    [SerializeField] AudioClip crashExp;
    [SerializeField] AudioClip winSFX;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem winParticle;

    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisabled = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update() 
    {
        Cheat();    
    }


    void Cheat()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisabled){  return;  }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Friendly");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        audioSource.Stop();
        isTransitioning = true;
        // todo add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(winSFX);
        winParticle.Play();
        Invoke("NextLevel", lvlEndDelay);
    }

        void StartCrashSequence()
    {
        audioSource.Stop();
        isTransitioning = true;
        // todo add particle effect upon crash
        GetComponent<Movement>().enabled = false;
        audioSource.PlayOneShot(crashExp);
        crashParticle.Play();
        Invoke("ReloadLevel", lvlEndDelay);
    }

    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadLevel()
    {        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
