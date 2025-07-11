using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;

public class ShareScreenShot : MonoBehaviour 
{
	private string shareScore;
	//Public string subject for share
	public string subject = "Casino Combo 3 in 1- Best Casino Game";
	public string URLShare = "";

	public void ButtonShare()
	{
		SoundController.Sound.CameraSound ();
		shareScore = "" +DataManager.Instance.Coins;;
		StartCoroutine( TakeSSAndShare() );
	}

	private IEnumerator TakeSSAndShare()
	{
		yield return new WaitForEndOfFrame();

		Texture2D ss = new Texture2D( Screen.width, Screen.height, TextureFormat.RGB24, false );
		ss.ReadPixels( new Rect( 0, 0, Screen.width, Screen.height ), 0, 0 );
		ss.Apply();

		string filePath = Path.Combine( Application.temporaryCachePath, "shared img.png" );
		File.WriteAllBytes( filePath, ss.EncodeToPNG() );

		// To avoid memory leaks
		Destroy( ss );
		//new NativeShare().AddFile( filePath ).SetSubject( subject ).SetText( " Huuugeee!!! I'm playing awesome CASINO CLASSIC GAME, My total coins is " + shareScore+ " download "+URLShare).Share();

		// Share on WhatsApp only, if installed (Android only)
		//if( NativeShare.TargetExists( "com.whatsapp" ) )
		//	new NativeShare().AddFile( filePath ).SetText( "Hello world!" ).SetTarget( "com.whatsapp" ).Share();
	}

	//	void Update()
	//	{
	//		if( Input.GetMouseButtonDown( 0 ) )
	//			StartCoroutine( TakeSSAndShare() );
	//	}


		
	}