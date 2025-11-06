using Battle.Combat.Calculations;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

namespace Battle.Combat.AttackSources
{
    /// <summary>
    /// Determines whether attacks hit or miss their target.
    /// </summary>
    [UpdateInGroup(typeof(AttackSystemsGroup))]
    public class DetermineAttackMissSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            uint seed = (uint)UnityEngine.Random.Range(1,100000);

            Entities
                .ForEach( ( Entity entity , int entityInQueryIndex , ref Attack attack , in Target target ) =>
                {
                    if( SystemAPI.HasComponent<Evasion>(target.Value) )
                    {
                        float evasion = SystemAPI.GetComponent<Evasion>(target.Value).Rating;
                        float hitChance = math.exp( -evasion / attack.Accuracy );

                        var rnd = new Random( seed + (uint)(entityInQueryIndex*1000) );
                        if( rnd.NextFloat()>hitChance )
                            attack.Result = Attack.eResult.Miss;
                    }
                } )
                .WithBurst()
                .ScheduleParallel();
        }
    }
}