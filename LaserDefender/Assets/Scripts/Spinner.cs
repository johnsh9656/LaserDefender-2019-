using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{

    [SerializeField] float speedOfSPin = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speedOfSPin * Time.deltaTime);
    }
}
