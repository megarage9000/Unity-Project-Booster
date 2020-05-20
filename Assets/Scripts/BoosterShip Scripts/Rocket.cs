using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class Rocket : MonoBehaviour
{
    
    enum RocketState { Alive, Transcending, Dead}

    //Components of rocket
    private Rigidbody rigidBody;
    private AudioSource audio;
    private RocketState state;

    public UnityEvent disableRocketControl;
    


    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip deathNoise;
    [SerializeField] AudioClip newLevelChime;
    [SerializeField] ParticleSystem deathNoiseParticles;
    [SerializeField] ParticleSystem newLevelChimeParticles;

    void Start()
    {
        audio = GetComponent<AudioSource>();
        state = RocketState.Alive;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(state == RocketState.Alive)
        {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    break;

                case "Finish":
                    ExecuteTranscending();
                    break;

                default:
                    ExecuteDeath();
                    disableRocketControl.Invoke();
                    break;

            }
        }
    }

    

    private void ExecuteTranscending()
    {
        state = RocketState.Transcending;
        audio.Stop();
        audio.PlayOneShot(newLevelChime);
        newLevelChimeParticles.Play();
        Invoke("loadNextLevel", levelLoadDelay);
    }

    private void ExecuteDeath()
    {
        state = RocketState.Dead;
        audio.Stop();
        audio.PlayOneShot(deathNoise);
        deathNoiseParticles.Play();
        Invoke("loadFirstLevel", levelLoadDelay);
    }

    private void loadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void loadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
        
                             

}
