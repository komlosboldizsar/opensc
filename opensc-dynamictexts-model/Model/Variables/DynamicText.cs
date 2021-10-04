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
            CurrentTextChanged = null;
            substitutes.Clear();
            substituteValues.Clear();
        }

        #region ID validation
        protected override void validateIdForDatabase(int id)
        {
            if (!DynamicTextDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = DynamicTextDatabase.Instance;
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
