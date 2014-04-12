using UnityEngine;
using System.Collections;
using System;

public class WrenchRay : MonoBehaviour {

    private Particle[] particles;
    public int particleCount = 100;
    private float invParticleCount;
    public bool emitEnabled = false;

    public Transform origin;
    public Vector3 target;
    private Perlin noise = null;
    public float speed = 1.0f;
    public float scale = 1.0f;
    public float frequency = 5.0f;
    public float amplitude = 0.3f;
    public float noiseFactor = 1.0f;
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
        Debug.Log(origin.position);

        if (Constants.pause || !emitEnabled)
            return;

        if (noise == null)
            noise = new Perlin();

        float timex = Time.time * noiseFactor * 0.1365143f;
        float timey = Time.time * noiseFactor * 1.21688f;
        float timez = Time.time * noiseFactor * 2.5564f;
        float dist = (target - origin.position).magnitude;
        for (int i = 0; i < particles.Length; i++)
        {
            float t = (float)i * invParticleCount;

            Vector3 position = Vector3.Lerp(origin.position, target, t);
            Vector3 offset2 = new Vector3(0.0f, (float)Math.Cos((position - origin.position).magnitude * Time.time) * 1.0f / dist, 0.0f);
            Vector3 offset = new Vector3(noise.Noise(timex + position.x, timex + position.y, timex + position.z),
                                        noise.Noise(timey + position.x, timey + position.y, timey + position.z),
                                        noise.Noise(timez + position.x, timez + position.y, timez + position.z));
            position += offset2;//+(offset * scale * ((float)i * invParticleCount));
            /*position += offset;
            Vector3 offnoise = new Vector3(noise.Noise(position.x * timex, position.x * timex, position.x * timex),
                                           noise.Noise(position.y * timey, position.y * timey, position.y * timey),
                                           noise.Noise(position.z * timez, position.z * timez, position.z * timez));*/

            particles[i].position = position;
            particles[i].color = Color.white;
            particles[i].energy = 1f;
        }

        particleEmitter.particles = particles;
	}
}
