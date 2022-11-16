using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace _21stMortgageInterviewApplication
{
    internal class MainViewModel : BaseViewModel
    {
        private IEnumerable<int> _values;

        /// <summary>
        /// Computes either the largest, sum of even or sum of odd numbers
        /// </summary>
        public ICommand ComputeCommand { get; set; }

        private bool _isInputValid;

        public bool IsInputValid
        {
            get { return _isInputValid; }
            set
            {
                if (_isInputValid == value) return;
                _isInputValid = value;
            }
        }


        private string _result;
        /// <summary>
        /// The result, computed as requested by the user
        /// </summary>
        public string Result
        {
            get => _result;
            set
            {
                if (_result == value) return;
                _result = value;
                OnPropertyChanged();
            }
        }

        private string _userInput;

        /// <summary>
        /// The user input, a comma separated list of integers
        /// </summary>
        public string UserInput
        {
            get => _userInput;
            set
            {
                if (_userInput == value) return;
                _userInput = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel() : base()
        {
            ComputeCommand = new RelayCommand(ExecuteCompute, CanExecuteCompute);
            _values = new List<int>();

        }

        private void ValidateInput()
        {
            var res = false;
            var members = UserInput?.Split(',', StringSplitOptions.RemoveEmptyEntries);
            if (members != null && members.Any())
            {
                var numbers = new List<int>();
                foreach (var member in members)
                {
                    if (!int.TryParse(member, out int number))
                    {
                        res = false;
                        break;
                    }
                    numbers.Add(number);
                }
                _values = numbers;
                res = _values.Any();
            }
            IsInputValid = res;
        }

        private bool CanExecuteCompute(object arg)
        {
            ValidateInput();
            return IsInputValid;
        }

        private void ExecuteCompute(object obj)
        {
            switch (obj)
            {
                case "Largest":
                    Result = GetLargest().ToString();
                    break;
                case "SumOdd":
                    Result = GetSumOdd().ToString();
                    break;
                case "SumEven":
                    Result = GetSumEven().ToString();
                    break;
                default: 
                    Result=String.Empty;
                    break;
            }
        }

        private int GetLargest()
        {
            var res = _values.Max<int>();
            return res;
        }

        private int GetSumOdd()
        {
            var res = _values.Where(o => IsOdd(o)).Sum();
            return res;
        }

        private int GetSumEven()
        {
            var res = _values.Where(o => !IsOdd(o)).Sum();
            return res;
        }

        private bool IsOdd(int value)
        {
            return value % 2 != 0;
        }
    }
}
