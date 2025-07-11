using UnityEngine;
using System.Collections;

/// <summary>
/// sound in game : button, effect, win, lose...
/// </summary>
public class SoundController : MonoBehaviour
{

    public static SoundController Sound; // instance of SoundController

    public AudioClip[] SoundClips;      // array sound clips

    public AudioSource audiosource;     // audio source
    void Awake()
    {
        if (Sound == null)
        {
            DontDestroyOnLoad(gameObject);
            Sound = this;
        }
        else if (Sound != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// sound on state
    /// </summary>
    public void SoundON()
    {
        audiosource.mute = false;
    }

    /// <summary>
    /// sound off state
    /// </summary>
    public void SoundOFF()
    {
        audiosource.mute = true;
    }

    public void ClickBtn()
    {
        audiosource.PlayOneShot(SoundClips[0]);

    }
    public void VideoPoker_DealOrDrawCard()
    {
        audiosource.PlayOneShot(SoundClips[1]);
    }
	public void VideoPoker_Lose()
	{
		audiosource.PlayOneShot(SoundClips[2]);
	}

	public void VideoPoker_MoreCoin()
    {
        audiosource.PlayOneShot(SoundClips[3]);
    }
	public void PopupShow()
    {
        audiosource.PlayOneShot(SoundClips[4]);
    }

	public void VideoPoker_Win()
    {
        audiosource.PlayOneShot(SoundClips[5]);
    }
    public void BlackJack_Start()
    {
        audiosource.PlayOneShot(SoundClips[6]);
    }
	public void OpenGift()
	{
		audiosource.PlayOneShot(SoundClips[7]);
	}
	public void CloseBtn()
	{
		audiosource.PlayOneShot(SoundClips[8]);
	}
	public void BlackJack_Win()
	{
		audiosource.PlayOneShot(SoundClips[9]);
	}
	public void DisactiveButtonSound()
	{
		audiosource.PlayOneShot(SoundClips[10]);
	}
	public void OpenGiftEnd()
	{
		audiosource.PlayOneShot(SoundClips[11]);
	}
	public void CallBackSuccess()
	{
		audiosource.PlayOneShot(SoundClips[12]);
	}
	public void SpinningSound()
	{
		audiosource.PlayOneShot(SoundClips[13]);
	}
	public void In()
	{
		audiosource.PlayOneShot(SoundClips[14]);
	}
	public void Out()
	{
		audiosource.PlayOneShot(SoundClips[15]);
	}
	public void CameraSound()
	{
		audiosource.PlayOneShot(SoundClips[16]);
	}
	public void Intro()
	{
		audiosource.PlayOneShot(SoundClips[17]);
	}
	public void EarnSmall()
	{
		audiosource.PlayOneShot(SoundClips[18]);
	}
	public void EarnBig()
	{
		audiosource.PlayOneShot(SoundClips[19]);
	}
	public void Pay()
	{
		audiosource.PlayOneShot(SoundClips[20]);
	}
	public void Spin()
	{
		audiosource.PlayOneShot(SoundClips[21]);
	}
	public void SpinLoop()
	{
		audiosource.PlayOneShot(SoundClips[22]);
	}
	public void ReelStop()
	{
		audiosource.PlayOneShot(SoundClips[23]);
	}
	public void Click()
	{
		audiosource.PlayOneShot(SoundClips[24]);
	}
	public void WinSmall()
	{
		audiosource.PlayOneShot(SoundClips[25]);
	}
	public void WinMedium()
	{
		audiosource.PlayOneShot(SoundClips[26]);
	}
	public void WinBig()
	{
		audiosource.PlayOneShot(SoundClips[27]);
	}
	public void Lose()
	{
		audiosource.PlayOneShot(SoundClips[28]);
	}
	public void Bet()
	{
		audiosource.PlayOneShot(SoundClips[29]);
	}
	public void Impact()
	{
		audiosource.PlayOneShot(SoundClips[30]);
	}
	public void Beep()
	{
		audiosource.PlayOneShot(SoundClips[31]);
	}
	public void Bonus()
	{
		audiosource.PlayOneShot(SoundClips[32]);
	}
	public void WinSpecial()
	{
		audiosource.PlayOneShot(SoundClips[33]);
	}
	public void SpinBonus()
	{
		audiosource.PlayOneShot(SoundClips[34]);
	}
	public void SpinLoopFast()
	{
		audiosource.PlayOneShot(SoundClips[35]);
	}



}
