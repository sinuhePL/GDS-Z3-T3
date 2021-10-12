using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class BackButtonController : MonoBehaviour
    {
        public void BackButtonPressed()
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
