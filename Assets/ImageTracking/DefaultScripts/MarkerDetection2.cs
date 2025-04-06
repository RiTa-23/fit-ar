using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MarkerDetection2 : MonoBehaviour
{
    [SerializeField] private MarkerDetectionManager m_markerDetectionManager;

    [SerializeField] private GameObject m_createdObject;

    [SerializeField] private TextMeshProUGUI m_text;

    private Dictionary<TrackableId,GameObject> m_currentObjects = new Dictionary<TrackableId, GameObject>();

    private void Start()
    {
        // 追加された時呼ばれる
        m_markerDetectionManager.OnAddImage.Subscribe(image =>
        {
            m_currentObjects[image.trackableId] = Instantiate(m_createdObject, image.transform);
        }).AddTo(this);

        // 更新された時呼ばれる
        m_markerDetectionManager.OnUpdateImage.Subscribe(image =>
        {
            // トラッキングがされている場合
            if (image.trackingState == TrackingState.Tracking)
            {
                m_currentObjects[image.trackableId].SetActive(true);
                var _transform = image.transform;
                m_text.text = $"Position : {_transform.position} rotation(euler) : {_transform.rotation.eulerAngles}";
            }
            // トラッキングがされていない場合＝マーカーが認識されていない場合
            else
            {
                m_currentObjects[image.trackableId].SetActive(false);
            }
        }).AddTo(this);

        // 削除された時呼ばれる
        m_markerDetectionManager.OnRemovedImage.Subscribe(image =>
        {
            m_currentObjects[image.trackableId].SetActive(false);
        }).AddTo(this);
    }
}
