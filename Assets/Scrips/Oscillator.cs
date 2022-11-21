using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startPosition;
    [SerializeField] Vector3 moveVector;
    [SerializeField][Range(0,1)] float moveFactor;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = moveVector * moveFactor;
        transform.position = startPosition + offset;
    }
}
