using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmotionEffect : MonoBehaviour
{
    [SerializeField] Material[] materials;

    [SerializeField] ParticleSystem particle;
    [SerializeField] ParticleSystemRenderer particleRenderer;


    public void PlayEmotion(string emotion)
    {
        particle.Stop();

        Material targetMat = null;

        switch(emotion)
        {
            case "/효과1":
                targetMat = materials[0];
                break;

            case "/효과2":
                targetMat = materials[1];
                break;

            case "/효과3":
                targetMat = materials[2];
                break;
        }

        particleRenderer.material = targetMat;
        particle.Play();
    }
}
