
    using UnityEngine;

    public class SignupContentHandler : MonoBehaviour
    {
        [Header("Game Events")]
        [SerializeField] private GameEventSignupData signupGameEvent;
        [Header("References")] [SerializeField]
        private ValidateInput firstName;
        [SerializeField]
        private ValidateInput lastName;
        [SerializeField]
        private ValidateInput username;
        [SerializeField] 
        private ValidateEmail email;
        [SerializeField] 
        private ValidateInput password;
        [SerializeField] 
        private ValidatePassword confirmPassword;

        private ValidateTMP_InputField[] signupInputFields;
        private void Start()
        {
            signupInputFields = new ValidateTMP_InputField[]{firstName, lastName, username, email, password, confirmPassword};
        }

        public void TrySignup()
        {
            bool canSignup = true;
            foreach (var signupInputField in signupInputFields)
            {
                if (signupInputField.IsValid()) continue;
                canSignup = false;
                print($"{signupInputField.name} is invalid!");
            }

            if (!canSignup) return;
            SignupData signupData = new SignupData()
            {
                firstName = firstName.GetText(),
                lastName = lastName.GetText(),
                username = username.GetText(),
                email = email.GetText(),
                password = password.GetText()
            };
            print(signupData);

            signupGameEvent.Raise(signupData);
        }
    }
