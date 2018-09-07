using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSmokePosition : MonoBehaviour {
	public Transform muzzlePosition;
	ParticleSystem.Particle[] particles;
	ParticleSystem system;
	public float timeToPause;
	public float lifespan;
	float time;
	
	void Awake() {
    system = GetComponent<ParticleSystem>();
  }
	
	void Update () {
		transform.position = muzzlePosition.position;

		time += Time.deltaTime;
		if (time > timeToPause) {
				particles = new ParticleSystem.Particle[system.particleCount];
				system.GetParticles(particles);
		}
		
		if (particles != null) {
				for (int p = 0; p < particles.Length; p++) {
						Color color = particles[p].startColor;
						color.a = ((lifespan - time + timeToPause) / lifespan);
						particles[p].startColor = color;
				}
				system.SetParticles(particles, particles.Length);
		}

	}
}
