using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;
    const float TAU = Mathf.PI  * 2;

    float movementFactor;
    Vector3 startingPosition;


    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Assert.IsTrue(period > Mathf.Epsilon);
        // Sine work
        float cycles = Time.time / period;
        float rawSineWave = Mathf.Sin(cycles * TAU);
        
        // Applying Sine wave to obstacle movement
        movementFactor = rawSineWave / 2f + 0.5f;
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPosition + offset;
    }
}
