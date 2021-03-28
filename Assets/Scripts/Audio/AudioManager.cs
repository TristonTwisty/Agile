using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instace;

    [SerializeField] private int _sfxSourceLength;

    private AudioSource _BGM;
    private AudioSource[] _sfxSources;
    private int _curSfxIndex = 0;


    private void Awake()
    {
        if(AudioManager.instace == null)
        {
            AudioManager.instace = this;
        }
        else if (AudioManager.instace != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        _BGM = gameObject.AddComponent<AudioSource>();
        _sfxSources = new AudioSource[_sfxSourceLength];

        for (int i = 0; i < _sfxSourceLength; i++)
        {
            _sfxSources[i] = gameObject.AddComponent<AudioSource>();
            _sfxSources[i].spatialBlend = 0;
        }
    }

    public void PlaySfx(AudioClip cliptoplay)
    {
        _sfxSources[_curSfxIndex].clip = cliptoplay;
        _sfxSources[_curSfxIndex].Play();

        _curSfxIndex++;
        if(_curSfxIndex > _sfxSourceLength - 1)
        {
            _curSfxIndex = 0;
        }
    }

    public void PlaySfx(AudioClip clipToPlay, Transform orgin, float spatialBlend)
    {
        AudioSource temp = orgin.gameObject.AddComponent<AudioSource>();
        temp.clip = clipToPlay;
        temp.spatialBlend = spatialBlend;
        temp.Play();
        StartCoroutine(DestroySourceAfterDuration(temp, clipToPlay.length));
    }

    private IEnumerator DestroySourceAfterDuration(AudioSource SourceToDestroy, float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(SourceToDestroy);
    }

    public void playBGM(AudioClip Music,float fadeDuration)
    {
        StartCoroutine(PlayBGMCo(Music, fadeDuration));
    }

    private IEnumerator PlayBGMCo(AudioClip Music, float fadeDuration)
    {
        float t = 0;

        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        newSource.clip = Music;
        newSource.Play();
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            _BGM.volume = Mathf.Lerp(1, 0, t / fadeDuration);
            newSource.volume = Mathf.Lerp(0, 1, t / fadeDuration);
            yield return null;
        }

        Destroy(_BGM);
        _BGM = newSource;
    }
}
