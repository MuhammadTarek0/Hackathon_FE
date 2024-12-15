
    using System.Text.RegularExpressions;
    using UnityEngine;

    public class ValidateEmail : ValidateTMP_InputField
    {
        [SerializeField] // Scene reference: for the error message 
        private GameObject errorPanel;

        [SerializeField] 
        private string regex;
        
        protected override void OnEndEdit(string input)
        {
            isValid = Regex.IsMatch(input.Trim(), regex, RegexOptions.IgnoreCase);
            errorPanel.SetActive(!isValid);
        }

        protected override void OnValueChanged(string input)
        {
            errorPanel.SetActive(false);
            base.OnValueChanged(input);
        }
    }
