using UnityEngine;
public class AudioManager : MonoBehaviour
{    
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;
    
    [Header("Audio Clips")]
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip cardFlipSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip scoreSound;
    [SerializeField] private AudioClip buttonSound;
    
    private void Start()
    {
        
        EventBusModel.playAudio.Subscribe(PlayAudio);
        PlayBackgroundMusic();
    }
    
    public void PlayBackgroundMusic()
    {
        if (backgroundMusic != null && !musicSource.isPlaying)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }
    
    private void PlayAudio()
    {
        switch (EventBusModel.playAudio.Value)
        {
            case AudioType.FLIP:
            PlaySFX(cardFlipSound);
            break;
            case AudioType.SCORE:
            PlaySFX(scoreSound);
            break;
            case AudioType.WIN:
            PlaySFX(winSound);
            break;
            case AudioType.BUTTON:
            PlaySFX(buttonSound);
            break;
        }
    }
    public void PlaySFX(AudioClip clip, float volumeMultiplier = 1f)
    {
        if (clip != null)
        {
            sfxSource.PlayOneShot(clip, volumeMultiplier);
        }
    }
}