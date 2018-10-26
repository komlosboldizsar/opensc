using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{

    public delegate void DynamicTextIdChangingDelegate(DynamicText text, int oldValue, int newValue);
    public delegate void DynamicTextIdChangedDelegate(DynamicText text, int oldValue, int newValue);

    public delegate void DynamicTextLabelChangingDelegate(DynamicText text, string oldLabel, string newLabel);
    public delegate void DynamicTextLabelChangedDelegate(DynamicText text, string oldLabel, string newLabel);

    public delegate void DynamicTextCurrentTextChangingDelegate(DynamicText text, string oldText, string newText);
    public delegate void DynamicTextCurrentTextChangedDelegate(DynamicText text, string oldText, string newText);

    public class DynamicText: IModel
    {

        public virtual void Restored()
        {
            buildSubstitutesFromFormula();
            totalUpdate();
        }

        public event DynamicTextIdChangingDelegate IdChanging;
        public event DynamicTextIdChangedDelegate IdChanged;
        public event ParameterlessChangeNotifierDelegate IdChangingPCN;
        public event ParameterlessChangeNotifierDelegate IdChangedPCN;

        public int id = 0;

        public int ID
        {
            get { return id; }
            set
            {
                ValidateId(value);
                if (value == id)
                    return;
                int oldValue = id;
                IdChanging?.Invoke(this, oldValue, value);
                IdChangingPCN?.Invoke();
                id = value;
                IdChanged?.Invoke(this, oldValue, value);
                IdChangedPCN?.Invoke();
            }
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            if (!DynamicTextDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }

        public event DynamicTextLabelChangingDelegate LabelChanging;
        public event DynamicTextLabelChangedDelegate LabelChanged;
        public event ParameterlessChangeNotifierDelegate LabelChangingPCN;
        public event ParameterlessChangeNotifierDelegate LabelChangedPCN;

        [PersistAs("label")]
        private string label;

        public string Label
        {
            get { return label; }
            set
            {
                ValidateLabel(label);
                if (value == label)
                    return;
                string oldLabel = label;
                LabelChanging?.Invoke(this, oldLabel, value);
                LabelChangingPCN?.Invoke();
                label = value;
                LabelChanged?.Invoke(this, oldLabel, value);
                LabelChangedPCN?.Invoke();
            }
        }

        public void ValidateLabel(string label)
        {
            if (string.IsNullOrWhiteSpace(label))
                throw new ArgumentException();
        }

        public event DynamicTextCurrentTextChangingDelegate CurrentTextChanging;
        public event DynamicTextCurrentTextChangedDelegate CurrentTextChanged;
        public event ParameterlessChangeNotifierDelegate CurrentTextChangingPCN;
        public event ParameterlessChangeNotifierDelegate CurrentTextChangedPCN;

        private string currentText;

        public string CurrentText
        {
            get { return currentText; }
            private set
            {
                if (value == currentText)
                    return;
                string oldValue = currentText;
                CurrentTextChanging?.Invoke(this, oldValue, value);
                CurrentTextChangingPCN?.Invoke();
                currentText = value;
                CurrentTextChanged?.Invoke(this, oldValue, value);
                CurrentTextChangedPCN?.Invoke();
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
                buildSubstitutesFromFormula();
            }
        }

        private List<IDynamicTextFunctionSubstitute> substitutes = new List<IDynamicTextFunctionSubstitute>();

        private Dictionary<IDynamicTextFunctionSubstitute, string> substituteValues = new Dictionary<IDynamicTextFunctionSubstitute, string>();

        private void buildSubstitutesFromFormula()
        {
            // TODO: State machine here
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

    }

}
