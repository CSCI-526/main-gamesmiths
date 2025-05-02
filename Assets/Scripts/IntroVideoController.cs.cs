
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

//     void Awake()
//     {
//         _vp = GetComponent<VideoPlayer>();
//         _vp.playOnAwake       = false;
//         _vp.isLooping         = false;
//         _vp.loopPointReached += OnVideoEnd;

//         rawImage.gameObject.SetActive(false);
//     }

//     void Start()
//     {
//         // Only gate on the explicit flag
//         if (TutorialVideoTrigger.ShouldPlayTutorialVideo)
//         {
//             // reset the flag so it only fires once per click
//             TutorialVideoTrigger.ShouldPlayTutorialVideo = false;
//             StartCoroutine(PlayWithDelay());
//         }
//         else
//         {
//             rawImage.gameObject.SetActive(false);
//             _vp.Stop();
//         }
//     }

//     private IEnumerator PlayWithDelay()
//     {
//         yield return new WaitForSecondsRealtime(startDelay);
//         rawImage.gameObject.SetActive(true);
//         _vp.Play();
//     }

//     private void OnVideoEnd(VideoPlayer vp)
//     {
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

        // Log any errors
        _vp.errorReceived += (vp, msg) =>
            Debug.LogError($"[VideoPlayer Error] {msg}");

#if UNITY_WEBGL && !UNITY_EDITOR
        // WebGL must stream from URL in StreamingAssets
        _vp.source = VideoSource.Url;
        _vp.url    = Application.streamingAssetsPath + "/tutorial2.mp4";
#endif

        // Begin loading/preparing the clip
        _vp.Prepare();

        rawImage.gameObject.SetActive(false);
    }

    void Start()
    {
        if (TutorialVideoTrigger.ShouldPlayTutorialVideo)
        {
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
        // wait real-time (unaffected by timeScale)
        yield return new WaitForSecondsRealtime(startDelay);

        // wait until VideoPlayer has prepared the clip
        while (!_vp.isPrepared)
            yield return null;

        rawImage.gameObject.SetActive(true);
        _vp.Play();

        Debug.Log("[IntroVideoController] Play() called on WebGL build");
    }

    private void OnVideoEnd(VideoPlayer vp)
    {
        rawImage.gameObject.SetActive(false);
    }
}
