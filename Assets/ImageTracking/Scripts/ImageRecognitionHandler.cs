using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageRecognitionHandler : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager trackedImageManager;

    // 各画像に対応するオブジェクトやイベントを設定する
    [System.Serializable]
    public class ImageEvent
    {
        public string imageName;
        public GameObject associatedObject; // 画像認識時に表示するオブジェクト
        public UnityEngine.Events.UnityEvent onRecognized; // カスタムイベント
    }

    public List<ImageEvent> imageEvents = new List<ImageEvent>();

    private HashSet<string> currentlyTrackedImages = new HashSet<string>();

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        // 認識されている画像を更新するためにセットをクリア
        currentlyTrackedImages.Clear();

        // 認識された新しい画像
        foreach (var trackedImage in eventArgs.added)
        {
            HandleImageRecognized(trackedImage);
        }

        // 更新された画像
        foreach (var trackedImage in eventArgs.updated)
        {
            HandleImageRecognized(trackedImage);
        }

        // トラッキングが失われた画像
        foreach (var trackedImage in eventArgs.removed)
        {
            HandleImageLost(trackedImage);
        }

        // 認識されていない画像に紐づくオブジェクトを非表示
        HideUntrackedObjects();
    }

    private void HandleImageRecognized(ARTrackedImage trackedImage)
    {
        currentlyTrackedImages.Add(trackedImage.referenceImage.name);

        foreach (var imageEvent in imageEvents)
        {
            if (trackedImage.referenceImage.name == imageEvent.imageName)
            {
                // イベントを実行
                imageEvent.onRecognized?.Invoke();

                // 必要に応じてオブジェクトを表示
                if (imageEvent.associatedObject != null)
                {
                    imageEvent.associatedObject.SetActive(true);
                    imageEvent.associatedObject.transform.position = trackedImage.transform.position;
                    imageEvent.associatedObject.transform.rotation = trackedImage.transform.rotation;
                }
            }
        }
    }

    private void HandleImageLost(ARTrackedImage trackedImage)
    {
        foreach (var imageEvent in imageEvents)
        {
            if (trackedImage.referenceImage.name == imageEvent.imageName && imageEvent.associatedObject != null)
            {
                // 必要に応じてオブジェクトを非表示
                imageEvent.associatedObject.SetActive(false);
            }
        }
    }

    private void HideUntrackedObjects()
    {
        foreach (var imageEvent in imageEvents)
        {
            if (!currentlyTrackedImages.Contains(imageEvent.imageName) && imageEvent.associatedObject != null)
            {
                imageEvent.associatedObject.SetActive(false);
            }
        }
    }
}
