using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ImageRecognitionHandler : MonoBehaviour
{
    [SerializeField]
    private ARTrackedImageManager trackedImageManager;

    // �e�摜�ɑΉ�����I�u�W�F�N�g��C�x���g��ݒ肷��
    [System.Serializable]
    public class ImageEvent
    {
        public string imageName;
        public GameObject associatedObject; // �摜�F�����ɕ\������I�u�W�F�N�g
        public UnityEngine.Events.UnityEvent onRecognized; // �J�X�^���C�x���g
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
        // �F������Ă���摜���X�V���邽�߂ɃZ�b�g���N���A
        currentlyTrackedImages.Clear();

        // �F�����ꂽ�V�����摜
        foreach (var trackedImage in eventArgs.added)
        {
            HandleImageRecognized(trackedImage);
        }

        // �X�V���ꂽ�摜
        foreach (var trackedImage in eventArgs.updated)
        {
            HandleImageRecognized(trackedImage);
        }

        // �g���b�L���O������ꂽ�摜
        foreach (var trackedImage in eventArgs.removed)
        {
            HandleImageLost(trackedImage);
        }

        // �F������Ă��Ȃ��摜�ɕR�Â��I�u�W�F�N�g���\��
        HideUntrackedObjects();
    }

    private void HandleImageRecognized(ARTrackedImage trackedImage)
    {
        currentlyTrackedImages.Add(trackedImage.referenceImage.name);

        foreach (var imageEvent in imageEvents)
        {
            if (trackedImage.referenceImage.name == imageEvent.imageName)
            {
                // �C�x���g�����s
                imageEvent.onRecognized?.Invoke();

                // �K�v�ɉ����ăI�u�W�F�N�g��\��
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
                // �K�v�ɉ����ăI�u�W�F�N�g���\��
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
