using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    public class EnemySpawnerController : MonoBehaviour
    {
        [SerializeField] private GameObject _enemy;
        [SerializeField] private UnityEvent _enemyActivatedEvent;
        [SerializeField] private UnityEvent _enemyAvoidedEvent;
        private bool _isEnemyKilled = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.tag == "Player" && !_isEnemyKilled)
            {
                _enemy.SetActive(true);
                _enemyActivatedEvent.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player" && !_isEnemyKilled)
            {
                _enemy.SetActive(false);
                _enemyAvoidedEvent.Invoke();
            }
        }

        public void SetKilledFlag()
        {
            _isEnemyKilled = true;
            _enemyAvoidedEvent.Invoke();
        }
    }
}
