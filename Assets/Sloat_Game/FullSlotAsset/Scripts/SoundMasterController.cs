using UnityEngine;
using System.Collections;
using System;

public class SoundMasterController : MonoBehaviour {

    [Space(8, order = 0)]
    [Header("Default Sound Settings", order = 1)]
    public bool soundOn = true;
    public bool musicOn = true;

    public static SoundMasterController Instance;
    [Space(8, order = 0)]
    [Header("Audio Sources", order = 1)]
    public AudioSource aSclick;
    public AudioSource aSbkg;
    public AudioSource aSloop;

    [Space(8, order = 0)]
    [Header("AudioClips", order = 1)]
    public AudioClip menuClick;
    public AudioClip menuPopup;
    public AudioClip menuCheck;
    public AudioClip screenChange;
    public AudioClip bkgMusic;
    public AudioClip winCoins;
    public AudioClip slotRotation;
    public AudioClip slotLoose;

    WaitForEndOfFrame wff;
    WaitForSeconds wfs0_1;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
      wff = new WaitForEndOfFrame();
      wfs0_1 = new WaitForSeconds(0.1f);
    }

    void Start()
    {
        PlayBkgMusik(musicOn);
    }

    #region play sounds

    public void SoundPlayClick(float playDelay, Action callBack)
    {
        StartCoroutine(PlayClip(playDelay, menuClick, callBack));
    }

    public void SoundPlayPopUp(float playDelay, Action callBack)
    {
        StartCoroutine(PlayClip(playDelay, menuPopup, callBack));
    }

    public void SoundPlayCheck(float playDelay, Action callBack)
    {
        StartCoroutine(PlayClip(playDelay, menuCheck, callBack));
    }

    public void SoundPlayScreenChange(float playDelay, Action callBack)
    {
        StartCoroutine(PlayClip(playDelay, screenChange, callBack));
    }

    public void SoundPlayWinCoins(float playDelay, Action callBack)
    {
        StartCoroutine(PlayClip(playDelay, winCoins, callBack));
    }

    public void SoundPlayRotation(float playDelay, bool loop, Action callBack)
    {
        StartCoroutine(PlayLoopClip(playDelay, loop, slotRotation, callBack));
    }

    public void SoundPlaySlotLoose(float playDelay, bool loop, Action callBack)
    {
        StartCoroutine(PlayLoopClip(playDelay, loop, slotLoose, callBack));
    }

    IEnumerator PlayClip(float playDelay, AudioClip aC, Action callBack)
    {
        if (soundOn)
        {
            if (!aSclick) aSclick = GetComponent<AudioSource>();
            float delay = 0f;
            while (delay < playDelay)
            {
                delay += Time.deltaTime;
                yield return wff;
            }

            if (aSclick && aC)
            {
                aSclick.clip = aC;
                aSclick.Play();
            }

            while (aSclick.isPlaying)
                yield return wff;
            if (callBack != null)
            {
                callBack();
            }
        }
    }

    IEnumerator PlayLoopClip(float playDelay, bool loop, AudioClip aC, Action callBack)
    {
        if (soundOn)
        {
            if (!aSloop) aSloop = GetComponent<AudioSource>();
            float delay = 0f;
            while (delay < playDelay)
            {
                delay += Time.deltaTime;
                yield return wff;
            }

            if (aSloop && aC)
            {
                aSloop.clip = aC;
                aSloop.loop = loop;
                aSloop.Play();
            }
            while (aSloop.isPlaying)
                yield return wff;
            if (callBack != null)
            {
                callBack();
            }
        }
    }

    public void StopLoopClip()
    {
        if (aSloop)
        {
            aSloop.Stop();
        }
    }

    public void PlayBkgMusik(bool play)
    {
        if (play && aSbkg && !aSbkg.isPlaying)
        {
            aSbkg.volume = 0;
            aSbkg.Play();
            LeanTween.value(gameObject, 0.0f, 0.45f, 3.5f).setOnUpdate((float val) => { aSbkg.volume = val; }).
                   setOnComplete(() => { });
        }

        else if (!play && aSbkg)
        {
            LeanTween.value(gameObject, 0.45f, 0.0f, 2f).setOnUpdate((float val) => { aSbkg.volume = val; }).
                  setOnComplete(() => { aSbkg.Stop(); });
            
        }
    }

    #endregion play sounds

}
