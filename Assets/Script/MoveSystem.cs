using Unity.Entities;
using Unity.Mathematics;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

namespace Script
{
    public class MoveSystem : JobComponentSystem
    {
        private const float ZOffset = 55f;
        private const float ZOffsetMin = -8f;
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            var lambdaJobDescriptionJcs = Entities.WithName("MoveSystem");
            var handler = lambdaJobDescriptionJcs.ForEach((ref Translation position, ref Rotation rotation) =>
            {
                position.Value += 0.1f * math.forward(rotation.Value);
                if (!(position.Value.z > ZOffset)) return;
                position.Value.z = ZOffsetMin;
            }).Schedule(inputDeps);
            return handler;
        }
    }
}
