using UnityEngine;
using System.Collections;

public class WrenchRay : MonoBehaviour {

    private Particle[] particles;
    public int particleCount = 100;
    private float invParticleCount;
    public bool emitEnabled = false;

    public Vector3 target;
    private Perlin noise = null;
    public float speed = 1.0f;
    public float scale = 1.0f;
	// Use this for initialization
	void Start () {
        
	}

    public void StartEmit()
    {
        particleEmitter.emit = false;
        particleEmitter.ClearParticles();
        particleEmitter.Emit(particleCount);

        particles = particleEmitter.particles;
        particleCount = particleEmitter.particleCount;
        invParticleCount = 1.0f / (float)particleCount;
        emitEnabled = true;
    }

    public void StopEmit()
    {
        particleEmitter.emit = false;
        emitEnabled = false;
        particleEmitter.ClearParticles();
    }
	
	// Update is called once per frame
	void Update () {
        if (Constants.pause || !emitEnabled)
            return;

        if (noise == null)
            noise = new Perlin();

        float timex = Time.time * speed * 0.1365143f;
        float timey = Time.time * speed * 1.21688f;
        float timez = Time.time * speed * 2.5564f;

        for (int i = 0; i < particles.Length; i++)
        {
            Vector3 position = Vector3.Lerp(transform.position, target, invParticleCount * (float)i);
            Vector3 offset = new Vector3(noise.Noise(timex + position.x, timex + position.y, timex + position.z),
                                        noise.Noise(timey + position.x, timey + position.y, timey + position.z),
                                        noise.Noise(timez + position.x, timez + position.y, timez + position.z));
            position += (offset * scale * ((float)i * invParticleCount));

            particles[i].position = position;
            particles[i].color = Color.white;
            particles[i].energy = 1f;
        }

        particleEmitter.particles = particles;
	}
}
