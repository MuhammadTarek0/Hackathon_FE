
    using UnityEngine;
    using TMPro;

    [RequireComponent(typeof(TMP_InputField))]
    public abstract class ValidateTMP_InputField : MonoBehaviour
    {
        [SerializeField] 
        private GameObject mandatoryFieldObjectIdentifier;

        protected bool isValid;
        protected TMP_InputField inputField;

        protected virtual void Awake()
        {
            inputField = GetComponent<TMP_InputField>();
            if (!inputField)
            {
                enabled = false;
            }

            isValid = false;
            inputField.onEndEdit.AddListener(OnEndEdit);
            inputField.onValueChanged.AddListener(OnValueChanged);
        }

        protected virtual void OnDestroy()
        {
            inputField.onEndEdit.RemoveListener(OnEndEdit);
            inputField.onValueChanged.RemoveListener(OnValueChanged);
        }

        /// <summary>
        /// A checker to run at the end of the text edits. 
        /// </summary>
        /// <param name="input">input string</param>
        protected virtual void OnEndEdit(string input)
        {
            isValid = !string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input);
            if (!isValid)
                ShowMandatoryField();
        }

        /// <summary>
        /// A checker to run on each update of the input field.
        /// </summary>
        /// <param name="input">updated string</param>
        protected virtual void OnValueChanged(string input)
        {
            isValid = !string.IsNullOrEmpty(input) && !string.IsNullOrWhiteSpace(input);
        }

        /// <summary>
        /// Is the current input field is valid or not.
        /// </summary>
        /// <returns>boolean value</returns>
        public bool IsValid()
        {
            return isValid;
        }

        /// <summary>
        /// Get the input field text.
        /// </summary>
        /// <returns></returns>
        public string GetText()
        {
            return inputField.text.Trim();
        }

        /// <summary>
        /// Toggle the mandatory field identifier in the scene.
        /// </summary>
        private void ShowMandatoryField()
        {
            if (mandatoryFieldObjectIdentifier)
                mandatoryFieldObjectIdentifier.SetActive(!isValid);
        }
    }
