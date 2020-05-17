using UnityEngine.SceneManagement;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    
    enum RocketState { Alive, Transcending, Dead}

    //Components of rocket
    private Rigidbody rigidBody;
    private AudioSource audio;
    private RocketState state;
    

    [SerializeField] float rcsRotationSpeed = 100f;
    [SerializeField] float rcsThrusterSpeed = 100f;
    [SerializeField] float rcsGravityModifier = 5f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathNoise;
    [SerializeField] AudioClip newLevelChime;
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        state = RocketState.Alive;

    }

    // Update is called once per frame
    void Update()
    {
        if(state == RocketState.Alive)
        {
            RespondToThrustInput();
            RespondToRotationInput();
        }

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
                    break;

            }
        }
    }

    

    private void ExecuteTranscending()
    {
        state = RocketState.Transcending;
        audio.Stop();
        audio.PlayOneShot(newLevelChime);
        Invoke("loadNextLevel", 1f);
    }

    private void ExecuteDeath()
    {
        state = RocketState.Dead;
        audio.Stop();
        audio.PlayOneShot(deathNoise);
        Invoke("loadFirstLevel", 1f);
    }

    private void loadNextLevel()
    {
        SceneManager.LoadScene(1);
    }

    private void loadFirstLevel()
    {
        SceneManager.LoadScene(0);
    }
        
                                
    // Handles the input of the Rocket
    // - Rotation and thrusters
    private void RespondToRotationInput()
    {
        //Freeze rotation prior to rotation
        rigidBody.freezeRotation = true;

        //Calculating rotation per frame with deltaTime for consistent rotation
        float rotationAtFrame = rcsRotationSpeed * Time.deltaTime; 

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationAtFrame);
            Debug.Log("Rotate Left");
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationAtFrame);
            Debug.Log("Rotate Right");
        }

        //Enabling rotation after rotation has been added
        rigidBody.freezeRotation = false; 

    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            //Reducing gravity by adding a constant opposite force
            rigidBody.AddForce(Vector3.down * rcsGravityModifier * Time.deltaTime, ForceMode.Acceleration);
            audio.Stop();
        }
    }

    private void ApplyThrust()
    {
        //Calculating thrust per frame with deltaTime for conisitent thrusting
        float thrustAtFrame = rcsThrusterSpeed * Time.deltaTime;

        rigidBody.AddRelativeForce(Vector3.up * thrustAtFrame, ForceMode.Impulse);

        //play thrust audio on press
        if (!audio.isPlaying)
        {
            audio.PlayOneShot(mainEngine);
        }
    }

}
