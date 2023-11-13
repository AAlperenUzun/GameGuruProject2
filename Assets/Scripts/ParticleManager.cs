using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public ParticleSystem perfectParticle;

    public void PlayPerfectParticle(Transform platform)
    {
        var tempEffect = Instantiate(perfectParticle, platform.position, quaternion.identity, platform);
        Destroy(tempEffect, 3f);
    }
}
