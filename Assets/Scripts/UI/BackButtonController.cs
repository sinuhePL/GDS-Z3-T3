using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BackButtonController : MonoBehaviour
{
    [SerializeField]

    public void BackButtonPressed()
    {
        Destroy(gameObject);
    }
}
