using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;


public class QRCodeScaner : MonoBehaviour
{
    [SerializeField] private GameObject
        scanCanvas,
        userCanvas;

    [SerializeField] private RawImage _rawImageBackground;
    [SerializeField] private AspectRatioFitter _aspectRatioFitter;
    [SerializeField] private Text textOut;
    [SerializeField] private RectTransform scanZone;
    [SerializeField] private GameObject mapPlane;
    [SerializeField] private Text scanedURL;

    [SerializeField] private List<string> urlText = new List<string>();

    public bool isScanSuccessful;

    private bool isCameraAvaliable;
    private WebCamTexture _camTexture;

    private ScanedItemsCounter _scanedItemsCounter;


    private void Start()
    {
        _scanedItemsCounter = FindObjectOfType<ScanedItemsCounter>();
        SetUpCamera();
        isScanSuccessful = false;
    }

    private void Update()
    {
        UpdateCameraRender();
        ResetScanBool();
    }

    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            isCameraAvaliable = false;
            return;
        }

        for (int i = 0; i < devices.Length; i++)
        {
            if (devices[i].isFrontFacing == false)
            {
                _camTexture = new WebCamTexture(devices[i].name, (int) scanZone.rect.width, (int) scanZone.rect.height);
            }
        }

        _camTexture.Play();
        _rawImageBackground.texture = _camTexture;
        isCameraAvaliable = true;
    }

    private void UpdateCameraRender()
    {
        if (isCameraAvaliable == false)
        {
            return;
        }

        float ratio = (float) _camTexture.width / (float) _camTexture.height;

        _aspectRatioFitter.aspectRatio = ratio;

        int orientation = -_camTexture.videoRotationAngle;
        _rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }

    public void OnClickScan()
    {
        Scan();
    }

    private void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(_camTexture.GetPixels32(), _camTexture.width, _camTexture.height);
            if (result != null)
            {
                if (urlText.Contains(textOut.ToString()))
                {
                    textOut.text = "ALREADY SCANNED!";
                    Debug.Log("Trying to scan same code!");
                    //ResetScanBool();
                }
                else
                {
                    textOut.text = result.Text;
                    urlText.Add(textOut.ToString());
                    Debug.Log(urlText.Capacity);
                    isScanSuccessful = true;
                }

                foreach (var l in urlText)
                {
                    Debug.Log(urlText.ToString());
                }
            }
            else
            {
                textOut.text = "FAILED TO READ QR CODE!";
            }
        }
        catch
        {
            textOut.text = "FAILED IN TRY";
        }
    }

    private void ResetScanBool()
    {
        if (isScanSuccessful)
        {
            _scanedItemsCounter.scanedItems += 1;
            isScanSuccessful = false;
            scanCanvas.SetActive(false);
            userCanvas.SetActive(true);
            mapPlane.SetActive(true);
        }
    }
}