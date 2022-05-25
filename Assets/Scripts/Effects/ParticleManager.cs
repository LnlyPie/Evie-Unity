using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParticleManager : MonoBehaviour {
   public ParticleSystem leafSystem;

   public bool leavesParticle;

    void Start() {
        leafSystem = GetComponent<ParticleSystem>();
        leavesParticle = false;
    }

    void Update() {
        if (leavesParticle) {
            leafSystem.Play();
        } else {
            leafSystem.Stop();
        }
    }

    void ParticleEnabler(string name, bool status) {
        if (name == "leaves") {
            if (status) {
                leavesParticle = true;
            } else {
                leavesParticle = false;
            }
        }
    }
}