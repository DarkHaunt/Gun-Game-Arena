using Game.Scripts.Gameplay.Input.Event_Handling.Events;
using Game.Scripts.Gameplay.Input.Event_Handling;
using AB_Utility.FromSceneToEntityConverter;
using Game.Scripts.Gameplay.Move.Grounding;
using Leopotam.EcsLite.ExtendedSystems;
using Game.Scripts.Gameplay.Input.Move;
using Game.Scripts.Gameplay.Move.Jump;
using Game.Scripts.Gameplay.Move.Walk;
using Game.Scripts.Input;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using LeoEcsPhysics;
using UnityEngine;

namespace Game.Scripts.Gameplay.Boot
{
    public class GameplayEscHandler : MonoBehaviour
    {
        private EcsSystems _fixedUpdateSystems;


        private void Start()
        {
            var physicWorld = new EcsWorld();

            _fixedUpdateSystems = new EcsSystems(physicWorld);
            _fixedUpdateSystems
                .Add(new InputEventsSendSystem())
                .Add(new InputMoveHandleSystem())
                .Add(new GroundSystem())
                .Add(new WalkSystem())
                .Add(new JumpSystem());
            
            EcsPhysicsEvents.ecsWorld = physicWorld;

            SetUpCleanupEvents();
            Initialize();
        }

        private void SetUpCleanupEvents()
        {
            _fixedUpdateSystems
                .DelHere<AttackEvent>()
                .DelHere<JumpEvent>()
                .DelHere<DownEvent>()
                .DelHerePhysics();
        }
        
        private void Initialize()
        {
            _fixedUpdateSystems
                .ConvertScene()
                .Inject(new InputActions())
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