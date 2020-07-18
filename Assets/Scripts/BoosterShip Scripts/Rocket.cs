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
    public GameObject thrusters;
    public GameObject turret;

    private ThrusterControl thrusterControl;
    private BoosterTurretControl turretControl;
    private float paralysisDuration = 1f;

    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip deathNoise;
    [SerializeField] AudioClip newLevelChime;
    [SerializeField] ParticleSystem deathNoiseParticles;
    [SerializeField] ParticleSystem newLevelChimeParticles;
    [SerializeField] ParticleSystem paralysisEffects;

    

    void Start()
    {
        audio = GetComponent<AudioSource>();
        thrusterControl = thrusters.GetComponent<ThrusterControl>();
        turretControl = turret.GetComponent<BoosterTurretControl>();
        state = RocketState.Alive;
    }

    void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;

        if(state == RocketState.Alive && tag != "Player Projectile")
        {
            switch (collision.gameObject.tag)
            {
                case "Friendly":
                    break;

                case "Finish":
                    ExecuteTranscending();
                    break;

                default:
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
        disableRocketControl.Invoke();
    }

    public void ExecuteDeath()
    {
        state = RocketState.Dead;
        audio.Stop();
        audio.PlayOneShot(deathNoise);
        deathNoiseParticles.Play();
        Invoke("loadFirstLevel", levelLoadDelay);
        disableRocketControl.Invoke();
    }

    public void ExecuteParalysis()
    {
        if(state == RocketState.Alive)
        {
            thrusterControl.InitializeParalyzeEffect(paralysisDuration);
            turretControl.InitializeParalyzeEffect(paralysisDuration);
        }
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
