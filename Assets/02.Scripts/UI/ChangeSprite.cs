using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChangeSprite : MonoBehaviour
{
    #region Fields

    [SerializeField] private float _minChangeTime = 8f;
    [SerializeField] private float _maxChangeTime = 10f;
    [SerializeField] private float _initialWaitTime = 2f;  // 맨 처음 이미지 대기 시간
    [SerializeField] private float _fadeDuration = 5f;     // 페이드 인&아웃 시간

    private int _currentImageIndex = 1;
    private float _timer = 0f;
    private float _changeTime;
    private bool _fadeInProgress = false;
    private bool _fadeOutProgress = false;
    private bool _initialWaitCompleted = false;

    private Image _imageComponent;

    #endregion

    #region MonoBehaviours

    private void Awake()
    {
        _imageComponent = GetComponent<Image>();
        SetRandomChangeTime();
    }

    private void Start()
    {
        StartCoroutine(InitialWait());
    }

    private void Update()
    {
        if (!_initialWaitCompleted)
            return;

        _timer += Time.deltaTime;

        if (_fadeInProgress)
        {
            float alpha = Mathf.Clamp01(_timer / _fadeDuration);
            Color newColor = _imageComponent.color;
            newColor.a = alpha;
            _imageComponent.color = newColor;

            if (_timer >= _fadeDuration)
            {
                _fadeInProgress = false;
                FadeOutImage();
            }
        }

        if (_fadeOutProgress)
        {
            float alpha = Mathf.Clamp01(1 - (_timer / _fadeDuration));
            Color newColor = _imageComponent.color;
            newColor.a = alpha;
            _imageComponent.color = newColor;

            if (_timer >= _fadeDuration)
            {
                _fadeOutProgress = false;

                _currentImageIndex = (_currentImageIndex % 4) + 1;
                LoadSprite(_currentImageIndex);

                SetRandomChangeTime();
                FadeInImage();
            }
        }

        if (!_fadeInProgress && !_fadeOutProgress)
        {
            // 페이드 인과 페이드 아웃이 모두 진행 중이 아닐 때 타이머 초기화
            _timer = 0f;
        }
    }

    #endregion

    #region Methods

    private void LoadSprite(int index)
    {
        string path = "Images/hand" + index;
        Sprite newSprite = Resources.Load<Sprite>(path);
        if (newSprite != null)
        {
            _imageComponent.sprite = newSprite;
        }
        else
        {
            Debug.LogError("이미지를 로드할 수 없습니다. 경로 : " + path);
        }
    }

    private void SetRandomChangeTime()
    {
        _changeTime = Random.Range(_minChangeTime, _maxChangeTime);
    }

    private void FadeInImage()
    {
        _timer = 0f;
        _fadeInProgress = true;
        _fadeOutProgress = false; // 페이드 아웃이 진행 중이 아닌지 확인
    }

    private void FadeOutImage()
    {
        _timer = 0f;
        _fadeOutProgress = true;
        _fadeInProgress = false; // 페이드 인이 진행 중이 아닌지 확인
    }

    private IEnumerator InitialWait()
    {
        yield return new WaitForSeconds(_initialWaitTime);
        _initialWaitCompleted = true;
        LoadSprite(_currentImageIndex);
        FadeInImage();
    }

    #endregion
}
