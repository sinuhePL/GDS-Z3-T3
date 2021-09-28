using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDS3
{
    public class EndGameController : Interactable
    {
        [SerializeField] private ParticleSystem _meteorShower;
        [SerializeField] private ParticleSystem _distantFirePrefab;
        [SerializeField] private Sound _meteorImpactSound;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float _impactVolume;
        [SerializeField] private BoolReference _isInputBlocked;
        [SerializeField] private FadeOutController _endFade;
        private int _lastParticleCount;
        private ParticleSystem.Particle[] _lastParticles;
        private List<ParticleSystem> _distantFiresList;
        private List<uint> _currentParticlesIdsList;

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
            ParticleSystem.Particle[] currentParticles;
            _currentParticlesIdsList.Clear();
            currentParticles = new ParticleSystem.Particle[_meteorShower.main.maxParticles];
            if (_lastParticleCount > _meteorShower.particleCount)
            {
                _meteorShower.GetParticles(currentParticles);
                foreach(ParticleSystem.Particle currentParticle in currentParticles)
                {
                    _currentParticlesIdsList.Add(currentParticle.randomSeed);
                }
                for(int i=0; i < _lastParticleCount; i++ )
                {
                    if (!_currentParticlesIdsList.Contains(_lastParticles[i].randomSeed))
                    {
                        _distantFiresList.Add(Instantiate(_distantFirePrefab, _lastParticles[i].position, _distantFirePrefab.transform.rotation));
                    }
                }
            }
            _lastParticleCount = _meteorShower.GetParticles(_lastParticles);
        }

        public override void Interact(PlayerCharacterController player)
        {
            _isInputBlocked.Value = true;
            _meteorShower.Play();
            _endFade.EndFade();
        }
    }
}
