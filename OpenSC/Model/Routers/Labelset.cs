﻿using OpenSC.Model.General;
using OpenSC.Model.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSC.Model.Routers
{
    public class Labelset : ModelBase
    {
        public Labelset()
        { }

        public override void RestoredOwnFields()
        {
            updateLabelLabelsetAssociations();
            notifyLabelsRestored();
        }

        public override void Removed()
        {
            base.Removed();
            NameChanged = null;
        }

        #region ID validation
        protected override void validateIdForDatabase(int id)
        {
            if (!LabelsetDatabase.Instance.CanIdBeUsedForItem(id, this))
                throw new ArgumentException();
        }
        #endregion

        #region Property: Name
        public event PropertyChangedTwoValuesDelegate<Labelset, string> NameChanged;

        [PersistAs("name")]
        private string name;
        
        public string Name
        {
            get => name;
            set => setProperty(this, ref name, value, NameChanged, validator: ValidateName);
        }
        public void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException();
        }
        #endregion

        #region Label collection
        private ObservableList<Label> labels = new ObservableList<Label>();

        public ObservableList<Label> Labels
        {
            get { return labels; }
        }

        [PersistAs("labels")]
        [PersistAs("label", 1)]
        private Label[] _labels
        {
            get { return labels.ToArray(); }
            set
            {
                labels.Clear();
                if (value != null)
                    labels.AddRange(value);
            }
        }
        private void updateLabelLabelsetAssociations()
        {
            foreach (Label label in labels)
                label.Labelset = this;
        }

        private void notifyLabelsRestored()
        {
            foreach (Label label in labels)
                label.Restored();
        }

        private Label getLabel(RouterInput routerInput)
        {
            foreach (Label label in labels)
                if (label.RouterInput == routerInput)
                    return label;
            return null;
        }
        #endregion

        #region Label text getters, setters and events
        public string GetText(RouterInput routerInput)
        {
            return getLabel(routerInput)?.Text;
        }

        public void SetText(RouterInput routerInput, string text)
        {
            Label label = getLabel(routerInput);
            if (label != null)
            {
                label.Text = text;
                return;
            }
            label = new Label(this, text, routerInput);
            labels.Add(label);
        }

        public delegate void LabelTextChangedDelegate(Labelset labelset, RouterInput routerInput, string oldText, string newText);
        public event LabelTextChangedDelegate LabelTextChanged;

        internal void NotifyLabelTextChanged(Label label, string oldText, string newText)
        {
            if (label.Labelset != this)
                return;
            LabelTextChanged?.Invoke(this, label.RouterInput, oldText, newText);
        }
        #endregion

    }

}
