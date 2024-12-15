
    using UnityEngine;
    using TMPro;

    public class ValidatePassword : ValidateTMP_InputField
    {
        [SerializeField] // Scene reference: for the error message 
        private GameObject errorPanel;
        [SerializeField] // Scene reference: for the main password input
        private TMP_InputField passwordInputField;
        
        // Module reference: was the confirmation input field touched before,
        // to avoid showing the error message before the user touch this input,
        // and persist the state after the first touch to alert the user.
        private bool isSelected;

        /// <summary>
        /// Initialize variables and register for engine callbacks.
        /// </summary>
        protected override void Awake()
        {
            base.Awake();
            passwordInputField.onEndEdit.AddListener(ValidatePasswordInput);
            inputField.onSelect.AddListener(SelectedForFirstTime);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            passwordInputField.onEndEdit.RemoveListener(ValidatePasswordInput);
            inputField.onSelect.RemoveListener(SelectedForFirstTime);
        }

        /// <summary>
        /// Check if both passwords are match, if not show error message.
        /// </summary>
        /// <param name="input">confirm password input</param>
        protected override void OnValueChanged(string input)
        {
            // if the player has selected this input before and the main
            // field is not empty so we avoid spamming error message.
            if (!isSelected && (string.IsNullOrEmpty(input) ||
                                string.IsNullOrWhiteSpace(input)))
            {
                errorPanel.SetActive(false);
                return;
            }

            isValid = input.Equals(passwordInputField.text);
            errorPanel.SetActive(!isValid);
        }

        /// <summary>
        /// Modify isSelected attribute after the first selection, to avoid showing the alert without,
        /// selecting the field. 
        /// </summary>
        /// <param name="text"></param>
        private void SelectedForFirstTime(string text)
        {
            if (string.IsNullOrEmpty(passwordInputField.text) || string.IsNullOrWhiteSpace(passwordInputField.text))
            {
                return;
            }

            isSelected = true;

            inputField.onSelect.RemoveListener(SelectedForFirstTime);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="password">main password input field</param>
        private void ValidatePasswordInput(string password)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password))
            {
                errorPanel.SetActive(false);
                return;
            }

            OnValueChanged(inputField.text);
        }
    }
