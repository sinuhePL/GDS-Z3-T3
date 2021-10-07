using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    public class EnemySpawnerController : MonoBehaviour
    {
        [SerializeField] private GameObject _enemy;
        private bool _isEnemyKilled = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player" && !_isEnemyKilled)
            {
                _enemy.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                _enemy.SetActive(false);
            }
        }

        public void SetKilledFlag()
        {
            _isEnemyKilled = true;
        }
    }
}
