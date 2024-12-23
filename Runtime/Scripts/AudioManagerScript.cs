using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{

    public AudioClip TapSound;
    public AudioClip WrongTapSound;
    public AudioClip CollectingItemSound;
    public AudioClip GettingIntoBasketSound;
    public AudioClip ItemChangeSound;
    public AudioClip FireworksSound;
    public AudioClip BackgroundMusic;
    public AudioClip StartingSound;
    public AudioClip[] EvaluationSounds;



    [Range(0.0f, 1f)]
    public float TapSoundVolume;
    [Range(0.0f, 1f)]
    public float WrongTapSoundVolume;
    [Range(0.0f, 1f)]
    public float CollectingItemSoundVolume;
    [Range(0.0f, 1f)]
    public float GettingIntoBasketSoundVolume;
    [Range(0.0f, 1f)]
    public float ItemChangeSoundVolume;
    [Range(0.0f, 1f)]
    public float EvaluationSoundVolume;
    [Range(0.0f, 1f)]
    public float FireworksSoundVolume;
    [Range(0.0f, 1f)]
    public float BackgroundMusicVolume;
    [Range(0.0f, 1f)]
    public float StartingSoundVolume;



    public AudioSource TapSoundSrc;
    public AudioSource WrongTapSoundSrc;
    public AudioSource CollectingItemSoundSrc;
    public AudioSource GettingIntoBasketSoundSrc;
    public AudioSource ItemChangeSoundSrc;
    public AudioSource EvaluationSoundSrc;
    public AudioSource FireworksSoundSrc;
    public AudioSource BackgroundMusicSrc;
    public AudioSource StartingSoundSrc;


    int EvaluationSoundIndex;



    public void PlayTapSound()
    {
        TapSoundSrc.volume = TapSoundVolume;
        TapSoundSrc.PlayOneShot(TapSound);
    }


    public void PlayWrongTapSound()
    {
            // WrongTapSoundSrc.volume = WrongTapSoundVolume;
            // WrongTapSoundSrc.PlayOneShot(WrongTapSound);
    }


    public void PlayCollectingItemSound()
    {
        CollectingItemSoundSrc.volume = CollectingItemSoundVolume;
        CollectingItemSoundSrc.PlayOneShot(CollectingItemSound);
    }


    public void PlayGettingIntoBasketSound()
    {
        GettingIntoBasketSoundSrc.volume = GettingIntoBasketSoundVolume;
        GettingIntoBasketSoundSrc.PlayOneShot(GettingIntoBasketSound);
    }


    public void PlayItemChangeSound()
    {
        ItemChangeSoundSrc.volume = ItemChangeSoundVolume;
        ItemChangeSoundSrc.PlayOneShot(ItemChangeSound);
    }


    public void PlayEvaluationSound()
    {
        EvaluationSoundSrc.volume = EvaluationSoundVolume;

       //EvaluationSoundSrc.PlayOneShot(EvaluationSounds[EvaluationSoundIndex]);

        if(EvaluationSoundIndex < EvaluationSounds.Length - 1)
        {
            EvaluationSoundIndex++;
           // print(EvaluationSoundIndex);
        }
        else
        {
            EvaluationSoundIndex = 0;
           // print(EvaluationSoundIndex);
        }
    }


    public void PlayFireworksSound()
    {
        FireworksSoundSrc.volume = FireworksSoundVolume;
        FireworksSoundSrc.PlayOneShot(FireworksSound);
    }


    public void PlayBackgroundMusic()
    {
        //BackgroundMusic.Play(BackgroundMusic);
    }


    public void PlayStartingSound()
    {
        StartingSoundSrc.volume = StartingSoundVolume;
        StartingSoundSrc.PlayOneShot(StartingSound);
    }



    IEnumerator DelayStartSound()
    {

        yield return new WaitForSeconds(0.3f);
        PlayStartingSound();

    }


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayStartSound());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
