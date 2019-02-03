using UnityEngine;
using UnityEngine.UI;

namespace Geekbrains
{
	public class FlashLightUi : MonoBehaviour
	{
		private Text _text;
        [SerializeField]private Image _chargeBar;
        [SerializeField]private Image _fadeImage;

		private void Awake()
		{
			_text = GetComponentInChildren<Text>();
        }

        public float Value
        {
            set
            {
                _chargeBar.fillAmount = value;
                _text.text = string.Format("{0}%", (value * 100).ToString("f0"));
            }
        }

        public void SetActive(bool value)
		{
            _fadeImage.gameObject.SetActive(!value);
            //_text.gameObject.SetActive(value);
            //_chargeBar.gameObject.SetActive(value);
        }
	}
}
