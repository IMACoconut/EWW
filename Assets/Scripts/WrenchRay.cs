using UnityEngine;
using System.Collections;
using System;

public class WrenchRay : MonoBehaviour {

    private Particle[] particles;
    public int particleCount = 100;
    private float invParticleCount;
    public bool emitEnabled = false;
    SoundBankManager SoundBank;

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
        if (emitEnabled)
            return;

        particleEmitter.emit = false;
        particleEmitter.ClearParticles();
        particleEmitter.Emit(particleCount);

        particles = particleEmitter.particles;
        particleCount = particleEmitter.particleCount;
        invParticleCount = 1.0f / (float)particleCount;
        emitEnabled = true;
        Debug.Log("Start emit");
        
    }

    public void StopEmit()
    {
        if (!emitEnabled)
            return;

        particleEmitter.emit = false;
        emitEnabled = false;
        particleEmitter.ClearParticles();
        Debug.Log("Stop emit");
    }
	
	// Update is called once per frame
	void Update () {

        if (Constants.pause || !emitEnabled || target == null)
            return;

        if (noise == null)
            noise = new Perlin();

        Vector3 orig = origin.position;
        float timex = Time.time * noiseFactor * 0.1365143f;
        float timey = Time.time * noiseFactor * 1.21688f;
        float timez = Time.time * noiseFactor * 2.5564f;
        float dist = (target - orig).magnitude;
        for (int i = 0; i < particles.Length; i++)
        {
            float t = (float)i * invParticleCount;

            Vector3 position = Vector3.Lerp(orig, target, t);
            Vector3 offset2 = new Vector3(0.0f, (float)Math.Cos((position - orig).magnitude * Time.time) * 1.0f / dist, 0.0f);
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
