using Game.Scripts.Gameplay.Input.Event_Handling;
using Game.Scripts.Gameplay.Move.Grounding;
using Game.Scripts.Gameplay.StaticData;
using Game.Scripts.Gameplay.Input.Move;
using Game.Scripts.Gameplay.Move.Jump;
using Game.Scripts.Gameplay.Move.Walk;
using Game.Scripts.Gameplay.Physic;
using Game.Scripts.Gameplay.Player;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite;
using UnityEngine;

namespace Game.Scripts.Gameplay.Spawn
{
    public class PlayerSpawnSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world = default;
        
        
        public void Init(IEcsSystems systems)
        {
            var view = Object.Instantiate(Resources.Load<PlayerView>(Indents.Path.PlayerViewPath));
            var config = Resources.Load<PlayerConfig>(Indents.Path.PlayerConfigPath);
            
            var world = _world.Value;
            var player = world.NewEntity();

            world.GetPool<InputEventsListener>().Add(player);
            world.GetPool<PlayerTag>().Add(player);
            world.GetPool<InputMove>().Add(player);

            ref var walk = ref world.GetPool<Walk>().Add(player);
            walk.Speed = config.MoveForce;
            
            ref var jump = ref world.GetPool<Jump>().Add(player);
            jump.Force = config.JumpForce;

            ref var physic = ref world.GetPool<Physical2D>().Add(player);
            physic.Collider = view.Collider;
            physic.Body = view.Rigidbody;
            
            ref var grounding = ref world.GetPool<Grounding>().Add(player);
            grounding.groundLayer = config.GroundLayer;
        }
    }
}