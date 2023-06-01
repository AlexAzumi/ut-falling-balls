using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource bounceSound;
    public AudioSource explosionSound;

    public void PlayBounceSound()
    {
        bounceSound.Play();
    }

    public void PlayExplosionSound()
    {
        explosionSound.Play();
    }
}
