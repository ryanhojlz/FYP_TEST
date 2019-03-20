using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rain : MonoBehaviour
{
    public RainList currentRain;
    private rainSort currentRainSort;

    public List<rainSort> rainSorts = new List<rainSort>();

    public ParticleSystem cameraParticleSystem;
    public ParticleSystem worldParticleSystem;
    public ParticleSystem cloudParticleSystem;
    public ParticleSystem boltParticleSystem;

    public AudioSource audioSource;

    public enum RainList
    {
        none,
        drizzle,
        rain,
        thunder,
        snow,
        custom1,
        custom2,
        custom3
    }

    [System.Serializable]
    public class rainSort
    {
        public string name;
        public int intensity;
        public Material cameraMaterial;
        public Material worldMaterial;
        public AudioClip sound;
        public int volume;
        public bool haveClouds = true;
        public bool thunder = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //when not currentrainsort
        currentRainSort = rainSorts[(int)currentRain];
        startRain(currentRainSort);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startRain(rainSort rain)
    {
        if (rain.thunder)
        {
            boltParticleSystem.Play();
        }
        else
            boltParticleSystem.Stop();

        if (rain.haveClouds)
            cloudParticleSystem.Play();
        else
            cloudParticleSystem.Stop();
        if(rain.intensity != 0)
        {
            cameraParticleSystem.Play();
            cameraParticleSystem.emissionRate = rain.intensity;
            cameraParticleSystem.GetComponent<ParticleSystemRenderer>().material = rain.cameraMaterial;
            worldParticleSystem.Play();
            worldParticleSystem.emissionRate = rain.intensity * 416 * 1.5f;
            worldParticleSystem.GetComponent<ParticleSystemRenderer>().material = rain.worldMaterial;
            if (rain.sound != null)
            {
                audioSource.clip = rain.sound;
                audioSource.volume = rain.volume;
                audioSource.Play();
            }
            else
                audioSource.Stop();
        }
        else
        {
            worldParticleSystem.Stop();
            cameraParticleSystem.Stop();
            audioSource.Stop();
        }
    }
}
