using System;
using UnityEngine;
using System.Collections;
using Improbable.Life;
using Improbable.Unity.Visualizer;

public class DeathVisualizer : MonoBehaviour
{

    [Require] private LifeStateReader lifeState;

    public Animator animator;
        
	// Use this for initialization
	void OnEnable () {
	    if (!lifeState.Alive)
	    {
	        animator.SetBool("InstaDeath", !lifeState.Alive);
	    }
	    lifeState.AliveUpdated += LifeStateOnAliveUpdated;
	}

    private void LifeStateOnAliveUpdated(bool isAlive)
    {
        GetComponent<CapsuleCollider>().radius = isAlive ? 0.5f : 0.1f; 
        animator.SetBool("Death_b", !isAlive);
    }

}
