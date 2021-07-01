using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class EnemyDeathController : MonoBehaviour
    {
        public void HandleDeath()
        {
            gameObject.SetActive(false);
        }
    }
}
