// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.Video;
// using System.Collections;

// [RequireComponent(typeof(VideoPlayer))]
// public class IntroVideoController : MonoBehaviour
// {
//     [Tooltip("Drag your full-screen RawImage here")]
//     public RawImage rawImage;

//     [Tooltip("Seconds to wait before starting the video")]
//     public float startDelay = 1f;

//     private VideoPlayer _vp;
//     private static bool _hasPlayedThisSession = false;

//     void Awake()
//     {
//         // Grab the VideoPlayer and configure it
//         _vp = GetComponent<VideoPlayer>();
//         _vp.playOnAwake       = false;
//         _vp.isLooping         = false;
//         _vp.loopPointReached += OnVideoEnd;
//          _vp = GetComponent<VideoPlayer>();
//         _vp.playOnAwake       = false;
//         _vp.isLooping         = false;
//         _vp.loopPointReached += OnVideoEnd;

//         // LOG when the VideoPlayer actually begins
//         _vp.started += vp => Debug.Log("[IntroVideoController] VideoPlayer.started event fired");

//         rawImage.gameObject.SetActive(false);
//     }

        

//     void Start()
//     {
//         Debug.Log($"IntroVideoController Start: flag={TutorialVideoTrigger.ShouldPlayTutorialVideo}, playedAlready={_hasPlayedThisSession}");

//         // Only play if we were explicitly flagged, and haven't already
//         if (TutorialVideoTrigger.ShouldPlayTutorialVideo && !_hasPlayedThisSession)
//         {
//             // Reset the flag so it can't fire again
//             TutorialVideoTrigger.ShouldPlayTutorialVideo = false;
//             _hasPlayedThisSession = true;

//             // Kick off the delayed play
//             StartCoroutine(PlayWithDelay());
//         }
//         else
//         {
//             // Hide everything immediately
//             rawImage.gameObject.SetActive(false);
//             _vp.Stop();
//         }
//     }
//     private IEnumerator PlayWithDelay()
//     {
//         Debug.Log($"[IntroVideoController] Waiting {startDelay}s before playing video…");
//         yield return new WaitForSecondsRealtime(startDelay);
//         Debug.Log("[IntroVideoController] Delay over—now enabling UI and calling Play()");
//         rawImage.gameObject.SetActive(true);
//         _vp.Play();
//     }

//     private void OnVideoEnd(VideoPlayer vp)
//     {
//         // When it finishes, hide the UI again
//         rawImage.gameObject.SetActive(false);
//     }
// }
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.Collections;

[RequireComponent(typeof(VideoPlayer))]
public class IntroVideoController : MonoBehaviour
{
    [Tooltip("Drag your full-screen RawImage here")]
    public RawImage rawImage;

    [Tooltip("Seconds to wait before starting the video")]
    public float startDelay = 1f;

    private VideoPlayer _vp;

    void Awake()
    {
        _vp = GetComponent<VideoPlayer>();
        _vp.playOnAwake       = false;
        _vp.isLooping         = false;
        _vp.loopPointReached += OnVideoEnd;

        rawImage.gameObject.SetActive(false);
    }

    void Start()
    {
        // Only gate on the explicit flag
        if (TutorialVideoTrigger.ShouldPlayTutorialVideo)
        {
            // reset the flag so it only fires once per click
            TutorialVideoTrigger.ShouldPlayTutorialVideo = false;
            StartCoroutine(PlayWithDelay());
        }
        else
        {
            rawImage.gameObject.SetActive(false);
            _vp.Stop();
        }
    }

    private IEnumerator PlayWithDelay()
    {
        yield return new WaitForSecondsRealtime(startDelay);
        rawImage.gameObject.SetActive(true);
        _vp.Play();
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        rawImage.gameObject.SetActive(false);
    }
}
