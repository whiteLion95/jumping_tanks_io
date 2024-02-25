using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;
using Sirenix.OdinInspector;

public class SliderBar : MonoBehaviour
{
    [SerializeField] private float changeSmoothness;
    [SerializeField] private bool lookAtCamera;
    [ShowIf("lookAtCamera")] [SerializeField] private bool updateLook;

    public Action OnZero = delegate { };

    private Slider _slider;
    private Camera _mainCam;

    private void Awake()
    {
        Init();

        _slider = GetComponent<Slider>();

        if (lookAtCamera)
        {
            _mainCam = Camera.main;
            LookAtCamera();
        }  
    }

    protected virtual void Init() { }

    protected virtual void Update()
    {
        if (updateLook)
            LookAtCamera();
    }

    public void SetMaxValue(float value)
    {
        if (_slider != null)
        {
            _slider.maxValue = value;
            SetValue(value);
        }
    }

    public void SetValue(float value)
    {
        _slider.value = value;
    }

    public void ChangeValue(float value)
    {
        _slider.DOValue(value, changeSmoothness).SetUpdate(true).onComplete +=
            () => { if (value <= 0) OnZero?.Invoke(); };
    }

    private void LookAtCamera()
    {
        Vector3 mainCamPos = _mainCam.transform.position;
        transform.parent.LookAt(new Vector3(transform.position.x, mainCamPos.y, mainCamPos.z));
    }
}
