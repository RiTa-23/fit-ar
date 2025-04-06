using System;
using UniRx;
using UnityEngine;

public class MarkerDetection3 : MonoBehaviour
{
    [SerializeField] private MarkerDetectionManager m_markerDetectionManager;
    
    [SerializeField] private GameObject m_createdObject;
    private void Start()
    {
        // 追加された時呼ばれる
        // 複数回生成されると困るのでTake(1)で一回だけ処理をするようにしている
        m_markerDetectionManager.OnAddImage.Take(1).Subscribe(image =>
        {
           Instantiate(m_createdObject, image.transform.position,image.transform.rotation);
        }).AddTo(this);
    }
}
