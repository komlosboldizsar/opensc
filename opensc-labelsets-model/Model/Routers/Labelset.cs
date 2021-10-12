using OpenSC.Model.General;
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

        #region Instantiation, restoration
        public Labelset()
        {
            LabelableObjectRegister.Instance.ItemsRemoved += labelableItemsRemoved;
        }

        public override void TotallyRestored()
        {
            base.TotallyRestored();
            labels.Foreach(lkvp => lkvp.Value.AssociatedObject = lkvp.Key);
        }

        public override void Removed()
        {
            base.Removed();
            // remove event subscriptions
        }
        #endregion

        #region Owner database
        public override sealed IDatabaseBase OwnerDatabase { get; } = LabelsetDatabase.Instance;
        #endregion

        #region Label collection
        [PersistAs("labels")]
        [PersistAs(null, 1, LabelXmlSerializer.ATTRIBUTE_OBJECT)]
        private ObservableDictionary<ISystemObject, Label> labels = new();
        public ObservableDictionary<ISystemObject, Label> Labels => labels;

        [TempForeignKey(nameof(labels))]
        private ObservableDictionary<string, Label> _labels = new();

        public Label CreateLabel() => new(this);

        public Label GetLabel(ISystemObject forObject)
        {
            if (labels.TryGetValue(forObject, out Label label))
                return label;
            return null;
        }

        public Label this[ISystemObject forObject] => GetLabel(forObject);

        private void labelableItemsRemoved(IEnumerable<IObservableEnumerable<ISystemObject>.ItemWithPosition> affectedItemsWithPositions)
            => affectedItemsWithPositions.Foreach(aiwp => labels.Remove(aiwp.Item));
        #endregion

        #region Label text getters, setters
        public string GetText(ISystemObject forObject) => GetLabel(forObject)?.Text;

        public void SetText(ISystemObject forObject, string text)
        {
            if (!LabelableObjectRegister.Instance.Contains(forObject))
                return;
            Label label = GetLabel(forObject);
            if (label != null)
            {
                label.Text = text;
                return;
            }
            label = new Label(this, forObject) { Text = text };
            labels.Add(forObject, label);
            LabelTextChanged?.Invoke(label, text);
        }
        #endregion

        #region Label text changed event
        internal void NotifyLabelTextChanged(Label label, string newText) => LabelTextChanged?.Invoke(label, newText);

        public event PropertyChangedOneValueDelegate<Label, string> LabelTextChanged;
        #endregion

    }

}
