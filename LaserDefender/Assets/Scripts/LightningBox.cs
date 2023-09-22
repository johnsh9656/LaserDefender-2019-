using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBox : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        FindObjectOfType<PlayerScript>().LightningBox();
    }
}
