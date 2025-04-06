using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MarkerDetection1 : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager m_trackedImageManager;

    [SerializeField] private GameObject m_createdObject;

    [SerializeField] private TextMeshProUGUI m_text;

    private Dictionary<TrackableId, GameObject> m_currentObjects = new Dictionary<TrackableId, GameObject>();

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
        // マーカーを初めて認識した時にこの箇所が呼ばれる
        foreach (var _arTrackedImage in imagesChangedEventArgs.added)
        {
            m_currentObjects[_arTrackedImage.trackableId] = Instantiate(m_createdObject, _arTrackedImage.transform);
        }

        // マーカーを認識した後、トラッキング状態になっている間ここが呼ばれる
        foreach (var _arTrackedImage in imagesChangedEventArgs.updated)
        {
            // トラッキングがされている場合
            if (_arTrackedImage.trackingState == TrackingState.Tracking)
            {
                m_currentObjects[_arTrackedImage.trackableId].SetActive(true);
                var _transform = _arTrackedImage.transform;
                m_text.text = $"Position : {_transform.position} rotation(euler) : {_transform.rotation.eulerAngles}"; 
            }
            // トラッキングがされていない場合＝マーカーが認識されていない場合
            else
            {
                m_currentObjects[_arTrackedImage.trackableId].SetActive(false);
            }

        }
        
        
        // マーカーを認識しなくなったときに呼ばれる。
        foreach (var _arTrackedImage in imagesChangedEventArgs.removed)
        {
            m_currentObjects[_arTrackedImage.trackableId].SetActive(false);
        }
    }
}
