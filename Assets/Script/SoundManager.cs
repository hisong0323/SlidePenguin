using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource _audioSource;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }
    public void PlaySFX(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip, 0.1f);
    }

    public void PlaySound(AudioClip audioClip)
    {
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }

    public void StopSound()
    {
        _audioSource.Stop();
    }
}
