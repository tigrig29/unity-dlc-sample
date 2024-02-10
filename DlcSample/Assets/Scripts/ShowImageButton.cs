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
    }

    private async Task<Sprite> LoadSprite(string address)
    {
        var handle = Addressables.LoadAssetAsync<Sprite>(address);
        await handle.Task;
        return handle.Result;
    }
}