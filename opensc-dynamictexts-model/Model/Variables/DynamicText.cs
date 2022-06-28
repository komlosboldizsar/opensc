using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using OpenSC.Model.SourceGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Variables
{

    public partial class DynamicText : ModelBase
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

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = DynamicTextDatabase.Instance;
        #endregion

        #region Property: CurrentText
        [AutoProperty]
        private string currentText = "";
        #endregion

        #region Property: Formula
        [AutoProperty]
        [AutoProperty.AfterChange(nameof(_formula_afterChange))]
        [PersistAs("formula")]
        private string formula;

        private void _formula_afterChange(string oldValue, string newValue) => formulaUpdated();

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
