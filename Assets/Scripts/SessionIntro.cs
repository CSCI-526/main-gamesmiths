using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class SessionIntroVideo : MonoBehaviour
{
    [Tooltip("Drag your full-screen RawImage here")]
    public RawImage rawImage;

    private VideoPlayer _vp;

    // static flag: only plays once per session
    private static bool _hasPlayedThisSession = false;

    void Awake()
    {
        _vp = GetComponent<VideoPlayer>();
        _vp.playOnAwake = false;      // we’ll start it in code
        _vp.isLooping  = false;       // make sure it actually ends
        _vp.loopPointReached += OnVideoEnd;
    }

    void Start()
    {
        if (!_hasPlayedThisSession)
        {
            rawImage.gameObject.SetActive(true);
            _vp.Play();
            _hasPlayedThisSession = true;
        }
        else
        {
            // skip it entirely
            rawImage.gameObject.SetActive(false);
            _vp.gameObject.SetActive(false);
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        rawImage.gameObject.SetActive(false);
    }
}
