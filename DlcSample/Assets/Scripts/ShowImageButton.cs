using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class ShowImageButton : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(ShowImage);
    }

    private async void ShowImage()
    {
        var sprite = await LoadSprite("Assets/DefaultResources/default-image.png");
        _image.sprite = sprite;

        await Task.Delay(500);

        var dlcSettings = await LoadDlcSettings("Assets/DlcResources/dlc-settings.json");
        if (dlcSettings.enabled)
        {
            var dlcSprite = await LoadSprite("Assets/DlcResources/dlc-image.png");
            _image.sprite = dlcSprite;
        }
    }

    private async Task<Sprite> LoadSprite(string address)
    {
        var handle = Addressables.LoadAssetAsync<Sprite>(address);
        await handle.Task;
        return handle.Result;
    }

    private async Task<DlcSettings> LoadDlcSettings(string address)
    {
        var handle = Addressables.LoadAssetAsync<TextAsset>(address);
        await handle.Task;
        return JsonUtility.FromJson<DlcSettings>(handle.Result.ToString());
    }
}
