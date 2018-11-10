using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{
  
    public class DynamicText : ModelBase
    {

        public override void Restored()
        {
            formulaUpdated();
        }

        public delegate void IdChangedDelegate(DynamicText text, int oldValue, int newValue);
        public event IdChangedDelegate IdChanged;

        public int id = 0;

        public override int ID
        {
            get { return id; }
            set
            {
                ValidateId(value);
                if (value == id)
                    return;
                int oldValue = id;
                id = value;
                IdChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(ID));
            }
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            if (!DynamicTextDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

        public delegate void LabelChangedDelegate(DynamicText text, string oldLabel, string newLabel);
        public event LabelChangedDelegate LabelChanged;

        [PersistAs("label")]
        private string label;

        public string Label
        {
            get { return label; }
            set
            {
                ValidateLabel(value);
                if (value == label)
                    return;
                string oldLabel = label;
                label = value;
                LabelChanged?.Invoke(this, oldLabel, value);
                RaisePropertyChanged(nameof(Label));
            }
        }

        public void ValidateLabel(string label)
        {
            if (string.IsNullOrWhiteSpace(label))
                throw new ArgumentException();
        }

        public delegate void CurrentTextChangedDelegate(DynamicText text, string oldText, string newText);
        public event CurrentTextChangedDelegate CurrentTextChanged;

        private string currentText;

        public string CurrentText
        {
            get { return currentText; }
            private set
            {
                if (value == currentText)
                    return;
                string oldValue = currentText;
                currentText = value;
                CurrentTextChanged?.Invoke(this, oldValue, value);
                RaisePropertyChanged(nameof(CurrentText));
            }
        }

        [PersistAs("formula")]
        private string formula;

        public string Formula
        {
            get { return formula; }
            set
            {
                formula = value;
                formulaUpdated();
            }
        }

        private void formulaUpdated()
        {
            try
            {
                buildSubstitutesFromFormula();
                totalUpdate();
            }
            catch (DynamicTextSubstituteBuilder.FormulaSyntaxErrorException ex)
            {
                CurrentText = string.Format("Syntax error in formula at character position {0}.", ex.Position);
            }
        }

        private List<IDynamicTextFunctionSubstitute> substitutes = new List<IDynamicTextFunctionSubstitute>();

        private Dictionary<IDynamicTextFunctionSubstitute, string> substituteValues = new Dictionary<IDynamicTextFunctionSubstitute, string>();

        private void buildSubstitutesFromFormula()
        {
            DynamicTextSubstituteBuilder substituteBuilder = new DynamicTextSubstituteBuilder(formula);
            substitutes = substituteBuilder.Build();
            foreach (var substitute in substitutes)
                substitute.ValueChanged += dynamicTextFunctionSubstituteValueChangedHandler;
        }

        private void dynamicTextFunctionSubstituteValueChangedHandler(IDynamicTextFunctionSubstitute substitute)
        {
            if (!substitutes.Contains(substitute))
                return;
            substituteValues[substitute] = substitute.CurrentValue;
            buildCurrentValueFromParts();
        }

        private void buildCurrentValueFromParts()
        {
            StringBuilder strBuilder = new StringBuilder();
            foreach(IDynamicTextFunctionSubstitute substitute in substitutes)
            {
                substituteValues.TryGetValue(substitute, out string partValue);
                strBuilder.Append(partValue);
            }
            CurrentText = strBuilder.ToString();
        }

        private void updateAllParts()
        {
            foreach (var substitute in substitutes)
                substituteValues[substitute] = substitute.CurrentValue;
        }

        private void totalUpdate()
        {
            updateAllParts();
            buildCurrentValueFromParts();
        }

        protected override void afterUpdate()
        {
            base.afterUpdate();
            DynamicTextDatabase.Instance.ItemUpdated(this);
        }

    }

}
