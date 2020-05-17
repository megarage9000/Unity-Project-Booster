using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] Vector3 movementVector;

    //TODO remove from inspector
    [Range(0, 1)][SerializeField] float movementFactor;

    Vector3 startingPosition;


    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = movementFactor * movementVector;
        transform.position = startingPosition + offset;
    }
}
