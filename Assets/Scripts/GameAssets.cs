using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class GameAssets : MonoBehaviour
    {
        public static GameAssets _instance;

        private void Awake()
        {
            if (_instance == null)
                _instance = this;
            else
            {
                Destroy(this);
            }
        }
    }
}