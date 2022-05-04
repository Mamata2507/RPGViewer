using UnityEngine;

namespace RPG
{
    public class TokenSelection : MonoBehaviour
    {
        [SerializeField] private GameObject configPanel;

        private GameObject config = null;

        private void Update()
        {
            if (gameObject.name == "Selection")
            {
                if (Input.GetMouseButtonUp(0) && !RectTransformUtility.RectangleContainsScreenPoint(GetComponent<RectTransform>(), Camera.main.ScreenToWorldPoint(Input.mousePosition))) gameObject.SetActive(false);
            }
        }

        #region Buttons
        public void AddToken()
        {
            if (config != null) return;

            config = Instantiate(configPanel, GameObject.FindGameObjectWithTag("Main Canvas").transform, false);
            config.transform.localPosition = new Vector2(0, 0);
            config.GetComponent<TokenConfigPanel>().LoadData(null, gameObject);
        }
        #endregion
    }
}