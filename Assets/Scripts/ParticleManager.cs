using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DeactivateParticleSystem(); // Başlangıçta particle systemlerin deaktif başlaması için ilgili fonksiyonun çağırılması
    }

    public void ActivateParticleSystem()
    {
        GetComponent<ParticleSystem>().Play(); // Particle system componentinin oynatılması
        ParticleSystem.EmissionModule emission = GetComponent<ParticleSystem>().emission; // Emission içeriğinin alınması
        emission.enabled = true; // Emisyon içeriğinin true değeri atanarak aktif halegetirilmesi
        
    }
    public void DeactivateParticleSystem()
    {
        GetComponent<ParticleSystem>().Stop(); // Particle system componentinin durdurulması
        ParticleSystem.EmissionModule emission = GetComponent<ParticleSystem>().emission; // Emission içeriğinin alınması
        emission.enabled = false; // Emisyon içeriğinin false değeri atanarak deaktif halegetirilmesi
    }

}
