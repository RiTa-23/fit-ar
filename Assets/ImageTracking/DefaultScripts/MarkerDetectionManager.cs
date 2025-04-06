using System;
using UniRx;
using UnityEngine;
using UnityEngine.XR.ARFoundation;


public class MarkerDetectionManager : MonoBehaviour
{
    
    [SerializeField] private ARTrackedImageManager m_trackedImageManager;

    public IObservable<ARTrackedImage> OnAddImage => m_onAddImage;
    private readonly Subject<ARTrackedImage> m_onAddImage = new Subject<ARTrackedImage>();

    public IObservable<ARTrackedImage> OnUpdateImage => m_onUpdateImage;
    private readonly Subject<ARTrackedImage> m_onUpdateImage = new Subject<ARTrackedImage>();

    public IObservable<ARTrackedImage> OnRemovedImage => m_onRemovedImage;
    private readonly Subject<ARTrackedImage> m_onRemovedImage = new Subject<ARTrackedImage>();
    private void OnEnable()
    {
        m_trackedImageManager.trackedImagesChanged += OnChangeMarker;
    }

    private void OnDisable()
    {
        m_trackedImageManager.trackedImagesChanged -= OnChangeMarker;
    }
    
    private void OnChangeMarker(ARTrackedImagesChangedEventArgs imagesChangedEventArgs)
    {
        // マーカーを初めて認識した時に呼ばれる
        foreach (var _arTrackedImage in imagesChangedEventArgs.added)
        {
            m_onAddImage.OnNext(_arTrackedImage);
        }

        // マーカーを認識した後、トラッキング状態になっている間ここが呼ばれる
        foreach (var _arTrackedImage in imagesChangedEventArgs.updated)
        {
            m_onUpdateImage.OnNext(_arTrackedImage);
        }
        
        // マーカーを認識しなくなったときに呼ばれる。
        foreach (var _arTrackedImage in imagesChangedEventArgs.removed)
        {
            m_onRemovedImage.OnNext(_arTrackedImage);
        }
    }
}