using AB_Utility.FromSceneToEntityConverter;
using Game.Scripts.Gameplay.Environment;
using Leopotam.EcsLite.ExtendedSystems;
using Game.Scripts.Gameplay.Cameras;
using Game.Scripts.Gameplay.Input;
using Game.Scripts.Gameplay.Input.Events;
using Game.Scripts.Gameplay.Moving;
using Game.Scripts.Input;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using LeoEcsPhysics;
using UnityEngine;

namespace Game.Scripts.Gameplay.Boot
{
    public class GameplayEscBootstrapper : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        private EcsSystems _fixedUpdateSystems;

        private void Start()
        {
            var physicWorld = new EcsWorld();

            _fixedUpdateSystems = new EcsSystems(physicWorld);
            _fixedUpdateSystems
                .Add(new EnvironmentSetupSystem())
                .Add(new InputHandleSystem())
                .Add(new MoveSystem())
                .Add(new FollowSystem());
            
            EcsPhysicsEvents.ecsWorld = physicWorld;

            SetUpCleanupEvents();
            Initialize();
        }

        private void SetUpCleanupEvents()
        {
            _fixedUpdateSystems
                .DelHere<AttackEvent>()
                .DelHerePhysics();
        }
        
        private void Initialize()
        {
            _fixedUpdateSystems
                .ConvertScene()
                .Inject(new InputActions(), _camera)
                .Init();
        }

        private void FixedUpdate()
            => _fixedUpdateSystems?.Run();

        private void OnDestroy()
        {
            EcsPhysicsEvents.ecsWorld = null;
            
            _fixedUpdateSystems.Destroy();
            _fixedUpdateSystems
                .GetWorld()
                .Destroy();
        }
    }
}