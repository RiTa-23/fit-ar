using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class AddReferenceImage : MonoBehaviour
{
    [SerializeField]
    ARTrackedImageManager m_TrackedImageManager;

    public void AddImage(Texture2D imageToAdd)
    {
        if (m_TrackedImageManager.referenceLibrary is MutableRuntimeReferenceImageLibrary mutableLibrary)
        {
            mutableLibrary.ScheduleAddImageWithValidationJob(
                imageToAdd,
                "my new image",
                0.5f /* 50 cm */);
        }
    }

    public void CreateLibraryAndAddImage(Texture2D imageToAdd)
    {
        void AddImage(Texture2D imageToAdd)
        {
            m_TrackedImageManager.enabled = false;
            var library = m_TrackedImageManager.CreateRuntimeLibrary();
            m_TrackedImageManager.referenceLibrary = library;
            m_TrackedImageManager.enabled = true;
            if (library is MutableRuntimeReferenceImageLibrary mutableLibrary)
            {
                mutableLibrary.ScheduleAddImageWithValidationJob(
                    imageToAdd,
                    "my new image",
                    0.5f /* 50 cm */);
            }
        }
    }
}
