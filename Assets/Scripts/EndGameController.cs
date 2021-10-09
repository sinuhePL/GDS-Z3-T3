using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GDS3
{
    public class EndGameController : Interactable
    {
        [SerializeField] private ParticleSystem _meteorShower;
        [SerializeField] private ParticleSystem _distantFirePrefab;
        [SerializeField] private BoolReference _isInputBlocked;
        [SerializeField] private UnityEvent _meteorHitEvent;
        [SerializeField] private UnityEvent _endGameEvent;
        private int _lastParticleCount;
        private ParticleSystem.Particle[] _lastParticles;
        private List<ParticleSystem> _distantFiresList;
        private List<uint> _currentParticlesIdsList;
        private bool _endGameStarted = false;

        private new void Awake()
        {
            base.Awake();
        }

        // Start is called before the first frame update
        void Start()
        {
            _lastParticles = new ParticleSystem.Particle[_meteorShower.main.maxParticles];
            _distantFiresList = new List<ParticleSystem>();
            _currentParticlesIdsList = new List<uint>();
        }

        // Update is called once per frame
        new void Update()
        {
            base.Update();
            if (_endGameStarted)
            {
                ParticleSystem.Particle[] currentParticles;
                float scaleFactor;
                ParticleSystem newParticleSystem;
                _currentParticlesIdsList.Clear();
                currentParticles = new ParticleSystem.Particle[_meteorShower.main.maxParticles];
                if (_lastParticleCount > _meteorShower.particleCount)
                {
                    _meteorShower.GetParticles(currentParticles);
                    foreach (ParticleSystem.Particle currentParticle in currentParticles)
                    {
                        _currentParticlesIdsList.Add(currentParticle.randomSeed);
                    }
                    for (int i = 0; i < _lastParticleCount; i++)
                    {
                        if (!_currentParticlesIdsList.Contains(_lastParticles[i].randomSeed))
                        {
                            scaleFactor = 1.0f - (_lastParticles[i].position.y + 38.0f) / 5.0f;
                            newParticleSystem = Instantiate(_distantFirePrefab, _lastParticles[i].position, _distantFirePrefab.transform.rotation);
                            newParticleSystem.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);
                            _distantFiresList.Add(newParticleSystem);
                            _meteorHitEvent.Invoke();
                        }
                    }
                }
                _lastParticleCount = _meteorShower.GetParticles(_lastParticles);
            }
        }

        public override void Interact(PlayerCharacterController player)
        {
            _isInputBlocked.Value = true;
            _meteorShower.Play();
            _endGameEvent.Invoke();
            _endGameStarted = true;
        }
    }
}
