using DG.Tweening;
using TMPro;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private Sequence sequence;
    private Sequence cameraShake;

    [SerializeField]
    private TMP_Text textToEdit;
    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private float wantedSize;
    [SerializeField] 
    private float duration;
    [SerializeField]
    private float wantedCameraZoom;
    [SerializeField]
    private float shakeForce;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*sequence = DOTween.Sequence();
        sequence.Append(DOTween.To(() => textToEdit.fontSize, x => textToEdit.fontSize = x, wantedSize - 200, duration));
        sequence.Append(DOTween.To(() => textToEdit.fontSize, x => textToEdit.fontSize = x, wantedSize, duration+.5f));
        sequence.Append(DOTween.To(() => mainCamera.fieldOfView, x => mainCamera.fieldOfView = x, wantedCameraZoom, duration));
        sequence.Append(DOTween.To(() => mainCamera.fieldOfView, x => mainCamera.fieldOfView = x, 60, .1f));
        sequence.Play();*/

        cameraShake = DOTween.Sequence();
        cameraShake.Append(DOTween.To(() => mainCamera.transform.position, x => mainCamera.transform.position = x, mainCamera.transform.position + Vector3.left * shakeForce, .1f));
        cameraShake.Append(DOTween.To(() => mainCamera.transform.position, x => mainCamera.transform.position = x, mainCamera.transform.position + Vector3.right * shakeForce, .1f));
        cameraShake.Append(DOTween.To(() => mainCamera.transform.position, x => mainCamera.transform.position = x, mainCamera.transform.position + Vector3.left * shakeForce, .1f));
        cameraShake.Append(DOTween.To(() => mainCamera.transform.position, x => mainCamera.transform.position = x, mainCamera.transform.position + Vector3.up * shakeForce, .1f));
        cameraShake.Append(DOTween.To(() => mainCamera.transform.position, x => mainCamera.transform.position = x, mainCamera.transform.position + Vector3.right * shakeForce, .1f));
        cameraShake.Append(DOTween.To(() => mainCamera.transform.position, x => mainCamera.transform.position = x, mainCamera.transform.position + Vector3.left * shakeForce, .1f));
        cameraShake.Append(DOTween.To(() => mainCamera.transform.position, x => mainCamera.transform.position = x, mainCamera.transform.position + Vector3.right * shakeForce, .1f));
        cameraShake.Append(DOTween.To(() => mainCamera.transform.position, x => mainCamera.transform.position = x, mainCamera.transform.position + Vector3.left * shakeForce, .1f));
        cameraShake.Append(DOTween.To(() => mainCamera.transform.position, x => mainCamera.transform.position = x, mainCamera.transform.position + Vector3.down * shakeForce, .1f));
        cameraShake.Append(DOTween.To(() => mainCamera.transform.position, x => mainCamera.transform.position = x, mainCamera.transform.position + Vector3.right * shakeForce, .1f));
        cameraShake.Append(DOTween.To(() => mainCamera.transform.position, x => mainCamera.transform.position = x, mainCamera.transform.position + Vector3.left * shakeForce, .1f));
        cameraShake.Append(DOTween.To(() => mainCamera.transform.position, x => mainCamera.transform.position = x, mainCamera.transform.position + Vector3.right * shakeForce, .1f));

        cameraShake.Play();

    }

    // Update is called once per frame
    void Update()
    {
    }
}
