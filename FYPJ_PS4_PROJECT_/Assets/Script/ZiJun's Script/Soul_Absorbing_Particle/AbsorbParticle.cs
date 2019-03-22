using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbParticle : MonoBehaviour
{
    public List<Transform>targetPosition;

    ParticleSystem m_System;
    ParticleSystem.Particle[] m_Particles;
    public float m_Drift = 0.01f;

    // Update is called once per frame
    private void LateUpdate()
    {
        InitializeIfNeeded();

        // GetParticles is allocation free because we reuse the m_Particles buffer between updates
        int numParticlesAlive = m_System.GetParticles(m_Particles);

        if (targetPosition.Count > 0)
        {
            // Change only the particles that are alive
            for (int i = 0; i < numParticlesAlive; i++)
            {
                Vector3 PosZero = new Vector3(0, 0, 0);
                Vector3 PosToOffSet = PosZero - m_Particles[i].position;

                float nearest = float.MaxValue;
                int nearestIndex = 0;

                for (int j = 0; j < targetPosition.Count; ++j)
                {
                    float distance = (m_Particles[i].position - (targetPosition[j].position - this.transform.position)).magnitude;

                    if (distance < nearest)
                    {
                        nearestIndex = j;
                        nearest = distance;
                    }
                }

                Vector3 Direction = (m_Particles[i].position - (targetPosition[nearestIndex].position - this.transform.position)).normalized;

                if (m_Particles[i].remainingLifetime / m_Particles[i].startLifetime < 0.5)
                    m_Particles[i].position = new Vector3(m_Particles[i].position.x - Direction.x, m_Particles[i].position.y - Direction.y, m_Particles[i].position.z - Direction.z);
            }

        }
       
        // Apply the particle changes to the Particle System
        m_System.SetParticles(m_Particles, numParticlesAlive);
    }

    void InitializeIfNeeded()
    {
        if (m_System == null)
            m_System = GetComponent<ParticleSystem>();

        if (m_Particles == null || m_Particles.Length < m_System.main.maxParticles)
            m_Particles = new ParticleSystem.Particle[m_System.main.maxParticles];
    }
}
