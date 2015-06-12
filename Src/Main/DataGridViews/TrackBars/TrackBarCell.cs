using System;
using System.Windows.Forms;

namespace USC.GISResearchLab.Common.Forms.DataGridViews.TrackBars
{
    public class TrackBarCell : DataGridViewTextBoxCell
    {
        private int minValue;
        private int maxValue;
        private int currentValue;

        public TrackBarCell()
            : base()
        {
            this.minValue = 0;
            this.maxValue = 100;
            this.currentValue = Convert.ToInt32(this.Value);
            this.ToolTipText = Convert.ToString(this.Value);
        }

        ///   Whenever the user is to begin editing a cell of this type, the editing
        ///   control must be created, which in this column type's
        ///   case is a subclass of the MaskedTextBox control.
        /// 
        ///   This routine sets up all the properties and values
        ///   on this control before the editing begins.
        public override void InitializeEditingControl(int rowIndex,
                                                      object initialFormattedValue,
                                                      DataGridViewCellStyle dataGridViewCellStyle)
        {
            TrackBarEditingControl trackarEditingControl;
            TrackBarColumn trackBarColumn;
            DataGridViewColumn dataGridViewColumn;

            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                                          dataGridViewCellStyle);

            trackarEditingControl = DataGridView.EditingControl as TrackBarEditingControl;

            //
            // set up props that are specific to the TrackBar
            //

            dataGridViewColumn = this.OwningColumn;   // this.DataGridView.Columns[this.ColumnIndex];
            if (dataGridViewColumn is TrackBarColumn)
            {
                trackBarColumn = dataGridViewColumn as TrackBarColumn;

                trackarEditingControl.Minimum = this.minValue;
                trackarEditingControl.Maximum = this.maxValue;
                trackarEditingControl.Value = this.currentValue;
                trackarEditingControl.Text = Convert.ToString(this.currentValue);
            }
        }

        //  Returns the type of the control that will be used for editing
        //  cells of this type.  This control must be a valid Windows Forms
        //  control and must implement IDataGridViewEditingControl.
        public override Type EditType
        {
            get
            {
                return typeof(TrackBarEditingControl);
            }
        }

        public virtual int MinValue
        {
            get
            {
                return this.minValue;
            }
            set
            {
                this.minValue = value;
            }
        }

        public virtual int MaxValue
        {
            get
            {
                return this.maxValue;
            }
            set
            {
                this.maxValue = value;
            }
        }

        public virtual int CurrentValue
        {
            get
            {
                return this.currentValue;
            }
            set
            {
                this.currentValue = value;
            }
        }

    }
}
