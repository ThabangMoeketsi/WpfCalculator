using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace WpfCalculator
{
    public class CalculatorViewModel: INotifyPropertyChanged
    {
        private string _displayText = "0";
        private string _currentInput = string.Empty;
        private string _operator = string.Empty;
        private double _memory = 0.0;

        private List<string> _history = new List<string>();

        public List<string> History
        {
            get { return _history; }
            set
            {
                if (_history != value)
                {
                    _history = value;
                    OnPropertyChanged();
                }
            }
        }

        public string DisplayText
        {
            get { return _displayText; }
            set
            {
                if (_displayText != value)
                {
                    _displayText = value;
                    OnPropertyChanged();
                }
            }
        }

        public void AppendToDisplay(string input)
        {
            if (_currentInput.Length < 8)
            {
                _currentInput += input;
                DisplayText = _currentInput;
            }
        }

        public void SetOperator(string op)
        {
            if (!string.IsNullOrEmpty(_operator))
            {
                Calculate();
            }

            if (string.IsNullOrEmpty(_currentInput))
            {
                _history.Add($"Error: Input is empty.");
                DisplayText = "Error";
                return;
            }

            _operator = op;
            _memory = double.Parse(_currentInput);
            _currentInput = string.Empty;
        }

        public void Clear()
        {
            _currentInput = string.Empty;
            _operator = string.Empty;
            DisplayText = "0";
        }

        public void Calculate()
        {
            if (double.TryParse(_currentInput, out double currentInputNum))
            {
                switch (_operator)
                {
                    case "+":
                        _memory += currentInputNum;
                        break;
                    case "-":
                        _memory -= currentInputNum;
                        break;
                    case "*":
                        _memory *= currentInputNum;
                        break;
                    case "/":
                        if (currentInputNum != 0)
                        {
                            _memory /= currentInputNum;
                        }
                        else
                        {
                            DisplayText = "Error";
                            return;
                        }
                        break;
                }

                _history.Add($"{_memory} {_operator} {currentInputNum} = {_memory}");
                DisplayText = _memory.ToString();
                _currentInput = string.Empty;
                _operator = string.Empty;
            } else
            {
                _history.Add($"Error: Invalid input '{_currentInput}' for Calculate.");
                Clear();
            }
        }

        public void MemoryAdd()
        {
            if (double.TryParse(_currentInput, out double currentInputNum))
            {
                _memory += double.Parse(_currentInput);
                _history.Add($"Memory add by {_currentInput}. Current memory value: {_memory}");
                _currentInput = _memory.ToString();
                OnPropertyChanged(nameof(DisplayText));
            }
            else
            {
                _history.Add($"Error: Invalid input '{_currentInput}' for MemoryAdd.");
            }
        }

        public void MemorySubtract()
        {
            if (double.TryParse(_currentInput, out double currentInputNum))
            {
                _memory -= currentInputNum;
                _history.Add($"Memory subtracted by {_currentInput}. Current memory value: {_memory}");
                _currentInput = _memory.ToString();
                OnPropertyChanged(nameof(DisplayText));
            }
            else
            {
                _history.Add($"Error: Invalid input '{_currentInput}' for MemorySubtract.");
            }
        }

        public void MemoryRecall()
        {
            _currentInput = _memory.ToString();
            DisplayText = _currentInput;

            if (_memory != 0)
            {
                _currentInput = _memory.ToString();
                OnPropertyChanged(nameof(DisplayText));
                _history.Add($"Memory recalled: {_memory}");
            }
            else
            {
                _history.Add("Error: Memory is empty.");
            }
        }

        public void ShowHistory()
        {
            History = new List<string>(_history);
        }

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
