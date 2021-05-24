using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class BackButtonController : MonoBehaviour
    {
        [SerializeField]

        public void BackButtonPressed()
        {
            Destroy(gameObject);
        }
    }
}
