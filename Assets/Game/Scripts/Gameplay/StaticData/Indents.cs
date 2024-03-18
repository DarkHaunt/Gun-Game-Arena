using Game.Scripts.Gameplay.Weapons;

namespace Game.Scripts.Gameplay.StaticData
{
    public static class Indents
    {
        public static class PlayerIndents
        {
            public static readonly WeaponHandleData DefaultWeaponHandle = new()
            {
                Duration = float.PositiveInfinity,
            };
        }
        
        public static class World
        {
            public const string Events = "events";
        }

        public static class Path
        {
            public const string EnemyViewPath = "Enemy";
            public const string LevelViewPath = "Level";
            public const string PlayerViewPath = "Player";
            
            public const string EnemyConfigPath = "Gameplay/EnemyConfig";
            public const string PlayerConfigPath = "Gameplay/PlayerConfig";
            public const string EnvironmentConfigPath = "Gameplay/EnvironmentConfig";
        }
    }
}