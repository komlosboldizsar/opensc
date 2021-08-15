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

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            formulaUpdated();
        }

        public override void Removed()
        {
            base.Removed();
            IdChanged = null;
            LabelChanged = null;
            CurrentTextChanged = null;
            substitutes.Clear();
            substituteValues.Clear();
        }

        #region Property: ID
        public event PropertyChangedTwoValuesDelegate<DynamicText, int> IdChanged;

        public int id = 0;

        public override int ID
        {
            get => id;
            set
            {
                ValidateId(value);
                setProperty(this, ref id, value, IdChanged);
            }
        }

        public void ValidateId(int id)
        {
            if (id <= 0)
                throw new ArgumentException();
            if (!DynamicTextDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Label
        public event PropertyChangedTwoValuesDelegate<DynamicText, string> LabelChanged;

        [PersistAs("label")]
        private string label;

        public string Label
        {
            get => label;
            set
            {
                ValidateLabel(value);
                setProperty(this, ref label, value, LabelChanged);
            }
        }

        public void ValidateLabel(string label)
        {
            if (string.IsNullOrWhiteSpace(label))
                throw new ArgumentException();
        }
        #endregion

        #region Property: CurrentText
        public event PropertyChangedTwoValuesDelegate<DynamicText, string> CurrentTextChanged;

        private string currentText = "";

        public string CurrentText
        {
            get => currentText;
            private set => setProperty(this, ref currentText, value, CurrentTextChanged);
        }
        #endregion

        #region Property: Formula
        public event PropertyChangedTwoValuesDelegate<DynamicText, string> FormulaChanged;

        [PersistAs("formula")]
        private string formula;

        public string Formula
        {
            get => formula;
            set
            {
                if (!setProperty(this, ref formula, value, FormulaChanged))
                    return;
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
        #endregion

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

    }

}
